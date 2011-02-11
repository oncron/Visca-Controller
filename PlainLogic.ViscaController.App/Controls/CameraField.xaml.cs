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

namespace PlainLogic.ViscaController.App.Controls
{
    /// <summary>
    /// Interaction logic for CameraField.xaml
    /// </summary>
    public partial class CameraField : UserControl
    {
        #region Properties

        public static readonly DependencyProperty MinFOVProperty = DependencyProperty.Register( "MinFOV", typeof( Point ), typeof( CameraField ), new FrameworkPropertyMetadata( new Point( -100, -100 ), FrameworkPropertyMetadataOptions.AffectsRender ) );
        public static readonly DependencyProperty MaxFOVProperty = DependencyProperty.Register( "MaxFOV", typeof( Point ), typeof( CameraField ), new FrameworkPropertyMetadata( new Point( 100, 100 ), FrameworkPropertyMetadataOptions.AffectsRender ) );

        public static readonly DependencyProperty FocalPointLocationProperty = DependencyProperty.Register( "FocalPointLocation", typeof( Point ), typeof( CameraField ), new FrameworkPropertyMetadata( new Point( 0, 0 ), FrameworkPropertyMetadataOptions.AffectsRender ) );

        public static readonly DependencyProperty FocalPointSizeProperty = DependencyProperty.Register( "FocalPointSize", typeof( Size ), typeof( CameraField ), new FrameworkPropertyMetadata( new Size( .05, .05 ), FrameworkPropertyMetadataOptions.AffectsRender ), FocalPointProperty_ValidateValueCallback );

        static bool FocalPointProperty_ValidateValueCallback( object value )
        {
            Size newSize = (Size)value;

            if( newSize.Width < 0.0 || newSize.Width > 1.0 ) return false;
            if( newSize.Height < 0.0 || newSize.Height > 1.0 ) return false;

            return true;
        }

        public Point MinFOV
        {
            get { return (Point)GetValue( MinFOVProperty ); }
            set { SetValue( MinFOVProperty, value ); }
        }

        public Point MaxFOV
        {
            get { return (Point)GetValue( MaxFOVProperty ); }
            set { SetValue( MaxFOVProperty, value ); }
        }

        public Point FocalPointLocation
        {
            get { return (Point)GetValue( FocalPointLocationProperty ); }
            set { SetValue( FocalPointLocationProperty, value ); }
        }

        public Size FocalPointSize
        {
            get { return (Size)GetValue( FocalPointSizeProperty ); }
            set { SetValue( FocalPointSizeProperty, value ); }
        }


        #endregion

        public CameraField()
        {
            InitializeComponent();
        }

        protected override void OnRender( DrawingContext drawingContext )
        {
            base.OnRender( drawingContext );

            Vector fov = MaxFOV - MinFOV;
            Point worldMultiplier = new Point( ActualWidth / fov.X, ActualHeight / fov.Y );

            Rect rect = new Rect();
            rect.Size = new Size( ActualWidth * FocalPointSize.Width, ActualHeight * FocalPointSize.Height );
            rect.X = ( ( FocalPointLocation.X + ( fov.X / 2 ) ) * worldMultiplier.X ) - rect.Width / 2 ;
            rect.Y = ( ( FocalPointLocation.Y + ( fov.Y / 2 ) ) * worldMultiplier.Y ) - rect.Height / 2;

            drawingContext.DrawRectangle( null, new Pen( Brushes.Black, 2 ), rect );

            lblLocation.Text = String.Format( "X:{0} Y:{1}", FocalPointLocation.X, FocalPointLocation.Y );
        }
    }
}
