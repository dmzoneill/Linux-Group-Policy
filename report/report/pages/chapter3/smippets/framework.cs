namespace LGP.Components.Factory
{
	// Simplified factory for the loading of Modules and application components
	public static class Framework
	{
		public delegate void ShuttingDown( object sender , CancelEventArgs e );
		public static ShuttingDown ShutDown;
		private static readonly LibraryHandler ClassLibraryHandler;
		private static Window _applicationWindow;
		private static IMenu _applicationMenu;
		private static IPanel _applicationPanel;

		static Framework()
		{
			// loads up all the libraries
			ClassLibraryHandler = Internal.LibraryHandler.GetInstance();
		}

		public static IPanel Panels{...}
		public static IPanel Panel{...}
		public static INotification Notification{...}
		public static IDialog Dialog{...}
		public static IMenu Menu{...}
		public static IContextMenus ContextMenus{...}
		public static IRegistryHandler Registry{...}
		public static IImageHandler Images{...}
		public static IDatabase Database{...}
		public static IEventSystem EventBus{...}
		public static INetwork Network{...}
		public static Window ApplicationWindow{...}
		public static List< IModule > Modules{...}
		public static string[ ] Libraries{...}
		public static List< Type > DatabaseTypes{...}
		public static List< Type > PreferencesPanes{...}
		public static IUtilities Utils{...}
		public static bool HasError{...}
		public static string Error{...}
		public static IClassLibraryHandler LibraryHandler{...}
		public static IDragDrop DragDrop{...}
		private static void Shutdown( object sender , CancelEventArgs e ){...}
	}
}