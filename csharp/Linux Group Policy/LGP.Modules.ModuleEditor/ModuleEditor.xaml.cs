#region

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Components.Factory.Interfaces.Module;
using LGP.Modules.ModuleEditor.Internal;
using LGP.Modules.ModuleEditor.Internal.Modals.Controls;
using LGP.Modules.ModuleEditor.Internal.NodeViews;
using IModule = LGP.Components.Factory.Interfaces.Database.IModule;

#endregion

namespace LGP.Modules.ModuleEditor
{
    /// <summary>
    ///   Interaction logic for ModuleEditor.xaml
    /// </summary>
    public partial class ModuleEditor : IUserControl , IModuleObserver
    {
        private readonly ModuleDocument _documentHandler;
        private readonly DispatcherTimer _editedCheckTimer;
        private IModule _module;

        /// <summary>
        ///   Constructor for the module
        /// </summary>
        public ModuleEditor( IModule module )
        {
            try
            {
                this.InitializeComponent();
                this._module = module;
                this._module.Attach( this );
                this._documentHandler = new ModuleDocument( this._module , this , this.textEditor1 , this.textEditor2 );
                this.Prepare();

                this._editedCheckTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds( 3 )
                };
                this._editedCheckTimer.Tick += this.IsEditedTick;
                this._editedCheckTimer.Start();

                this.ModuleViewBorderContainer.Child = new ModulePane( this._module , this._documentHandler );
                this.Tag = this._module.GetModuleName() + ".pm";

                Framework.ShutDown += this.Terminate;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void Terminate( object sender , CancelEventArgs args )
        {
            try
            {
                this.Dispose();
            }
            catch( Exception )
            {
            }
        }

        private void IsEditedTick( object sender , EventArgs e )
        {
            try
            {
                if( this._module == null )
                {
                    return;
                }

                if( this._documentHandler.GetHash() != this._module.GetHash() )
                {
                    this.SaveButton.IsEnabled = true;
                    this.SaveButton.Opacity = 1.0;
                }
                else
                {
                    this.SaveButton.IsEnabled = false;
                    this.SaveButton.Opacity = 0.3;
                }
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
                this.moduleNameLabel.Content = this._module.GetModuleName();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void MenuItemCopyClick( object sender , RoutedEventArgs e )
        {
            try
            {
                Clipboard.SetText( this.textEditor1.SelectedText , TextDataFormat.Text );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void MenuItemCutClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var text = this.textEditor1.SelectedText;
                this.textEditor1.SelectedText = "";
                Clipboard.SetText( text , TextDataFormat.Text );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void MenuItemPasteClick( object sender , RoutedEventArgs e )
        {
            try
            {
                if( Clipboard.ContainsText( TextDataFormat.Text ) )
                {
                    var text = Clipboard.GetText( TextDataFormat.Text );
                    this.textEditor1.Document.Insert( this.textEditor1.CaretOffset , text );
                    Clipboard.SetText( text , TextDataFormat.Text );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void GridViewSplitterChecked( object sender , RoutedEventArgs e )
        {
            try
            {
                var checkbox = sender as CheckBox;

                var editorGridWidth = this.editorGrid.ActualWidth;
                var editor1Column = this.editorGrid.ColumnDefinitions[ 0 ];
                var editor2Column = this.editorGrid.ColumnDefinitions[ 2 ];

                if( checkbox != null && checkbox.IsChecked.HasValue )
                {
                    var checkedb = ( bool ) checkbox.GetValue( ToggleButton.IsCheckedProperty );
                    if( checkedb )
                    {
                        editor1Column.Width = new GridLength( ( editorGridWidth / 2 ) - 1 , GridUnitType.Star );
                        editor2Column.Width = new GridLength( ( editorGridWidth / 2 ) - 1 , GridUnitType.Star );
                    }
                    else
                    {
                        editor1Column.Width = new GridLength( editorGridWidth - 2 , GridUnitType.Star );
                        editor2Column.Width = new GridLength( 0 , GridUnitType.Star );
                    }
                }
                else
                {
                    editor1Column.Width = new GridLength( editorGridWidth - 2 , GridUnitType.Star );
                    editor2Column.Width = new GridLength( 0 , GridUnitType.Star );
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
                this.SplitViewImage.Source = Framework.Images.GetImage( "LayoutSelectColumn" , "VS2010ImageLibrary" , 16 ).Source;
                this.ImageCopy.Source = Framework.Images.GetImage( "Copy" , "VS2010ImageLibrary" ).Source;
                this.ImageCut.Source = Framework.Images.GetImage( "Cut" , "VS2010ImageLibrary" ).Source;
                this.ImageDelete.Source = Framework.Images.GetImage( "Delete" , "VS2010ImageLibrary" ).Source;
                this.ImagePaste.Source = Framework.Images.GetImage( "Paste" , "VS2010ImageLibrary" ).Source;
                this.ImageRedo.Source = Framework.Images.GetImage( "Redo" , "VS2010ImageLibrary" ).Source;
                this.ImageSave.Source = Framework.Images.GetImage( "Save" , "VS2010ImageLibrary" ).Source;
                this.ImageUndo.Source = Framework.Images.GetImage( "Undo" , "VS2010ImageLibrary" ).Source;
                this.ImageWrap.Source = Framework.Images.GetImage( "WordWrap" , "VS2010ImageLibrary" ).Source;
                this.ModuleImage.Source = Framework.Images.GetImage( "lgp" , "128x128" , 28 ).Source;
                this.DeleteMenuItem.Icon = Framework.Images.GetImage( "DeleteHS" , "VS2010ImageLibrary" , 12 );
                this.RenameMenuItem.Icon = Framework.Images.GetImage( "RenameFolderHS" , "VS2010ImageLibrary" , 12 );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void RenameMenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                this.ModuleLabelEditBox.Text = this.moduleNameLabel.Content.ToString();
                this.moduleNameLabel.Visibility = Visibility.Hidden;
                this.ModuleLabelEditBox.Visibility = Visibility.Visible;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void DeleteMenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var popup = Framework.Dialog;
                var cou = new DeleteModule( this , popup , this._module );
                popup.SetChild( cou );
                popup.ShowBlocking();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void ModuleLabelEditBoxTextChanged( object sender , TextChangedEventArgs e )
        {
            try
            {
                this.ModuleLabelEditBox.Text = Framework.Utils.CleanString( this.ModuleLabelEditBox.Text );
                this.ModuleLabelEditBox.CaretIndex = this.ModuleLabelEditBox.Text.Length;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void ModuleEditBoxKeyUp( object sender , KeyEventArgs e )
        {
            try
            {
                if( e.Key == Key.Return && this.ModuleLabelEditBox.Text.Length > 0 )
                {
                    this.ModuleLabelEditBox.Visibility = Visibility.Hidden;
                    this.moduleNameLabel.Visibility = Visibility.Visible;
                    this._module.SetModuleName( this.ModuleLabelEditBox.Text );
                    this.moduleNameLabel.Content = this._module.GetModuleName();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void SaveMenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                this._module.FileContents = this._documentHandler.Document.Text;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void ToolBarLoaded( object sender , RoutedEventArgs e )
        {
            try
            {
                var toolBar = sender as ToolBar;
                if( toolBar != null )
                {
                    var overflowGrid = toolBar.Template.FindName( "OverflowGrid" , toolBar ) as FrameworkElement;
                    if( overflowGrid != null )
                    {
                        overflowGrid.Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region Implementation of IUserControl

        /// <summary>
        ///   Asks the UserControl to do some clean up!
        /// </summary>
        public void Dispose()
        {
            try
            {
                this._documentHandler.Dispose( this );
                this._module.Detach( this );
                this._editedCheckTimer.Stop();
                this._module = null;

                var pane = ( ModulePane ) this.ModuleViewBorderContainer.Child;
                pane.Dispose( this );
            }
            catch( Exception )
            {
            }
        }

        /// <summary>
        /// Asks the Usercontrol to refresh itself
        /// </summary>
        public void Refresh()
        {
            this.Tag = this._module.GetModuleName() + ".pm";
        }

        /// <summary>
        /// When the control is no longer in focus, asks it to pause ( heavy duty non critical, ie threads updating graphs )
        /// </summary>
        public void Pause()
        {
            try
            {
            }
            catch( Exception )
            {
            }
        }

        /// <summary>
        /// When the tab comes back into focus, resume ( heavy duty non critical, ie threads updating graphs )
        /// </summary>
        public void Resume()
        {
            try
            {
            }
            catch( Exception )
            {
            }
        }

        #endregion

        #region Implementation of IModuleObserver

        /// <summary>
        ///   Update this observer with a refernece to the module
        /// </summary>
        /// <param name = "module">IModule</param>
        /// <param name = "source">IModuleObserver</param>
        public void Update( IModule module , IModuleObserver source )
        {
            try
            {
                if( source == this )
                {
                    return;
                }

                this._module = module;
                this.moduleNameLabel.Content = this._module.GetModuleName();
                this.Tag = this._module.GetModuleName() + " Module";
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Attach objewcts that observer this module
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        public void Attach( IModuleObserver obj )
        {
            try
            {
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Detach objewcts that observer this module
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        public void Detach( IModuleObserver obj )
        {
            try
            {
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Notify observers of this module
        /// </summary>
        public void Notify()
        {
            try
            {
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Dispose this object and all the observers
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        public void Dispose( IModuleObserver obj )
        {
            try
            {
                this._documentHandler.Dispose( this );
                this._module.Detach( this );
                this._editedCheckTimer.Stop();
                this._module = null;
                Framework.ShutDown -= this.Terminate;
                Framework.Panels.RemoveMainComponent( this );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion
    }
}