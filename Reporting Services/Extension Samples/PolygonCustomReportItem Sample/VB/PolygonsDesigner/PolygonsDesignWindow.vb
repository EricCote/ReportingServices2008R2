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

#Region "Using directives"

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
Imports Rdl = Microsoft.ReportingServices.RdlObjectModel
Imports System.Xml
Imports System.Xml.Xsl
Imports System.Xml.XPath

#End Region

''' <summary>
''' implementation of the adornment class, which allows us to 
''' draw outside of the main rectangle of the design surface
''' and handle UI events
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class PolygonsDesignWindow
    Inherits Adornment

    Private Const BorderWidth As Integer = 4

    Private componentAdornerService As AdornerService
    Private font As Font
    Private frameBounds As Rectangle
    Private frameHeight As Integer
    Private frameWidth As Integer
    Private component As PolygonsDesigner
    Private shapeFrame As Frame
    Private pointFrame As Frame
    Private coordinateFrameX As Frame
    Private coordinateFrameY As Frame
    Private frames() As Frame
    Private dragFields As IFieldsDataObject
    Private textFormat As StringFormat

    Public Sub New(ByVal component As CustomReportItemDesigner)
        Me.component = CType(component, PolygonsDesigner)

        ' this.host = (IDesignerHost)component.Site.GetService(typeof(IDesignerHost));
        Me.componentAdornerService = CType(Me.component.Site.GetService(GetType(AdornerService)), AdornerService)

        Me.font = New Font("Tahoma", 8.0F)
        Me.frameHeight = Me.font.Height + 3 * BorderWidth + 7
        Me.frameWidth = 3 * Me.frameHeight

        Me.textFormat = New StringFormat()
        Me.textFormat.FormatFlags = StringFormatFlags.FitBlackBox Or StringFormatFlags.NoWrap
        Me.textFormat.Trimming = StringTrimming.EllipsisCharacter
        Me.textFormat.Alignment = StringAlignment.Center
        Me.textFormat.LineAlignment = StringAlignment.Center

        Me.shapeFrame = New Frame(Me, DockStyle.Right, My.Resources.Shape)
        Me.pointFrame = New Frame(Me, DockStyle.Top, My.Resources.Point)
        Me.coordinateFrameX = New Frame(Me, DockStyle.Bottom, My.Resources.XCoordinate)
        Me.coordinateFrameY = New Frame(Me, DockStyle.Left, My.Resources.YCoordinate)
        Me.frames = New Frame() {Me.shapeFrame, Me.pointFrame, Me.coordinateFrameX, Me.coordinateFrameY}
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> Public Property AdornerSvc() As AdornerService
        Get
            Return Me.componentAdornerService
        End Get

        Set(ByVal value As AdornerService)
            Me.componentAdornerService = value
        End Set
    End Property

    Public Overrides Sub OnShow()
        AddHandler Me.component.ChangeService().ComponentChanged, AddressOf Me.OnComponentChanged

        Me.UpdateUI()
    End Sub

    Public Overrides Sub OnHide()
        RemoveHandler Me.component.ChangeService().ComponentChanged, AddressOf Me.OnComponentChanged
    End Sub

    Public Sub Paint(ByVal e As PaintEventArgs)
        If e Is Nothing Then
            Throw New ArgumentNullException("e")
        End If

        Dim gr As Graphics = e.Graphics

        For i As Integer = 0 To (Me.frames.Length) - 1
            Me.frames(i).DoPaint(gr)
        Next
    End Sub

    Public Overrides Sub OnDragEnter(ByVal de As DragEventArgs)
        If de Is Nothing Then
            Throw New ArgumentNullException("de")
        End If

        Me.dragFields = TryCast(de.Data.GetData(GetType(IReportItemDataObject)), IFieldsDataObject)

        If Me.dragFields IsNot Nothing Then
            de.Effect = DragDropEffects.Copy
        Else
            de.Effect = DragDropEffects.None
        End If
    End Sub

    Public Overrides Sub OnDragOver(ByVal de As DragEventArgs)
        If de Is Nothing Then
            Throw New ArgumentNullException("de")
        End If

        If Me.dragFields IsNot Nothing Then
            de.Effect = DragDropEffects.Copy
        Else
            de.Effect = DragDropEffects.None
        End If
    End Sub

    Public Overrides Sub OnDragLeave(ByVal e As EventArgs)
        Me.dragFields = Nothing
    End Sub

    Public Overrides Sub OnDragDrop(ByVal de As DragEventArgs)
        If de Is Nothing Then
            Throw New ArgumentNullException("de")
        End If

        If Me.dragFields Is Nothing Then
            Return
        End If

        Me.DropField(Me.GetHitFrame(Me.componentAdornerService.PointToAdorner(New Point(de.X, de.Y))))
        Me.dragFields = Nothing
    End Sub

    Private Shared Function FrameLabel(ByVal Expression As String, ByVal [Default] As String) As String
        If String.IsNullOrEmpty(Expression) Then
            Return [Default]
        End If

        Return Expression
    End Function

    Private Function GetHitFrame(ByVal pt As Point) As Frame
        Dim frameHit As Frame = Nothing

        For i As Integer = 0 To (Me.frames.Length) - 1
            If Me.frames(i).Bounds.Contains(pt) Then
                frameHit = Me.frames(i)
                Exit For
            End If
        Next

        Return frameHit
    End Function

    Private Sub OnComponentChanged(ByVal sender As Object, ByVal e As ComponentChangedEventArgs)
        If e.Component Is Me.component Then
            Me.UpdateUI()
        End If
    End Sub

    Private Sub UpdateUI()
        Dim rect As Rectangle = Me.componentAdornerService.ComponentRectInDesignerFrame(Me.component)

        Me.frameBounds.Location = New Point(rect.X - Me.frameWidth, rect.Y - Me.frameHeight)
        Me.frameBounds.Size = New Size(rect.Width + 2 * Me.frameWidth + 1, rect.Height + 2 * Me.frameHeight + 1)
        Me.componentAdornerService.AdornerWindowBounds = Me.frameBounds

        Me.shapeFrame.Bounds = New Rectangle(Me.frameWidth, 0, rect.Width, Me.frameHeight)
        Me.pointFrame.Bounds = New Rectangle(rect.Width + Me.frameWidth, Me.frameHeight, Me.frameWidth, rect.Height)
        Me.coordinateFrameY.Bounds = New Rectangle(0, Me.frameHeight, Me.frameWidth, rect.Height)
        Me.coordinateFrameX.Bounds = New Rectangle(Me.frameWidth, rect.Height + Me.frameHeight, rect.Width, Me.frameHeight)

        Dim [region] As New [Region](Me.shapeFrame.Bounds)
        [region].Union(Me.pointFrame.Bounds)
        [region].Union(Me.coordinateFrameY.Bounds)
        [region].Union(Me.coordinateFrameX.Bounds)
        Me.componentAdornerService.AdornerWindowRegion = [region]

        Me.coordinateFrameX.Text = FrameLabel(Me.component.XExpression, My.Resources.XCoordinate)
        Me.coordinateFrameY.Text = FrameLabel(Me.component.YExpression, My.Resources.YCoordinate)
        Me.shapeFrame.Text = FrameLabel(Me.component.ShapeExpression, My.Resources.Shape)
        Me.pointFrame.Text = FrameLabel(Me.component.PointExpression, My.Resources.Point)

        Me.componentAdornerService.InvalidateAdorner()
        [region].Dispose()
    End Sub

    Private Function DropField(ByVal state As Object) As Boolean
        Dim frame As Frame = CType(state, Frame)
        Dim field As Rdl.Field = Me.dragFields.Fields(0)

        Me.component.ChangeService().OnComponentChanging(Me.component, Nothing)

        If frame Is Me.coordinateFrameY Then
            Me.component.YExpression = "=Fields!" & field.Name & ".Value"
        Else
            If frame Is Me.coordinateFrameX Then
                Me.component.XExpression = "=Fields!" & field.Name & ".Value"
            Else
                If frame Is Me.shapeFrame Then
                    Me.component.ShapeExpression = "=Fields!" & field.Name & ".Value"
                Else
                    If frame Is Me.pointFrame Then
                        Me.component.PointExpression = "=Fields!" & field.Name & ".Value"
                    End If
                End If
            End If
        End If

        If String.IsNullOrEmpty(Me.component.DataSetName) Then
            Me.component.DataSetName = Me.dragFields.DataSetName
        End If

        Me.component.ChangeService().OnComponentChanged(Me.component, Nothing, Nothing, Nothing)
        Return True
    End Function

    Private Class Frame
        Private frameControl As PolygonsDesignWindow
        Private frameDockStyle As DockStyle
        Private frameBounds As Rectangle
        Private frameTextSize As Size
        Private frameText As String

        Public Sub New(ByVal control As PolygonsDesignWindow, ByVal dock As DockStyle, ByVal [text] As String)
            Me.frameControl = control
            Me.frameDockStyle = dock
            Me.frameText = [text]
        End Sub

        Public Property Bounds() As Rectangle
            Get
                Return Me.frameBounds
            End Get

            Set(ByVal value As Rectangle)
                Me.frameBounds = value
            End Set
        End Property

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _
        Public ReadOnly Property Control() As PolygonsDesignWindow
            Get
                Return Me.frameControl
            End Get
        End Property

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _
        Public ReadOnly Property Dock() As DockStyle
            Get
                Return Me.frameDockStyle
            End Get
        End Property

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _
        Public Property TextSize() As Size
            Get
                Return Me.frameTextSize
            End Get

            Set(ByVal value As Size)
                Me.frameTextSize = value
            End Set
        End Property

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _
        Public Property [Text]() As String
            Get
                Return Me.frameText
            End Get

            Set(ByVal value As String)
                Me.frameText = value

                If Not (value Is Nothing) Then
                    Using gr As Graphics = Me.frameControl.componentAdornerService.AdornerWindowGraphics
                        Dim sizeF As SizeF

                        If Me.frameDockStyle = DockStyle.Top Or Me.frameDockStyle = DockStyle.Bottom Then
                            sizeF = gr.MeasureString(value, Me.frameControl.font)
                        Else
                            sizeF = gr.MeasureString(value, Me.frameControl.font, _
                                Me.frameControl.frameWidth - 2 * BorderWidth, Me.frameControl.textFormat)
                        End If

                        Me.frameTextSize = New Size(CInt(Math.Ceiling(sizeF.Width)), CInt(Math.Ceiling(sizeF.Height)))
                    End Using
                End If
            End Set
        End Property

        Public Overridable Sub DoPaint(ByVal gr As Graphics)
            Dim rect As Rectangle = Me.frameBounds
            rect.Inflate(CInt(-BorderWidth / 2), CInt(-BorderWidth / 2))

            Using pen As New Pen(SystemColors.Control, BorderWidth)
                gr.DrawRectangle(pen, rect)
            End Using

            Dim clipRegion As [Region] = gr.Clip
            rect.Inflate(CInt(-BorderWidth / 2), CInt(-BorderWidth / 2))
            gr.IntersectClip(rect)

            If Not (Me.Text Is Nothing) Then
                Using brush As New SolidBrush(SystemColors.WindowText)
                    Dim format As StringFormat = Me.frameControl.textFormat

                    If Me.frameDockStyle = DockStyle.Top Or Me.frameDockStyle = DockStyle.Bottom Then
                        format.FormatFlags = format.FormatFlags Or StringFormatFlags.NoWrap
                    Else
                        format.FormatFlags = format.FormatFlags And Not StringFormatFlags.NoWrap
                    End If

                    Dim TextRect As Rectangle = Me.frameBounds
                    TextRect.Inflate(-BorderWidth, -BorderWidth)
                    If TextRect.Width > BorderWidth And TextRect.Height > 0 Then
                        gr.DrawString(Me.Text, Me.frameControl.font, brush, TextRect, format)
                    End If
                End Using
            End If

            gr.Clip = clipRegion
        End Sub
    End Class
End Class
