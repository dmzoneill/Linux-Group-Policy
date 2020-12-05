#region

using System;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Modules.OrganizationUnitExplorer.Internal.NodeViews
{
    /// <summary>
    ///   Interaction logic for ClientPane.xaml
    /// </summary>
    public partial class ClientPane : IClientObserver
    {
        private IClient _client;

        /// <summary>
        ///   Constructor
        /// </summary>
        public ClientPane()
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
        public ClientPane( IClient client )
        {
            try
            {
                this.InitializeComponent();
                client.Attach( this );
                this._client = client;
                this.LoadClientInformation();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void LoadClientInformation()
        {
            try
            {
                var lastseenStr = "";
                long lastseen;

                if( long.TryParse( this._client.GetLastseen() , out lastseen ) )
                {
                    lastseenStr = Framework.Utils.FromUnixTime( lastseen ).ToString();
                }

                var bindedStr = "";
                long binded;

                if( long.TryParse( this._client.GetBinded() , out binded ) )
                {
                    bindedStr = Framework.Utils.FromUnixTime( binded ).ToString();
                }

                this.ClientGuidLabel.Text = this._client.GetGuid().Replace( "_" , "__" );
                this.ClientNameLabel.Content = this._client.GetName().Replace( "_" , "__" );
                this.ClientOSLabel.Content = this._client.GetOs().Replace( "_" , "__" );
                this.ClientArchLabel.Content = this._client.GetArch().Replace( "_" , "__" );
                this.ClientKernelLabel.Content = this._client.GetKernel().Replace( "_" , "__" );
                this.ClientPsuedonameLabel.Content = this._client.GetPseudoName().Replace( "_" , "__" );
                this.ClientDistributionLabel.Content = this._client.GetDistribution().Replace( "_" , "__" );
                this.ClientDistributionVersionLabel.Content = this._client.GetVersion();
                this.ClientLastSeenLabel.Content = lastseenStr;
                this.ClientBindedLabel.Content = bindedStr;
                this.ClientVersionLabel.Content = this._client.GetClientVersion();
                this.ClientIpAddressLabel.Content = this._client.GetIpaddress();
                this.ClientPortLabel.Content = this._client.GetPort();

                this.image1.Source = this._client.GetDistroImage( 64 ).Source;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region Implementation of IClientObserver

        /// <summary>
        ///   Update this observer with a refernece to the client
        /// </summary>
        /// <param name = "client">IClient</param>
        /// <param name = "source">source observer</param>
        public void Update( IClient client , IClientObserver source )
        {
            if( this == source )
            {
                return;
            }
            try
            {
                this._client = client;
                this.LoadClientInformation();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Attach objewcts that observer this client
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Attach( IClientObserver obj )
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
        ///   Detach objewcts that observer this client
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Detach( IClientObserver obj )
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
        ///   Notify observers of this client
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
        public void Dispose( IClientObserver obj )
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


        /// <summary>
        ///   Get the client associated with this wrapper
        /// </summary>
        /// <returns>IOu</returns>
        public IClient GetClient()
        {
            return this._client;
        }

        #endregion
    }
}