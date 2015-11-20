'-----------------------------------------------------------------------
'  This file is part of the Microsoft Code Samples.
' 
'  Copyright (C) Microsoft Corporation.  All rights reserved.
' 
'  This source code is intended only as a supplement to Microsoft
'  Development Tools and/or on-line documentation.  See these other
'  materials for detailed information regarding Microsoft code samples.
' 
'  THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
'  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'  PARTICULAR PURPOSE.
'-----------------------------------------------------------------------

Public Class CopyProgress
    Public ReadOnly Property ProgressBar() As ProgressBar
        Get
            Return progressBar1
        End Get
    End Property


    Public ReadOnly Property ItemNameLabel() As Label
        Get
            Return _itemNameLabel
        End Get
    End Property

End Class