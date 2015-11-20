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
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.ReportDesigner
Imports Microsoft.ReportDesigner.Design
Imports Microsoft.ReportingServices.Interfaces
Imports Rdl = Microsoft.ReportingServices.RdlObjectModel


Partial Public Class PolygonProperties
    Inherits Form
    Private component As PolygonsDesigner
    Private reportDataSet As Rdl.DataSet
    Private oldComboValue As String
    Private launchEditor As Boolean

    Public Sub New()
        InitializeComponent()
    End Sub

    <System.CLSCompliant(False)> _
    Public Property DesignerComponent() As PolygonsDesigner
        Get
            Return Me.component
        End Get

        Set(ByVal value As PolygonsDesigner)
            Me.component = value
        End Set
    End Property

    Private Sub PolygonProperties_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Me.DataSetName.Text = Me.component.DataSetName
        Dim datasets As IList(Of Rdl.DataSet) = New List(Of Rdl.DataSet)
        For i As Integer = 0 To datasets.Count - 1
            Me.DataSetName.Items.Add(datasets(i).Name)
        Next

        Me.UpdateDataSet()
        Me.ShapeColor.Text = Me.component.ShapeColor
        If Me.component.GetCustomProperty("poly:Proportional") = Boolean.TrueString Then
            ProportionalScaling.Checked = True
        Else
            ProportionalScaling.Checked = False
        End If

        Translucency.Text = Me.component.Translucency
        Me.MinX.Text = Me.component.GetCustomProperty("poly:MinX")
        Me.MaxX.Text = Me.component.GetCustomProperty("poly:MaxX")
        Me.MinY.Text = Me.component.GetCustomProperty("poly:MinY")
        Me.MaxY.Text = Me.component.GetCustomProperty("poly:MaxY")
        Me.PointID.Text = Me.component.PointExpression
        Me.ShapeID.Text = Me.component.ShapeExpression
        Me.XValue.Text = Me.component.XExpression
        Me.YValue.Text = Me.component.YExpression
        Me.Hyperlink.Text = Me.component.ShapeHyperlink
    End Sub

    Private Sub OK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OK.Click
        Me.component.DataSetName = Me.DataSetName.Text
        Me.component.ShapeColor = Me.ShapeColor.Text
        If ProportionalScaling.Checked = True Then
            Me.component.SetCustomProperty("poly:Proportional", Boolean.TrueString)
        Else
            Me.component.SetCustomProperty("poly:Proportional", Boolean.FalseString)
        End If

        Me.component.Translucency = Translucency.Text
        Me.component.SetCustomProperty("poly:MinX", MinX.Text)
        Me.component.SetCustomProperty("poly:MaxX", MaxX.Text)
        Me.component.SetCustomProperty("poly:MinY", MinY.Text)
        Me.component.SetCustomProperty("poly:MaxY", MaxY.Text)
        Me.component.PointExpression = PointID.Text
        Me.component.ShapeExpression = ShapeID.Text
        Me.component.XExpression = XValue.Text
        Me.component.YExpression = YValue.Text
        Me.component.ShapeHyperlink = Me.Hyperlink.Text
    End Sub

    Private Sub DataSetName_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DataSetName.SelectedIndexChanged
        Me.UpdateDataSet()
    End Sub

    Private Sub UpdateDataSet()
        Me.reportDataSet = Nothing
        Dim datasets As IList(Of Rdl.DataSet) = New List(Of Rdl.DataSet)
        For i As Integer = 0 To datasets.Count - 1
            If datasets(i).Name = Me.DataSetName.Text Then
                Me.reportDataSet = datasets(i)
                Exit For
            End If
        Next

        Me.PopulateFieldSelector(Me.ShapeID)
        Me.PopulateFieldSelector(Me.ShapeColor)
        Me.PopulateFieldSelector(Me.Hyperlink)
        Me.PopulateFieldSelector(Me.PointID)
        Me.PopulateFieldSelector(Me.XValue)
        Me.PopulateFieldSelector(Me.YValue)
    End Sub

    Private Sub PopulateFieldSelector(ByVal combo As ComboBox)
        combo.Items.Clear()
        combo.Items.Add("<Expression...>")
        If Me.reportDataSet Is Nothing Then
            Return
        End If

        For i As Integer = 0 To (Me.reportDataSet.Fields.Count) - 1
            combo.Items.Add(("=Fields!" & Me.reportDataSet.Fields(i).Name & ".Value"))
        Next
    End Sub

    Private Sub EditableCombo_DropDown(ByVal sender As Object, ByVal e As EventArgs) Handles Hyperlink.DropDown, PointID.DropDown, ShapeID.DropDown, XValue.DropDown, YValue.DropDown, Translucency.DropDown, ShapeColor.DropDown
        Dim combo As ComboBox = CType(sender, ComboBox)
        If combo.SelectedIndex = 0 Then
            Dim currentValue As String = combo.Text
            combo.SelectedIndex = -1
            combo.Items(0) = "<Expression...>"
            combo.Text = currentValue
        Else
            combo.Items(0) = "<Expression...>"
        End If
    End Sub

    Private Sub EditableCombo_SelectionChangeCommitted(ByVal sender As Object, ByVal e As EventArgs) Handles Hyperlink.SelectionChangeCommitted, PointID.SelectionChangeCommitted, ShapeID.SelectionChangeCommitted, XValue.SelectionChangeCommitted, YValue.SelectionChangeCommitted, Translucency.SelectionChangeCommitted, ShapeColor.SelectionChangeCommitted
        Dim combo As ComboBox = CType(sender, ComboBox)
        Me.oldComboValue = combo.Text
        Me.launchEditor = True
    End Sub

    Private Sub EditableCombo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Hyperlink.SelectedIndexChanged, PointID.SelectedIndexChanged, ShapeID.SelectedIndexChanged, XValue.SelectedIndexChanged, YValue.SelectedIndexChanged, Translucency.SelectedIndexChanged, ShapeColor.SelectedIndexChanged
        Dim combo As ComboBox = CType(sender, ComboBox)
        If combo.SelectedIndex = 0 And Me.launchEditor Then
            Me.launchEditor = False

            ' if <Expression...> is selected in the combo box,
            ' invoke the report builder expression editor
            Dim editor As New ExpressionEditor()
            Dim newValue As String
            newValue = CStr(editor.EditValue(Nothing, Me.component.Site, Me.oldComboValue))
            combo.Items(0) = newValue
        End If
    End Sub
End Class
