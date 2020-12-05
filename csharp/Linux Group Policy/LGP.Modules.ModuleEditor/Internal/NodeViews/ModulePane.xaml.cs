#region

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Modules.ModuleEditor.Internal.NodeViews
{
    /// <summary>
    ///   Interaction logic for ModulePane.xaml
    /// </summary>
    public partial class ModulePane : IModuleObserver
    {
        private readonly ModuleDocument _document;
        private IModule _module;


        /// <summary>
        /// </summary>
        public ModulePane( IModule module , ModuleDocument documentHandler )
        {
            try
            {
                this.InitializeComponent();
                this._module = module;
                this._document = documentHandler;
                this._module.Attach( this );
                this.Prepare();
                this.image1.Source = Framework.Images.GetImage( "lgp" , "128x128" , 64 ).Source;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region IModuleObserver Members

        /// <summary>
        ///   Update this observer with a refernece to the module
        /// </summary>
        /// <param name = "module">IModule</param>
        /// <param name = "source">IModuleObserver</param>
        public void Update( IModule module , IModuleObserver source )
        {
            try
            {
                if( source == this )
                {
                    return;
                }
                this._module = module;
                this.Prepare();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Attach objewcts that observer this module
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        public void Attach( IModuleObserver obj )
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
        ///   Detach objewcts that observer this module
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        public void Detach( IModuleObserver obj )
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
        ///   Notify observers of this moduleFile
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
        /// <param name = "obj">IModuleObserver</param>
        public void Dispose( IModuleObserver obj )
        {
            try
            {
                this._module.Detach( this );
                this._module = null;
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
                this.stackPanel1.Children.Clear();

                foreach( var grammer in this._module.GetAllGrammers() )
                {
                    var tbox = new TextBox
                    {
                        Text = grammer.GetKey() , FontSize = 10 , Height = 22 , Padding = new Thickness( 1 ) , Margin = new Thickness( 2 , 0 , 2 , 0 ) , IsReadOnly = true , BorderThickness = new Thickness( 1 ) , BorderBrush = Brushes.Transparent
                    };
                    tbox.PreviewMouseDown += this.TextBoxMouseDown;
                    this.stackPanel1.Children.Add( tbox );
                }

                this.ModuleNameLabel.Content = this._module.GetModuleName();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void TextBoxMouseDown( object sender , MouseButtonEventArgs e )
        {
            try
            {
                var tbox = sender as TextBox;
                this._document.GoTo( tbox.Text );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}