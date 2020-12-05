#region

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Component;
using LGP.Components.Factory.Interfaces.Module;
using LGP.Components.Factory.Publishers.Events;

#endregion

namespace LGP.Controls
{
    /// <summary>
    ///   Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : ISettings
    {
        private List< UserControl > _controls;
        private List< TreeViewItem > _items;

        /// <summary>
        ///   Constructor
        /// </summary>
        public Settings()
        {
            try
            {
                this.InitializeComponent();
                this.Prepare();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tevent">PreferencesEvent</param>
        public Settings( PreferencesEvent tevent )
        {
            try
            {
                this.InitializeComponent();
                this.Prepare();
                this.SelectPreferencesControl( tevent );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void SelectPreferencesControl( PreferencesEvent tevent )
        {
            try
            {
                foreach( var control in this._controls )
                {
                    var controltype = ( ( IPreferences ) control ).GetControlType();

                    if( controltype != tevent.Type )
                    {
                        continue;
                    }

                    foreach( var item in this._items )
                    {
                        var ns = control.GetType().Namespace;

                        if( ns == null || ns.CompareTo( item.Tag.ToString() ) != 0 )
                        {
                            continue;
                        }

                        item.IsSelected = true;
                        break;
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
            try
            {
                var color = new Color
                {
                    A = 100 , B = 255 , G = 255 , R = 255
                };

                this.Background = new SolidColorBrush( color );
                this._items = new List< TreeViewItem >();
                this._controls = new List< UserControl >();
                this.treeView1.Items.Clear();

                this.dragBar.PreviewMouseLeftButtonDown += this.ChromeLessWindowSpaceMouseLeftButtonDown;

                foreach( var pVisual in Framework.PreferencesPanes )
                {
                    try
                    {
                        var module = ( IPreferences ) Framework.LibraryHandler.CreateObject( pVisual );
                        this._controls.Add( ( UserControl ) module );
                        this.AddOption( module );
                    }
                    catch( Exception error )
                    {
                        Framework.EventBus.Publish( error );
                    }
                }

                var thisprefs = new Preferences();
                this._controls.Add( thisprefs );
                this.AddOption( thisprefs , thisprefs.GetType().Namespace , thisprefs.GetName() , Framework.Images.ConvertBitmapToImage( Properties.Images.lgpico1 , 14 ) );

                this.OrganizeNodes();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void CloseButtonClick( object sender , RoutedEventArgs e )
        {
            try
            {
                foreach( var control in this._controls )
                {
                    var prefs = ( IPreferences ) control;
                    prefs.Save();
                }
                this.Close();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Title bar click event handler
        /// </summary>
        /// <param name = "sender">The object that raised the click event</param>
        /// <param name = "e">Event arguments associated with the event</param>
        private void ChromeLessWindowSpaceMouseLeftButtonDown( object sender , MouseButtonEventArgs e )
        {
            try
            {
                this.Cursor = Cursors.SizeAll;
                this.DragMove();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
            this.Cursor = Cursors.Arrow;
        }


        private void OrganizeNodes()
        {
            var start = 0;
            var end = this._items.Count;

            while( start < end )
            {
                var tag = this._items[ start ].Tag.ToString();

                if( tag.Contains( "." ) )
                {
                    this.CheckParents( tag );
                }

                end = this._items.Count;
                start++;
            }


            start = 0;
            end = this._items.Count;

            while( start < end )
            {
                var item = this._items[ start ];
                item.IsExpanded = true;
                var itemTag = item.Tag.ToString();

                if( itemTag.Contains( "." ) )
                {
                    itemTag = itemTag.Substring( 0 , itemTag.LastIndexOf( "." ) );

                    for( var j = 0; j < end; j++ )
                    {
                        var iteritem = this._items[ j ];
                        var iteritemTag = iteritem.Tag.ToString();

                        if( itemTag.CompareTo( iteritemTag ) == 0 )
                        {
                            iteritem.Items.Add( item );
                        }
                    }
                }

                start++;
            }

            start = 0;
            end = this._items.Count;

            while( start < end )
            {
                var tag = this._items[ start ].Tag.ToString();

                if( !tag.Contains( "." ) )
                {
                    this.treeView1.Items.Add( this._items[ start ] );
                }

                start++;
            }
        }


        private void CheckParents( string tag )
        {
            var parent = tag.Substring( 0 , tag.LastIndexOf( "." ) );

            if( parent.Contains( "." ) )
            {
                this.CheckParents( parent );
            }

            if( this.Exists( parent ) == false )
            {
                var item = new TreeViewItem
                {
                    Tag = parent
                };


                var panel = new StackPanel
                {
                    Orientation = Orientation.Horizontal ,
                    Margin = new Thickness( 2 )
                };

                var block = new TextBlock( new Run( " " + parent.Substring( parent.IndexOf( "." ) + 1 ) ) );

                var img = Framework.Images.ConvertBitmapToImage( Properties.Images.lgpico1 , 14 );
                img.MaxHeight = 14;
                img.MaxWidth = 14;

                panel.Children.Add( img );
                panel.Children.Add( block );

                item.Header = panel;

                this._items.Add( item );
            }
        }


        private bool Exists( string name )
        {
            for( var y = 0; y < this._items.Count; y++ )
            {
                var tag = this._items[ y ].Tag.ToString();

                if( tag.CompareTo( name ) == 0 )
                {
                    return true;
                }
            }

            return false;
        }


        private void AddOption( IPreferences module )
        {
            try
            {
                var item = new TreeViewItem
                {
                    Tag = module.GetType().Namespace
                };


                var panel = new StackPanel
                {
                    Orientation = Orientation.Horizontal ,
                    Margin = new Thickness( 2 )
                };

                var block = new TextBlock( new Run( " " + module.GetName() ) );

                var img = module.GetIcon();
                img.MaxHeight = 14;
                img.MaxWidth = 14;

                panel.Children.Add( img );
                panel.Children.Add( block );

                item.Header = panel;

                this._items.Add( item );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private void AddOption( IPreferences module , string mnamespace , string name , Image img )
        {
            try
            {
                var item = new TreeViewItem
                {
                    Tag = mnamespace
                };


                var panel = new StackPanel
                {
                    Orientation = Orientation.Horizontal ,
                    Margin = new Thickness( 2 )
                };

                var block = new TextBlock( new Run( " " + name ) );

                img.MaxHeight = 14;
                img.MaxWidth = 14;

                panel.Children.Add( img );
                panel.Children.Add( block );

                item.Header = panel;

                this._items.Add( item );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void TreeView1SelectedItemChanged( object sender , RoutedPropertyChangedEventArgs< object > e )
        {
            try
            {
                var treeviewitem = this.treeView1.SelectedItem as TreeViewItem;
                if( treeviewitem != null )
                {
                    var tag = treeviewitem.Tag.ToString();

                    foreach( var control in this._controls )
                    {
                        var s = control.GetType().Namespace;
                        if( s != null && s.CompareTo( tag ) == 0 )
                        {
                            control.Background = Brushes.Transparent;
                            this.prefsName.Content = tag;
                            control.VerticalAlignment = VerticalAlignment.Top;
                            ( ( IPreferences ) control ).Load( this );

                            if( this.paneContainer.Content != null )
                            {
                                ( ( IPreferences ) this.paneContainer.Content ).Save();
                            }
                            this.paneContainer.Content = control;
                        }
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region Implementation of ISettings

        /// <summary>
        /// Ask the settings window to refresh itself
        /// </summary>
        public void Refresh()
        {
            this.Prepare();
            Framework.Panels.UpdateTabNames();
        }

        #endregion
    }
}