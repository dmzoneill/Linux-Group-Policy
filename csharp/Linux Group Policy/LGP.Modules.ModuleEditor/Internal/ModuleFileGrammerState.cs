#region

using System;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Modules.ModuleEditor.Internal
{
    /// <summary>
    /// </summary>
    public class ModuleFileGrammerState : IGrammer
    {
        private static int _maxValue;
        private readonly IModule _module;
        private IGrammer _dbGrammer;
        private bool _deleted;
        private int _id;
        private string _key;
        private int _moduleid;
        private string _value;

        static ModuleFileGrammerState()
        {
            _maxValue = int.MaxValue;
        }

        /// <summary>
        /// </summary>
        /// <param name = "module"></param>
        /// <param name = "key"></param>
        /// <param name = "value"></param>
        public ModuleFileGrammerState( IModule module , string key , string value )
        {
            try
            {
                this._module = module;
                this._id = _maxValue;
                this._key = key;
                this._value = Framework.Utils.Base64Encode( value );
                this._moduleid = this._module.GetModuleId();
                _maxValue--;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        /// </summary>
        /// <param name = "dbGrammer"></param>
        /// <param name = "module"></param>
        public ModuleFileGrammerState( IGrammer dbGrammer , IModule module )
        {
            try
            {
                this._module = module;
                this._dbGrammer = dbGrammer;

                this._id = this._dbGrammer.GetId();
                this._key = this._dbGrammer.GetKey();
                this._value = this._dbGrammer.GetValue();
                this._moduleid = this._dbGrammer.GetModuleId();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// </summary>
        public void Save()
        {
            try
            {
                if( this._dbGrammer == null )
                {
                    if( !this._deleted )
                    {
                        this._dbGrammer = this._module.AddGrammer( this._key , this._value );
                        this._id = this._dbGrammer.GetId();
                    }
                }
                else
                {
                    if( this._deleted )
                    {
                        this._module.DeleteGrammer( this._key );
                    }
                    else
                    {
                        this._dbGrammer.SetKey( this._key );
                        this._dbGrammer.SetValue( this._value );
                        this._dbGrammer.SetModuleId( this._moduleid );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool IsDeleted()
        {
            return this._deleted;
        }


        /// <summary>
        /// </summary>
        /// <param name = "deleted"></param>
        public void SetDeleted( bool deleted )
        {
            this._deleted = deleted;
        }

        #region Implementation of IGrammerObserver

        /// <summary>
        ///   Update this observer with a refernece to the grammer
        /// </summary>
        /// <param name = "grammer">IGrammer</param>
        /// <param name = "source">IGrammerObserver</param>
        public void Update( IGrammer grammer , IGrammerObserver source )
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
        ///   Attach objewcts that observer this grammer
        /// </summary>
        /// <param name = "obj">IGrammerObserver</param>
        public void Attach( IGrammerObserver obj )
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
        ///   Detach objewcts that observer this grammer
        /// </summary>
        /// <param name = "obj">IGrammerObserver</param>
        public void Detach( IGrammerObserver obj )
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
        ///   Notify observers of this grammer
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
        /// <param name = "obj">IGrammerObserver</param>
        public void Dispose( IGrammerObserver obj )
        {
            try
            {
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        #region Implementation of IGrammer

        /// <summary>
        ///   Gets the grammer rowid
        /// </summary>
        /// <returns>int</returns>
        public int GetId()
        {
            return this._id;
        }

        /// <summary>
        ///   Gets the grammer moduleid
        /// </summary>
        /// <returns>int</returns>
        public int GetModuleId()
        {
            return this._moduleid;
        }

        /// <summary>
        ///   Gets the grammer key
        /// </summary>
        /// <returns>string</returns>
        public string GetKey()
        {
            return this._key;
        }

        /// <summary>
        ///   Gets the grammer value
        /// </summary>
        /// <returns>string</returns>
        public string GetValue()
        {
            return this._value;
        }

        /// <summary>
        ///   Sets the grammer moduleid
        /// </summary>
        /// <param name = "val">int</param>
        public void SetModuleId( int val )
        {
            this._moduleid = val;
        }

        /// <summary>
        ///   Sets the grammer key
        /// </summary>
        /// <param name = "val">string</param>
        public void SetKey( string val )
        {
            this._key = val;
        }

        /// <summary>
        ///   Sets the grammer value
        /// </summary>
        /// <param name = "val">string</param>
        public void SetValue( string val )
        {
            this._value = val;
        }

        #endregion
    }
}