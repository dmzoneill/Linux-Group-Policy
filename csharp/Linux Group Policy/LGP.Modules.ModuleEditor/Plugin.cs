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

namespace LGP.Modules.ModuleEditor
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
            return null;
        }


        /// <summary>
        ///   Gets the sidebar component the module provides
        /// </summary>
        /// <returns>An instance of UserControl</returns>
        public UserControl GetSidebarControl()
        {
            return ModuleList.GetInstance();
        }


        /// <summary>
        ///   Gets the name of the main component
        /// </summary>
        /// <returns>String name</returns>
        public string GetName()
        {
            return Properties.Resources.ModuleEditor;
        }


        /// <summary>
        ///   Registers this components menu handlers
        /// </summary>
        /// <param name = "menu">The program main menu</param>
        public void RegisterMenuEntries( IMenu menu )
        {
            try
            {
                var resource = Framework.Utils.LoadResource( Assembly.GetExecutingAssembly() , this , ".Internal.ContextMenuItems.ModuleEditor.xaml" );

                if( resource == null )
                {
                    return;
                }

                var moduleToolbarsItem = resource as MenuItem;

                if( moduleToolbarsItem == null )
                {
                    return;
                }

                moduleToolbarsItem.Icon = this.GetIcon();
                moduleToolbarsItem.Click += this.ModulesToolbarsItemClick;
                menu.AddSubSubMenuItem( 2 , 2 , moduleToolbarsItem );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Gets the image that represents the module
        /// </summary>
        /// <returns>An image</returns>
        public Image GetIcon()
        {
            return Framework.Images.GetImage( "pencil" , "16X16" , 14 );
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
        ///   Gets the name of the sidebar component
        /// </summary>
        /// <returns>String name</returns>
        public string GetComponentSideBarName()
        {
            return Properties.Resources.Modules;
        }


        /// <summary>
        ///   Gets the name of the component
        /// </summary>
        /// <returns>String name</returns>
        public string GetComponentContentName()
        {
            return Properties.Resources.ModuleEditor;
        }


        /// <summary>
        ///   Handler for this componets menu entries
        ///   Forwards the event to the message bus for subscribers interest
        /// </summary>
        /// <param name = "sender">The menu item that was clicked</param>
        /// <param name = "e">The event details</param>
        private void ModulesToolbarsItemClick( object sender , RoutedEventArgs e )
        {
            Framework.Panels.AddSideComponent( this.GetSidebarControl() , this.GetComponentSideBarName() , this.GetIcon() );

            var eventDetails = new MenuEvent( sender , "new" );
            Framework.EventBus.Publish( eventDetails );
        }
    }
}