#region

using System;
using LGP.Components.Factory.Interfaces.Infrastructure;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LGP.Components.Factory.Internal
{
    /// <summary>
    ///   Event aggregator wrapper by Rachel Lim
    /// </summary>
    internal class EventSystem : IEventSystem
    {
        private static IEventAggregator _current;
        private static EventSystem _instance;

        /// <summary>
        /// </summary>
        public EventSystem()
        {
            _current = new EventAggregator();
        }

        /// <summary>
        /// </summary>
        public static EventSystem Current
        {
            get
            {
                return _instance ?? ( _instance = new EventSystem() );
            }
        }

        #region IEventSystem Members

        /// <summary>
        /// </summary>
        /// <typeparam name = "TEvent"></typeparam>
        public void Publish < TEvent >()
        {
            this.Publish( default( TEvent ) );
        }

        /// <summary>
        /// </summary>
        /// <param name = "event"></param>
        /// <typeparam name = "TEvent"></typeparam>
        public void Publish < TEvent >( TEvent @event )
        {
            this.GetEvent< TEvent >().Publish( @event );
        }

        /// <summary>
        /// </summary>
        /// <param name = "action"></param>
        /// <param name = "threadOption"></param>
        /// <param name = "keepSubscriberReferenceAlive"></param>
        /// <typeparam name = "TEvent"></typeparam>
        /// <returns></returns>
        public SubscriptionToken Subscribe < TEvent >( Action action , ThreadOption threadOption = ThreadOption.PublisherThread , bool keepSubscriberReferenceAlive = false )
        {
            return this.Subscribe< TEvent >( e => action() , threadOption , keepSubscriberReferenceAlive );
        }

        /// <summary>
        /// </summary>
        /// <param name = "action"></param>
        /// <param name = "threadOption"></param>
        /// <param name = "keepSubscriberReferenceAlive"></param>
        /// <param name = "filter"></param>
        /// <typeparam name = "TEvent"></typeparam>
        /// <returns></returns>
        public SubscriptionToken Subscribe < TEvent >( Action< TEvent > action , ThreadOption threadOption = ThreadOption.PublisherThread , bool keepSubscriberReferenceAlive = false , Predicate< TEvent > filter = null )
        {
            return this.GetEvent< TEvent >().Subscribe( action , threadOption , keepSubscriberReferenceAlive , filter );
        }

        /// <summary>
        /// </summary>
        /// <param name = "token"></param>
        /// <typeparam name = "TEvent"></typeparam>
        public void Unsubscribe < TEvent >( SubscriptionToken token )
        {
            this.GetEvent< TEvent >().Unsubscribe( token );
        }

        /// <summary>
        /// </summary>
        /// <param name = "subscriber"></param>
        /// <typeparam name = "TEvent"></typeparam>
        public void Unsubscribe < TEvent >( Action< TEvent > subscriber )
        {
            this.GetEvent< TEvent >().Unsubscribe( subscriber );
        }

        #endregion

        private CompositePresentationEvent< TEvent > GetEvent < TEvent >()
        {
            return _current.GetEvent< CompositePresentationEvent< TEvent > >();
        }
    }
}