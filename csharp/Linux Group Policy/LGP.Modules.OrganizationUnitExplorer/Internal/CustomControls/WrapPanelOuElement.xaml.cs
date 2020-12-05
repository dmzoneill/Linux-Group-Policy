#region

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Modules.OrganizationUnitExplorer.Internal.Modals.Controls;
using LGP.Modules.OrganizationUnitExplorer.Internal.NodeViews;

#endregion

namespace LGP.Modules.OrganizationUnitExplorer.Internal.CustomControls
{
    /// <summary>
    ///   Interaction logic for WrapPanelOuElement.xaml
    /// </summary>
    public partial class WrapPanelOuElement : IOuObserver
    {
        private static WrapPanelOuElement _selectedElement;
        private readonly OuViewer _parent;
        private readonly int _size;
        private IOu _ou;
        private bool _selected;

        /// <summary>
        /// </summary>
        public WrapPanelOuElement()
        {
            try
            {
                this.InitializeComponent();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        /// </summary>
        /// <param name = "ou"></param>
        /// <param name = "size"></param>
        /// <param name = "parent"></param>
        public WrapPanelOuElement( IOu ou , int size , OuViewer parent )
        {
            try
            {
                this.InitializeComponent();
                ou.Attach( this );
                this._ou = ou;
                this._parent = parent;
                this._size = size;
                this.AllowDrop = true;
                this.Prepare();
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
            if( this == source )
            {
                return;
            }
            try
            {
                this._ou = ou;
                this.ItemLabel.Content = this._ou.GetName();
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
            try
            {
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Detach objewcts that observer this IOu
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Detach( IOuObserver obj )
        {
            try
            {
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Notify observers of this IOu
        /// </summary>
        public void Notify()
        {
            try
            {
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Dispose this object and all the observers
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Dispose( IOuObserver obj )
        {
            try
            {
                obj.Detach( this );
                var parent = ( WrapPanel ) this.Parent;
                parent.Children.Remove( this );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        private void Prepare()
        {
            try
            {
                var fakeSize = 0;

                if( this._size < 40 )
                {
                    fakeSize = 32;
                }
                else if( this._size < 56 && this._size > 40 )
                {
                    fakeSize = 48;
                }
                else if( this._size < 90 && this._size >= 56 )
                {
                    fakeSize = 64;
                }
                else if( this._size >= 90 )
                {
                    fakeSize = 128;
                }

                this.ItemImage.Source = this._ou.GetOuImage( fakeSize ).Source;
                this.ItemImage.Width = this._size;
                this.ItemImage.Height = this._size;
                this.Width = this._size + 58;
                this.Height = this._size + 48;
                this.ItemImage.Source = this._ou.GetOuImage( fakeSize ).Source;
                this.ItemLabel.Content = this._ou.GetName();
                this.ItemLabel.AllowDrop = true;
                this.ItemImage.MouseDown += this.EntityMouseDown;
                this.ItemImage.AllowDrop = true;

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

                    this.ItemImage.ContextMenu = menu;
                    this.ItemImage.ContextMenuOpening += this.MenuContextMenuOpening;
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void MenuContextMenuOpening( object sender , ContextMenuEventArgs e )
        {
            var menu = this.ItemImage.ContextMenu;
            Framework.ContextMenus.AttachMenuItemRegistrations( ref menu , typeof( IOu ) , this.MenuItemClick );
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


        /// <summary>
        ///   Get the ou associated with this wrapper
        /// </summary>
        /// <returns>IOu</returns>
        public IOu GetOu()
        {
            return this._ou;
        }


        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool IsSelected()
        {
            return this._selected;
        }


        /// <summary>
        /// </summary>
        public void Select()
        {
            if( _selectedElement != null && _selectedElement != this )
            {
                _selectedElement.DeSelect();
            }
            try
            {
                this.ItemBorder.BorderBrush = Brushes.LightBlue;
                _selectedElement = this;
                this._selected = true;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// </summary>
        public void DeSelect()
        {
            try
            {
                this.ItemBorder.BorderBrush = Brushes.Transparent;
                _selectedElement = null;
                this._selected = false;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// </summary>
        /// <param name = "resize"></param>
        public void Resize( int resize )
        {
            try
            {
                var fakeSize = 0;

                if( resize < 40 )
                {
                    fakeSize = 32;
                }
                else if( resize < 56 && resize > 40 )
                {
                    fakeSize = 48;
                }
                else if( resize < 90 && resize >= 56 )
                {
                    fakeSize = 64;
                }
                else if( resize >= 90 )
                {
                    fakeSize = 128;
                }

                this.ItemImage.Width = resize;
                this.ItemImage.Height = resize;
                this.ItemImage.Source = this._ou.GetOuImage( fakeSize ).Source;
                this.Width = resize + 58;
                this.Height = resize + 48;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void EntityMouseDown( object sender , MouseButtonEventArgs e )
        {
            try
            {
                this.Select();
                this._parent.SetDetailsPane( new OuPane( this._ou ) );

                if( e.ClickCount == 2 )
                {
                    var ouViewer = new OuViewer( this._ou );
                    Framework.Panels.AddMainComponent( ouViewer , ouViewer.Name );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void AddOuMenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var popup = Framework.Dialog;
                var cou = new CreateOu( this._parent , this._ou , popup );
                popup.SetChild( cou );
                popup.ShowBlocking();
                this._parent.Update();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void RenameOuMenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var popup = Framework.Dialog;
                var cou = new RenameOu( this._parent , this._ou , popup );
                popup.SetChild( cou );
                popup.ShowBlocking();
                this._parent.Update();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void MoveOuMenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var popup = Framework.Dialog;
                var cou = new MoveOu( this._parent , this._ou , popup );
                popup.SetChild( cou );
                popup.ShowBlocking();
                this._parent.Update();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void DeleteOuMenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var popup = Framework.Dialog;
                var cou = new DeleteOu( this._parent , this._ou , popup , false );
                popup.SetChild( cou );
                popup.ShowBlocking();
                this._parent.Update();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
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
                if( e.Source == this || e.Source == this.ItemImage || e.Source == this.ItemLabel )
                {
                    var ele1 = ( TreeViewOuElement ) e.Data.GetData( typeof( TreeViewOuElement ) );
                    if( ele1 != null )
                    {
                        ele1.GetOu().SetParentOuId( this._ou.GetOuId() );
                        this._ou.Notify();
                    }

                    var ele2 = ( WrapPanelOuElement ) e.Data.GetData( typeof( WrapPanelOuElement ) );
                    if( ele2 != null )
                    {
                        ele2.GetOu().SetParentOuId( this._ou.GetOuId() );
                        this._ou.Notify();
                    }

                    var ele3 = ( WrapPanelClientElement ) e.Data.GetData( typeof( WrapPanelClientElement ) );
                    if( ele3 != null )
                    {
                        ele3.GetClient().Setouid( this._ou.GetOuId() );
                        this._ou.Notify();
                    }

                    this._parent.Update();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Invoked when an unhandled <see cref = "E:System.Windows.Input.Mouse.PreviewMouseMove" /> attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name = "e">The <see cref = "T:System.Windows.Input.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnPreviewMouseMove( MouseEventArgs e )
        {
            base.OnPreviewMouseMove( e );

            if( this != _selectedElement )
            {
                return;
            }
            try
            {
                Framework.DragDrop.StartDrag( this , e );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            this._parent.Update();
        }
    }
}