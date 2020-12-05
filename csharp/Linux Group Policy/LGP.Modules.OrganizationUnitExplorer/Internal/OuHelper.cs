#region

using System;
using System.Collections.Generic;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using LGP.Modules.OrganizationUnitExplorer.Internal.CustomControls;

#endregion

namespace LGP.Modules.OrganizationUnitExplorer.Internal
{
    internal class OuHelper
    {
        public static readonly IOuGateway OuGateway;
        public static IClientGateway ClientGateway;

        static OuHelper()
        {
            try
            {
                ClientGateway = Framework.Database.CreateClientGateway();
                OuGateway = Framework.Database.CreateOuGateway();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        public static void BuildOuTree( TreeView treeview , bool enableActions , IOu disableThisAndBelow )
        {
            try
            {
                var domainNode = new TreeViewOuElement( treeview );
                treeview.Items.Add( domainNode );
                BuildOuTree( treeview , OuGateway.GetRoots() , domainNode , enableActions , disableThisAndBelow );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private static void BuildOuTree( TreeView treeview , List< IOu > ous , TreeViewOuElement parent , bool enableActions , IOu disableThisAndBelow )
        {
            try
            {
                for( var y = 0; y < ous.Count; y++ )
                {
                    var item = enableActions ? new TreeViewOuElement( ous[ y ] , treeview , true ) : new TreeViewOuElement( ous[ y ] , treeview );

                    if( disableThisAndBelow != null )
                    {
                        if( ous[ y ].GetOuId() == disableThisAndBelow.GetOuId() )
                        {
                            item.IsEnabled = false;
                        }
                    }


                    if( parent == null )
                    {
                        treeview.Items.Add( item );
                    }
                    else
                    {
                        parent.Items.Add( item );
                    }

                    var children = OuGateway.GetChildren( ous[ y ].GetOuId() );

                    if( children != null )
                    {
                        BuildOuTree( treeview , children , item , enableActions , disableThisAndBelow );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}