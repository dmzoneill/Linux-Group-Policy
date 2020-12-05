#region

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Modules.OrganizationUnitExplorer.Internal.Modals.Controls;

#endregion

namespace LGP.Modules.OrganizationUnitExplorer.Internal.CustomControls
{
    internal class TreeViewOuElement : TreeViewItem , IOuObserver
    {
        private static TreeViewOuElement RootNode;
        private static TreeView _parent;
        private static readonly List< TreeViewOuElement > Nodes;
        private readonly bool _isRootNode;
        private readonly TreeView _parentTree;
        private Image _image;
        private IOu _ou;
        private StackPanel _panel;
        private TextBlock _spacer;
        private Run _text;
        private TextBlock _textBlock;

        static TreeViewOuElement()
        {
            Nodes = new List< TreeViewOuElement >();
        }


        public TreeViewOuElement( TreeView tree )
        {
            try
            {
                this.Tag = "root";
                this._isRootNode = true;
                this._parentTree = tree;
                this.AllowDrop = true;

                this._spacer = new TextBlock( new Run( " " ) );
                this._image = Framework.Images.GetImage( "workgroup" , "distros/32" );

                this._image.Width = 14;
                this._text = new Run( Properties.Resources.Domain );

                this._textBlock = new TextBlock( this._text );

                this._panel = new StackPanel
                {
                    Orientation = Orientation.Horizontal , Margin = new Thickness( 2 )
                };

                this._panel.Children.Add( this._image );
                this._panel.Children.Add( this._spacer );
                this._panel.Children.Add( this._textBlock );

                this.Header = this._panel;

                RootNode = this;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        public TreeViewOuElement( IOu ou , TreeView tree )
        {
            try
            {
                this._isRootNode = false;
                this._parentTree = tree;
                this.AllowDrop = true;

                this.Tag = "Ou";
                this._ou = ou;
                this.Prepare();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        public TreeViewOuElement( IOu ou , TreeView tree , bool enableActions )
        {
            try
            {
                Nodes.Add( this );
                ou.Attach( this );

                _parent = tree;

                this._isRootNode = false;
                this._parentTree = tree;
                this.AllowDrop = true;

                this.Tag = "Ou";
                this._ou = ou;
                this.Prepare();

                var resource = Framework.Utils.LoadResource( Assembly.GetExecutingAssembly() , "LGP.Modules.OrganizationUnitExplorer.Internal.ContextMenuItems.OuContextMenu.xaml" );

                var menu = resource as ContextMenu;
                if( menu != null )
                {
                    var item = ( MenuItem ) menu.Items[ 0 ];
                    item.Click += this.AddOuMenuItemClick;
                    item.Icon = Framework.Images.GetImage( "workgroup" , "distros/32" , 12 );

                    item = ( MenuItem ) menu.Items[ 1 ];
                    item.Click += this.RenameOuMenuItemClick;
                    item.Icon = Framework.Images.GetImage( "RenameFolderHS" , "VS2010ImageLibrary" , 12 );

                    item = ( MenuItem ) menu.Items[ 2 ];
                    item.Click += this.MoveOuMenuItemClick;
                    item.Icon = Framework.Images.GetImage( "Move" , "VS2010ImageLibrary" , 12 );

                    item = ( MenuItem ) menu.Items[ 3 ];
                    item.Click += this.DeleteOuMenuItemClick;
                    item.Icon = Framework.Images.GetImage( "DeleteHS" , "VS2010ImageLibrary" , 12 );

                    this.ContextMenu = menu;
                    this.ContextMenuOpening += this.MenuContextMenuOpening;
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region IOuObserver Members

        /// <summary>
        ///   Update this observer with a refernece to the ou
        /// </summary>
        /// <param name = "ou">Iou</param>
        /// <param name = "source">source observer</param>
        public void Update( IOu ou , IOuObserver source )
        {
            try
            {
                if( this == source || this._isRootNode )
                {
                    return;
                }

                this._ou = ou;
                this.Prepare();
                this.IsSelected = true;
                this.IsExpanded = true;

                // moved
                this.Move();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Attach objewcts that observer this IOu
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Attach( IOuObserver obj )
        {
            if( this._isRootNode )
            {
                return;
            }
        }


        /// <summary>
        ///   Detach objewcts that observer this IOu
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Detach( IOuObserver obj )
        {
            if( this._isRootNode )
            {
                return;
            }
        }


        /// <summary>
        ///   Notify observers of this IOu
        /// </summary>
        public void Notify()
        {
            if( this._isRootNode )
            {
                return;
            }
        }


        /// <summary>
        ///   Dispose this object and all the observers
        /// </summary>
        /// <param name = "ou">IOuObserver</param>
        public void Dispose( IOuObserver ou )
        {
            if( this._isRootNode )
            {
                return;
            }
            try
            {
                Nodes.Remove( this );
                ou.Detach( this );
                var parent = ( TreeViewOuElement ) this.Parent;
                parent.Items.Remove( this );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        private void MenuContextMenuOpening( object sender , ContextMenuEventArgs e )
        {
            if( this != this._parentTree.SelectedItem )
            {
                return;
            }

            var menu = this.ContextMenu;
            Framework.ContextMenus.AttachMenuItemRegistrations( ref menu , typeof( IOu ) , this.MenuItemClick );
        }

        private void Move()
        {
            if( this._isRootNode )
            {
                return;
            }
            try
            {
                var parent = ( TreeViewOuElement ) this.Parent;
                var parentOu = parent.GetOu();

                if( parentOu == null )
                {
                    if( this._ou.GetOuId() != this._ou.GetParentOuId() )
                    {
                        foreach( var ele in Nodes )
                        {
                            if( ele.GetOu().GetOuId() == this._ou.GetParentOuId() )
                            {
                                parent.Items.Remove( this );
                                ele.Items.Add( this );
                            }
                        }
                    }

                    return;
                }

                if( this._ou.GetParentOuId() != parent.GetOu().GetOuId() )
                {
                    if( this._ou.GetParentOuId() == this._ou.GetOuId() )
                    {
                        parent.Items.Remove( this );
                        ( ( TreeViewOuElement ) this._parentTree.Items[ 0 ] ).Items.Add( this );
                        return;
                    }

                    foreach( var ele in Nodes )
                    {
                        if( ele.GetOu().GetOuId() == this._ou.GetParentOuId() )
                        {
                            parent.Items.Remove( this );
                            ele.Items.Add( this );
                        }
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void Prepare()
        {
            if( this._isRootNode )
            {
                return;
            }
            try
            {
                this._spacer = new TextBlock( new Run( " " ) );
                this._image = this._ou.GetOuImage( 16 );
                this._image.Width = 14;
                this._text = new Run( this._ou.GetName() );
                this._textBlock = new TextBlock( this._text );
                this._panel = new StackPanel
                {
                    Orientation = Orientation.Horizontal , Margin = new Thickness( 2 )
                };

                this._panel.Children.Add( this._image );
                this._panel.Children.Add( this._spacer );
                this._panel.Children.Add( this._textBlock );

                this.Header = this._panel;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        public IOu GetOu()
        {
            if( this._isRootNode )
            {
                return null;
            }
            return this._ou;
        }


        public void Redraw()
        {
            if( this._isRootNode )
            {
                return;
            }
            this.Prepare();
        }


        /// <summary>
        ///   Invoked when an unhandled <see cref = "E:System.Windows.UIElement.MouseRightButtonDown" /> routed event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name = "e">The <see cref = "T:System.Windows.Input.MouseButtonEventArgs" /> that contains the event data. The event data reports that the right mouse button was pressed.</param>
        protected override void OnMouseRightButtonDown( MouseButtonEventArgs e )
        {
            base.OnMouseRightButtonDown( e );

            if( this != this._parentTree.SelectedItem )
            {
                return;
            }

            this.IsSelected = true;
        }


        /// <summary>
        ///   Invoked when an unhandled <see cref = "E:System.Windows.UIElement.PreviewMouseRightButtonDown" /> routed event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name = "e">The <see cref = "T:System.Windows.Input.MouseButtonEventArgs" /> that contains the event data. The event data reports that the right mouse button was pressed.</param>
        protected override void OnPreviewMouseRightButtonDown( MouseButtonEventArgs e )
        {
            base.OnPreviewMouseRightButtonDown( e );

            if( this._text == e.OriginalSource || this._image == e.OriginalSource )
            {
                this.IsSelected = true;
            }
        }


        /// <summary>
        ///   Raises the <see cref = "E:System.Windows.Controls.Control.MouseDoubleClick" /> routed event.
        /// </summary>
        /// <param name = "e">The event data.</param>
        protected override void OnMouseDoubleClick( MouseButtonEventArgs e )
        {
            base.OnMouseDoubleClick( e );

            try
            {
                if( this._isRootNode )
                {
                    return;
                }

                if( this != this._parentTree.SelectedItem )
                {
                    return;
                }

                try
                {
                    var ouViewer = new OuViewer( this._ou );
                    Framework.Panels.AddMainComponent( ouViewer , ouViewer.Name );
                }
                catch( Exception error )
                {
                    Framework.EventBus.Publish( error );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void AddOuMenuItemClick( object sender , RoutedEventArgs e )
        {
            if( this != this._parentTree.SelectedItem )
            {
                return;
            }
            try
            {
                var popup = Framework.Dialog;
                var cou = new CreateOu( OuTreeView.GetInstance() , this._ou , popup );
                popup.SetChild( cou );
                popup.ShowBlocking();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void DeleteOuMenuItemClick( object sender , RoutedEventArgs e )
        {
            if( this != this._parentTree.SelectedItem )
            {
                return;
            }
            try
            {
                var popup = Framework.Dialog;
                var cou = new DeleteOu( OuTreeView.GetInstance() , this._ou , popup , false );
                popup.SetChild( cou );
                popup.ShowBlocking();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void RenameOuMenuItemClick( object sender , RoutedEventArgs e )
        {
            if( this != this._parentTree.SelectedItem )
            {
                return;
            }
            try
            {
                var popup = Framework.Dialog;
                var cou = new RenameOu( OuTreeView.GetInstance() , this._ou , popup );
                popup.SetChild( cou );
                popup.ShowBlocking();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void MoveOuMenuItemClick( object sender , RoutedEventArgs e )
        {
            if( this != this._parentTree.SelectedItem )
            {
                return;
            }
            try
            {
                var popup = Framework.Dialog;
                var cou = new MoveOu( OuTreeView.GetInstance() , this._ou , popup );
                popup.SetChild( cou );
                popup.ShowBlocking();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void MenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var menuitem = sender as MenuItem;
                Framework.ContextMenus.CallBack( menuitem , this.GetOu() );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        public static void CreateEntry( IOu ou )
        {
            try
            {
                var newEle = new TreeViewOuElement( ou , ( ( OuTreeView ) OuTreeView.GetInstance() ).treeView1 , true );

                foreach( var ele in Nodes )
                {
                    if( ele.GetOu().GetOuId() == newEle._ou.GetParentOuId() )
                    {
                        ele.Items.Add( newEle );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.UIElement.PreviewMouseLeftButtonDown"/> routed event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs"/> that contains the event data. The event data reports that the left mouse button was pressed.</param>
        protected override void OnMouseLeftButtonDown( MouseButtonEventArgs e )
        {
            base.OnMouseLeftButtonDown( e );

            try
            {
                if( this != this._parentTree.SelectedItem )
                {
                    return;
                }

                if( e.LeftButton == MouseButtonState.Pressed )
                {
                    Framework.DragDrop.StartDrag( this , e );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private bool CheckDropItemTargetIsNotLogicalChild( IOu dropitem , TreeViewOuElement targetitem )
        {
            var parentElement = targetitem.Parent as TreeViewOuElement;

            if( parentElement == null )
            {
                return false;
            }

            if( parentElement.GetOu() == null )
            {
                return false;
            }

            return dropitem.GetOuId() == parentElement.GetOu().GetOuId() || this.CheckDropItemTargetIsNotLogicalChild( dropitem , parentElement );
        }


        /// <summary>
        ///   Invoked when an unhandled <see cref = "E:System.Windows.DragDrop.DragEnter" /> attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name = "e">The <see cref = "T:System.Windows.DragEventArgs" /> that contains the event data.</param>
        protected override void OnDrop( DragEventArgs e )
        {
            base.OnDrop( e );
            try
            {
                if( e.Source == this._text || e.Source == this._image )
                {
                    var ele1 = ( TreeViewOuElement ) e.Data.GetData( typeof( TreeViewOuElement ) );
                    if( ele1 != null )
                    {
                        if( this._isRootNode )
                        {
                            ele1.GetOu().SetParentOuId( ele1.GetOu().GetOuId() );
                            return;
                        }

                        if( this._ou.GetOuId() == ele1.GetOu().GetOuId() || this.CheckDropItemTargetIsNotLogicalChild( ele1.GetOu() , this ) )
                        {
                            return;
                        }

                        ele1.GetOu().SetParentOuId( this._ou.GetOuId() );
                        this._ou.Notify();
                    }

                    var ele2 = ( WrapPanelOuElement ) e.Data.GetData( typeof( WrapPanelOuElement ) );
                    if( ele2 != null )
                    {
                        if( this._isRootNode )
                        {
                            ele2.GetOu().SetParentOuId( ele2.GetOu().GetOuId() );
                            return;
                        }

                        if( this._ou.GetOuId() == ele2.GetOu().GetOuId() || this.CheckDropItemTargetIsNotLogicalChild( ele2.GetOu() , this ) )
                        {
                            return;
                        }

                        ele2.GetOu().SetParentOuId( this._ou.GetOuId() );
                        this._ou.Notify();
                        ele2.Update();
                    }

                    var ele3 = ( WrapPanelClientElement ) e.Data.GetData( typeof( WrapPanelClientElement ) );
                    if( ele3 != null )
                    {
                        ele3.GetClient().Setouid( this._ou.GetOuId() );
                        this._ou.Notify();
                        ele3.Update();
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Invoked when an unhandled <see cref = "E:System.Windows.DragDrop.GiveFeedback" /> attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name = "e">The <see cref = "T:System.Windows.GiveFeedbackEventArgs" /> that contains the event data.</param>
        protected override void OnGiveFeedback( GiveFeedbackEventArgs e )
        {
            base.OnGiveFeedback( e );
            e.UseDefaultCursors = false;
            e.Handled = true;
        }


        /// <summary>
        ///   Invoked when an unhandled <see cref = "E:System.Windows.DragDrop.DragEnter" /> attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name = "e">The <see cref = "T:System.Windows.DragEventArgs" /> that contains the event data.</param>
        protected override void OnDragEnter( DragEventArgs e )
        {
            base.OnDragEnter( e );

            if( e.Source == this._text || e.Source == this._image || e.Source != this )
            {
                return;
            }
            this.IsExpanded = true;
        }


        public static void Dispose()
        {
            var i = Nodes.Count - 1;

            while( i > -1 )
            {
                Nodes[ i ].Dispose( Nodes[ i ].GetOu() );
                i = Nodes.Count - 1;
            }
        }


        public void RefreshNode()
        {
            if( !this._isRootNode )
            {
                return;
            }

            this._panel.Children.Remove( this._textBlock );
            this._text = new Run( Properties.Resources.Domain );
            this._textBlock = new TextBlock( this._text );
            this._panel.Children.Add( this._textBlock );
        }


        public static void Refresh()
        {
            RootNode.RefreshNode();
        }
    }
}