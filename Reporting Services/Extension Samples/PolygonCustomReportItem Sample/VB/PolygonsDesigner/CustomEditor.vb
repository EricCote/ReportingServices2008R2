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

Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Collections
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.ReportDesigner
Imports Microsoft.ReportDesigner.Design
Imports Microsoft.ReportingServices.Interfaces
Imports System.Xml
Imports System.Xml.Xsl
Imports System.Xml.XPath

Namespace Microsoft.Samples.ReportingServices

    ''' <summary>
    ''' Show our custom properties editor window
    ''' </summary>
    ''' <remarks></remarks>
    Friend NotInheritable Class CustomEditor
        Inherits ComponentEditor
        Public Overrides Function EditComponent(ByVal context As ITypeDescriptorContext, ByVal component As Object) As Boolean
            Dim designer As PolygonsDesigner = CType(component, PolygonsDesigner)
            Dim dialog As New PolygonProperties()
            dialog.DesignerComponent = designer
            Dim result As DialogResult = dialog.ShowDialog()
            dialog.Dispose()
            If result = DialogResult.OK Then
                designer.Invalidate()
                designer.ChangeService().OnComponentChanged(designer, Nothing, Nothing, Nothing)
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace