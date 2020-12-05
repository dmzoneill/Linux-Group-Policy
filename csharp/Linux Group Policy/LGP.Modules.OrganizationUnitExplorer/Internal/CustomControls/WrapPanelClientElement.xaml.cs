#region

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Modules.OrganizationUnitExplorer.Internal.NodeViews;

#endregion

namespace LGP.Modules.OrganizationUnitExplorer.Internal.CustomControls
{
    /// <summary>
    ///   Interaction logic for WrapPanelClientElement.xaml
    /// </summary>
    public partial class WrapPanelClientElement : IClientObserver
    {
        private static WrapPanelClientElement _selectedElement;
        private readonly OuViewer _parent;
        private readonly int _size;
        private IClient _client;
        private bool _selected;

        /// <summary>
        /// </summary>
        public WrapPanelClientElement()
        {
            this.InitializeComponent();
            this.AllowDrop = true;
        }


        /// <summary>
        /// </summary>
        /// <param name = "client"></param>
        /// <param name = "size"></param>
        /// <param name = "parent"></param>
        public WrapPanelClientElement( IClient client , int size , OuViewer parent )
        {
            try
            {
                this.InitializeComponent();
                client.Attach( this );
                this._client = client;
                this._size = size;
                this._parent = parent;
                this.Prepare();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

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

                this.ItemImage.Source = this._client.GetDistroImage( fakeSize ).Source;
                this.ItemImage.Width = this._size;
                this.ItemImage.Height = this._size;
                this.Width = this._size + 58;
                this.Height = this._size + 48;
                this.ItemLabel.Content = this._client.GetName();
                this.ItemImage.MouseDown += this.EntityMouseDown;

                var resource = Framework.Utils.LoadResource( Assembly.GetExecutingAssembly() , "LGP.Modules.OrganizationUnitExplorer.Internal.ContextMenuItems.ClientContextMenu.xaml" );

                var menu = resource as ContextMenu;
                if( menu != null )
                {
                    var item = ( MenuItem ) menu.Items[ 0 ];
                    item.Click += this.MoveClientMenuItemClick;
                    item.Icon = Framework.Images.GetImage( "Move" , "VS2010ImageLibrary" , 12 );

                    item = ( MenuItem ) menu.Items[ 1 ];
                    item.Click += this.DeleteClientMenuItemClick;
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
        }


        /// <summary>
        ///   Get the client associated with this wrapper
        /// </summary>
        /// <returns>IOu</returns>
        public IClient GetClient()
        {
            return this._client;
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
                this.ItemImage.Source = this._client.GetDistroImage( fakeSize ).Source;
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
                this._parent.SetDetailsPane( new ClientPane( this._client ) );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void MoveClientMenuItemClick( object sender , RoutedEventArgs e )
        {
            this._parent.Update();
        }


        private void DeleteClientMenuItemClick( object sender , RoutedEventArgs e )
        {
            this._parent.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            this._parent.Update();
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

        #region Implementation of IClientObserver

        /// <summary>
        ///   Update this observer with a refernece to the client
        /// </summary>
        /// <param name = "client">IClient</param>
        /// <param name = "source">source observer</param>
        public void Update( IClient client , IClientObserver source )
        {
            if( this == source )
            {
                return;
            }

            try
            {
                this._client = client;
                this.ItemLabel.Content = this._client.GetName();
                this._parent.Update();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Attach objewcts that observer this client
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Attach( IClientObserver obj )
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
        ///   Detach objewcts that observer this client
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Detach( IClientObserver obj )
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
        ///   Notify observers of this client
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
        public void Dispose( IClientObserver obj )
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
    }
}