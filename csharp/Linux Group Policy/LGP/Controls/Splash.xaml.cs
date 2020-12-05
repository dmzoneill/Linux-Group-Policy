#region

using System;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using LGP.Components.Factory;
using Timer = System.Timers.Timer;

#endregion

namespace LGP.Controls
{
    /// <summary>
    ///   Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash
    {
        private readonly string _loadingText;
        private bool _closingFininshed;
        private Thread _thread;
        private ThreadStart _threadStart;
        private int _tickNum;
        private Timer _tickticktick;


        /// <summary>
        ///   Constructor for the splash screen
        /// </summary>
        public Splash()
        {
            try
            {
                this.InitializeComponent();
                this._loadingText = this.textBlockLoading.Text;
                this._tickNum = 3;
                this.textBlockCopyright.Text = "David O Neill" + Environment.NewLine + "© Feeditout.com 2012" + Environment.NewLine + "dave@feeditout.com";
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private static string Repeat( string value , int count )
        {
            try
            {
                return new StringBuilder().Insert( 0 , value , count ).ToString();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }


        private void WindowLoaded( object sender , RoutedEventArgs e )
        {
            try
            {
                this.image1.Source = Framework.Images.GetImage( "lgp" , "512x512" ).Source;

                this._tickticktick = new Timer
                {
                    Enabled = true
                };

                this._tickticktick.Elapsed += this.TickticktickElapsed;
                this._tickticktick.Interval = 500;
                this._tickticktick.Start();

                this._threadStart = this.Loader;
                this._thread = new Thread( this._threadStart );
                this._thread.Start();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void TickticktickElapsed( object sender , ElapsedEventArgs e )
        {
            try
            {
                this._tickNum += 1;

                var mod = ( this._tickNum % 3 ) + 1;
                var text = this._loadingText + Repeat( "." , mod );

                this.textBlockLoading.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( () =>
                {
                    this.textBlockLoading.Text = text;
                } ) );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void UpdateGuiTextBoxes( string dllpath , string dllComponentName , string dllVersion , int progress , int total )
        {
            try
            {
                this.textBlockDllPath.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( delegate
                {
                    this.textBlockDllPath.Text = dllpath;
                } ) );


                this.textBlockComponentName.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( delegate
                {
                    this.textBlockComponentName.Text = dllComponentName;
                } ) );


                this.textBlockComponentVersion.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( delegate
                {
                    this.textBlockComponentVersion.Text = dllVersion;
                } ) );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void Loader()
        {
            try
            {
                var count = 1;

                var asmNum = Framework.Libraries.Length;

                foreach( var dllPath in Framework.Libraries )
                {
                    Framework.LibraryHandler.LoadIComponent( dllPath );
                    var asmName = Framework.LibraryHandler.GetAssemblyName( dllPath );
                    var asmVersion = Framework.LibraryHandler.GetAssemblyVersion( dllPath );

                    if( Framework.HasError )
                    {
                        this.UpdateGuiTextBoxes( "" , "" , "" , count , Framework.Libraries.Length );
                    }
                    else
                    {
                        this.UpdateGuiTextBoxes( dllPath , asmName , asmVersion , count , asmNum );
                    }

                    count++;
                    Thread.Sleep( 200 );
                }

                this.Dispatcher.BeginInvoke( DispatcherPriority.Normal , ( Action ) ( this.Close ) );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void FormFadeOutCompleted( object sender , EventArgs e )
        {
            try
            {
                this._closingFininshed = true;
                this.Close();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void WindowClosing( object sender , CancelEventArgs e )
        {
            try
            {
                if( !this._closingFininshed )
                {
                    this._tickticktick.Stop();
                    this.FormFadeOut.Begin();
                    e.Cancel = true;
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}