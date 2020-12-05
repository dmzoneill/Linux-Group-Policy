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
    ///   Edit menu
    /// </summary>
    public partial class MenuHandler
    {
        /// <summary>
        ///   Creates the edit menu
        /// </summary>
        private void CreateEditMenu()
        {
            try
            {
                var resource = Framework.Utils.LoadResource( Assembly.GetExecutingAssembly() , this , ".EditMenu.xaml" );

                if( resource != null )
                {
                    var menu = resource as MenuItem;
                    if( menu != null )
                    {
                        var menuitem = ( MenuItem ) menu.FindName( "Cut" );
                        if( menuitem != null )
                        {
                            menuitem.Click += this.MenuItemCutClick;
                            menuitem.Icon = Framework.Images.GetImage( "CutHS" , "VS2010ImageLibrary" , 16 );
                        }

                        menuitem = ( MenuItem ) menu.FindName( "Copy" );
                        if( menuitem != null )
                        {
                            menuitem.Click += this.MenuItemCopyClick;
                            menuitem.Icon = Framework.Images.GetImage( "CopyHS" , "VS2010ImageLibrary" , 16 );
                        }

                        menuitem = ( MenuItem ) menu.FindName( "Paste" );
                        if( menuitem != null )
                        {
                            menuitem.Click += this.MenuItemPasteClick;
                            menuitem.Icon = Framework.Images.GetImage( "PasteHS" , "VS2010ImageLibrary" , 16 );
                        }

                        menuitem = ( MenuItem ) menu.FindName( "Preferences" );
                        if( menuitem != null )
                        {
                            menuitem.Click += this.MenuItemPreferencesClick;
                            menuitem.Icon = Framework.Images.GetImage( "Gear" , "VS2010ImageLibrary" , 16 );
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
        ///   Takes the paste menu item click events and publishes it to subscribers
        /// </summary>
        /// <param name = "sender">The object that raised the event</param>
        /// <param name = "e">The event arguments</param>
        private void MenuItemPasteClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var eventDetails = new MenuEvent( sender , "paste" );
                Framework.EventBus.Publish( eventDetails );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Takes the copy menu item click events and publishes it to subscribers
        /// </summary>
        /// <param name = "sender">The object that raised the event</param>
        /// <param name = "e">The event arguments</param>
        private void MenuItemCopyClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var eventDetails = new MenuEvent( sender , "copy" );
                Framework.EventBus.Publish( eventDetails );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Takes the cut menu item click events and publishes it to subscribers
        /// </summary>
        /// <param name = "sender">The object that raised the event</param>
        /// <param name = "e">The event arguments</param>
        private void MenuItemCutClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var eventDetails = new MenuEvent( sender , "cut" );
                Framework.EventBus.Publish( eventDetails );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Takes the cut menu item click events and publishes it to subscribers
        /// </summary>
        /// <param name = "sender">The object that raised the event</param>
        /// <param name = "e">The event arguments</param>
        private void MenuItemPreferencesClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var eventDetails = new MenuEvent( sender , "preferences" );
                Framework.EventBus.Publish( eventDetails );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}