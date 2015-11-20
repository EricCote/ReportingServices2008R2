'=========================================================================

'  File:      ADDataReader.vb

'  Summary:   Provides a means of reading one or more forward-only streams 
'    of result sets obtained by executing a command at a data source, 
'    and is implemented by AD Data Processing Extensions 
'    that access Active Directory.

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

Imports System.Data.OleDb
Imports Microsoft.ReportingServices.DataProcessing

Public Class ADDataReader
    Implements IDataReader

#Region "Variables"
    '============================================

    'The Instance Variables for the DataReader Class

    '============================================
    Private _Connection As ADConnection
    Private _Command As String
    Private _CurrentRow As Integer = -1
    Private _DataTable As DataTable = New DataTable
#End Region


#Region "Constructors"
    '============================================

    'The Constructors for the DataReader Class

    '============================================
    Friend Sub New()
        'TODO: Implement the default constructor.
    End Sub

    Friend Sub New(ByVal cmdText As String)
        _Command = cmdText
    End Sub

    Friend Sub New(ByVal cmdText As String, ByVal connection As ADConnection)
        _Connection = connection
        _Command = cmdText
    End Sub

#End Region


#Region "IDataReader Members"

    '==============================================

    'The Implementation of IDataReader Members

    '==============================================

    'Returns the total number of columns. Gets the number of fields in the data reader.
    Public ReadOnly Property FieldCount() As Integer Implements IDataReader.FieldCount
        Get
            Return _DataTable.Columns.Count
        End Get
    End Property

    'Gets the Type information corresponding to the type of object that is returned from GetValue.
    'Returns the Datatype of the current column.
    Public Function GetFieldType(ByVal fieldIndex As Integer) As System.Type Implements IDataReader.GetFieldType
        Return (_DataTable.Columns(fieldIndex).DataType)
    End Function

    'Gets the name of the field to find.
    Public Function GetName(ByVal fieldIndex As Integer) As String Implements IDataReader.GetName
        Return (_DataTable.Columns(fieldIndex).ColumnName)
    End Function

    'Return the index of the named field.
    Public Function GetOrdinal(ByVal fieldName As String) As Integer Implements IDataReader.GetOrdinal
        Return _DataTable.Columns(fieldName).Ordinal
    End Function

    'Return the value of the specified field.
    Public Function GetValue(ByVal fieldIndex As Integer) As Object Implements IDataReader.GetValue
        Return (_DataTable.Rows(_CurrentRow)(fieldIndex))
    End Function

    'Reads one row after the other. Advances the IDataReader to the next record.
    'The DataReader object enables a client to retrieve a read-only, forward-only stream of data 
    'from a data source. Results are returned as the query executes and are stored in the network buffer 
    'on the client until you request them using the Read method of the DataReader class
    Public Function Read() As Boolean Implements IDataReader.Read
        _CurrentRow = _CurrentRow + 1
        If (_CurrentRow >= _DataTable.Rows.Count) Then
            Return (False)
        Else
            Return (True)
        End If

    End Function

#End Region


#Region "GetData Method"

    'We are executing the command using the connection string that connects to the Active Directory.
    'Hard coding of the connection string is because it is the same for all the ADs.
    'Once we read the data using a DataReader, we place the same in a DataTable so that can be used for
    'Other processings.
    Public Sub GetData(ByVal _CommandText As String)

        Using Adcon As New OleDbConnection("Provider=ADSDSOObject;")
            Dim AdCmd As New OleDbCommand(_CommandText)
            Dim AdReader As OleDbDataReader
            Try
                Adcon.Open()
                AdCmd.Connection = Adcon
                'Executes the Active Directory command and places the data in a DataTable.
                AdReader = AdCmd.ExecuteReader
                _DataTable.Load(AdReader)

            Catch ex As Exception

            Finally
                'Closes the connection.
                Adcon.Close()
            End Try

        End Using


    End Sub
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
        Try
            Dispose(True)
            GC.SuppressFinalize(Me)
        Catch ex As Exception

        End Try

    End Sub
#End Region

End Class
