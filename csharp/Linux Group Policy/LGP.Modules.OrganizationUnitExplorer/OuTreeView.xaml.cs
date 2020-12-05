#region

using System;
using System.Windows;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Components.Factory.Interfaces.Module;
using LGP.Components.Factory.Publishers.Events;
using LGP.Modules.OrganizationUnitExplorer.Internal;
using LGP.Modules.OrganizationUnitExplorer.Internal.CustomControls;

#endregion

namespace LGP.Modules.OrganizationUnitExplorer
{
    /// <summary>
    ///   Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class OuTreeView : IUserControl
    {
        private static UserControl _instance;

        /// <summary>
        ///   Constructor
        /// </summary>
        public OuTreeView()
        {
            try
            {
                this.InitializeComponent();
                this.ImageRefresh.Source = Framework.Images.GetImage( "RepeatHS" , "VS2010ImageLibrary" ).Source;
                this.Tag = Properties.Resources.OuExplorer;

                if( Framework.Database.IsConnected() == false )
                {
                    Framework.Notification.Display( Properties.Resources.DatabaseConnectionRequired , 5000 );
                    Framework.EventBus.Publish( new PreferencesEvent( this , typeof( IDatabase ) ) );
                    return;
                }

                OuHelper.BuildOuTree( this.treeView1 , true , null );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Get the instance of the module ( Singleton )
        /// </summary>
        /// <returns>Usercontrol instance</returns>
        public static UserControl GetInstance()
        {
            return _instance ?? ( _instance = new OuTreeView() );
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

                var gw = Framework.Database.CreateOuGateway();
                gw.Refresh();
                TreeViewOuElement.Dispose();
                this.treeView1.Items.Clear();
                OuHelper.BuildOuTree( this.treeView1 , true , null );
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
            this.Tag = Properties.Resources.OuExplorer;
            TreeViewOuElement.Refresh();
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