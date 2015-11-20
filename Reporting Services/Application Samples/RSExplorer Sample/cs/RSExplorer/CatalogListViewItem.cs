#region "Copyright Microsoft Corporation. All rights reserved."

/*=============================================================================
  File:      CatalogListViewItem.cs

  Summary:  Demonstrates an implementation of a ListViewItem that can
         be used to display report server items in a list view
         control.

---------------------------------------------------------------------
  This file is part of Microsoft SQL Server Code Samples.
  
  Copyright (C) Microsoft Corporation.  All rights reserved.

 This source code is intended only as a supplement to Microsoft
 Development Tools and/or on-line documentation.  See these other
 materials for detailed information regarding Microsoft code samples.

 THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
 KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 PARTICULAR PURPOSE.
=============================================================================*/

#endregion

#region Using directives

using System;
using System.Windows.Forms;
using Microsoft.SqlServer.ReportingServices2010;

#endregion

namespace Microsoft.Samples.ReportingServices.RSExplorer
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2229:ImplementSerializationConstructors"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly"), Serializable]
	public class CatalogListViewItem : ListViewItem
	{
      CatalogItem m_catalogItem;

      // How To - Create a Listview class that displays CatalogItems
      public CatalogListViewItem ( CatalogItem catalogItem ) : base()                                         
      {                                                                                   
         m_catalogItem = catalogItem;                                                                  
                                                                                              
         // Assign Name to the base ListViewItem Text property -                                 
         // this will cause Name to display by default:                                      
         Text = m_catalogItem.Name;                                                               
                                                                                              
         // Map the other class data to sub-items of the ListItem                            
         // These aren't necessarily displayed...
         this.SubItems.Add(m_catalogItem.Size.ToString() );                                
         this.SubItems.Add(m_catalogItem.TypeName );
         this.SubItems.Add(m_catalogItem.ModifiedDate.ToString());                                            
                                           
      }                                                                                   
                                                                                          
      public CatalogItem Item                                                               
      {                                                                                   
         get 
         { 
            return m_catalogItem;
         }                                                            
         
         set 
         { 
            m_catalogItem = value; 
         }                                                           
      }        
   }
}

