#region

using System;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Modules.OrganizationUnitExplorer.Internal.NodeViews
{
    /// <summary>
    ///   Interaction logic for OuPane.xaml
    /// </summary>
    public partial class OuPane : IOuObserver
    {
        private IOu _ou;

        /// <summary>
        ///   Constructor
        /// </summary>
        public OuPane()
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
        public OuPane( IOu ou )
        {
            try
            {
                this.InitializeComponent();
                ou.Attach( this );
                this._ou = ou;
                this.LoadOuInformation();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region IOuObserver Members

        /// <summary>
        ///   Update this observer with a refernece to the ou
        /// </summary>
        /// <param name = "ou">Iou</param>
        /// <param name = "source">source observer</param>
        public void Update( IOu ou , IOuObserver source )
        {
            if( this == source )
            {
                return;
            }
            try
            {
                this._ou = ou;
                this.LoadOuInformation();
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
                obj.Detach( this );
                var parent = ( Grid ) this.Parent;
                parent.Children.Remove( this );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        private void LoadOuInformation()
        {
            try
            {
                this.OuNameLabel.Content = this._ou.GetName();
                this.image1.Source = this._ou.GetOuImage( 64 ).Source;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Get the ou associated with this wrapper
        /// </summary>
        /// <returns>IOu</returns>
        public IOu GetOu()
        {
            return this._ou;
        }
    }
}