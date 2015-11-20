'=============================================================================
'  File:    CatalogListViewItem.vb
'
'  Summary: Demonstrates an implementation of a ListViewItem that can
'           be used to display report server items in a list view
'           control.
'
'---------------------------------------------------------------------
'  This file is part of the Microsoft SQL Server Code Samples.
'  Copyright (C) Microsoft Corporation.  All rights reserved.
'
'  This source code is intended only as a supplement to Microsoft
'  Development Tools and/or on-line documentation.  See these other
'  materials for detailed information regarding Microsoft code samples.
'
'  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
'  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'  PARTICULAR PURPOSE.
'=============================================================================

#Region "Using directives"

Imports System
Imports System.Windows.Forms
Imports Microsoft.Samples.ReportingServices.RSExplorer.Microsoft.SqlServer.ReportingServices2010

'Imports Microsoft.Samples.ReportingServices.RSExplorer.ReportService2005

#End Region

<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")> Public Class CatalogListViewItem
    Inherits ListViewItem
    Private m_catalogItem As CatalogItem

    ' How To - Create a Listview class that displays CatalogItems
    Public Sub New(ByVal catalogItem As CatalogItem)
        m_catalogItem = catalogItem

        ' Assign Name to the base ListViewItem Text property -                                 
        ' this will cause Name to display by default:                                      
        [Text] = m_catalogItem.Name

        ' Map the other class data to sub-items of the ListItem                            
        ' These aren't necessarily displayed...
        Me.SubItems.Add(m_catalogItem.Size.ToString( _
            System.Globalization.CultureInfo.InvariantCulture))
        Me.SubItems.Add(m_catalogItem.TypeName)
        Me.SubItems.Add(m_catalogItem.ModifiedDate.ToString( _
            System.Globalization.CultureInfo.InvariantCulture))
    End Sub

    Public Property Item() As CatalogItem
        Get
            Return m_catalogItem
        End Get

        Set(ByVal value As CatalogItem)
            m_catalogItem = value
        End Set
    End Property
End Class
