#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

#endregion

namespace ResxTranslator
{
    public partial class MainForm : Form
    {
        private readonly List< string > _paths;

        public MainForm()
        {
            this.InitializeComponent();
            this._paths = new List< string >();
        }

        private void BtnInputBrowseClick( object sender , EventArgs e )
        {
            if( this.folderBrowserDialog1.ShowDialog() == DialogResult.OK )
            {
                this.CreateResxList( this.folderBrowserDialog1.SelectedPath );
            }
        }

        private void CreateResxList( string path )
        {
            foreach( var dir in Directory.GetDirectories( path ) )
            {
                this.CreateResxList( dir );
            }

            foreach( var file in Directory.GetFiles( path ) )
            {
                if( file.EndsWith( "Resources.resx" ) )
                {
                    this._paths.Add( file );
                }
            }
        }

        private void BtnGoClick( object sender , EventArgs e )
        {
            var translator = new Translator();

            foreach( var path in _paths )
            {
                translator.Translate( path );
            }
        }
    }
}