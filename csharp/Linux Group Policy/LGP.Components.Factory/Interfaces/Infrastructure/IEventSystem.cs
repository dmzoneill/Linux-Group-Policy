#region

using System;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LGP.Components.Factory.Interfaces.Infrastructure
{
    /// <summary>
    /// </summary>
    public interface IEventSystem
    {
        /// <summary>
        /// </summary>
        /// <typeparam name = "TEvent"></typeparam>
        void Publish < TEvent >();

        /// <summary>
        /// </summary>
        /// <param name = "event"></param>
        /// <typeparam name = "TEvent"></typeparam>
        void Publish < TEvent >( TEvent @event );

        /// <summary>
        /// </summary>
        /// <param name = "action"></param>
        /// <param name = "threadOption"></param>
        /// <param name = "keepSubscriberReferenceAlive"></param>
        /// <typeparam name = "TEvent"></typeparam>
        /// <returns></returns>
        SubscriptionToken Subscribe < TEvent >( Action action , ThreadOption threadOption = ThreadOption.PublisherThread , bool keepSubscriberReferenceAlive = false );

        /// <summary>
        /// </summary>
        /// <param name = "action"></param>
        /// <param name = "threadOption"></param>
        /// <param name = "keepSubscriberReferenceAlive"></param>
        /// <param name = "filter"></param>
        /// <typeparam name = "TEvent"></typeparam>
        /// <returns></returns>
        SubscriptionToken Subscribe < TEvent >( Action< TEvent > action , ThreadOption threadOption = ThreadOption.PublisherThread , bool keepSubscriberReferenceAlive = false , Predicate< TEvent > filter = null );

        /// <summary>
        /// </summary>
        /// <param name = "token"></param>
        /// <typeparam name = "TEvent"></typeparam>
        void Unsubscribe < TEvent >( SubscriptionToken token );

        /// <summary>
        /// </summary>
        /// <param name = "subscriber"></param>
        /// <typeparam name = "TEvent"></typeparam>
        void Unsubscribe < TEvent >( Action< TEvent > subscriber );
    }
}