using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Module;

namespace LGP.Components.Notifications
{
    /// <summary>
    ///   Plugin
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
            return Properties.Resources.Notifications;
        }


        /// <summary>
        ///   Gets the image that represents the module
        /// </summary>
        /// <returns>An image</returns>
        public Image GetIcon()
        {
            return Framework.Images.GetImage( "database2" , "VS2010ImageLibrary" , 14 );
        }


        /// <summary>
        ///   Asks the component to register is menu components
        /// </summary>
        /// <param name = "menu">The program main menu</param>
        public void RegisterMenuEntries( IMenu menu )
        {
        }

        #endregion

        #region Implementation of IModule

        /// <summary>
        ///   Gets the main component the module provides
        /// </summary>
        /// <returns>An instance of UserControl</returns>
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