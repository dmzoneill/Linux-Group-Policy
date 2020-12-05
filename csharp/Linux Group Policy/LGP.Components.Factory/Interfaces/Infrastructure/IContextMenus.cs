#region

using System;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace LGP.Components.Factory.Interfaces.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public interface IContextMenus
    {
        /// <summary>
        ///   Asks the Context menu handler to append menu entries for a given context menu
        /// </summary>
        /// <param name = "menuparent">Conext menu to add entries to</param>
        /// <param name = "type">Type of item that the conext is being appended to</param>
        /// <param name = "action">callback</param>
        void AttachMenuItemRegistrations( ref Menu menuparent , Type type , Action< Object , RoutedEventArgs > action );

        /// <summary>
        ///   Asks the Context menu handler to append menu entries for a given context menu
        /// </summary>
        /// <param name = "menuparent">Conext menu to add entries to</param>
        /// <param name = "type">Type of item that the conext is being appended to</param>
        /// <param name = "action">callback</param>
        void AttachMenuItemRegistrations( ref MenuItem menuparent , Type type , Action< Object , RoutedEventArgs > action );

        /// <summary>
        ///   Asks the Context menu handler to append menu entries for a given context menu
        /// </summary>
        /// <param name = "menuparent">Conext menu to add entries to</param>
        /// <param name = "type">Type of item that the conext is being appended to</param>
        /// <param name = "action">callback</param>
        void AttachMenuItemRegistrations( ref ContextMenu menuparent , Type type , Action< Object , RoutedEventArgs > action );


        /// <summary>
        /// The call back mechanism
        /// </summary>
        /// <param name="sender">the meuu item clicked</param>
        /// <param name="theobject"></param>
        void CallBack( MenuItem sender , object theobject );


        /// <summary>
        ///   Plugin registers for context menus that are on type
        /// </summary>
        /// <param name = "type">type</param>
        /// <param name="item"></param>
        /// <param name = "action">The call back mechanism</param>
        void RegisterContextMenuItem( Type type , MenuItem item , Action< Object > action );
    }
}