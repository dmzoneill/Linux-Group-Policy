#region

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Components.Factory.Interfaces.Infrastructure;
using LGP.Modules.OrganizationUnitExplorer.Internal.CustomControls;

#endregion

namespace LGP.Modules.OrganizationUnitExplorer.Internal.Modals.Controls
{
    /// <summary>
    ///   Interaction logic for MoveOu.xaml
    /// </summary>
    public partial class MoveOu
    {
        private readonly IOu _ou;
        private readonly UserControl _parentControl;
        private readonly IDialog _popupWindow;

        /// <summary>
        ///   Constructor
        /// </summary>
        public MoveOu()
        {
            try
            {
                this.InitializeComponent();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Constructor
        /// </summary>
        /// <param name = "parent">parent control</param>
        /// <param name = "ou">ou</param>
        /// <param name = "window">popup window</param>
        public MoveOu( UserControl parent , IOu ou , IDialog window )
        {
            try
            {
                this.InitializeComponent();
                this._ou = ou;
                this._popupWindow = window;
                this._parentControl = parent;
                this.image1.Source = this._ou.GetOuImage( 32 ).Source;
                OuHelper.BuildOuTree( this.treeView1 , false , ou );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Raises the <see cref = "E:System.Windows.Controls.Control.MouseDoubleClick" /> routed event.
        /// </summary>
        /// <param name = "sender">the object sending the event</param>
        /// <param name = "e">The event data.</param>
        public void NodeMouseDoubleClick( object sender , MouseButtonEventArgs e )
        {
            this.Move();
        }

        private void MoveButtonClick( object sender , RoutedEventArgs e )
        {
            this.Move();
        }


        private void CancelButtonClick( object sender , RoutedEventArgs e )
        {
            try
            {
                this._popupWindow.CloseDialog();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void Move()
        {
            try
            {
                if( this.treeView1.SelectedItem != null )
                {
                    var item = ( TreeViewOuElement ) this.treeView1.SelectedItem;
                    this._ou.SetParentOuId( item.Tag.ToString().CompareTo( "root" ) == 0 ? this._ou.GetOuId() : item.GetOu().GetOuId() );

                    this._popupWindow.CloseDialog();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void UserControlLoaded( object sender , RoutedEventArgs e )
        {
            try
            {
                this._popupWindow.ResizePopup( ( int ) this.Width , ( int ) this.Height );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}