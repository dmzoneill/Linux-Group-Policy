#region

using System;

#endregion

namespace LGP.Components.Factory.Publishers.Events
{
    /// <summary>
    ///   Abstract type for the publish subscribe message
    /// </summary>
    public class Event
    {
        /// <summary>
        ///   Base Constructor
        /// </summary>
        /// <param name = "sender">The object that raised the event</param>
        public Event( Object sender )
        {
            this.Sender = sender;
        }


        /// <summary>
        ///   Accessor Mutator for the sender property
        /// </summary>
        public Object Sender
        {
            get;
            set;
        }
    }
}