#region

using System;
using System.Windows.Controls;
using System.Windows.Input;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Modules.ModuleEditor.Internal.CustomControls
{
    /// <summary>
    ///   Interaction logic for StackPanelModuleElement.xaml
    /// </summary>
    public partial class StackPanelModuleElement : IModuleObserver
    {
        private IModule _module;

        /// <summary>
        /// </summary>
        public StackPanelModuleElement( IModule module )
        {
            try
            {
                this.InitializeComponent();
                this._module = module;
                this._module.Attach( this );
                this.Prepare();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// </summary>
        public StackPanelModuleElement( IModule module , int h )
        {
            try
            {
                this.InitializeComponent();
                this._module = module;
                this._module.Attach( this );
                this.Prepare();
                this.LoadedDoubleAnimation.BeginTime = new TimeSpan( 0 , 0 , 0 , 0 , ( h + 1 ) * 100 );
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
                this.rectangle1.ToolTip = this._module.GetModuleLocation();
                var location = this._module.GetModuleLocation().Split( '\\' );
                this.PanelItemDetails.Content = location[ location.Length - 1 ];
                this.PanelItemImage.Source = Framework.Images.GetImage( "lgp" , "128x128" , 28 ).Source;
                this.PanelItemModuleLabel.Content = this._module.GetModuleName();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void OnDoubleClick( object sender , MouseButtonEventArgs e )
        {
            try
            {
                if( e.ClickCount == 2 )
                {
                    Framework.Panels.AddMainComponent( new ModuleEditor( this._module ) , this._module.GetModuleLocation() );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// </summary>
        public void Activate()
        {
            try
            {
                Framework.Panels.AddMainComponent( new ModuleEditor( this._module ) , this._module.GetModuleLocation() );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

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
                this.PanelItemModuleLabel.Content = this._module.GetModuleName();
                var location = this._module.GetModuleLocation().Split( '\\' );
                this.PanelItemDetails.Content = location[ location.Length - 1 ];
                this.PanelItemImage.Source = Framework.Images.GetImage( "lgp" , "128x128" , 28 ).Source;
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
                this._module.Detach( this );
                this._module = null;

                var parent = ( StackPanel ) this.Parent;
                parent.Children.Remove( this );
            }
            catch( Exception )
            {
            }
        }

        #endregion
    }
}