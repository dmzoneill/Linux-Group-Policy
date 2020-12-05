namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    ///   Observer interface
    /// </summary>
    public interface IOuObserver
    {
        /// <summary>
        ///   Update this observer with a refernece to the ou
        /// </summary>
        /// <param name = "ou">Iou</param>
        /// <param name = "source">source observer</param>
        void Update( IOu ou , IOuObserver source );


        /// <summary>
        ///   Attach objewcts that observer this IOu
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        void Attach( IOuObserver obj );


        /// <summary>
        ///   Detach objewcts that observer this IOu
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        void Detach( IOuObserver obj );


        /// <summary>
        ///   Notify observers of this IOu
        /// </summary>
        void Notify();


        /// <summary>
        ///   Dispose this object and all the observers
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        void Dispose( IOuObserver obj );
    }
}