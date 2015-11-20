'=========================================================================

'  File:      ADDataParameterCollection.vb

'  Summary:   Represents a collection of parameters.

'-------------------------------------------------------------------------
'  This file is part of Microsoft SQL Server Code Samples.

'  Copyright (C) Microsoft Corporation.  All rights reserved.

'  This source code is intended only as a supplement to Microsoft
'  Development Tools and/or on-line documentation.  See these other
'  materials for detailed information regarding Microsoft code samples.

'  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
'  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'  PARTICULAR PURPOSE.
'=========================================================================

Imports Microsoft.ReportingServices.DataProcessing
Imports System.Collections.ArrayList

Public Class ADDataParameterCollection
    Inherits ArrayList
    Implements IDataParameterCollection


#Region "IDataParameterCollection Members"
    'Adds an object to the end of the parameter collection.
    Public Overloads Function Add(ByVal parameter As IDataParameter) As Integer Implements IDataParameterCollection.Add
        If Not (CType(parameter, ADDataParameter).ParameterName Is Nothing) Then
            Return MyBase.Add(parameter)
        Else
            Throw New ArgumentException("parameter must be named")
        End If
    End Function

#End Region

#Region "Item"
    'Gets or sets the element at the specified index.
    Default Public Overloads Property Item(ByVal index As String) As Object
        Get
            Return Me(IndexOf(index))
        End Get
        Set(ByVal Value As Object)
            Me(IndexOf(index)) = Value
        End Set
    End Property

#End Region

#Region "Add"
    'Adds an object to the end of the ArrayList.
    Public Overloads Overrides Function Add(ByVal value As Object) As Integer
        Return Add(CType(value, IDataParameter))
    End Function

#End Region

End Class
