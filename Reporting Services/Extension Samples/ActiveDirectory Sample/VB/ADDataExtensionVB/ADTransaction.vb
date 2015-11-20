'=========================================================================

'  File:      ADTransaction.vb

'  Summary:   Represents a transaction to be performed at a data source.
'    This is not applicable for the current Data Extension

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

Public Class ADTransaction
    Implements IDbTransaction

#Region "IDbTransaction Methods"
    '==============================================

    'The Implementation of IDbTransaction Methods
    'TODO: Add your transaction logic here

    '==============================================


    Public Sub Commit() Implements IDbTransaction.Commit
        'TODO: Transactions are not supported with Active Directory. 
        'This method is basically used to Commits the database transaction. 
    End Sub

    Public Sub Rollback() Implements IDbTransaction.Rollback
        'TODO: Transactions are not supported with Active Directory. 
        'This method is basically used to roll back transactions from pending state.
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
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
