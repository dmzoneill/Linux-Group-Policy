#region

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Components.Factory.Interfaces.Module;
using LGP.Modules.OrganizationUnitExplorer.Internal;
using LGP.Modules.OrganizationUnitExplorer.Internal.CustomControls;
using LGP.Modules.OrganizationUnitExplorer.Internal.Modals.Controls;
using LGP.Modules.OrganizationUnitExplorer.Internal.NodeViews;

#endregion

namespace LGP.Modules.OrganizationUnitExplorer
{
    /// <summary>
    ///   Interaction logic for OuViewer.xaml
    /// </summary>
    public partial class OuViewer : IOuObserver , IUserControl
    {
        private IOu _ou;
        private int _viewType1;
        private int _viewType2;

        /// <summary>
        ///   Constructor
        /// </summary>
        public OuViewer()
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
        ///   Constructor
        /// </summary>
        public OuViewer( IOu ou )
        {
            try
            {
                this.InitializeComponent();

                OuHelper.ClientGateway.Refresh();

                ou.Attach( this );

                this.Name = ou.GetName();
                this.Tag = ou.GetName();
                this._ou = ou;

                this._viewType1 = 1;
                this._viewType2 = 1;

                this.GridViewImage1.Source = Framework.Images.GetImage( "graphic-design" , "16x16" , 12 ).Source;
                this.ListViewImage1.Source = Framework.Images.GetImage( "attibutes" , "16x16" , 12 ).Source;
                this.GridViewImage2.Source = Framework.Images.GetImage( "graphic-design" , "16x16" , 12 ).Source;
                this.ListViewImage2.Source = Framework.Images.GetImage( "attibutes" , "16x16" , 12 ).Source;
                this.ImageMinusZoom.Source = Framework.Images.GetImage( "zoomout" , "VS2010ImageLibrary" , 12 ).Source;
                this.ImagePlusZoom.Source = Framework.Images.GetImage( "zoomin" , "VS2010ImageLibrary" , 12 ).Source;
                this.OuImage.Source = this._ou.GetOuImage( 16 ).Source;

                var resource = Framework.Utils.LoadResource( Assembly.GetExecutingAssembly() , "LGP.Modules.OrganizationUnitExplorer.Internal.ContextMenuItems.OuContextMenu.xaml" );

                var menu = resource as ContextMenu;
                if( menu != null )
                {
                    var item = ( MenuItem ) menu.Items[ 0 ];
                    item.Click += this.AddMenuItemClick;
                    item.Icon = Framework.Images.GetImage( "workgroup" , "distros/32" , 12 );
                    menu.Items.Remove( item );
                    this.OuImageMenu.Items.Add( item );

                    item = ( MenuItem ) menu.Items[ 0 ];
                    item.Click += this.RenameMenuItemClick;
                    item.Icon = Framework.Images.GetImage( "RenameFolderHS" , "VS2010ImageLibrary" , 12 );
                    menu.Items.Remove( item );
                    this.OuImageMenu.Items.Add( item );

                    item = ( MenuItem ) menu.Items[ 0 ];
                    item.Click += this.MoveMenuItemClick;
                    item.Icon = Framework.Images.GetImage( "Move" , "VS2010ImageLibrary" , 12 );
                    menu.Items.Remove( item );
                    this.OuImageMenu.Items.Add( item );

                    item = ( MenuItem ) menu.Items[ 0 ];
                    item.Click += this.DeleteMenuItemClick;
                    item.Icon = Framework.Images.GetImage( "DeleteHS" , "VS2010ImageLibrary" , 12 );
                    menu.Items.Remove( item );
                    this.OuImageMenu.Items.Add( item );
                }

                this.OuLabel.Content = this._ou.GetName();
                this.ShowOrganizationalUnit( 64 );
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
                this.Name = this._ou.GetName();
                this.OuLabel.Content = this._ou.GetName();
                this.Tag = this._ou.GetName();
                Framework.Panels.UpdateTabNames();
                this.ShowOrganizationalUnit( ( ( int ) this.progressBar1.Value ) + 32 );
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
                Framework.Panels.RemoveMainComponent( this );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        private void MenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var menuitem = sender as MenuItem;
                Framework.ContextMenus.CallBack( menuitem , this._ou );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void ShowOrganizationalUnit( int size )
        {
            try
            {
                if( this.viewer1.Children != null )
                {
                    foreach( WrapPanelOuElement item in this.viewer1.Children )
                    {
                        item.GetOu().Detach( item );
                    }

                    this.viewer1.Children.Clear();
                }

                if( this.viewer2.Children != null )
                {
                    foreach( WrapPanelClientElement item in this.viewer2.Children )
                    {
                        item.GetClient().Detach( item );
                    }

                    this.viewer2.Children.Clear();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }

            try
            {
                var ous = OuHelper.OuGateway.GetChildren( this._ou.GetOuId() );

                if( ous != null )
                {
                    for( var y = 0; y < ous.Count; y++ )
                    {
                        var ele = new WrapPanelOuElement( ous[ y ] , size , this );
                        this.viewer1.Children.Add( ele );
                    }

                    this.mainGrid.RowDefinitions[ 2 ].Height = new GridLength( 140 , GridUnitType.Star );
                }
                else
                {
                    this.mainGrid.RowDefinitions[ 2 ].Height = new GridLength( 22 , GridUnitType.Star );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }

            try
            {
                var clients = OuHelper.ClientGateway.GetOuClients( this._ou.GetOuId() );

                if( clients != null )
                {
                    for( var y = 0; y < clients.Count; y++ )
                    {
                        var ele = new WrapPanelClientElement( clients[ y ] , size , this );
                        this.viewer2.Children.Add( ele );
                    }
                    this.mainGrid.RowDefinitions[ 4 ].Height = new GridLength( 140 , GridUnitType.Star );
                }
                else
                {
                    this.mainGrid.RowDefinitions[ 4 ].Height = new GridLength( 22 , GridUnitType.Star );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// </summary>
        /// <param name = "pane"></param>
        public void SetDetailsPane( UserControl pane )
        {
            try
            {
                if( this.ClientViewBorderContainer.Child != null )
                {
                    if( this.ClientViewBorderContainer.Child.GetType() == typeof( ClientPane ) )
                    {
                        var client = ( ClientPane ) this.ClientViewBorderContainer.Child;
                        client.GetClient().Detach( client );
                    }

                    if( this.ClientViewBorderContainer.Child.GetType() == typeof( OuPane ) )
                    {
                        var ou = ( OuPane ) this.ClientViewBorderContainer.Child;
                        ou.GetOu().Detach( ou );
                    }
                }
                this.ClientViewBorderContainer.Child = pane;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void GridViewImageMouseDown1( object sender , MouseButtonEventArgs e )
        {
            this._viewType1 = 1;
            this.ListViewImage1.Opacity = 0.2;
        }


        private void ListViewImageMouseDown1( object sender , MouseButtonEventArgs e )
        {
            this._viewType1 = 0;
            this.GridViewImage1.Opacity = 0.2;
        }


        private void ImageMouseEnter1( object sender , MouseEventArgs e )
        {
            try
            {
                var image = ( Image ) sender;
                image.Opacity = 0.8;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void ImageMouseLeave1( object sender , MouseEventArgs e )
        {
            try
            {
                var image = ( Image ) sender;

                if( this._viewType1 == 1 && image.Tag.ToString().CompareTo( "ListView" ) == 0 )
                {
                    image.Opacity = 0.2;
                }
                else if( this._viewType1 == 0 && image.Tag.ToString().CompareTo( "GridView" ) == 0 )
                {
                    image.Opacity = 0.2;
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void GridViewImageMouseDown2( object sender , MouseButtonEventArgs e )
        {
            this._viewType2 = 1;
            this.ListViewImage2.Opacity = 0.2;
        }


        private void ListViewImageMouseDown2( object sender , MouseButtonEventArgs e )
        {
            this._viewType2 = 0;
            this.GridViewImage2.Opacity = 0.2;
        }


        private void ImageMouseEnter2( object sender , MouseEventArgs e )
        {
            try
            {
                var image = ( Image ) sender;
                image.Opacity = 0.8;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void ImageMouseLeave2( object sender , MouseEventArgs e )
        {
            try
            {
                var image = ( Image ) sender;

                if( this._viewType2 == 1 && image.Tag.ToString().CompareTo( "ListView" ) == 0 )
                {
                    image.Opacity = 0.2;
                }
                else if( this._viewType2 == 0 && image.Tag.ToString().CompareTo( "GridView" ) == 0 )
                {
                    image.Opacity = 0.2;
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void ResizeOrganizationalUnits( int pos )
        {
            try
            {
                foreach( UIElement ele in this.viewer1.Children )
                {
                    ( ( WrapPanelOuElement ) ele ).Resize( pos );
                }

                foreach( UIElement ele in this.viewer2.Children )
                {
                    ( ( WrapPanelClientElement ) ele ).Resize( pos );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void ProgressBar1MouseMove( object sender , MouseEventArgs e )
        {
            try
            {
                if( e.LeftButton == MouseButtonState.Pressed )
                {
                    var pos = ( ( decimal ) e.GetPosition( this.progressBar1 ).X / ( decimal ) this.progressBar1.Width ) * 100;
                    this.progressBar1.Value = Convert.ToInt32( pos );
                    pos = Convert.ToInt32( pos ) + 32;

                    if( pos > 128 )
                    {
                        pos = 128;
                    }

                    this.ResizeOrganizationalUnits( ( int ) pos );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void ProgressBar1MouseLeftButtonDown( object sender , MouseButtonEventArgs e )
        {
            try
            {
                if( e.LeftButton == MouseButtonState.Pressed )
                {
                    var pos = ( ( decimal ) e.GetPosition( this.progressBar1 ).X / ( decimal ) this.progressBar1.Width ) * 100;
                    this.progressBar1.Value = Convert.ToInt32( pos );
                    pos = Convert.ToInt32( pos ) + 32;

                    if( pos > 128 )
                    {
                        pos = 128;
                    }

                    this.ResizeOrganizationalUnits( ( int ) pos );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void AddMenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var popup = Framework.Dialog;
                var cou = new CreateOu( this , this._ou , popup );
                popup.SetChild( cou );
                popup.ShowBlocking();
                this.ShowOrganizationalUnit( ( ( int ) this.progressBar1.Value ) + 32 );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void RenameMenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                this.OuLabelEditBox.Text = this.OuLabel.Content.ToString();
                this.OuLabel.Visibility = Visibility.Hidden;
                this.OuLabelEditBox.Visibility = Visibility.Visible;
                this.ShowOrganizationalUnit( ( ( int ) this.progressBar1.Value ) + 32 );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void MoveMenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var popup = Framework.Dialog;
                var cou = new MoveOu( this , this._ou , popup );
                popup.SetChild( cou );
                popup.ShowBlocking();
                this.ShowOrganizationalUnit( ( ( int ) this.progressBar1.Value ) + 32 );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void DeleteMenuItemClick( object sender , RoutedEventArgs e )
        {
            try
            {
                var popup = Framework.Dialog;
                var cou = new DeleteOu( this , this._ou , popup , true );
                popup.SetChild( cou );
                popup.ShowBlocking();
                this.ShowOrganizationalUnit( ( ( int ) this.progressBar1.Value ) + 32 );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void OuEditBoxKeyUp( object sender , KeyEventArgs e )
        {
            try
            {
                if( e.Key == Key.Return && this.OuLabelEditBox.Text.Length > 0 )
                {
                    this.OuLabelEditBox.Visibility = Visibility.Hidden;
                    this.OuLabel.Visibility = Visibility.Visible;
                    this._ou.SetName( this.OuLabelEditBox.Text );
                    this.OuLabel.Content = this._ou.GetName();
                    this.Tag = this._ou.GetName();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Used bu children to request update
        /// </summary>
        public void Update()
        {
            this.Update( this._ou , null );
        }


        private void OuLabelEditBoxTextChanged( object sender , TextChangedEventArgs e )
        {
            this.OuLabelEditBox.Text = Framework.Utils.CleanString( this.OuLabelEditBox.Text );
            this.OuLabelEditBox.CaretIndex = this.OuLabelEditBox.Text.Length;
        }

        private void OuImageMenuSubmenuOpened( object sender , RoutedEventArgs e )
        {
            var menu = this.OuImageMenu;
            Framework.ContextMenus.AttachMenuItemRegistrations( ref menu , typeof( IOu ) , this.MenuItemClick );
        }

        #region Implementation of IUserControl

        /// <summary>
        ///   Aks the UserControl to do some clean up!
        /// </summary>
        public void Dispose()
        {
            try
            {
                if( this.viewer1.Children != null )
                {
                    foreach( WrapPanelOuElement item in this.viewer1.Children )
                    {
                        item.GetOu().Detach( item );
                    }

                    this.viewer1.Children.Clear();
                }

                if( this.viewer2.Children != null )
                {
                    foreach( WrapPanelClientElement item in this.viewer2.Children )
                    {
                        item.GetClient().Detach( item );
                    }

                    this.viewer2.Children.Clear();
                }

                if( this.ClientViewBorderContainer.Child != null )
                {
                    if( this.ClientViewBorderContainer.Child.GetType() == typeof( ClientPane ) )
                    {
                        var client = ( ClientPane ) this.ClientViewBorderContainer.Child;
                        client.GetClient().Detach( client );
                    }

                    if( this.ClientViewBorderContainer.Child.GetType() == typeof( OuPane ) )
                    {
                        var ou = ( OuPane ) this.ClientViewBorderContainer.Child;
                        ou.GetOu().Detach( ou );
                    }
                }

                this._ou.Detach( this );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        /// Asks the Usercontrol to refresh itself
        /// </summary>
        public void Refresh()
        {
            this.Tag = this._ou.GetName();
        }

        /// <summary>
        /// When the control is no longer in focus, asks it to pause ( heavy duty non critical, ie threads updating graphs )
        /// </summary>
        public void Pause()
        {
            try
            {
            }
            catch( Exception )
            {
            }
        }

        /// <summary>
        /// When the tab comes back into focus, resume ( heavy duty non critical, ie threads updating graphs )
        /// </summary>
        public void Resume()
        {
            try
            {
            }
            catch( Exception )
            {
            }
        }

        #endregion
    }
}