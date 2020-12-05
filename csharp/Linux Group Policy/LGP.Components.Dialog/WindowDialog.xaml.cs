#region

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Infrastructure;

#endregion

namespace LGP.Components.Dialog
{
    /// <summary>
    ///   Interaction logic for WindowDialog.xaml
    /// </summary>
    public partial class WindowDialog : IDialog
    {
        private int _pheight;
        private int _pwidth;

        /// <summary>
        ///   Constructor
        /// </summary>
        public WindowDialog()
        {
            try
            {
                this.InitializeComponent();
                this.Owner = Framework.ApplicationWindow;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region IDialog Members

        /// <summary>
        ///   Sets the popup window child control
        /// </summary>
        /// <param name = "control">usercontrol</param>
        public void SetChild( UserControl control )
        {
            try
            {
                control.HorizontalAlignment = HorizontalAlignment.Center;
                control.VerticalAlignment = VerticalAlignment.Center;
                this.popupPane.Children.Add( control );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// </summary>
        /// <param name = "width"></param>
        /// <param name = "height"></param>
        public void ResizePopup( int width , int height )
        {
            try
            {
                this._pwidth = width + 40;
                this._pheight = height + 40;
                var duration = new Duration( new TimeSpan( 0 , 0 , 0 , 0 , 250 ) );


                var leftPopup = ( ( Framework.ApplicationWindow.Width / 2 ) - ( ( double ) width / 2 ) ) + Framework.ApplicationWindow.Left;
                var topPopup = ( Framework.ApplicationWindow.Height / 2 - ( ( double ) height / 2 ) ) + Framework.ApplicationWindow.Top;

                var opacityAnimation = new DoubleAnimation
                {
                    Duration = duration , From = this.Opacity , To = 0.95
                };
                var horizontalAnimation = new DoubleAnimation
                {
                    Duration = duration , From = this.Left , To = leftPopup
                };
                var verticalAnimation = new DoubleAnimation
                {
                    Duration = duration , From = this.Top , To = topPopup
                };
                var widthAnimation = new DoubleAnimation
                {
                    Duration = duration , From = this.ActualWidth , To = this._pwidth
                };
                var heightAnimation = new DoubleAnimation
                {
                    Duration = duration , From = this.ActualHeight , To = this._pheight
                };

                var sb = new Storyboard
                {
                    Duration = duration
                };

                Storyboard.SetTargetProperty( widthAnimation , new PropertyPath( WidthProperty ) );
                Storyboard.SetTargetProperty( heightAnimation , new PropertyPath( HeightProperty ) );
                Storyboard.SetTargetProperty( horizontalAnimation , new PropertyPath( LeftProperty ) );
                Storyboard.SetTargetProperty( verticalAnimation , new PropertyPath( TopProperty ) );
                Storyboard.SetTargetProperty( opacityAnimation , new PropertyPath( OpacityProperty ) );

                sb.Children.Add( heightAnimation );
                sb.Children.Add( widthAnimation );
                sb.Children.Add( horizontalAnimation );
                sb.Children.Add( verticalAnimation );
                sb.Children.Add( opacityAnimation );

                Storyboard.SetTarget( widthAnimation , this.PWindow );
                Storyboard.SetTarget( heightAnimation , this.PWindow );
                Storyboard.SetTarget( horizontalAnimation , this.PWindow );
                Storyboard.SetTarget( verticalAnimation , this.PWindow );
                Storyboard.SetTarget( opacityAnimation , this.PWindow );

                sb.Completed += this.SbCompleted;
                sb.Begin();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Closes the dialog
        /// </summary>
        public void CloseDialog()
        {
            this.Close();
        }


        /// <summary>
        ///   Show the dialog, non blocking
        /// </summary>
        public void ShowNonBlocking()
        {
            this.Show();
        }


        /// <summary>
        ///   Show the dialog, blocking
        /// </summary>
        public void ShowBlocking()
        {
            this.ShowDialog();
        }

        #endregion

        private void SbCompleted( object sender , EventArgs e )
        {
            this.Opacity = 0.95;
            this.Width = this._pwidth;
            this.Height = this._pheight;
        }
    }
}