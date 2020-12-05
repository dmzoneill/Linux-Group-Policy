#region

using System;
using System.Windows;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Infrastructure;

#endregion

namespace LGP.Modules.ModuleEditor.Internal.Modals.Controls
{
    /// <summary>
    ///   Interaction logic for AddModule.xaml
    /// </summary>
    public partial class AddModule
    {
        private readonly ModuleList _parentControl;
        private readonly IDialog _popupWindow;

        /// <summary>
        ///   Constrcutor
        /// </summary>
        public AddModule()
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
        /// <param name = "moduleList">Creator</param>
        /// <param name = "popup">the parent window</param>
        public AddModule( ModuleList moduleList , IDialog popup )
        {
            try
            {
                this.InitializeComponent();
                this._parentControl = moduleList;
                this._popupWindow = popup;
                this.image1.Source = Framework.Images.GetImage( "NewNoteWhite_32x32" , "VS2010ImageLibrary" ).Source;
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
                if( this.ModuleNameTextBox.Text.Length > 0 )
                {
                    this._parentControl.CallBackDelegate.Invoke( this.ModuleNameTextBox.Text , this._parentControl.stackPanel1 );
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

        private void ModuleNameTextBoxTextChanged( object sender , TextChangedEventArgs e )
        {
            try
            {
                this.ModuleNameTextBox.Text = Framework.Utils.CleanString( this.ModuleNameTextBox.Text );
                this.ModuleNameTextBox.CaretIndex = this.ModuleNameTextBox.Text.Length;
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