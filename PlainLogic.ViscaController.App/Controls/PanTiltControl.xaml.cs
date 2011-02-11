using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace PlainLogic.ViscaController.App.Controls
{
    /// <summary>
    /// Interaction logic for PanTiltControl.xaml
    /// </summary>
    public partial class PanTiltControl : UserControl
    {
        #region Properties

        public static readonly DependencyProperty PanRangeProperty = DependencyProperty.Register( "PanRange", typeof( int ), typeof( PanTiltControl ), new FrameworkPropertyMetadata( 20 ) );
        public static readonly DependencyProperty TiltRangeProperty = DependencyProperty.Register( "TiltRange", typeof( int ), typeof( PanTiltControl ), new FrameworkPropertyMetadata( 10 ) );

        static readonly DependencyPropertyKey PanPropertyKey = DependencyProperty.RegisterReadOnly( "Pan", typeof( int ), typeof( PanTiltControl ), new FrameworkPropertyMetadata( 0, PanTilt_PropertyChangedCallback ) );
        public static readonly DependencyProperty PanProperty = PanPropertyKey.DependencyProperty;

        static readonly DependencyPropertyKey TiltPropertyKey = DependencyProperty.RegisterReadOnly( "Tilt", typeof( int ), typeof( PanTiltControl ), new FrameworkPropertyMetadata( 0, PanTilt_PropertyChangedCallback ) );
        public static readonly DependencyProperty TiltProperty = TiltPropertyKey.DependencyProperty;

        static void PanTilt_PropertyChangedCallback( DependencyObject obj, DependencyPropertyChangedEventArgs args )
        {
            ( (PanTiltControl)obj )._adorner.InvalidateVisual();

            if( args.Property == PanProperty )
            {
                VISCACommands.Pan.Execute( args.NewValue, (IInputElement)obj );
            }
            else
            {
                VISCACommands.Tilt.Execute( args.NewValue, (IInputElement)obj );
            }
        }

        public int PanRange
        {
            get { return (int)GetValue( PanRangeProperty ); }
            set { SetValue( PanRangeProperty, value ); }
        }

        public int TiltRange
        {
            get { return (int)GetValue( TiltRangeProperty ); }
            set { SetValue( TiltRangeProperty, value ); }
        }

        public int Pan
        {
            get { return (int)GetValue( PanProperty ); }
            private set { SetValue( PanPropertyKey, value ); }
        }

        public int Tilt
        {
            get { return (int)GetValue( TiltProperty ); }
            private set { SetValue( TiltPropertyKey, value ); }
        }

        #endregion

        TargetAdorner _adorner;

        public PanTiltControl()
        {
            InitializeComponent();

            _adorner = new TargetAdorner( this, target );
        }

        protected override Size ArrangeOverride( Size arrangeBounds )
        {
            double newSize = Math.Min( arrangeBounds.Height, arrangeBounds.Width );

            return base.ArrangeOverride( new Size( newSize, newSize ) );
        }

        Point? _dragStart;

        private void target_MouseDown( object sender, MouseButtonEventArgs e )
        {
            if( e.ChangedButton == MouseButton.Left && e.LeftButton == MouseButtonState.Pressed )
            {
                Mouse.Capture( target );
                Mouse.OverrideCursor = Cursors.None;

                var adornerLayer = AdornerLayer.GetAdornerLayer( target );
                adornerLayer.Add( _adorner );

                _dragStart = e.GetPosition( this );
            }
        }

        private void target_MouseMove( object sender, MouseEventArgs e )
        {
            if( _dragStart.HasValue )
            {
                Vector diff = e.GetPosition( this ) - _dragStart.Value;

                double maxWidth = ActualWidth / 2.0;
                double maxHeight = ActualHeight / 2.0;

                if( diff.X > maxWidth ) diff.X = maxWidth;
                if( diff.X < -maxWidth ) diff.X = -maxWidth;
                if( diff.Y > maxHeight ) diff.Y = maxHeight;
                if( diff.Y < -maxHeight ) diff.Y = -maxHeight;

                Pan = (int)Math.Round( ( diff.X * PanRange / maxWidth ) );
                Tilt = (int)Math.Round( ( diff.Y * TiltRange / maxHeight ) );

                target.RenderTransform = new TranslateTransform( diff.X, diff.Y );
            }
        }

        private void target_MouseUp( object sender, MouseButtonEventArgs e )
        {
            if( e.ChangedButton == MouseButton.Left && e.LeftButton == MouseButtonState.Released )
            {
                Mouse.Capture( null );
                Mouse.OverrideCursor = null;

                target.RenderTransform = null;

                var adornerLayer = AdornerLayer.GetAdornerLayer( target );
                adornerLayer.Remove( _adorner );

                Pan = 0;
                Tilt = 0;

                _dragStart = null;
            }
        }

        class TargetAdorner : Adorner
        {
            public TargetAdorner( PanTiltControl owner, Ellipse element )
                : base( element )
            {
                _owner = owner;
                _element = element;
            }

            PanTiltControl _owner;

            Ellipse _element;

            protected override void OnRender( DrawingContext drawingContext )
            {
                double pan = Math.Abs( _owner.Pan );
                double tilt = Math.Abs( _owner.Tilt );

                var panText = FormatText( String.Format( "{0} ({1:P0})", pan, pan / (double)_owner.PanRange ) );
                drawingContext.DrawText( panText, new Point( -panText.Width - 5, ( _element.ActualHeight / 2.0 ) - ( panText.Height / 2.0 ) ) );

                var tiltText = FormatText( String.Format( "{0} ({1:P0})", tilt, tilt / (double)_owner.TiltRange ) );
                drawingContext.DrawText( tiltText, new Point( ( _element.ActualWidth / 2.0 ) - ( tiltText.Width / 2.0 ), -tiltText.Height - 5 ) );
            }

            FormattedText FormatText( string text )
            {
                return new FormattedText( text, CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface( "Times New Roman" ), 14, Brushes.Black );
            }
        }
    }
}
