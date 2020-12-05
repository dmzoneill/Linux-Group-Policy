#region

using System;

#endregion

namespace LGP.Components.Factory.Publishers.Events
{
    /// <summary>
    ///   Derived Event to which subscribers can listen for
    /// </summary>
    public class PreferencesEvent : Event
    {
        /// <summary>
        ///   Constructor for the derived type
        /// </summary>
        /// <param name = "sender">The object that raised the event</param>
        /// <param name = "type">The command that is intended</param>
        public PreferencesEvent( Object sender , Type type ) : base( sender )
        {
            this.Type = type;
        }


        /// <summary>
        ///   Accessor Mutator for the command property
        /// </summary>
        public Type Type
        {
            get;
            set;
        }
    }
}