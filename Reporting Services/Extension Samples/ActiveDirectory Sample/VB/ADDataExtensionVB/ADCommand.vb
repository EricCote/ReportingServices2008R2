'=========================================================================

'  File:      ADCommand.vb

'  Summary:   Describes the attributes of a command, and the implementation
'  Active Directory Command class for the Active Directory data 
'  processing extension

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

Public Class ADCommand
    Implements IDbCommand


#Region "Variables"

    '============================================

    'The Instance Variables for the Command Class

    '============================================

    Private _Connection As ADConnection
    Private _CommandTimeout As Integer = 0
    Private _CommandText As String
    Private _CommandType As CommandType = Microsoft.ReportingServices.DataProcessing.CommandType.Text
    Private _parameters As ADDataParameterCollection
#End Region


#Region "Constructors"
    '============================================

    'The Constructors for the Command Class

    '============================================

    Sub New()
        Me.New("", Nothing)
    End Sub

    Sub New(ByVal Connection As ADConnection)
        Me.New("", Connection)
    End Sub

    Sub New(ByVal cmdText As String, ByVal Connection As ADConnection)
        _CommandText = cmdText
        _Connection = Connection
        _parameters = New ADDataParameterCollection
    End Sub
#End Region


#Region "IDbCommand Methods"

    '==============================================

    'The Implementation of IDbCommand Class Methods

    '==============================================

    Public Sub Cancel() Implements IDbCommand.Cancel
        'TODO: Attempts to cancel the execution of an IDbCommand
    End Sub

    'Creates a new instance of an IDataParameter object.Here it is ADDataParameter.
    Public Function CreateParameter() As IDataParameter Implements IDbCommand.CreateParameter
        Return New ADDataParameter()
    End Function

    'Executes the CommandText against the Connection and builds an IDataReader.
    Public Function ExecuteReader(ByVal behavior As CommandBehavior) As IDataReader Implements IDbCommand.ExecuteReader
        'We call the GetData method of the DataReader method 
        'This method basically connects to the Active Directory and reads the data into a DataTable
        Dim reader As New ADDataReader()
        reader.GetData(_CommandText)
        Return reader

    End Function

    'Gets or sets the text command to run against the data source.
    Public Property CommandText() As String Implements IDbCommand.CommandText
        Get
            Return _CommandText
        End Get
        Set(ByVal value As String)
            _CommandText = value
        End Set
    End Property

    'Gets or sets the wait time before terminating the attempt to execute a command and generating an error.
    Public Property CommandTimeout() As Integer Implements IDbCommand.CommandTimeout
        Get
            Return _CommandTimeout
        End Get
        Set(ByVal value As Integer)
            _CommandTimeout = value
        End Set
    End Property

    'Indicates or specifies how the CommandText property is interpreted. Here we only have the CommandText as TEXT.
    Public Property CommandType() As CommandType Implements IDbCommand.CommandType
        Get
            Return _CommandType
        End Get
        Set(ByVal value As CommandType)
            _CommandType = value
        End Set
    End Property

    'Gets the IDataParameterCollection.
    Public ReadOnly Property Parameters() As IDataParameterCollection Implements IDbCommand.Parameters
        Get
            Return _parameters
        End Get
    End Property

    'Gets or sets the transaction in which the Command object of a SQL Server Reporting Services data provider executes.
    'Active Directory doesnt support Transactions. So we return nothing.
    Public Property Transaction() As IDbTransaction Implements IDbCommand.Transaction
        Get
            Return Nothing
        End Get
        Set(ByVal value As IDbTransaction)
            Throw New NotSupportedException("Transactions are not supported")
        End Set
    End Property

#End Region


#Region " IDisposable Support "


    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub


    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)

    End Sub

#End Region

End Class
