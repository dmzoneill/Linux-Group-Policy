#region

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Components.Factory.Interfaces.Module;
using LGP.Modules.PolicyEditor.Internal;
using LGP.Modules.PolicyEditor.Internal.CustomControls;

#endregion

namespace LGP.Modules.PolicyEditor
{
    /// <summary>
    ///   Interaction logic for PolicyEditor.xaml
    /// </summary>
    public partial class PolicyEditor : IUserControl , IOuObserver
    {
        private readonly IPolicy _policy;
        private readonly PolicyHandler _policyHandler;
        private IOu _ou;

        /// <summary>
        ///   Constructor
        /// </summary>
        public PolicyEditor()
        {
            try
            {
                this.InitializeComponent();
                this.Prepare();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Constructor
        /// </summary>
        /// <param name = "ou"></param>
        public PolicyEditor( IOu ou )
        {
            try
            {
                this.InitializeComponent();

                this._ou = ou;
                this._ou.Attach( this );
                this.PolicyOverviewContainer.Child = new OuPolicyPane( this._ou );
                var pgw = Framework.Database.CreatePolicyGateway();

                this._policy = pgw.GetPolicyByOu( this._ou );
                if( this._policy == null )
                {
                    var policy = File.ReadAllText( "PolicyTemplate.txt" );
                    this._policy = pgw.CreatePolicy( this._ou , policy );
                }

                this._policyHandler = new PolicyHandler( this , this.PolicyDSLEditor1 , this._policy );
                this.VPEditor.SetPolicy( this._policy , this , this._policyHandler );
                this.Prepare();
                this.Tag = this._ou.GetName() + " " + Properties.Resources.Policy;

                this.PolicyOverviewContainer.Child = new OuPolicyPane( this._ou );
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
                this.ImageCopy.Source = Framework.Images.GetImage( "Copy" , "VS2010ImageLibrary" ).Source;
                this.ImageCut.Source = Framework.Images.GetImage( "Cut" , "VS2010ImageLibrary" ).Source;
                this.ImageDelete.Source = Framework.Images.GetImage( "Delete" , "VS2010ImageLibrary" ).Source;
                this.ImagePaste.Source = Framework.Images.GetImage( "Paste" , "VS2010ImageLibrary" ).Source;
                this.ImageRedo.Source = Framework.Images.GetImage( "Redo" , "VS2010ImageLibrary" ).Source;
                this.ImageSave.Source = Framework.Images.GetImage( "Save" , "VS2010ImageLibrary" ).Source;
                this.ImageSave1.Source = Framework.Images.GetImage( "Save" , "VS2010ImageLibrary" ).Source;
                this.ImageUndo.Source = Framework.Images.GetImage( "Undo" , "VS2010ImageLibrary" ).Source;
                this.ImageWrap.Source = Framework.Images.GetImage( "WordWrap" , "VS2010ImageLibrary" ).Source;
                this.ImageDeleteRule.Source = Framework.Images.GetImage( "Delete-PermissionHH" , "VS2010ImageLibrary" ).Source;
                this.ImageAddRule.Source = Framework.Images.GetImage( "New-Permission" , "VS2010ImageLibrary" ).Source;
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
                this._policy.SetDsl( this._policyHandler.Document.Text );
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

        private void AddRowButtonClick( object sender , RoutedEventArgs e )
        {
            this.VPEditor.AddRow();
        }

        private void DeleteRowButtonClick( object sender , RoutedEventArgs e )
        {
            this.VPEditor.DeleteSelectedRows();
        }

        private void PolicyDslEditor1KeyUp( object sender , KeyEventArgs e )
        {
            if( e.Key == Key.Return )
            {
                this.VPEditor.Update( this._policy , null );
            }
        }

        #region Implementation of IUserControl

        /// <summary>
        ///   Aks the UserControl to do some clean up!
        /// </summary>
        public void Dispose()
        {
            try
            {
                this._ou.Detach( this );
                this._policyHandler.Detach( this._policyHandler );
                this.VPEditor.Detach( this.VPEditor );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        /// Asks the Usercontrol to refresh itself
        /// </summary>
        public void Refresh()
        {
            this.Tag = this._ou.GetName() + " " + Properties.Resources.Policy;
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

        #region Implementation of IOuObserver

        /// <summary>
        ///   Update this observer with a refernece to the ou
        /// </summary>
        /// <param name = "ou">Iou</param>
        /// <param name = "source">source observer</param>
        public void Update( IOu ou , IOuObserver source )
        {
            try
            {
                this._ou = ou;
                this.Tag = this._ou.GetName() + " Policy";
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Attach objewcts that observer this IOu
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Attach( IOuObserver obj )
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
        ///   Detach objewcts that observer this IOu
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Detach( IOuObserver obj )
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
        ///   Notify observers of this IOu
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
        /// <param name = "obj">IOuObserver</param>
        public void Dispose( IOuObserver obj )
        {
            try
            {
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion
    }
}