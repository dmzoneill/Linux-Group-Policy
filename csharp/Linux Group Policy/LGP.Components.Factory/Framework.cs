#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Infralution.Localization.Wpf;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Components.Factory.Interfaces.Infrastructure;
using LGP.Components.Factory.Interfaces.Network;
using LGP.Components.Factory.Internal;
using LGP.Components.Factory.Internal.ServerControl;
using IModule = LGP.Components.Factory.Interfaces.Module.IModule;

#endregion

namespace LGP.Components.Factory
{
    /// <summary>
    ///   Simplied factory for the loading of Modules and application components
    /// </summary>
    public static class Framework
    {
        #region Delegates

        /// <summary>
        /// Delegate to encapsulate subscrive of the shutton down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ShuttingDown( object sender , CancelEventArgs e );

        #endregion

        /// <summary>
        /// Anyone who wants to receive the application closing event should subscribe to this delegate
        /// </summary>
        public static ShuttingDown ShutDown;

        private static readonly LibraryHandler ClassLibraryHandler;
        private static Window _applicationWindow;
        private static IMenu _applicationMenu;
        private static IPanel _applicationPanel;


        static Framework()
        {
            ClassLibraryHandler = Internal.LibraryHandler.GetInstance();
        }


        /// <summary>
        ///   Gets the panels controller
        /// </summary>
        /// <returns>Avalondock panel</returns>
        public static IPanel Panels
        {
            get
            {
                return _applicationPanel ?? ( _applicationPanel = ( IPanel ) ClassLibraryHandler.CreateObject( ClassLibraryHandler.GetPanelType() ) );
            }
        }


        /// <summary>
        ///   Creates a temporary panel instance
        /// </summary>
        /// <returns>Avalondock panel</returns>
        public static IPanel Panel
        {
            get
            {
                return ( IPanel ) ClassLibraryHandler.CreateObject( ClassLibraryHandler.GetPanelType() );
            }
        }



        /// <summary>
        ///   Creates a temporary popup instance
        /// </summary>
        /// <returns>INotifcation</returns>
        public static INotification Notification
        {
            get
            {
                return ( INotification ) ClassLibraryHandler.CreateObject( ClassLibraryHandler.GetNotifcationType() );
            }
        }


        /// <summary>
        ///   Creates a temporary Dialog instance
        /// </summary>
        /// <returns>IDialog</returns>
        public static IDialog Dialog
        {
            get
            {
                return ( IDialog ) ClassLibraryHandler.CreateObject( ClassLibraryHandler.GetDialogType() );
            }
        }


        /// <summary>
        ///   Gets the menu
        /// </summary>
        /// <returns>The main menu</returns>
        public static IMenu Menu
        {
            get
            {
                return _applicationMenu ?? ( _applicationMenu = ( IMenu ) ClassLibraryHandler.CreateObject( ClassLibraryHandler.GetMenuType() ) );
            }
        }


        /// <summary>
        ///   Creates a temporary Dialog instance
        /// </summary>
        /// <returns>IDialog</returns>
        public static IContextMenus ContextMenus
        {
            get
            {
                return ClassLibraryHandler.GetConextMenusHandler();
            }
        }

        /// <summary>
        ///   Get the registry handler object
        /// </summary>
        /// <returns></returns>
        public static IRegistryHandler Registry
        {
            get
            {
                return RegistryHandler.GetInstance();
            }
        }


        /// <summary>
        ///   Get the image handler object
        /// </summary>
        /// <returns></returns>
        public static IImageHandler Images
        {
            get
            {
                return ImageHandler.GetInstance();
            }
        }


        /// <summary>
        ///   Gets a previously created database strategy context object
        /// </summary>
        /// <returns>IDatabaseModule type</returns>
        /// <exception cref = "Exception">Exception context strategy null</exception>
        public static IDatabase Database
        {
            get
            {
                return ClassLibraryHandler.GetDatabaseContextHandler();
            }
        }


        /// <summary>
        ///   Gets the Event aggregator
        /// </summary>
        /// <returns>IEventAggregator</returns>
        public static IEventSystem EventBus
        {
            get
            {
                return EventSystem.Current;
            }
        }


        /// <summary>
        ///   Server handler for connections and discovery
        /// </summary>
        /// <returns>ConnectionHandler</returns>
        public static INetwork Network
        {
            get
            {
                return NetworkController.GetInstance();
            }
        }


        /// <summary>
        ///   Get the application Window
        /// </summary>
        /// <returns>Window</returns>
        public static Window ApplicationWindow
        {
            get
            {
                return _applicationWindow;
            }
            set
            {
                var language = Registry.ReadKey( "Language" );

                if( language != null )
                {
                    CultureManager.UICulture = new System.Globalization.CultureInfo( language );
                }

                _applicationWindow = value;
                _applicationWindow.Closing += Shutdown;
            }
        }


        /// <summary>
        ///   Gets the listing of controls
        /// </summary>
        /// <returns>List of controls</returns>
        public static List< IModule > Modules
        {
            get
            {
                return ClassLibraryHandler.GetModules();
            }
        }


        /// <summary>
        ///   Gets the dll string listing
        /// </summary>
        /// <returns>String array</returns>
        public static string[ ] Libraries
        {
            get
            {
                return ClassLibraryHandler.GetDllPaths();
            }
        }


        /// <summary>
        ///   Gets the registered DB Types
        /// </summary>
        public static List< Type > DatabaseTypes
        {
            get
            {
                return ClassLibraryHandler.GetDatabaseContextHandler().GetDatabaseTypes();
            }
        }


        /// <summary>
        ///   Gets the preferences panes associated with the modules
        /// </summary>
        /// <returns></returns>
        public static List< Type > PreferencesPanes
        {
            get
            {
                return ClassLibraryHandler.GetPreferencesPanes();
            }
        }


        /// <summary>
        ///   Gets the utlies instance
        /// </summary>
        /// <returns>IUtilities</returns>
        public static IUtilities Utils
        {
            get
            {
                return Utilities.GetInstance();
            }
        }


        /// <summary>
        ///   Check to see if there was an error
        /// </summary>
        /// <returns>bool error</returns>
        public static bool HasError
        {
            get
            {
                return ClassLibraryHandler.HasError();
            }
        }


        /// <summary>
        ///   Get the last error occurred
        /// </summary>
        /// <returns>String error</returns>
        public static string Error
        {
            get
            {
                return ClassLibraryHandler.GetError();
            }
        }

        /// <summary>
        /// Returns a referenc to the class library loader
        /// </summary>
        public static IClassLibraryHandler LibraryHandler
        {
            get
            {
                return ClassLibraryHandler;
            }
        }


        /// <summary>
        /// Returns a reference to the drag and drag adorner handler
        /// </summary>
        public static IDragDrop DragDrop
        {
            get
            {
                return DragDropAdorner.GetInstance();
            }
        }


        private static void Shutdown( object sender , CancelEventArgs e )
        {
            try
            {
                ShutDown( sender , e );
            }
            catch( Exception )
            {
            }
        }
    }
}