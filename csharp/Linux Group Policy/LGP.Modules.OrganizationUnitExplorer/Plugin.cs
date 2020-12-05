#region

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Module;
using LGP.Components.Factory.Publishers.Events;

#endregion

namespace LGP.Modules.OrganizationUnitExplorer
{
    /// <summary>
    ///   Concreate Implmentation and boundary class to Module Compoents
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
            return new OuViewer();
        }


        /// <summary>
        ///   Gets the side bar component of the module
        /// </summary>
        /// <returns>Usercontrol component</returns>
        public UserControl GetSidebarControl()
        {
            return OuTreeView.GetInstance();
        }


        /// <summary>
        ///   Get the component name
        /// </summary>
        /// <returns>String name</returns>
        public string GetName()
        {
            return Properties.Resources.OuExplorer;
        }


        /// <summary>
        ///   Registers this components menu handlers
        /// </summary>
        /// <param name = "menu">The program main menu</param>
        public void RegisterMenuEntries( IMenu menu )
        {
            try
            {
                var resource = Framework.Utils.LoadResource( Assembly.GetExecutingAssembly() , this , ".Internal.ContextMenuItems.Ou.xaml" );

                if( resource == null )
                {
                    return;
                }

                var outreeToolbarsItem = resource as MenuItem;

                if( outreeToolbarsItem == null )
                {
                    return;
                }

                outreeToolbarsItem.Icon = this.GetIcon();
                outreeToolbarsItem.Click += this.OutreeToolbarsItemClick;
                menu.AddSubSubMenuItem( 2 , 2 , outreeToolbarsItem );
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
            return Framework.Images.GetImage( "category-2" , "16X16" , 14 );
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
            return Properties.Resources.OuTree;
        }


        /// <summary>
        ///   Gets the component comtent name
        /// </summary>
        /// <returns></returns>
        public string GetComponentContentName()
        {
            return Properties.Resources.OuView;
        }


        /// <summary>
        ///   Handler for this componets menu entries
        ///   Forwards the event to the message bus for subscribers interest
        /// </summary>
        /// <param name = "sender">The menu item that was clicked</param>
        /// <param name = "e">The event details</param>
        private void OutreeToolbarsItemClick( object sender , RoutedEventArgs e )
        {
            Framework.Panels.AddSideComponent( this.GetSidebarControl() , this.GetComponentSideBarName() , this.GetIcon() );

            var eventDetails = new MenuEvent( sender , "new" );
            Framework.EventBus.Publish( eventDetails );
        }
    }
}