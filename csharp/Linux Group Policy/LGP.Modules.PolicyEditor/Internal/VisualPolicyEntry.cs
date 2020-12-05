#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Modules.PolicyEditor.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class VisualPolicyEntry : INotifyPropertyChanged
    {
        private static readonly IGrammerGateway Ggw;
        private static readonly List< string > KeyValues;

        private string _entrykey;
        private bool _isSelected;
        private Regex _rule;
        private string _rvalue1;
        private string _rvalue10;
        private string _rvalue11;
        private string _rvalue2;
        private string _rvalue3;
        private string _rvalue4;
        private string _rvalue5;
        private string _rvalue6;
        private string _rvalue7;
        private string _rvalue8;
        private string _rvalue9;
        private string _strrule;

        static VisualPolicyEntry()
        {
            Ggw = Framework.Database.CreateGrammerGateway();
            KeyValues = new List< string >();
        }

        /// <summary>
        /// 
        /// </summary>
        public VisualPolicyEntry()
        {
            this.IsSelected = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selected"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public VisualPolicyEntry( bool selected , string key , params string[ ] values )
        {
            try
            {
                this.IsSelected = selected;
                this.EntryKey = key;

                for( var g = 0; g < values.Length; g++ )
                {
                    if( g == 0 )
                    {
                        this._rvalue1 = values[ 0 ];
                    }
                    if( g == 1 )
                    {
                        this._rvalue2 = values[ 1 ];
                    }
                    if( g == 2 )
                    {
                        this._rvalue3 = values[ 2 ];
                    }
                    if( g == 3 )
                    {
                        this._rvalue4 = values[ 3 ];
                    }
                    if( g == 4 )
                    {
                        this._rvalue5 = values[ 4 ];
                    }
                    if( g == 5 )
                    {
                        this._rvalue6 = values[ 5 ];
                    }
                    if( g == 6 )
                    {
                        this._rvalue7 = values[ 6 ];
                    }
                    if( g == 7 )
                    {
                        this._rvalue8 = values[ 7 ];
                    }
                    if( g == 8 )
                    {
                        this._rvalue9 = values[ 8 ];
                    }
                    if( g == 9 )
                    {
                        this._rvalue10 = values[ 9 ];
                    }
                    if( g == 10 )
                    {
                        this._rvalue11 = values[ 10 ];
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Boolean IsSelected
        {
            get
            {
                return this._isSelected;
            }
            set
            {
                this.OnPropertyChanged( "Selected" );
                this._isSelected = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string EntryKey
        {
            get
            {
                return this._entrykey;
            }
            set
            {
                try
                {
                    var grammerlist = Ggw.GetGrammer();

                    foreach( var grammer in grammerlist )
                    {
                        if( grammer.GetKey().CompareTo( value ) == 0 )
                        {
                            this._strrule = "\\s*" + grammer.GetKey() + "\\s*:" + Framework.Utils.Base64Decode( grammer.GetValue() ).Replace( "\\\\" , "\\" ) + "\\s*";
                            this.SetValidationRule( this._strrule );
                        }
                    }
                    this.OnPropertyChanged( "Selected" );
                    this._entrykey = value;
                }
                catch( Exception error )
                {
                    Framework.EventBus.Publish( error );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static List< string > Keys
        {
            get
            {
                return KeyValues;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Rvalue1
        {
            get
            {
                return this._rvalue1;
            }
            set
            {
                this.OnPropertyChanged( "Selected" );
                this._rvalue1 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Rvalue2
        {
            get
            {
                return this._rvalue2;
            }
            set
            {
                this.OnPropertyChanged( "Selected" );
                this._rvalue2 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Rvalue3
        {
            get
            {
                return this._rvalue3;
            }
            set
            {
                this.OnPropertyChanged( "Selected" );
                this._rvalue3 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Rvalue4
        {
            get
            {
                return this._rvalue4;
            }
            set
            {
                this.OnPropertyChanged( "Selected" );
                this._rvalue4 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Rvalue5
        {
            get
            {
                return this._rvalue5;
            }
            set
            {
                this.OnPropertyChanged( "Selected" );
                this._rvalue5 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Rvalue6
        {
            get
            {
                return this._rvalue6;
            }
            set
            {
                this.OnPropertyChanged( "Selected" );
                this._rvalue6 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Rvalue7
        {
            get
            {
                return this._rvalue7;
            }
            set
            {
                this.OnPropertyChanged( "Selected" );
                this._rvalue7 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Rvalue8
        {
            get
            {
                return this._rvalue8;
            }
            set
            {
                this.OnPropertyChanged( "Selected" );
                this._rvalue8 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Rvalue9
        {
            get
            {
                return this._rvalue9;
            }
            set
            {
                this.OnPropertyChanged( "Selected" );
                this._rvalue9 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Rvalue10
        {
            get
            {
                return this._rvalue10;
            }
            set
            {
                this.OnPropertyChanged( "Selected" );
                this._rvalue10 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Rvalue11
        {
            get
            {
                return this._rvalue11;
            }
            set
            {
                this.OnPropertyChanged( "Selected" );
                this._rvalue11 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regex"></param>
        public void SetValidationRule( string regex )
        {
            try
            {
                this._strrule = regex;
                this._rule = new Regex( regex , RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            if( this._rule == null )
            {
                return true;
            }

            var builder = new StringBuilder();
            builder.Append( this.EntryKey + ":" );
            builder.Append( this.Rvalue1 + " " + this.Rvalue2 + " " + this.Rvalue3 + " " + this.Rvalue4 + " " + this.Rvalue5 + " " + this.Rvalue6 + " " + this.Rvalue7 + " " + this.Rvalue8 + " " + this.Rvalue9 + " " + this.Rvalue10 + " " + this.Rvalue11 );

            return this._rule.IsMatch( builder.ToString() );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public static void AddKey( string key )
        {
            if( !KeyValues.Contains( key ) )
            {
                KeyValues.Add( key );
            }
        }

        #region INotifyPropertyChanged

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged( string propertyName )
        {
            if( this.PropertyChanged != null )
            {
                this.PropertyChanged( this , new PropertyChangedEventArgs( propertyName ) );
            }
        }

        #endregion INotifyPropertyChanged
    }
}