#region

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Publishers.Events;

#endregion

namespace LGP.Components.Menus
{
    /// <summary>
    ///   File menu
    /// </summary>
    public partial class MenuHandler
    {
        /// <summary>
        ///   Creates the file menu
        /// </summary>
        public void CreateFileMenu()
        {
            try
            {
                var resource = Framework.Utils.LoadResource( Assembly.GetExecutingAssembly() , this , ".FileMenu.xaml" );

                if( resource != null )
                {
                    var menu = resource as MenuItem;
                    if( menu != null )
                    {
                        var menuitem = ( MenuItem ) menu.FindName( "New" );
                        if( menuitem != null )
                        {
                            menuitem.Click += this.MenuItemNewClick;
                            menuitem.Icon = Framework.Images.GetImage( "DocumentHS" , "VS2010ImageLibrary" , 16 );
                        }

                        menuitem = ( MenuItem ) menu.FindName( "Open" );
                        if( menuitem != null )
                        {
                            menuitem.Click += this.MenuItemOpenClick;
                            menuitem.Icon = Framework.Images.GetImage( "openHS" , "VS2010ImageLibrary" , 16 );
                        }

                        menuitem = ( MenuItem ) menu.FindName( "Save" );
                        if( menuitem != null )
                        {
                            menuitem.Click += this.MenuItemSaveClick;
                            menuitem.Icon = Framework.Images.GetImage( "saveHS" , "VS2010ImageLibrary" , 16 );
                        }

                        menuitem = ( MenuItem ) menu.FindName( "Saveas" );
                        if( menuitem != null )
                        {
                            menuitem.Click += this.MenuItemSaveAsClick;
                            menuitem.Icon = Framework.Images.GetImage( "saveHS" , "VS2010ImageLibrary" , 16 );
                        }

                        menuitem = ( MenuItem ) menu.FindName( "Exit" );
                        if( menuitem != null )
                        {
                            menuitem.Click += this.MenuItemExitClick;
                            menuitem.Icon = Framework.Images.GetImage( "305_Close_16x16_72" , "VS2010ImageLibrary" , 16 );
                        }

                        this._mainMenu.Items.Add( menu );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Takes the exit menu item click events and publishes it to subscribers
        /// </summary>
        /// <param name = "sender">The object that raised the event</param>
        /// <param name = "e">The event arguments</param>
        private void MenuItemExitClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var eventDetails = new MenuEvent( sender , "exit" );
                Framework.EventBus.Publish( eventDetails );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Takes the saveas menu item click events and publishes it to subscribers
        /// </summary>
        /// <param name = "sender">The object that raised the event</param>
        /// <param name = "e">The event arguments</param>
        private void MenuItemSaveAsClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var eventDetails = new MenuEvent( sender , "saveas" );
                Framework.EventBus.Publish( eventDetails );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Takes the save menu item click events and publishes it to subscribers
        /// </summary>
        /// <param name = "sender">The object that raised the event</param>
        /// <param name = "e">The event arguments</param>
        private void MenuItemSaveClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var eventDetails = new MenuEvent( sender , "save" );
                Framework.EventBus.Publish( eventDetails );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Takes the open menu item click events and publishes it to subscribers
        /// </summary>
        /// <param name = "sender">The object that raised the event</param>
        /// <param name = "e">The event arguments</param>
        private void MenuItemOpenClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var eventDetails = new MenuEvent( sender , "open" );
                Framework.EventBus.Publish( eventDetails );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Takes the new menu item click events and publishes it to subscribers
        /// </summary>
        /// <param name = "sender">The object that raised the event</param>
        /// <param name = "e">The event arguments</param>
        private void MenuItemNewClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var eventDetails = new MenuEvent( sender , "new" );
                Framework.EventBus.Publish( eventDetails );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}