#region

using System;
using System.Windows;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Components.Factory.Interfaces.Infrastructure;

#endregion

namespace LGP.Modules.ModuleEditor.Internal.Modals.Controls
{
    /// <summary>
    ///   Interaction logic for DeleteModule.xaml
    /// </summary>
    public partial class DeleteModule
    {
        private readonly IModule _module;
        private readonly UserControl _parentControl;
        private readonly IDialog _popupWindow;

        /// <summary>
        ///   Constrcutor
        /// </summary>
        public DeleteModule()
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
        /// <param name = "module">IModule</param>
        public DeleteModule( UserControl moduleList , IDialog popup , IModule module )
        {
            try
            {
                this.InitializeComponent();
                this._parentControl = moduleList;
                this._popupWindow = popup;
                this._module = module;
                this.label3.Content = module.GetModuleName();
                this.image1.Source = Framework.Images.GetImage( "delete32" , "VS2010ImageLibrary" ).Source;
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

        private void DeleteButtonClick( object sender , RoutedEventArgs e )
        {
            try
            {
                ModuleHandler.DeleteModule( this._module );
                this._popupWindow.CloseDialog();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}