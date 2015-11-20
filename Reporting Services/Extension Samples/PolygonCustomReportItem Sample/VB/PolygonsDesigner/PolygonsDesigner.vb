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

Imports System
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
Imports Microsoft.ReportingServices.RdlObjectModel
Imports System.Xml
Imports System.Xml.Xsl
Imports System.Xml.XPath

''' <summary>
''' Polygon designer window
''' </summary>
''' <remarks></remarks>
<LocalizedName("Polygons")> _
<Editor(GetType(Microsoft.Samples.ReportingServices.CustomEditor), GetType(ComponentEditor))> _
<ToolboxBitmap(GetType(PolygonsDesigner), "Polygons.ico")> _
<CustomReportItem("Polygons"), System.CLSCompliant(False)> _
Public Class PolygonsDesigner
    Inherits CustomReportItemDesigner

    ' this CRI-specific attribute sets the name of the 
    ' custom report item which is referenced by the config
    ' files and saved in the report definition language 

    ' the main class for our CRI design-time component
    Private windowAdornment As PolygonsDesignWindow
    Private designerVerbs As DesignerVerbCollection
    Private componentChangeService As IComponentChangeService

    ' adds a designer verb and context menu handler to the collection 
    ' this item will display in a context menu when the control is right-clicked

    Public Overrides ReadOnly Property Verbs() As DesignerVerbCollection
        Get
            If Me.designerVerbs Is Nothing Then
                Me.designerVerbs = New DesignerVerbCollection()
                Me.designerVerbs.Add(New DesignerVerb("Proportional Scaling", New EventHandler(AddressOf Me.OnProportionalScaling)))
                Me.designerVerbs(0).Checked = Me.GetCustomProperty("poly:Proportional") = Boolean.TrueString
            End If

            Return Me.designerVerbs
        End Get
    End Property

    <Browsable(True), Category("Data")> _
    Public Property DataSetName() As String
        Get
            Return CustomData.DataSetName
        End Get

        Set(ByVal value As String)
            CustomData.DataSetName = value
        End Set
    End Property

    <Browsable(True), Category("Data")> _
    Public Property XExpression() As String
        Get
            If CustomData.DataRows.Count > 0 And CustomData.DataRows(0).Count > 0 Then
                Return GetDataValue(CType(CustomData.DataRows(0)(0), DataCell), "X")
            Else
                Return "X Coordinate"
            End If
        End Get

        Set(ByVal value As String)
            SetDataValue(CType(CustomData.DataRows(0)(0), DataCell), "X", value)
        End Set
    End Property

    <Browsable(True), Category("Data")> _
    Public Property YExpression() As String
        Get
            If CustomData.DataRows.Count > 0 And CustomData.DataRows(0).Count > 0 Then
                Return GetDataValue(CType(CustomData.DataRows(0)(0), DataCell), "Y")
            Else
                Return "Y Coordinate"
            End If
        End Get

        Set(ByVal value As String)
            SetDataValue(CType(CustomData.DataRows(0)(0), DataCell), "Y", value)
        End Set
    End Property

    <Browsable(True), Category("Data")> _
    Public Property ShapeExpression() As String
        Get
            If CustomData.DataRowHierarchy.DataMembers.Count > 0 Then
                Return GetGroupLabel(CustomData.DataRowHierarchy.DataMembers(0).Group)
            Else
                Return "Shape"
            End If
        End Get

        Set(ByVal value As String)
            CustomData.DataRowHierarchy.DataMembers(0).DataMembers(0).Group.GroupExpressions(0) = value
        End Set
    End Property

    <Browsable(True), Category("Data")> _
    Public Property PointExpression() As String
        Get
            If CustomData.DataRowHierarchy.DataMembers.Count > 0 Then
                Return GetGroupLabel(CustomData.DataRowHierarchy.DataMembers(0).DataMembers(0).Group)
            Else
                Return "Point"
            End If
        End Get

        Set(ByVal value As String)
            CustomData.DataRowHierarchy.DataMembers(0).DataMembers(0).Group.GroupExpressions(0) = value
        End Set
    End Property

    <Browsable(True), Category("Appearance"), DefaultValue(False)> _
    Public Property Translucency() As [String]
        Get
            Return Me.GetCustomProperty("poly:Translucency")
        End Get

        Set(ByVal value As [String])
            Me.SetCustomProperty("poly:Translucency", value)
            Invalidate()
        End Set
    End Property

    <Browsable(True), Category("Data")> _
    Public Property ShapeHyperlink() As String
        Get
            Return PolygonsDesigner.GetCustomProperty(CustomData.DataRowHierarchy.DataMembers(0).CustomProperties, "poly:Hyperlink")
        End Get

        Set(ByVal value As String)
            If CustomData.DataRowHierarchy.DataMembers(0).CustomProperties Is Nothing Then
                CustomData.DataRowHierarchy.DataMembers(0).CustomProperties = New RdlCollection(Of CustomProperty)()
            End If

            PolygonsDesigner.SetCustomProperty( _
                CustomData.DataRowHierarchy.DataMembers(0).CustomProperties, _
                "poly:Hyperlink", value)
        End Set
    End Property

    <Browsable(True), Category("Data")> _
    Public Property ShapeColor() As String
        Get
            Return PolygonsDesigner.GetCustomProperty(CustomData.DataRowHierarchy.DataMembers(0).CustomProperties, "poly:Color")
        End Get

        Set(ByVal value As String)
            If CustomData.DataRowHierarchy.DataMembers(0).CustomProperties Is Nothing Then
                CustomData.DataRowHierarchy.DataMembers(0).CustomProperties = New RdlCollection(Of CustomProperty)()
            End If

            PolygonsDesigner.SetCustomProperty(CustomData.DataRowHierarchy.DataMembers(0).CustomProperties, "poly:Color", value)
        End Set
    End Property

    ''' <summary>
    ''' this returns an adornment class that is used to draw outside
    ''' of the main design area rectangle in the host environment
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides ReadOnly Property Adornment() As Adornment
        Get
            If Me.windowAdornment Is Nothing Then
                Me.windowAdornment = New PolygonsDesignWindow(Me)
            End If

            Return Me.windowAdornment
        End Get
    End Property

    Public Function ChangeService() As IComponentChangeService
        If Me.componentChangeService Is Nothing Then
            Me.componentChangeService = CType(Site.GetService(GetType(IComponentChangeService)), IComponentChangeService)
        End If

        Return Me.componentChangeService
    End Function

    Public Sub OnPaint(ByVal e As PaintEventArgs)
        If e Is Nothing Then
            Throw New ArgumentNullException("e")
        End If

        Dim pixelWidth As Integer = CInt(Math.Round(Width))
        Dim pixelHeight As Integer = CInt(Math.Round(Height))
        If Me.GetCustomProperty("poly:Proportional") = Boolean.TrueString Then
            If pixelWidth > pixelHeight Then
                pixelWidth = pixelHeight
            Else
                pixelHeight = pixelWidth
            End If
        End If

        Dim alpha As Integer
        If Me.Translucency = "Transparent" Then
            alpha = 32
        Else
            If Me.Translucency = "Translucent" Then
                alpha = 128
            Else
                alpha = 255
            End If
        End If

        Dim color As Color = color.FromArgb(alpha, Style.Color.Value.Color)
        Dim borderColor As Color = Style.Border.Color.Value.Color
        Dim borderPen As New Pen(borderColor)
        Dim colorBrush As New SolidBrush(color)
        Dim backgroundColorBrush As New SolidBrush(Style.BackgroundColor.Value.Color)
        e.Graphics.FillRectangle(backgroundColorBrush, 0, 0, pixelWidth, pixelHeight)
        e.Graphics.FillRectangle(colorBrush, 3D * pixelWidth / 8D, 3D * pixelHeight / 8D, pixelWidth / 2D, pixelHeight / 2D)
        e.Graphics.DrawRectangle(borderPen, 3D * pixelWidth / 8D, 3D * pixelHeight / 8D, pixelWidth / 2D, pixelHeight / 2D)
        Dim points(2) As Point
        points(0) = New Point(CInt(3 * pixelWidth / 8), CInt(pixelHeight / 8))
        points(1) = New Point(CInt(pixelWidth / 8), CInt(3 * pixelHeight / 5))
        points(2) = New Point(CInt(5 * pixelWidth / 8), CInt(3 * pixelHeight / 5))
        e.Graphics.FillPolygon(colorBrush, points)
        e.Graphics.DrawPolygon(borderPen, points)
        borderPen.Dispose()
        colorBrush.Dispose()
        backgroundColorBrush.Dispose()
    End Sub

    Public Overrides Sub OnDragEnter(ByVal e As DragEventArgs)
        If e Is Nothing Then
            Throw New ArgumentNullException("e")
        End If

        Dim fieldsDataObject As IFieldsDataObject = TryCast(e.Data.GetData(GetType(IReportItemDataObject)), IFieldsDataObject)

        If Not (fieldsDataObject Is Nothing) Then
            BeginEdit()
        End If
    End Sub

    ''' <summary>
    ''' initialize our CustomData structure with default values
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub InitializeNewComponent()
        CustomData = New CustomData()
        CustomData.DataRowHierarchy = New DataHierarchy()

        ' Shape grouping
        CustomData.DataRowHierarchy.DataMembers.Add(New DataMember())
        CustomData.DataRowHierarchy.DataMembers(0).Group = New Group() '

        CustomData.DataRowHierarchy.DataMembers(0).Group.Name = Name + "_Shape" '
        CustomData.DataRowHierarchy.DataMembers(0).Group.GroupExpressions.Add(New ReportExpression()) '

        ' Point grouping
        CustomData.DataRowHierarchy.DataMembers(0).DataMembers.Add(New DataMember()) '
        CustomData.DataRowHierarchy.DataMembers(0).DataMembers(0).Group = New Group() '
        CustomData.DataRowHierarchy.DataMembers(0).DataMembers(0).Group.Name = Name + "_Point" '
        CustomData.DataRowHierarchy.DataMembers(0).DataMembers(0).Group.GroupExpressions.Add(New ReportExpression()) '

        ' Static column
        CustomData.DataColumnHierarchy = New DataHierarchy()
        CustomData.DataColumnHierarchy.DataMembers.Add(New DataMember())

        ' Points
        CustomData.DataRows.Add(New DataRow())
        CustomData.DataRows(0).Add(New DataCell())
        CustomData.DataRows(0)(0).Add(NewDataValue("X", ""))
        CustomData.DataRows(0)(0).Add(NewDataValue("Y", ""))

    End Sub
    Public Function GetCustomProperty(ByVal propertyname As String) As String
        For Each [property] As KeyValuePair(Of String, String) In CustomProperties
            If [property].Key = propertyname Then
                Return DirectCast([property].Value, String)
            End If
        Next
        Return Nothing
    End Function

    Public Sub SetCustomProperty(ByVal propertyname As String, ByVal value As String)
        If Not Me.CustomProperties.ContainsKey(propertyname) Then
            Me.CustomProperties.Add(propertyname, value)
        Else
            Me.CustomProperties(propertyname) = value
        End If
    End Sub

    Private Shared Function GetCustomProperty(ByVal prop As IList(Of CustomProperty), ByVal propertyname As String) As String
        If prop Is Nothing Then
            Return Nothing
        End If
        For i As Integer = 0 To prop.Count - 1

            If prop(i).Name = propertyname Then
                Return prop(i).Value.Value
            End If
        Next

        Return Nothing
    End Function

    Private Shared Sub SetCustomProperty(ByVal prop As IList(Of CustomProperty), ByVal propertyname As String, ByVal value As String)
        Dim found As Boolean = False
        For i As Integer = 0 To prop.Count - 1
            If prop(i).Name = propertyname Then
                prop(i).Value = value
                found = True
                Exit For
            End If
        Next

        If Not found Then
            Dim [property] As New CustomProperty()
            [property].Name = propertyname
            [property].Value = value
            prop.Add([property])
        End If
    End Sub

    Private Shared Function GetGroupLabel(ByVal group As Group) As String
        If Not (group.GroupExpressions Is Nothing) And group.GroupExpressions.Count > 0 Then
            Return group.GroupExpressions(0).Value
        End If

        Return Nothing
    End Function

    Private Shared Function GetDataValue(ByVal cell As DataCell, ByVal name As String) As String
        For Each value As DataValue In cell
            If value.Name = name Then
                Return CStr(value.Value)
            End If
        Next

        Return Nothing
    End Function

    Private Shared Sub SetDataValue(ByVal cell As DataCell, ByVal name As String, ByVal expression As String)
        For Each value As DataValue In cell
            If value.Name = name Then
                value.Value = expression
                Return
            End If
        Next

        Dim datavalue As DataValue = NewDataValue(name, expression)
        cell.Add(datavalue)
    End Sub

    ' method to handle the context menu designer verb
    Private Sub OnProportionalScaling(ByVal sender As Object, ByVal e As EventArgs)
        Dim proportional As Boolean = Not Me.GetCustomProperty("poly:Proportional") = Boolean.TrueString
        Me.designerVerbs(0).Checked = proportional
        Me.SetCustomProperty("poly:Proportional", proportional.ToString())
        Me.ChangeService().OnComponentChanged(Me, Nothing, Nothing, Nothing)
        Invalidate()
    End Sub

    Private Shared Function NewDataValue(ByVal name As String, ByVal value As String) As DataValue
        Dim dv As New DataValue()
        dv.Name = name
        dv.Value = value
        Return dv
    End Function

End Class
