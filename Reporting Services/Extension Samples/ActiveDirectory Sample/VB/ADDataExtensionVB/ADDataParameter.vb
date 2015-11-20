'=========================================================================

'  File:      ADDataParameter.vb

'  Summary:   Represents a parameter to a Command object.

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

Public Class ADDataParameter
    Implements IDataParameter


#Region "Variables"
    '============================================

    'The Instance Variables for the Parameter Class

    '============================================
    Private _ParameterName As String
    Private _ParameterValue As Object
#End Region


#Region "Constructors"
    '============================================

    'The Constructors for the Parameter Class

    '============================================

    Public Sub New()
        Me.New(Nothing, Nothing)
    End Sub

    Public Sub New(ByVal parameterName As String, ByVal parameterValue As Object)
        _ParameterName = parameterName
        _ParameterValue = parameterValue
    End Sub

#End Region


#Region "IDataParameter Members"
    '==============================================

    'The Implementation of IDataParameter Members

    '==============================================

    'Gets or sets the name of the IDataParameter.
    Public Property ParameterName() As String Implements IDataParameter.ParameterName
        Get
            Return _ParameterName
        End Get
        Set(ByVal value As String)
            _ParameterName = value
        End Set
    End Property

    'Gets or sets the value of the parameter.
    Public Property Value() As Object Implements IDataParameter.Value
        Get
            Return _ParameterValue
        End Get
        Set(ByVal value As Object)
            _ParameterValue = value
        End Set
    End Property

#End Region


End Class
