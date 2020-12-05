#region

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AvalonDock;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Module;
using LGP.Components.Factory.Publishers.Events;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LGP.Components.Panels
{
    /// <summary>
    ///   Interaction logic for Panels.xaml
    /// </summary>
    public partial class Panels : IPanel
    {
        private readonly ObservableCollection< ExceptionLine > _traceCollection = new ObservableCollection< ExceptionLine >();
        private DocumentContent _selectedMainComponentTab;
        private DockableContent _selectedSideComponentTab;

        /// <summary>
        ///   Constructor for panels
        /// </summary>
        public Panels()
        {
            try
            {
                this.InitializeComponent();
                this.SideBarExplorer.Hide();
                this.BottomBar.Hide();

                ThemeFactory.ChangeTheme( new Uri( "LGP.Components.Panels;component/Themes/lgp.xaml" , UriKind.RelativeOrAbsolute ) );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection< ExceptionLine > TraceCollection
        {
            get
            {
                return this._traceCollection;
            }
        }

        #region IPanel Members

        /// <summary>
        ///   Subscribes subscriber events
        /// </summary>
        public void SubscribeEvents()
        {
            Framework.EventBus.Subscribe< MenuEvent >( this.LoadInternalPane , ThreadOption.UIThread , true );
            Framework.EventBus.Subscribe< Exception >( this.HandleException , ThreadOption.UIThread , true );
        }

        /// <summary>
        ///   Gets the components name
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return Properties.Resources.Panels;
        }


        /// <summary>
        ///   Gets the icon associated with this component
        /// </summary>
        /// <returns>Image</returns>
        public Image GetIcon()
        {
            return Framework.Images.GetImage( "lgp" , "128x128" );
        }


        /// <summary>
        ///   Requests the Panel realizer to add a User control to its display
        /// </summary>
        /// <param name = "control">The user control</param>
        /// <param name = "toolbarName">The title on the toolbar header</param>
        /// <param name = "image">The image icon assoacited with the control</param>
        public void AddSideComponent( UserControl control , string toolbarName , Image image )
        {
            if( control == null )
            {
                return;
            }
            try
            {
                Framework.EventBus.Publish( new PanelEvent( this ) );

                var shown = false;

                foreach( DockableContent tabEntry in this.SideBarExplorer.Items )
                {
                    if( tabEntry.Content == control )
                    {
                        shown = true;
                        tabEntry.Show( this.dockManager );
                        tabEntry.Activate();
                    }
                }

                if( control.Parent == null && shown == false )
                {
                    var aSideBarcontrol = new DockableContent
                    {
                        Background = Brushes.White , Title = toolbarName , Icon = image.Source , Content = control , ToolTip = toolbarName , HideOnClose = false , IsLocked = false
                    };

                    aSideBarcontrol.Closing += this.ASideBarcontrolClosing;
                    this.SideBarExplorer.Items.Add( aSideBarcontrol );
                    aSideBarcontrol.Show( this.dockManager );
                    aSideBarcontrol.Activate();
                }

                control.Focus();
                control.BringIntoView();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Requests the Panel realizer to remove a User control from its display
        /// </summary>
        /// <param name = "control">The control to remove</param>
        public void RemoveSideComponent( UserControl control )
        {
            try
            {
                foreach( DocumentContent dCtrl in this.SideBarExplorer.Items )
                {
                    var cCtrl = ( ContentControl ) dCtrl.Content;
                    if( cCtrl.Content == control )
                    {
                        dCtrl.Close();
                        break;
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Adds a UserControl to the main component container
        /// </summary>
        /// <param name = "control">UserControl element</param>
        /// <param name = "controlTabTitle">The title to give the tab element</param>
        public void AddMainComponent( UserControl control , string controlTabTitle )
        {
            try
            {
                Framework.EventBus.Publish( new PanelEvent( this ) );

                var alreadyOpen = false;
                DocumentContent focusThis = null;

                foreach( DocumentContent cCtrl in this.MainComponentContainer.Items )
                {
                    if( cCtrl.Tag.ToString().CompareTo( controlTabTitle ) == 0 )
                    {
                        alreadyOpen = true;
                        focusThis = cCtrl;
                    }
                }

                if( alreadyOpen == false )
                {
                    var title = controlTabTitle;

                    if( controlTabTitle.Contains( "\\" ) )
                    {
                        var parts = controlTabTitle.Split( '\\' );
                        title = parts[ parts.Length - 1 ];
                    }

                    var newContentControl = new ContentControl
                    {
                        Margin = new Thickness( 0 ) , Content = control
                    };
                    var newDocumentContent = new DocumentContent
                    {
                        Title = title , Background = Brushes.White , Content = newContentControl , Icon = Framework.Images.GetImage( "CutHS" , "VS2010ImageLibrary" , 12 ).Source , Tag = controlTabTitle
                    };
                    newDocumentContent.Closing += this.NewDocumentContentClosing;

                    this.MainComponentContainer.Items.Add( newDocumentContent );
                    newDocumentContent.Show( this.dockManager );
                    newDocumentContent.Activate();
                }
                else
                {
                    focusThis.Show( this.dockManager );
                    focusThis.Activate();

                    var newWindow = ( IUserControl ) control;
                    newWindow.Dispose();
                }

                control.Focus();
                control.BringIntoView();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Requests the Panel realizer to remove a User control from its display
        /// </summary>
        /// <param name = "control">UserControl element</param>
        public void RemoveMainComponent( UserControl control )
        {
            try
            {
                foreach( DocumentContent dCtrl in this.MainComponentContainer.Items )
                {
                    var cCtrl = ( ContentControl ) dCtrl.Content;
                    if( cCtrl.Content == control )
                    {
                        dCtrl.Close();
                        GC.Collect();
                        break;
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Requests the Panel to update the panel tab names
        /// </summary>
        public void UpdateTabNames()
        {
            try
            {
                foreach( DocumentContent dCtrl in this.MainComponentContainer.Items )
                {
                    var cCtrl = ( ContentControl ) dCtrl.Content;
                    var uCtrl = ( UserControl ) cCtrl.Content;
                    var iCtrl = ( IUserControl ) uCtrl;
                    iCtrl.Refresh();
                    dCtrl.Title = ( uCtrl ).Tag.ToString();
                }

                foreach( DockableContent tabEntry in this.SideBarExplorer.Items )
                {
                    var uCtrl = ( UserControl ) tabEntry.Content;
                    var iCtrl = ( IUserControl ) uCtrl;
                    iCtrl.Refresh();
                    tabEntry.Title = uCtrl.Tag.ToString();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Load all side bar components provided by the modules
        /// </summary>
        public void LoadSideBarComponents()
        {
            try
            {
                for( var index = 0; index < Framework.Modules.Count; index++ )
                {
                    var control = Framework.Modules[ index ];
                    if( control.GetSidebarControl() == null )
                    {
                        continue;
                    }
                    var p = new DockableContent
                    {
                        Background = Brushes.White , Title = control.GetName() , Icon = control.GetIcon().Source , Content = control.GetSidebarControl()
                    };
                    this.SideBarExplorer.Items.Add( p );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Registers this components menu handlers
        /// </summary>
        /// <param name = "menu">The program main menu</param>
        public void RegisterMenuEntries( IMenu menu )
        {
        }

        #endregion

        private void NewDocumentContentClosing( object sender , CancelEventArgs e )
        {
            try
            {
                var dockable = ( DocumentContent ) sender;
                var contentControl = ( ContentControl ) dockable.Content;
                var userControl = ( IUserControl ) contentControl.Content;
                userControl.Dispose();

                this._selectedMainComponentTab = null;

                contentControl.Content = null;
                dockable.Content = null;
                this.SideBarExplorer.Items.Remove( dockable );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void ASideBarcontrolClosing( object sender , CancelEventArgs e )
        {
            try
            {
                var dockable = ( DockableContent ) sender;
                dockable.Content = null;
                this.SideBarExplorer.Items.Remove( dockable );
                this._selectedSideComponentTab = null;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void LoadInternalPane( MenuEvent details )
        {
            if( details.Command == null )
            {
                return;
            }

            try
            {
                Framework.EventBus.Publish( new PanelEvent( this ) );

                if( details.Command == "errorPane" )
                {
                    this.errorPane.Show( this.dockManager );
                    this.errorPane.Activate();
                }
                else if( details.Command == "outputPane" )
                {
                    this.outputPane.Show( this.dockManager );
                    this.outputPane.Activate();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void HandleException( Exception details )
        {
            try
            {
                this.outputPane.Show( this.dockManager );
                this.outputPane.Activate();

                this.AddException( details );
                this.listView1.ItemsSource = this.TraceCollection;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void MainComponentContainerSelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            try
            {
                var documentPane = sender as DocumentPane;

                if( documentPane != null )
                {
                    if( this._selectedMainComponentTab == null )
                    {
                        this._selectedMainComponentTab = documentPane.SelectedItem as DocumentContent;

                        if( this._selectedMainComponentTab != null )
                        {
                            var contentControl = ( ContentControl ) this._selectedMainComponentTab.Content;
                            var userControl = ( IUserControl ) contentControl.Content;
                            userControl.Resume();
                        }
                    }
                    else
                    {
                        var contentControl = ( ContentControl ) this._selectedMainComponentTab.Content;
                        var userControl = ( IUserControl ) contentControl.Content;
                        userControl.Pause();

                        this._selectedMainComponentTab = documentPane.SelectedItem as DocumentContent;
                        var selectedMainComponentTab = this._selectedMainComponentTab;

                        if( selectedMainComponentTab != null )
                        {
                            contentControl = ( ContentControl ) selectedMainComponentTab.Content;

                            userControl = ( IUserControl ) contentControl.Content;
                            userControl.Resume();
                        }
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void SideBarExplorerSelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            try
            {
                var documentPane = sender as DocumentPane;

                if( documentPane != null )
                {
                    if( this._selectedMainComponentTab == null )
                    {
                        this._selectedSideComponentTab = documentPane.SelectedItem as DockableContent;

                        if( this._selectedSideComponentTab != null )
                        {
                            var userControl = ( IUserControl ) this._selectedSideComponentTab.Content;
                            userControl.Resume();
                        }
                    }
                    else
                    {
                        var userControl = ( IUserControl ) this._selectedSideComponentTab.Content;
                        userControl.Pause();

                        this._selectedSideComponentTab = documentPane.SelectedItem as DockableContent;
                        var sideComponentTab = this._selectedSideComponentTab;

                        if( sideComponentTab != null )
                        {
                            userControl = ( IUserControl ) sideComponentTab.Content;
                        }
                        userControl.Resume();
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void AddException( Exception e )
        {
            var item = new ExceptionLine
            {
                Message = e.Message ,
                ExceptionType = e.GetType().Name ,
                Source = e.Source ,
                StackTrace = e.StackTrace ,
                TargetSite = e.TargetSite.Name
            };

            this._traceCollection.Insert( 0 , item );

            var ie = e.InnerException;

            if( ie != null )
            {
                this.AddException( ie );
            }
        }

        #region Nested type: ExceptionLine

        /// <summary>
        /// 
        /// </summary>
        public class ExceptionLine
        {
            /// <summary>
            /// 
            /// </summary>
            public string Message
            {
                get;
                set;
            }

            /// <summary>
            /// 
            /// </summary>
            public string ExceptionType
            {
                get;
                set;
            }

            /// <summary>
            /// 
            /// </summary>
            public string Source
            {
                get;
                set;
            }

            /// <summary>
            /// 
            /// </summary>
            public string StackTrace
            {
                get;
                set;
            }

            /// <summary>
            /// 
            /// </summary>
            public string TargetSite
            {
                get;
                set;
            }
        }

        #endregion
    }
}