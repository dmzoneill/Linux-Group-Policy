#region

using System;
using System.Windows;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Components.Factory.Interfaces.Module;
using LGP.Components.Factory.Interfaces.Network;
using LGP.Components.Factory.Publishers.Events;
using LGP.Modules.ModuleEditor.Internal;
using LGP.Modules.ModuleEditor.Internal.Modals.Controls;

#endregion

namespace LGP.Modules.ModuleEditor
{
    /// <summary>
    ///   Interaction logic for ModuleList.xaml
    /// </summary>
    public partial class ModuleList : IUserControl
    {
        #region Delegates

        /// <summary>
        /// </summary>
        /// <param name = "moduleName"></param>
        /// <param name = "panel"></param>
        public delegate void AddModuleDelegate( string moduleName , StackPanel panel );

        #endregion

        private static ModuleList _instance;

        /// <summary>
        ///   Call back delegate for the poup add module
        /// </summary>
        public AddModuleDelegate CallBackDelegate;

        /// <summary>
        ///   Constructor for the module
        /// </summary>
        public ModuleList()
        {
            try
            {
                this.InitializeComponent();

                ModuleHandler.Panel = this.stackPanel1;

                this.ImageNew.Source = Framework.Images.GetImage( "NewDocumentHS" , "VS2010ImageLibrary" ).Source;
                this.ImageRefresh.Source = Framework.Images.GetImage( "RepeatHS" , "VS2010ImageLibrary" ).Source;
                this.Tag = Properties.Resources.Modules;

                _instance = this;

                if( Framework.Database.IsConnected() == false )
                {
                    Framework.Notification.Display( Properties.Resources.DatabaseConnectionRequired , 5000 );
                    Framework.EventBus.Publish( new PreferencesEvent( this , typeof( IDatabase ) ) );
                    return;
                }

                if( ModuleHandler.GetClient() == null )
                {
                    Framework.Notification.Display( Properties.Resources.ServerConnectionRequired , 5000 );
                    Framework.EventBus.Publish( new PreferencesEvent( this , typeof( INetwork ) ) );
                    return;
                }

                ModuleHandler.RefreshModuleFiles();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static ModuleList GetInstance()
        {
            return _instance ?? ( _instance = new ModuleList() );
        }

        private void NewMenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                if( Framework.Database.IsConnected() == false )
                {
                    Framework.Notification.Display( Properties.Resources.DatabaseConnectionRequired , 5000 );
                    Framework.EventBus.Publish( new PreferencesEvent( this , typeof( IDatabase ) ) );
                    return;
                }

                if( ModuleHandler.GetClient() == null )
                {
                    Framework.Notification.Display( Properties.Resources.ServerConnectionRequired , 5000 );
                    Framework.EventBus.Publish( new PreferencesEvent( this , typeof( INetwork ) ) );
                    return;
                }

                this.CallBackDelegate += ModuleHandler.CreateModule;
                var popup = Framework.Dialog;
                var cou = new AddModule( this , popup );
                popup.SetChild( cou );
                popup.ShowBlocking();
                this.CallBackDelegate -= ModuleHandler.CreateModule;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void RefreshMenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                if( Framework.Database.IsConnected() == false )
                {
                    Framework.Notification.Display( Properties.Resources.DatabaseConnectionRequired , 5000 );
                    Framework.EventBus.Publish( new PreferencesEvent( this , typeof( IDatabase ) ) );
                    return;
                }

                if( ModuleHandler.GetClient() == null )
                {
                    Framework.Notification.Display( Properties.Resources.ServerConnectionRequired , 5000 );
                    Framework.EventBus.Publish( new PreferencesEvent( this , typeof( INetwork ) ) );
                    return;
                }

                ModuleHandler.RefreshModuleFiles();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void ToolBarLoaded( object sender , RoutedEventArgs e )
        {
            try
            {
                var toolBar = sender as ToolBar;
                if( toolBar != null )
                {
                    var overflowGrid = toolBar.Template.FindName( "OverflowGrid" , toolBar ) as FrameworkElement;
                    if( overflowGrid != null )
                    {
                        overflowGrid.Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region Implementation of IUserControl

        /// <summary>
        ///   Aks the UserControl to do some clean up!
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Asks the Usercontrol to refresh itself
        /// </summary>
        public void Refresh()
        {
            this.Tag = Properties.Resources.Modules;
        }


        /// <summary>
        /// When the control is no longer in focus, asks it to pause ( heavy duty non critical, ie threads updating graphs )
        /// </summary>
        public void Pause()
        {
            try
            {
            }
            catch( Exception )
            {
            }
        }

        /// <summary>
        /// When the tab comes back into focus, resume ( heavy duty non critical, ie threads updating graphs )
        /// </summary>
        public void Resume()
        {
            try
            {
            }
            catch( Exception )
            {
            }
        }

        #endregion
    }
}