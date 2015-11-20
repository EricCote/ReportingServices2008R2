#region Copyright Microsoft Corporation. All rights reserved.
/*=============================================================================
  File:      CatalogListViewItem.cs

  Summary:  Demonstrates an implementation of a ListViewItem that can
            be used to display report server items in a list view
            control.
      
---------------------------------------------------------------------
  This file is part of Microsoft SQL Server Code Samples.
  
 This source code is intended only as a supplement to Microsoft
 Development Tools and/or on-line documentation.  See these other
 materials for detailed information regarding Microsoft code samples.

 THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
 KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 PARTICULAR PURPOSE.
==============================================================================*/
#endregion

using System;
using System.Windows.Forms;
using Microsoft.SqlServer.ReportingServices2010;

namespace Microsoft.Samples.ReportingServices.FindRenderSave
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1405:ComVisibleTypeBaseTypesShouldBeComVisible")]
    public class CatalogListViewItem : ListViewItem
    {
        CatalogItem _catalogItem;

        public CatalogListViewItem ( CatalogItem catalogItem ) : base()                                         
        {                                                                                   
            
            _catalogItem = catalogItem;                                                                  
                                                                                                    
                // Assign Name to the base ListViewItem Text property -                                 
                // this will cause Name to display by default:                                      
                Text = _catalogItem.Name;                                                               
        }                                                                                   
                                                                                                
        public CatalogItem Item                                                               
        {                                                                                   
            get 
            { 
                return _catalogItem;
            }                                                            
            
            set 
            { 
                _catalogItem = value; 
            }                                                           
        }        
    }
}
