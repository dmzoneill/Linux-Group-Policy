public interface IContextMenus
{
	void AttachMenuItemRegistrations( ContextMenu menuparent , Type type , Action< Object , RoutedEventArgs > action );
	void CallBack( MenuItem sender , object theobject );
	void RegisterContextMenuItem( Type type , MenuItem item , Action< Object > action );
}