#region

using System;
using System.Windows;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Components.Factory.Interfaces.Infrastructure;
using LGP.Modules.OrganizationUnitExplorer.Internal.CustomControls;

#endregion

namespace LGP.Modules.OrganizationUnitExplorer.Internal.Modals.Controls
{
    /// <summary>
    ///   Interaction logic for CreateOu.xaml
    /// </summary>
    public partial class CreateOu
    {
        private readonly IOu _ou;
        private readonly UserControl _parentControl;
        private readonly IDialog _popupWindow;

        /// <summary>
        ///   Constructor
        /// </summary>
        public CreateOu()
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
        /// <param name = "parent">the parent control</param>
        /// <param name = "ou">IOu</param>
        /// <param name = "window">parent window</param>
        public CreateOu( UserControl parent , IOu ou , IDialog window )
        {
            try
            {
                this.InitializeComponent();
                this._ou = ou;
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
                this.ParentOuLabel.Content = this._ou.GetName();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
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


        private void CreateButtonClick( object sender , RoutedEventArgs e )
        {
            try
            {
                if( this.OuNameTextBox.Text.Length > 0 )
                {
                    var ou = OuHelper.OuGateway.CreateOu( this.OuNameTextBox.Text , this._ou.GetOuId() );
                    TreeViewOuElement.CreateEntry( ou );
                    this._ou.Notify();
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

        private void OuNameTextBoxTextChanged( object sender , TextChangedEventArgs e )
        {
            try
            {
                this.OuNameTextBox.Text = Framework.Utils.CleanString( this.OuNameTextBox.Text );
                this.OuNameTextBox.CaretIndex = this.OuNameTextBox.Text.Length;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}