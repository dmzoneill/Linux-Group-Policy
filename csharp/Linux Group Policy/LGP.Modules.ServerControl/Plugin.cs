#region

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Module;

#endregion

namespace LGP.Modules.ServerControl
{
    /// <summary>
    /// Implementation of IModule
    /// </summary>
    public class Plugin : IModule
    {
        #region Implementation of IComponent

        /// <summary>
        ///   Gets the name of the main component
        /// </summary>
        /// <returns>String name</returns>
        public string GetName()
        {
            return Properties.Resources.ServerControl;
        }

        /// <summary>
        ///   Gets the image that represents the module
        /// </summary>
        /// <returns>An image</returns>
        public Image GetIcon()
        {
            return Framework.Images.GetImage( "config" , "16X16" , 14 );
        }

        /// <summary>
        ///   Asks the component to register is menu components
        /// </summary>
        /// <param name = "menu">The program main menu</param>
        public void RegisterMenuEntries( IMenu menu )
        {
            try
            {
                var resource = Framework.Utils.LoadResource( Assembly.GetExecutingAssembly() , this , ".Internal.ContextMenuItems.ServerControl.xaml" );

                if( resource == null )
                {
                    return;
                }

                var serverToolbarsItem = resource as MenuItem;

                if( serverToolbarsItem == null )
                {
                    return;
                }

                serverToolbarsItem.Icon = this.GetIcon();
                serverToolbarsItem.Click += this.SeverControlClick;
                menu.AddSubSubMenuItem( 2 , 2 , serverToolbarsItem );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void SeverControlClick( object sender , RoutedEventArgs e )
        {
            Framework.Panels.AddMainComponent( this.GetMainControl() , this.GetName() );
        }

        #endregion

        #region Implementation of IModule

        /// <summary>
        ///   Gets the main component the module provides
        /// </summary>
        /// <returns>An instance of UserControl</returns>
        public UserControl GetMainControl()
        {
            return ConnectionController.GetInstance();
        }

        /// <summary>
        ///   Gets the sidebar component the module provides
        /// </summary>
        /// <returns>An instance of UserControl</returns>
        public UserControl GetSidebarControl()
        {
            return null;
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
    }
}