#region

using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Markup;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Modules.PolicyEditor
{
    /// <summary>
    ///   Concreate Implmentation and boundary class to Module Compoents
    /// </summary>
    public class Plugin : Components.Factory.Interfaces.Module.IModule
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
            return Properties.Resources.PolicyEditor;
        }


        /// <summary>
        ///   Registers this components menu handlers
        /// </summary>
        /// <param name = "menu">The program main menu</param>
        public void RegisterMenuEntries( IMenu menu )
        {
            var resource = Framework.Utils.LoadResource(Assembly.GetExecutingAssembly() , this , ".Internal.ContextMenuItems.PolicyEditor.xaml" );

            if( resource == null )
            {
                return;
            }

            var menuentry = resource as MenuItem;

            if( menuentry == null )
            {
                return;
            }

            menuentry.Icon = Framework.Images.GetImage( "327_Options_16x16_72" , "VS2010ImageLibrary" , 12 );
            Framework.ContextMenus.RegisterContextMenuItem( typeof( IOu ) , menuentry , this.MenuItemClicked );
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

        private void MenuItemClicked( Object theitem )
        {
            try
            {
                var cpe = ( IOu ) theitem;
                Framework.Panels.AddMainComponent( new PolicyEditor( cpe ) , cpe.GetName() + " " + Properties.Resources.Policy );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Gets the side bar component name
        /// </summary>
        /// <returns>String name</returns>
        public string GetComponentSideBarName()
        {
            return Properties.Resources.PolicyEditor;
        }


        /// <summary>
        ///   Gets the component comtent name
        /// </summary>
        /// <returns></returns>
        public string GetComponentContentName()
        {
            return Properties.Resources.PolicyEditor;
        }
    }
}