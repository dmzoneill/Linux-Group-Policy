namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    ///   Observer interface
    /// </summary>
    public interface IClientObserver
    {
        /// <summary>
        ///   Update this observer with a refernece to the client
        /// </summary>
        /// <param name = "client">IClient</param>
        /// <param name = "source">source observer</param>
        void Update( IClient client , IClientObserver source );


        /// <summary>
        ///   Attach objewcts that observer this client
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        void Attach( IClientObserver obj );


        /// <summary>
        ///   Detach objewcts that observer this client
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        void Detach( IClientObserver obj );


        /// <summary>
        ///   Notify observers of this client
        /// </summary>
        void Notify();


        /// <summary>
        ///   Dispose this object and all the observers
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        void Dispose( IClientObserver obj );
    }
}