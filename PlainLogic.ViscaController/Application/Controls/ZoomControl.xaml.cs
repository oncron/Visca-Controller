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

namespace PlainLogic.ViscaController.Application.Controls
{
    /// <summary>
    /// Interaction logic for ZoomControl.xaml
    /// </summary>
    public partial class ZoomControl : UserControl
    {
        #region Properties

        public static readonly DependencyProperty ZoomRangeProperty = DependencyProperty.Register( "ZoomRange", typeof( int ), typeof( ZoomControl ), new FrameworkPropertyMetadata( 7 ) );
        
        static readonly DependencyPropertyKey ZoomPropertyKey = DependencyProperty.RegisterReadOnly( "Zoom", typeof( int ), typeof( ZoomControl ), new FrameworkPropertyMetadata( 0, FrameworkPropertyMetadataOptions.AffectsRender, Zoom_PropertyChangedCallback ) );
        public static readonly DependencyProperty ZoomProperty = ZoomPropertyKey.DependencyProperty;

        static void Zoom_PropertyChangedCallback( DependencyObject obj, DependencyPropertyChangedEventArgs args )
        {
            VISCACommands.Zoom.Execute( args.NewValue, (IInputElement)obj );
        }

        public int ZoomRange
        {
            get { return (int)GetValue( ZoomRangeProperty ); }
            set { SetValue( ZoomRangeProperty, value ); }
        }

        public int Zoom
        {
            get { return (int)GetValue( ZoomProperty ); }
            private set { SetValue( ZoomPropertyKey, value ); }
        }

        #endregion

        public ZoomControl()
        {
            InitializeComponent();
        }

        protected override void OnRender( DrawingContext drawingContext )
        {
            base.OnRender( drawingContext );

            Pen pen = new Pen( Brushes.Black, 2 );

            Point pt1 = new Point( ( ActualWidth / 2.0 ), grid.Margin.Top );
            Point pt2 = new Point( pt1.X, ActualHeight - grid.Margin.Bottom );

            drawingContext.DrawLine( pen, pt1, pt2 );
        }



        double? _dragStart;

        private void target_MouseDown( object sender, MouseButtonEventArgs e )
        {
            if( e.ChangedButton == MouseButton.Left && e.LeftButton == MouseButtonState.Pressed )
            {
                Mouse.Capture( target );
                Mouse.OverrideCursor = Cursors.None;

                var adornerLayer = AdornerLayer.GetAdornerLayer( target );
                //adornerLayer.Add( _adorner );

                _dragStart = e.GetPosition( this ).Y;
            }
        }

        private void target_MouseMove( object sender, MouseEventArgs e )
        {

            if( _dragStart.HasValue )
            {
                var diff = e.GetPosition( this ).Y - _dragStart.Value;

                double maxHeight = ( ActualHeight - grid.Margin.Top - grid.Margin.Bottom ) / 2.0;

                if( diff > maxHeight ) diff = maxHeight;
                if( diff < -maxHeight ) diff = -maxHeight;

                Zoom = (int)Math.Round( ( diff * ZoomRange / maxHeight ) );

                target.RenderTransform = new TranslateTransform( 0, diff );
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
                //adornerLayer.Remove( _adorner );

                Zoom = 0;

                _dragStart = null;
            }
        }

    }
}
