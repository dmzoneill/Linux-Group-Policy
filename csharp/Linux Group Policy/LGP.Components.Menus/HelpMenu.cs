#region

using System;
using System.Reflection;
using System.Windows.Controls;
using LGP.Components.Factory;

#endregion

namespace LGP.Components.Menus
{
    /// <summary>
    ///   Help menu
    /// </summary>
    public partial class MenuHandler
    {
        /// <summary>
        ///   Creates the help menu
        /// </summary>
        private void CreateHelpMenu()
        {
            try
            {
                var resource = Framework.Utils.LoadResource( Assembly.GetExecutingAssembly() , this , ".HelpMenu.xaml" );

                if( resource != null )
                {
                    var menu = resource as MenuItem;
                    if( menu != null )
                    {
                        var menuitem = ( MenuItem ) menu.FindName( "Cut" );
                        if( menuitem != null )
                        {
                            menuitem.Icon = Framework.Images.GetImage( "CutHS" , "VS2010ImageLibrary" , 16 );
                        }

                        menuitem = ( MenuItem ) menu.FindName( "Copy" );
                        if( menuitem != null )
                        {
                            menuitem.Icon = Framework.Images.GetImage( "CopyHS" , "VS2010ImageLibrary" , 16 );
                        }

                        menuitem = ( MenuItem ) menu.FindName( "Paste" );
                        if( menuitem != null )
                        {
                            menuitem.Icon = Framework.Images.GetImage( "PasteHS" , "VS2010ImageLibrary" , 16 );
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
    }
}