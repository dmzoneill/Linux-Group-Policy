#region

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using LGP.Components.Factory;
using LGP.Components.Factory.Publishers.Events;
using LGP.Controls;
using Microsoft.Practices.Prism.Events;
using ContextMenu = System.Windows.Forms.ContextMenu;
using Cursors = System.Windows.Input.Cursors;
using MenuItem = System.Windows.Forms.MenuItem;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using UserControl = System.Windows.Controls.UserControl;

#endregion

namespace LGP
{
    /// <summary>
    ///   Interaction logic for LGPWindow.xaml
    /// </summary>
    public partial class LGPWindow
    {
        private NotifyIcon _notify;
        private double _prevHeight;
        private double _prevLeft;
        private double _prevTop;
        private double _prevWidth;
        private bool _shownInTaskbar;


        /// <summary>
        ///   Constructor
        ///   Shows the splash screen loader
        ///   Subscribes to events
        /// </summary>
        public LGPWindow()
        {
            try
            {
                this.InitializeComponent();

                Framework.ApplicationWindow = this;

                new Splash().ShowDialog();

                Framework.Panels.SubscribeEvents();

                this.Icon = Framework.Images.ConvertBitmapToImage( Properties.Images.lgpico1 ).Source;

                Framework.EventBus.Subscribe< MenuEvent >( this.HandleMenuItemClick , ThreadOption.UIThread , true );
                Framework.EventBus.Subscribe< PanelEvent >( this.HandlePanelRequest , ThreadOption.UIThread , true );
                Framework.EventBus.Subscribe< PreferencesEvent >( this.HandlePreferencesRequest , ThreadOption.UIThread , true );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void HandlePreferencesRequest( PreferencesEvent pevent )
        {
            var preferences = new Settings( pevent )
            {
                Owner = this
            };
            preferences.ShowDialog();
            this.InvalidateVisual();
        }


        /// <summary>
        ///   Generic message handler for menu item clicked events
        /// </summary>
        /// <param name = "details"></param>
        private void HandleMenuItemClick( MenuEvent details )
        {
            try
            {
                if( details.Command == null )
                {
                    return;
                }

                if( details.Command == "exit" )
                {
                    this.Close();
                }
                else if( details.Command == "preferences" )
                {
                    var preferences = new Settings
                    {
                        Owner = this
                    };
                    preferences.ShowDialog();
                    this.InvalidateVisual();
                }
                else if( details.Command == "statusBar" )
                {
                    this.statusBarRow.Height = ( int ) this.statusBarRow.Height.Value == 0 ? new GridLength( 25 ) : new GridLength( 0 );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Generic message handler for menu item clicked events
        /// </summary>
        /// <param name = "details"></param>
        private void HandlePanelRequest( PanelEvent details )
        {
            try
            {
                if( ( ( UserControl ) Framework.Panels ).Parent == null )
                {
                    Grid.SetColumn( ( UserControl ) Framework.Panels , 0 );
                    Grid.SetRow( ( UserControl ) Framework.Panels , 2 );
                    Grid.SetColumnSpan( ( UserControl ) Framework.Panels , 3 );
                    this.main.Children.Add( ( UserControl ) Framework.Panels );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Window loaded internal event
        /// </summary>
        /// <param name = "sender">The object that raised the window loaded event</param>
        /// <param name = "e">Event arguments associated with the event</param>
        private void WindowLoaded( object sender , RoutedEventArgs e )
        {
            try
            {
                this._shownInTaskbar = !this._shownInTaskbar;

                Framework.Menu.InitializeMenu( this.menu1 );

                this.dragBar.PreviewMouseLeftButtonDown += this.ChromeLessWindowSpaceMouseLeftButtonDown;
                this.dragBar.PreviewMouseDoubleClick += this.DragBarPreviewMouseDoubleClick;

                this._notify = new NotifyIcon
                {
                    Text = Properties.Resources.LinuxGroupPolicyStudio , Icon = Framework.Images.GetIcon( "lgpico" ) , Visible = true
                };
                this._notify.MouseClick += this.NotifyClick;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void NotifyClick( object sender , MouseEventArgs e )
        {
            if( e != null && e.Button == MouseButtons.Right )
            {
                // to do right click menu
            }
            else
            {
                this._shownInTaskbar = !this._shownInTaskbar;

                if( this._shownInTaskbar )
                {
                    this.ShowInTaskbar = this._shownInTaskbar;
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    this.ShowInTaskbar = this._shownInTaskbar;
                    this.WindowState = WindowState.Minimized;
                }
            }
        }


        /// <summary>
        ///   Generic window closing event
        /// </summary>
        /// <param name = "sender">The object that raised the window closing event</param>
        /// <param name = "e">Event arguments associated with the event</param>
        private void LGPMainWindowClosing( object sender , CancelEventArgs e )
        {
            try
            {
                this._notify.Dispose();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Title bar double click event handler
        /// </summary>
        /// <param name = "sender">The object that raised the double click event</param>
        /// <param name = "e">Event arguments associated with the event</param>
        private void DragBarPreviewMouseDoubleClick( object sender , MouseButtonEventArgs e )
        {
            try
            {
                this.Resize();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Title bar click event handler
        /// </summary>
        /// <param name = "sender">The object that raised the click event</param>
        /// <param name = "e">Event arguments associated with the event</param>
        private void ChromeLessWindowSpaceMouseLeftButtonDown( object sender , MouseButtonEventArgs e )
        {
            try
            {
                this.Cursor = Cursors.SizeAll;
                this.DragMove();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
            this.Cursor = Cursors.Arrow;
        }


        /// <summary>
        ///   The window close button event handler
        /// </summary>
        /// <param name = "sender">The object that raised the click event</param>
        /// <param name = "e">Event arguments associated with the event</param>
        private void ButtonWindowCloseClick( object sender , RoutedEventArgs e )
        {
            try
            {
                this.Close();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   The window maximize button event handler
        /// </summary>
        /// <param name = "sender">The object that raised the click event</param>
        /// <param name = "e">Event arguments associated with the event</param>
        private void ButtonWindowMaximizeClick( object sender , RoutedEventArgs e )
        {
            try
            {
                this.Resize();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Wpf window mode breaks maximize so we need to do it ourselves.
        /// </summary>
        private void Resize()
        {
            try
            {
                if( ( int ) this.Width == Screen.PrimaryScreen.WorkingArea.Width && ( int ) this.Height == Screen.PrimaryScreen.WorkingArea.Height )
                {
                    this.Left = this._prevLeft;
                    this.Top = this._prevTop;
                    this.Width = this._prevWidth;
                    this.Height = this._prevHeight;
                }
                else
                {
                    this._prevWidth = this.Width;
                    this._prevHeight = this.Height;
                    this._prevTop = this.Top;
                    this._prevLeft = this.Left;

                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    this.Left = 0;
                    this.Top = 0;
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   The window minimize button event handler
        /// </summary>
        /// <param name = "sender">The object that raised the click event</param>
        /// <param name = "e">Event arguments associated with the event</param>
        private void ButtonWindowMinimizeClick( object sender , RoutedEventArgs e )
        {
            try
            {
                this.NotifyClick( this._notify , null );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}