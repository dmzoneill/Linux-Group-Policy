#region

using System;
using System.Windows;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Components.Factory.Interfaces.Infrastructure;

#endregion

namespace LGP.Modules.OrganizationUnitExplorer.Internal.Modals.Controls
{
    /// <summary>
    ///   Interaction logic for DeleteOu.xaml
    /// </summary>
    public partial class DeleteOu
    {
        private readonly IOu _ou;
        private readonly bool _parentClose;
        private readonly UserControl _parentControl;
        private readonly IDialog _popupWindow;

        /// <summary>
        ///   Constructor
        /// </summary>
        public DeleteOu()
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
        /// <param name = "parent">The parent control</param>
        /// <param name = "ou">IOu</param>
        /// <param name = "window">parent window</param>
        /// <param name = "close"></param>
        public DeleteOu( UserControl parent , IOu ou , IDialog window , bool close )
        {
            try
            {
                this.InitializeComponent();
                this._ou = ou;
                this._parentClose = close;
                this._popupWindow = window;
                this._parentControl = parent;
                this.Prepare();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void Prepare()
        {
            try
            {
                this.image1.Source = this._ou.GetOuImage( 32 ).Source;
                var msg = string.Format( Properties.Resources.AreYouSureDelete + " '{0}' " + Properties.Resources.Ou + "?" , this._ou.GetName() );
                this.textBlock1.Text = msg;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void CancelOuButtonClick( object sender , RoutedEventArgs e )
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


        private void DeleteOuButtonClick( object sender , RoutedEventArgs e )
        {
            try
            {
                OuHelper.OuGateway.DeleteOu( this._ou.GetOuId() );
                this._ou.Dispose( this._ou );
                if( this._parentClose )
                {
                    Framework.Panels.RemoveMainComponent( this._parentControl );
                }

                this._popupWindow.CloseDialog();
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