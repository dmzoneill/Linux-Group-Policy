#region

using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Infrastructure;

#endregion

namespace LGP.Components.Notifications
{
    /// <summary>
    /// Interaction logic for Notifications.xaml
    /// </summary>
    public partial class Notifications : INotification
    {
        private static readonly double InitialLeft;
        private static readonly double InitialTop;
        private readonly int _popopTimeout = 1000;


        static Notifications()
        {
            try
            {
                InitialLeft = ( Framework.ApplicationWindow.Width / 2 ) + Framework.ApplicationWindow.Left;
                InitialTop = Framework.ApplicationWindow.Top + Framework.ApplicationWindow.Height - 50;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Constructor
        /// </summary>
        public Notifications()
        {
            try
            {
                this.InitializeComponent();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Constructor
        /// </summary>
        /// <param name = "message">text to show</param>
        /// <param name = "interval">poup timeout</param>
        public Notifications( string message , int interval )
        {
            try
            {
                this.InitializeComponent();

                this.ShowActivated = false;
                this.popupText.Text = message;
                this._popopTimeout = interval;

                var thread = Thread.CurrentThread;
                this.DataContext = new
                {
                    ThreadID = thread.ManagedThreadId
                };

                var typeface = new Typeface( new FontFamily( "Segoe UI" ) , FontStyles.Normal , FontWeights.Normal , FontStretches.Normal );

                GlyphTypeface glyphTypeface;
                if( typeface.TryGetGlyphTypeface( out glyphTypeface ) )
                {
                    var sizeWidth = MeasureTextWidth( glyphTypeface , 16 , this.popupText.Text );
                    this.Width = sizeWidth;
                    this.Left = InitialLeft - ( sizeWidth / 2 );
                    this.Top = InitialTop;
                    this.AnimateIn();
                }
                else
                {
                    this.Close();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region Implementation of INotifcation

        /// <summary>
        ///   Displays the popup for x microsecond
        /// </summary>
        /// <param name = "message">the message to display</param>
        /// <param name = "timeout">microseonds</param>
        public void Display( string message , int timeout )
        {
            try
            {
                var thread = new Thread( () =>
                {
                    var w = new Notifications( message , timeout );
                    w.Show();
                    w.Closed += ( sender1 , e1 ) => w.Dispatcher.InvokeShutdown();
                    Dispatcher.Run();
                } );
                thread.SetApartmentState( ApartmentState.STA );
                thread.IsBackground = true;
                thread.Start();
                this.Close();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        private static double MeasureTextWidth( GlyphTypeface glyphTypeface , double emSize , string s )
        {
            try
            {
                double width = 0;

                for( var i = 0; i < s.Length; i++ )
                {
                    var ch = s[ i ];
                    var glyph = glyphTypeface.CharacterToGlyphMap[ ch ];
                    var advanceWidth = glyphTypeface.AdvanceWidths[ glyph ];
                    width += advanceWidth;
                }
                return width * emSize;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return 0.0;
            }
        }

        private void AnimateIn()
        {
            try
            {
                var duration = new Duration( new TimeSpan( 0 , 0 , 0 , 0 , 500 ) );

                var opacityAnimation = new DoubleAnimation
                {
                    Duration = duration , From = this.Opacity , To = 0.9
                };

                var sb = new Storyboard
                {
                    Duration = duration
                };
                Storyboard.SetTargetProperty( opacityAnimation , new PropertyPath( OpacityProperty ) );
                sb.Children.Add( opacityAnimation );
                Storyboard.SetTarget( opacityAnimation , this.LGPPopup );

                sb.Completed += this.AnimateOut;
                sb.Begin();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void AnimateOut( object sender , EventArgs e )
        {
            try
            {
                this.Opacity = 0.9;

                Thread.Sleep( this._popopTimeout );

                var duration = new Duration( new TimeSpan( 0 , 0 , 0 , 1 , 500 ) );


                var opacityAnimation = new DoubleAnimation
                {
                    Duration = duration , From = 0.9 , To = 0.0
                };

                var sb1 = new Storyboard
                {
                    Duration = duration
                };
                Storyboard.SetTargetProperty( opacityAnimation , new PropertyPath( OpacityProperty ) );

                sb1.Children.Add( opacityAnimation );

                Storyboard.SetTarget( opacityAnimation , this.LGPPopup );

                sb1.Completed += this.CloseWindow;
                sb1.Begin();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void CloseWindow( object sender , EventArgs e )
        {
            this.Close();
        }
    }
}