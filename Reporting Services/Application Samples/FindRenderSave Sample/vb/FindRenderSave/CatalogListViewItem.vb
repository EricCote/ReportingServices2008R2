#Region "Copyright © 2004 Microsoft Corporation. All rights reserved."
'==============================================================================
'  File:    CatalogListViewItem.vb
'
'  Summary: Demonstrates an implementation of a Form that can
'           be used to render a report via the Reporting Services
'           Web service using asychronous Web service calls.
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
'==============================================================================
#End Region

Imports System
Imports System.Windows.Forms
Imports Microsoft.Samples.ReportingServices.FindRenderSave.Microsoft.SqlServer.ReportingServices2010

<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2229:ImplementSerializationConstructors")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly")> <Serializable()> _
Public Class CatalogListViewItem
    Inherits ListViewItem
    Private _catalogItem As CatalogItem

    Public Sub New(ByVal catalogItem As CatalogItem)

        _catalogItem = catalogItem

        ' Assign Name to the base ListViewItem Text property -                                 
        ' this will cause Name to display by default:                                      
        [Text] = _catalogItem.Name
    End Sub

    Public Property Item() As CatalogItem
        Get
            Return _catalogItem
        End Get

        Set(ByVal value As CatalogItem)
            _catalogItem = value
        End Set
    End Property
End Class
