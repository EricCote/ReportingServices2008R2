'=========================================================================

'  File:      ADConnection.vb

'  Summary:   Describes the attributes of a Connection, and the implementation
'  of Active Directory Connection class for the Active Directory data 
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


Imports System
Imports Microsoft.ReportingServices.DataProcessing
Imports Microsoft.ReportingServices.Interfaces
Imports System.Security.Permissions
Imports System.Security.Principal


Public Class ADConnection
    Implements IDbConnectionExtension


#Region "Variables"
    '============================================

    'The Instance Variables for the Connection Class

    '============================================
    Private _LocalizedName As String = "Active Directory Information"
    'The provider string is always the same for any Active Directory, so hardcoding the value
    'The Connection string if specified or not, we are using the same connection string.
    Private _ConnectionString As String = "Provider=ADSDSOObject;"
    Private _ConnectionTimeout As Integer = 0
    Private _Impersonate As String = ""
    Private _IntegratedSecurity As Boolean = False
    Private _Password As String = ""
    Private _Username As String = ""
    Private _ConnectionState As System.Data.ConnectionState = ConnectionState.Closed

    Private disposedValue As Boolean = False        ' To detect redundant calls

#End Region


#Region "IDbConnection Members"

    '==============================================

    'The Implementation of IDbConnection Class Methods

    '==============================================

    'This begins a database Transaction, but with Active directory doesnt support Transactions.
    Public Function BeginTransaction() As IDbTransaction Implements IDbConnection.BeginTransaction
        Throw New NotSupportedException()
    End Function


    'This is a member of IDbConnection class. This method closes the connection to the Database
    Public Sub Close() Implements IDbConnection.Close
        'Closing the connection only if the connection is open.
        If _ConnectionState = ConnectionState.Open Then
            _ConnectionState = ConnectionState.Closed
        End If

    End Sub

    'A public property which Gets or Sets the string used to open a database.
    Public Property ConnectionString() As String Implements IDbConnection.ConnectionString
        Get
            Return _ConnectionString
        End Get
        Set(ByVal value As String)
            _ConnectionString = value
        End Set
    End Property

    'Gets the time to wait, while trying to establish a connection, before terminating the attempt and generating an error.
    Public ReadOnly Property ConnectionTimeout() As Integer Implements IDbConnection.ConnectionTimeout
        Get
            Return _ConnectionTimeout
        End Get
    End Property

    'Creates and returns a Command object associated with the connection
    Public Function CreateCommand() As IDbCommand Implements IDbConnection.CreateCommand

            Return New ADCommand(Me)

    End Function

    'Opens a database connection with the settings specified by the ConnectionString property of the provider-specific Connection object.
    Public Sub Open() Implements IDbConnection.Open

        If _ConnectionState <> ConnectionState.Open Then
            _ConnectionState = ConnectionState.Open
        End If

    End Sub

#End Region


#Region "IDbConnectionExtension Members"

    '==============================================

    'The Implementation of IDbConnectionExtension Class Members

    '==============================================


    'Sets the username of the user that is impersonated while queries are executed. 
    'This property is ignored by the report server if impersonation is not supported by the data provider.
    Public WriteOnly Property Impersonate() As String Implements IDbConnectionExtension.Impersonate
        Set(ByVal value As String)
            _Impersonate = value
        End Set
    End Property

    'Indicates whether the connection should use integrated security rather than supply a username and password.
    Public Property IntegratedSecurity() As Boolean Implements IDbConnectionExtension.IntegratedSecurity
        Get
            Return _IntegratedSecurity
        End Get
        Set(ByVal value As Boolean)
            _IntegratedSecurity = value
        End Set
    End Property

    'Sets the password to use when connecting to the database. Overrides any password specified in the connection string.
    Public WriteOnly Property Password() As String Implements IDbConnectionExtension.Password
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property

    'Sets the username to use when connecting to the database. Overrides any username specified in the connection string.
    Public WriteOnly Property UserName() As String Implements IDbConnectionExtension.UserName
        Set(ByVal value As String)
            _Username = value
        End Set
    End Property

#End Region


#Region "IExtension Members"

    '==============================================

    'The Implementation of IExtension Class Members

    '==============================================

    'Gets the localized name of the extension to be displayed in a user interface.
    Public ReadOnly Property LocalizedName() As String Implements IExtension.LocalizedName
        Get
            Return _LocalizedName
        End Get
    End Property

    'Used to pass custom configuration data to an extension
    Public Sub SetConfiguration(ByVal configuration As String) Implements IExtension.SetConfiguration
        'TODO: Based on the requirement we can customize the code to pass the needed configuration.
    End Sub

#End Region


#Region " IDisposable Support "

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
