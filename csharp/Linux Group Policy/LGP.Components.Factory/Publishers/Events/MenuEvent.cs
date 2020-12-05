#region

using System;

#endregion

namespace LGP.Components.Factory.Publishers.Events
{
    /// <summary>
    ///   Derived Event to which subscribers can listen for
    /// </summary>
    public class MenuEvent : Event
    {
        /// <summary>
        ///   Constructor for the derived type
        /// </summary>
        /// <param name = "sender">The object that raised the event</param>
        /// <param name = "command">The command that is intended</param>
        public MenuEvent( Object sender , string command ) : base( sender )
        {
            this.Command = command;
        }


        /// <summary>
        ///   Accessor Mutator for the command property
        /// </summary>
        public string Command
        {
            get;
            set;
        }
    }
}