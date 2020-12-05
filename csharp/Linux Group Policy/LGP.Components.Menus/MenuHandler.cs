#region

using System;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;

#endregion

namespace LGP.Components.Menus
{
    /// <summary>
    ///   Concreate Implementation of the IMenu
    /// </summary>
    public partial class MenuHandler : IMenu
    {
        private bool _error;
        private Menu _mainMenu;
        private string _strerror;

        #region IMenu Members

        /// <summary>
        ///   Gets the components name
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return Properties.Resources.Menu;
        }


        /// <summary>
        ///   Gets the icon associated with this component
        /// </summary>
        /// <returns>Image</returns>
        public Image GetIcon()
        {
            return Framework.Images.GetImage( "lgp" , "128x128" );
        }


        /// <summary>
        ///   Registers this components menu handlers
        /// </summary>
        /// <param name = "menu">The program main menu</param>
        public void RegisterMenuEntries( IMenu menu )
        {
        }

        /// <summary>
        ///   Creates an instance of the menu controller and passes the appication menu reference to it
        /// </summary>
        /// <param name = "menu">The application menu</param>
        public void InitializeMenu( System.Windows.Controls.Menu menu )
        {
            try
            {
                this._mainMenu = menu;
                this.CreateFileMenu();
                this.CreateEditMenu();
                this.CreateViewMenu();
                this.CreateWindowMenu();
                this.CreateHelpMenu();

                foreach( var moduleComponent in Framework.LibraryHandler.GetModules() )
                {
                    moduleComponent.RegisterMenuEntries( this );
                }

                foreach( var component in Framework.LibraryHandler.GetComponents() )
                {
                    component.RegisterMenuEntries( this );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        /// <summary>
        ///   Creates a menu item
        /// </summary>
        /// <param name = "header">Name of the menu item</param>
        /// <returns></returns>
        public MenuItem CreateMenuItem( string header )
        {
            var menuitem = new MenuItem
            {
                Header = header
            };

            return menuitem;
        }


        /// <summary>
        ///   Creates a menu item with an image
        /// </summary>
        /// <param name = "header">Name of the menu item</param>
        /// <param name = "image">Image assocaited with the menu item</param>
        /// <returns></returns>
        public MenuItem CreateMenuItem( string header , Image image )
        {
            var menuitem = new MenuItem
            {
                Header = header , Icon = image
            };

            return menuitem;
        }

        #region IMenu Interface Realization

        /// <summary>
        ///   Adds a item to the main menu
        /// </summary>
        /// <param name = "item">The item to be added</param>
        public void AddMainItem( MenuItem item )
        {
            this._error = false;

            try
            {
                this._mainMenu.Items.Add( item );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                this._error = true;
                this._strerror = error.Message;
            }
        }


        /// <summary>
        ///   Remove an item from the menu at given index
        /// </summary>
        /// <param name = "indexItem">The index item to remove</param>
        public void RemoveMainMenuItem( int indexItem )
        {
            this._error = false;

            try
            {
                this._mainMenu.Items.RemoveAt( indexItem );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                this._error = true;
                this._strerror = error.Message;
            }
        }


        /// <summary>
        ///   Add an item under a given item specified by index
        /// </summary>
        /// <param name = "indexItem"></param>
        /// <param name = "subitem"></param>
        public void AddSubMenuItem( int indexItem , MenuItem subitem )
        {
            this._error = false;

            try
            {
                var menuitem = ( MenuItem ) this._mainMenu.Items[ indexItem ];
                menuitem.Items.Add( subitem );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                this._error = true;
                this._strerror = error.Message;
            }
        }


        /// <summary>
        ///   Remove an index given two indexes Item / subitem
        /// </summary>
        /// <param name = "indexItem">The main item index</param>
        /// <param name = "subitem">The sub item index</param>
        public void RemoveSubMenuItem( int indexItem , int subitem )
        {
            this._error = false;

            try
            {
                var menuitem = ( MenuItem ) this._mainMenu.Items[ indexItem ];
                menuitem.Items.RemoveAt( subitem );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                this._error = true;
                this._strerror = error.Message;
            }
        }


        /// <summary>
        ///   Add a subitem to a subitem item, item / subitem specified by indexes
        /// </summary>
        /// <param name = "indexItem">The item index</param>
        /// <param name = "indexSubItem">The sub item index</param>
        /// <param name = "subsubitem">The new item to add to the sub item</param>
        public void AddSubSubMenuItem( int indexItem , int indexSubItem , MenuItem subsubitem )
        {
            this._error = false;

            try
            {
                var menuitem = ( MenuItem ) this._mainMenu.Items[ indexItem ];
                var menusubitems = ( MenuItem ) menuitem.Items[ indexSubItem ];
                menusubitems.Items.Add( subsubitem );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                this._error = true;
                this._strerror = error.Message;
            }
        }


        /// <summary>
        ///   Remove a indexed subitem , given an item index and its sub item index
        /// </summary>
        /// <param name = "indexItem">The item index</param>
        /// <param name = "indexSubItem">The sub item index</param>
        /// <param name = "indexSubSubItem">The item index to remove</param>
        public void RemoveSubMenuItem( int indexItem , int indexSubItem , int indexSubSubItem )
        {
            this._error = false;

            try
            {
                var menuitem = ( MenuItem ) this._mainMenu.Items[ indexItem ];
                var menusubitems = ( MenuItem ) menuitem.Items[ indexSubItem ];
                menusubitems.Items.RemoveAt( indexSubSubItem );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                this._error = true;
                this._strerror = error.Message;
            }
        }


        /// <summary>
        ///   Used as an accessor to check if there was an operation error
        /// </summary>
        /// <returns>True or false if an error occured</returns>
        public bool GetError()
        {
            return this._error;
        }


        /// <summary>
        ///   Gets the error from the last operation that caused it
        /// </summary>
        /// <returns>The error describing the error</returns>
        public string GetStrError()
        {
            return this._strerror;
        }

        #endregion
    }
}