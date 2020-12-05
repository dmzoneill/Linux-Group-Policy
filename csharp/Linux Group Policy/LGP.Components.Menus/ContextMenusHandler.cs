#region

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Infrastructure;

#endregion

namespace LGP.Components.Menus
{
    /// <summary>
    /// Conext menus handler
    /// </summary>
    public class ContextMenusHandler : IContextMenus
    {
        private readonly List< Entry > _registrations;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContextMenusHandler()
        {
            this._registrations = new List< Entry >();
        }

        #region Implementation of IContextMenus

        /// <summary>
        ///   Asks the Context menu handler to append menu entries for a given context menu
        /// </summary>
        /// <param name = "menuparent">Conext menu to add entries to</param>
        /// <param name = "type">Type of item that the conext is being appended to</param>
        /// <param name = "action">callback</param>
        public void AttachMenuItemRegistrations( ref ContextMenu menuparent , Type type , Action< Object , RoutedEventArgs > action )
        {
            try
            {
                var newparent = menuparent as object;
                this.AttachToNewParent( ref newparent , type , action );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Asks the Context menu handler to append menu entries for a given context menu
        /// </summary>
        /// <param name = "menuparent">Conext menu to add entries to</param>
        /// <param name = "type">Type of item that the conext is being appended to</param>
        /// <param name = "action">callback</param>
        public void AttachMenuItemRegistrations( ref Menu menuparent , Type type , Action< Object , RoutedEventArgs > action )
        {
            try
            {
                var newparent = menuparent as object;
                this.AttachToNewParent( ref newparent , type , action );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// The call back mechanism
        /// </summary>
        /// <param name="sender">the meuu item clicked</param>
        /// <param name="theobject"></param>
        public void CallBack( MenuItem sender , object theobject )
        {
            try
            {
                foreach( var reg in this._registrations )
                {
                    if( sender == reg.MenuEntry )
                    {
                        reg.Action( theobject );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Plugin registers for context menus that are on type
        /// </summary>
        /// <param name = "type">type</param>
        /// <param name="item"></param>
        /// <param name = "action">The call back mechanism</param>
        public void RegisterContextMenuItem( Type type , MenuItem item , Action< Object > action )
        {
            try
            {
                item.Foreground = Brushes.Black;

                var entry = new Entry
                {
                    Action = action ,
                    Type = type ,
                    MenuEntry = item
                };

                if( this.CheckRegistrations( entry ) == false )
                {
                    this._registrations.Add( entry );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Asks the Context menu handler to append menu entries for a given context menu
        /// </summary>
        /// <param name = "menuparent">Conext menu to add entries to</param>
        /// <param name = "type">Type of item that the conext is being appended to</param>
        /// <param name = "action">callback</param>
        public void AttachMenuItemRegistrations( ref MenuItem menuparent , Type type , Action< Object , RoutedEventArgs > action )
        {
            try
            {
                var newparent = menuparent as object;
                this.AttachToNewParent( ref newparent , type , action );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private bool CheckRegistrations( Entry entry )
        {
            foreach( var reg in this._registrations )
            {
                if( reg.Type == entry.Type && reg.MenuEntry == entry.MenuEntry )
                {
                    return true;
                }
            }
            return false;
        }


        private void UnattachFromCurrentParent( int current )
        {
            if( this._registrations[ current ].MenuEntry.Parent != null )
            {
                var contextmenu = this._registrations[ current ].MenuEntry.Parent as ContextMenu;
                var menuitem = this._registrations[ current ].MenuEntry.Parent as MenuItem;
                var menu = this._registrations[ current ].MenuEntry.Parent as Menu;

                if( contextmenu != null )
                {
                    contextmenu.Items.Remove( this._registrations[ current ].MenuEntry );
                }

                if( menuitem != null )
                {
                    menuitem.Items.Remove( this._registrations[ current ].MenuEntry );
                }

                if( menu != null )
                {
                    menu.Items.Remove( this._registrations[ current ].MenuEntry );
                }

                this._registrations[ current ].MenuEntry.Click -= this._registrations[ current ].PrevHandler;
            }
        }


        private void AttachToNewParent( ref object menuparent , Type type , Action< Object , RoutedEventArgs > action )
        {
            try
            {
                for( var i = 0; i < this._registrations.Count; i++ )
                {
                    if( this._registrations[ i ].Type != type )
                    {
                        continue;
                    }

                    this.UnattachFromCurrentParent( i );

                    this._registrations[ i ].PrevHandler = new RoutedEventHandler( action );
                    this._registrations[ i ].MenuEntry.Click += this._registrations[ i ].PrevHandler;

                    if( menuparent is Menu )
                    {
                        var parentMenu = menuparent as Menu;
                        parentMenu.Items.Add( this._registrations[ i ].MenuEntry );
                    }

                    if( menuparent is MenuItem )
                    {
                        var parentMenu = menuparent as MenuItem;
                        parentMenu.Items.Add( this._registrations[ i ].MenuEntry );
                    }

                    if( menuparent is ContextMenu )
                    {
                        var parentMenu = menuparent as ContextMenu;
                        parentMenu.Items.Add( this._registrations[ i ].MenuEntry );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        #region Nested type: Entry

        private class Entry
        {
            public Action< Object > Action;
            public MenuItem MenuEntry;
            public RoutedEventHandler PrevHandler;
            public Type Type;
        }

        #endregion
    }
}