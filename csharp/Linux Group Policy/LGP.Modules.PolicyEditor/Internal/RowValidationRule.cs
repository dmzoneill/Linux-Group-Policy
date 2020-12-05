#region

using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

#endregion

namespace LGP.Modules.PolicyEditor.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class RowValidationRule : ValidationRule
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool Override
        {
            get;
            set;
        }

        /// <summary>
        /// When overridden in a derived class, performs validation checks on a value.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Controls.ValidationResult"/> object.
        /// </returns>
        /// <param name="value">The value from the binding target to check.</param><param name="cultureInfo">The culture to use in this rule.</param>
        public override ValidationResult Validate( object value , CultureInfo cultureInfo )
        {
            if( Override )
            {
                return ValidationResult.ValidResult;
            }

            try
            {
                var group = ( BindingGroup ) value;
                if( group == null )
                {
                    return ValidationResult.ValidResult;
                }

                var obj = value as BindingGroup;

                if( obj.Items != null && obj.Items.Count > 0 )
                {
                    var vpe = obj.Items[ 0 ] as VisualPolicyEntry;
                    if( vpe == null )
                    {
                        return ValidationResult.ValidResult;
                    }

                    if( vpe.Validate() == false )
                    {
                        return new ValidationResult( false , Properties.Resources.Invalidation );
                    }
                }

                return ValidationResult.ValidResult;
            }
            catch( Exception )
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}