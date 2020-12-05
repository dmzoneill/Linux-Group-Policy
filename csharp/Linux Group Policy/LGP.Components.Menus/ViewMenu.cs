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
    ///   View menu
    /// </summary>
    public partial class MenuHandler
    {
        /// <summary>
        ///   Creates the view menu
        /// </summary>
        private void CreateViewMenu()
        {
            try
            {
                var resource = Framework.Utils.LoadResource( Assembly.GetExecutingAssembly() , this , ".ViewMenu.xaml" );

                if( resource != null )
                {
                    var menu = resource as MenuItem;
                    if( menu != null )
                    {
                        var menuitem = ( MenuItem ) menu.FindName( "ErrorList" );
                        if( menuitem != null )
                        {
                            menuitem.Click += this.MenuItemErrorListClick;
                            menuitem.Icon = Framework.Images.GetImage( "base_exclamationmark_32" , "VS2010ImageLibrary" , 16 );
                        }

                        menuitem = ( MenuItem ) menu.FindName( "Output" );
                        if( menuitem != null )
                        {
                            menuitem.Click += this.MenuItemOutputClick;
                            menuitem.Icon = Framework.Images.GetImage( "EditCodeHS" , "VS2010ImageLibrary" , 16 );
                        }

                        menuitem = ( MenuItem ) menu.FindName( "Statusbar" );
                        if( menuitem != null )
                        {
                            menuitem.Click += this.MenuItemStatusClick;
                            menuitem.Icon = Framework.Images.GetImage( "109_AllAnnotations_Info_16x16_72" , "VS2010ImageLibrary" , 16 );
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
        ///   Takes the new menu item click events and publishes it to subscribers
        /// </summary>
        /// <param name = "sender">The object that raised the event</param>
        /// <param name = "e">The event arguments</param>
        private void MenuItemOutputClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var eventDetails = new MenuEvent( sender , "outputPane" );
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
        private void MenuItemErrorListClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var eventDetails = new MenuEvent( sender , "errorPane" );
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
        private void MenuItemStatusClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var eventDetails = new MenuEvent( sender , "statusBar" );
                Framework.EventBus.Publish( eventDetails );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}