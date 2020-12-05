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
    ///   Interaction logic for RenameOu.xaml
    /// </summary>
    public partial class RenameOu
    {
        private readonly IOu _ou;
        private readonly UserControl _parentControl;
        private readonly IDialog _popupWindow;


        /// <summary>
        ///   Constructor
        /// </summary>
        public RenameOu()
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
        public RenameOu( UserControl parent , IOu ou , IDialog window )
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
                this.OuNameTextBox.Text = this._ou.GetName();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void RenameButtonClick( object sender , RoutedEventArgs e )
        {
            try
            {
                if( this.OuNameTextBox.Text.Length > 0 )
                {
                    this._ou.SetName( this.OuNameTextBox.Text );
                    this._popupWindow.CloseDialog();
                }
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