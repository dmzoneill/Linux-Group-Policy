namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    /// </summary>
    public interface IGrammerObserver
    {
        /// <summary>
        ///   Update this observer with a refernece to the grammer
        /// </summary>
        /// <param name = "grammer">IGrammer</param>
        /// <param name = "source">IGrammerObserver</param>
        void Update( IGrammer grammer , IGrammerObserver source );


        /// <summary>
        ///   Attach objewcts that observer this grammer
        /// </summary>
        /// <param name = "obj">IGrammerObserver</param>
        void Attach( IGrammerObserver obj );


        /// <summary>
        ///   Detach objewcts that observer this grammer
        /// </summary>
        /// <param name = "obj">IGrammerObserver</param>
        void Detach( IGrammerObserver obj );


        /// <summary>
        ///   Notify observers of this grammer
        /// </summary>
        void Notify();


        /// <summary>
        ///   Dispose this object and all the observers
        /// </summary>
        /// <param name = "obj">IGrammerObserver</param>
        void Dispose( IGrammerObserver obj );
    }
}