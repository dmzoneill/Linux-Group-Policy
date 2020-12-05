#region

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Module;

#endregion

namespace LGP.Modules.Statistics
{
    /// <summary>
    ///   Plugin to realize the IModule interface
    /// </summary>
    public class Plugin : IModule
    {
        #region IModule Members

        /// <summary>
        ///   Gets the main component of the module
        /// </summary>
        /// <returns>Usercontrol component</returns>
        public UserControl GetMainControl()
        {
            return Viewer.GetInstance();
        }


        /// <summary>
        ///   Gets the side bar component of the module
        /// </summary>
        /// <returns>Usercontrol component</returns>
        public UserControl GetSidebarControl()
        {
            return null;
        }


        /// <summary>
        ///   Get the component name
        /// </summary>
        /// <returns>String name</returns>
        public string GetName()
        {
            return Properties.Resources.StatisticsViewer;
        }


        /// <summary>
        ///   Registers this components menu handlers
        /// </summary>
        /// <param name = "menu">The program main menu</param>
        public void RegisterMenuEntries( IMenu menu )
        {
            try
            {
                var resource = Framework.Utils.LoadResource( Assembly.GetExecutingAssembly() , this , ".Internal.ContextMenuItems.Statistics.xaml" );

                if( resource == null )
                {
                    return;
                }

                var statisticsToolbarsItem = resource as MenuItem;

                if( statisticsToolbarsItem == null )
                {
                    return;
                }

                statisticsToolbarsItem.Icon = this.GetIcon();
                statisticsToolbarsItem.Click += this.StatisticsViewerToolbarItemClick;
                menu.AddSubSubMenuItem( 2 , 2 , statisticsToolbarsItem );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Gets the icon associated with plugin
        /// </summary>
        /// <returns></returns>
        public Image GetIcon()
        {
            return Framework.Images.GetImage( "config" , "16X16" , 14 );
        }


        /// <summary>
        ///   Gets the bottombar component the module provides
        /// </summary>
        /// <returns>An instance of UserControl</returns>
        public UserControl GetBottombarControl()
        {
            return null;
        }


        /// <summary>
        ///   Gets the toolbar component the module provides
        /// </summary>
        /// <returns>An instance of UserControl</returns>
        public UserControl GetToolbarControl()
        {
            return null;
        }


        /// <summary>
        ///   Gets the preferences component the module provides
        /// </summary>
        /// <returns>An instance of UserControl</returns>
        public UserControl GetPreferencesControl()
        {
            return new Preferences();
        }

        #endregion

        /// <summary>
        ///   Gets the side bar component name
        /// </summary>
        /// <returns>String name</returns>
        public string GetComponentSideBarName()
        {
            return Properties.Resources.StatisticsViewer;
        }


        /// <summary>
        ///   Gets the component comtent name
        /// </summary>
        /// <returns></returns>
        public string GetComponentContentName()
        {
            return Properties.Resources.StatisticsViewer;
        }

        /// <summary>
        ///   Handler for this componets menu entries
        ///   Forwards the event to the message bus for subscribers interest
        /// </summary>
        /// <param name = "sender">The menu item that was clicked</param>
        /// <param name = "e">The event details</param>
        private void StatisticsViewerToolbarItemClick( object sender , RoutedEventArgs e )
        {
            Framework.Panels.AddMainComponent( this.GetMainControl() , this.GetName() );
        }
    }
}