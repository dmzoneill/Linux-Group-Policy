#region

using System;
using System.Text.RegularExpressions;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Modules.PolicyEditor.Internal.CustomControls
{
    /// <summary>
    ///   Interaction logic for OuPolicyPane.xaml
    /// </summary>
    public partial class OuPolicyPane : IOuObserver
    {
        private static readonly IGrammerGateway Gw;
        private IOu _ou;

        static OuPolicyPane()
        {
            Gw = Framework.Database.CreateGrammerGateway();
        }

        /// <summary>
        /// 
        /// </summary>
        public OuPolicyPane()
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
        /// 
        /// </summary>
        /// <param name="ou"></param>
        public OuPolicyPane( IOu ou )
        {
            try
            {
                this.InitializeComponent();
                this._ou = ou;
                this.image1.Source = Framework.Images.GetImage( "administrative-docs" , "32x32" , 64 ).Source;
                this.Prepare();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void Prepare()
        {
            this.listBox1.ItemsSource = Gw.GetGrammer();
            this.OuNameLabel.Content = this._ou.GetName();
        }

        private void ListBox1SelectionChanged( object sender , System.Windows.Controls.SelectionChangedEventArgs e )
        {
            try
            {
                if( this.listBox1.SelectedIndex > -1 )
                {
                    foreach( var grammer in Gw.GetGrammer() )
                    {
                        if( grammer.GetKey().CompareTo( this.listBox1.SelectedItem.ToString() ) == 0 )
                        {
                            var text = Framework.Utils.Base64Decode( grammer.GetValue() );
                            //text = Regex.Replace( text , @"(\n\n)+" , Environment.NewLine );
                            //text = Regex.Replace( text , @"(\s\s)+" , @"" );

                            text = Regex.Replace( text , @"(\t){5,9}" , @"  " );
                            text = Regex.Replace( text , @"(\t)" , @" " );
                            text = text.Replace( "\\\\" , "\\" );
                            this.textBox1.Text = text;
                            break;
                        }
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region Implementation of IOuObserver

        /// <summary>
        ///   Update this observer with a refernece to the ou
        /// </summary>
        /// <param name = "ou">Iou</param>
        /// <param name = "source">source observer</param>
        public void Update( IOu ou , IOuObserver source )
        {
            try
            {
                if( source != this )
                {
                    this._ou = ou;
                    this.Prepare();
                }
            }
            catch( Exception )
            {
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
            catch( Exception )
            {
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
            catch( Exception )
            {
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
            catch( Exception )
            {
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
            }
            catch( Exception )
            {
            }
        }

        #endregion
    }
}