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
Imports System.Text
Imports Microsoft.ReportingServices.ReportRendering
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Collections.Specialized

Namespace Microsoft.Samples.ReportingServices

    ''' <summary>
    ''' The main class of our custom report item design-time component
    ''' The report processor first calls the Process method, and then
    ''' reads the RenderItem property to get the rendered report item.
    ''' </summary>
    Public Class PolygonsCustomReportItem
        Implements ICustomReportItem
        Private customReportItem As Global.Microsoft.ReportingServices.ReportRendering.CustomReportItem
        Private polygonImage As Global.Microsoft.ReportingServices.ReportRendering.Image
        Private imageMap As Global.Microsoft.ReportingServices.ReportRendering.ImageMapAreasCollection

        ' holds the custom report item property values defined using the Report Designer
        ' and stored in the report definition
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly")> Public WriteOnly Property CustomItem() As CustomReportItem Implements ICustomReportItem.CustomItem
            Set(ByVal value As CustomReportItem)
                Me.customReportItem = value
            End Set
        End Property

        ''' <summary>
        ''' Returns the rendered custom report item
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property RenderItem() As ReportItem Implements ICustomReportItem.RenderItem
            Get
                If Me.polygonImage Is Nothing Then
                    Me.Process()
                End If

                Return Me.polygonImage
            End Get
        End Property

        ''' <summary>
        ''' This is not implemented for SQL Server 2005 and should always return null
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Action() As Action Implements ICustomReportItem.Action
            Get
                Return Nothing
            End Get
        End Property

        ' access the data saved with the custom report item in the design environment and
        ' creates the custom report item which is returned via the RenderItem property
        Public Function Process() As Global.Microsoft.ReportingServices.ReportRendering.ChangeType Implements Global.Microsoft.ReportingServices.ReportRendering.ICustomReportItem.Process
            Dim dpi As Integer = 96
            Dim imageWidth As Integer = CInt(Me.customReportItem.Width.ToInches() * dpi)
            Dim imageHeight As Integer = CInt(Me.customReportItem.Height.ToInches() * dpi)
            Dim image As New Bitmap(imageWidth, imageHeight)
            Dim graphics As Graphics = graphics.FromImage(image)
            Dim backgroundColor As Color = CType(Me.customReportItem.Style("BackgroundColor"), ReportColor).ToColor()
            If backgroundColor = Color.Transparent Then
                backgroundColor = Color.White
            End If

            graphics.Clear(backgroundColor)
            Dim maxX As Integer = CInt(LookupCustomProperty(Me.customReportItem.CustomProperties, "poly:MaxX", 100))
            Dim maxY As Integer = CInt(LookupCustomProperty(Me.customReportItem.CustomProperties, "poly:MaxY", 100))
            Dim minX As Integer = CInt(LookupCustomProperty(Me.customReportItem.CustomProperties, "poly:MinX", 0))
            Dim minY As Integer = CInt(LookupCustomProperty(Me.customReportItem.CustomProperties, "poly:MinY", 0))
            Dim scaleX As Single = imageWidth / CSng(maxX - minX)
            Dim scaleY As Single = imageHeight / CSng(maxY - minY)
            Dim proportional As String = CStr(LookupCustomProperty(Me.customReportItem.CustomProperties, "poly:Proportional", Boolean.FalseString))

            If CStr(proportional) = Boolean.TrueString Then
                If scaleX > scaleY Then
                    scaleX = scaleY
                Else
                    scaleY = scaleX
                End If
            End If

            Dim transString As String = CStr(LookupCustomProperty(Me.customReportItem.CustomProperties, "poly:Translucent", "Opaque"))
            Dim translucency As Integer = 255

            Select Case transString
                Case "Opaque"
                    translucency = 255

                Case "Translucent"
                    translucency = 128

                Case "Transparent"
                    translucency = 32
            End Select

            Dim shapes As DataMemberCollection = Me.customReportItem.CustomData.DataRowGroupings(0)

            Me.imageMap = New ImageMapAreasCollection()

            Dim row As Integer = 0

            Dim i As Integer
            For i = 0 To shapes.Count - 1
                Dim shape As DataMember = shapes(i)
                Dim colorname As String = CStr(LookupCustomProperty(shape.CustomProperties, "poly:Color", "Black"))
                Dim rptColor As New ReportColor(colorname)
                Dim color As Color = color.FromArgb(translucency, rptColor.ToColor())
                Dim brush As System.Drawing.Brush = New SolidBrush(color)
                Dim points As DataMemberCollection = shape.Children(0)
                Dim hyperlink As String = CStr(LookupCustomProperty(shape.CustomProperties, "poly:Hyperlink", ""))
                Dim imageMapArea As New ImageMapArea()
                Dim coordinates(points.Count * 2) As Single
                Dim drawpoints() As Point
                drawpoints = New Point(points.Count) {}
                Dim j As Integer
                For j = 0 To points.Count - 1
                    Dim point As DataCell = Me.customReportItem.CustomData.DataCells(row, 0)
                    Dim x As Integer = 0
                    Dim y As Integer = 0
                    Dim val As Integer
                    For val = 0 To point.DataValues.Count - 1
                        Dim name As String = point.DataValues(val).Name
                        Dim value As Object = point.DataValues(val).Value
                        Dim intvalue As Integer = 0
                        If value.GetType() Is GetType(Integer) Then
                            intvalue = CInt(value)
                        Else
                            If value.GetType() Is GetType(Double) Then
                                intvalue = CInt(CDbl(value))
                            End If
                        End If
                        If name = "X" Then
                            x = CInt((intvalue - minX) * scaleX)
                        Else
                            If name = "Y" Then
                                y = CInt((intvalue - minY) * scaleY)
                            End If
                        End If
                    Next val
                    drawpoints(j) = New Point(x, y)
                    coordinates((j * 2)) = Convert.ToSingle(100 * x / imageWidth)
                    coordinates((j * 2 + 1)) = Convert.ToSingle(100 * y / imageHeight)
                    row = row + 1
                Next j

                If Not String.IsNullOrEmpty(hyperlink) Then
                    imageMapArea.SetCoordinates(imageMapArea.ImageMapAreaShape.Polygon, coordinates)
                    Dim action As New Action()
                    action.SetHyperlinkAction(hyperlink)
                    If imageMapArea.ActionInfo Is Nothing Then
                        imageMapArea.ActionInfo = New ActionInfo()
                    End If

                    If imageMapArea.ActionInfo.Actions Is Nothing Then
                        imageMapArea.ActionInfo.Actions = New ActionCollection()
                    End If

                    imageMapArea.ActionInfo.Actions.Add(action)
                    Me.imageMap.Add(imageMapArea)
                End If

                graphics.FillPolygon(brush, drawpoints)
            Next i

            ' copy the rendered image to a ReportRendering Image type
            ' to be returned via the RenderItem property
            Me.polygonImage = New Global.Microsoft.ReportingServices.ReportRendering.Image(Me.customReportItem.Name, Me.customReportItem.ID)

            Dim stream As New System.IO.MemoryStream()
            image.Save(stream, ImageFormat.Bmp)

            Me.polygonImage.ImageData = New Byte(CInt(stream.Length)) {}
            stream.Seek(0, System.IO.SeekOrigin.Begin)
            stream.Read(Me.polygonImage.ImageData, 0, CInt(stream.Length))
            Me.polygonImage.MIMEType = "image/bmp"

            If Me.imageMap.Count > 0 Then
                Me.polygonImage.ImageMap = Me.imageMap
            End If

            stream.Close()

            ' this should always return ChangeType.None for SQL Server 2005
            Return ChangeType.None
        End Function 'Process

        ' utility method to return custom report item properties from the collection
        Private Shared Function LookupCustomProperty(ByVal properties As CustomPropertyCollection, ByVal name As String, ByVal defaultValue As Object) As Object
            If properties Is Nothing Then
                Return defaultValue
            End If

            Dim [property] As CustomProperty = properties(name)
            If [property] Is Nothing Then
                Return defaultValue
            End If

            If [property].Value.GetType() Is GetType([String]) And String.IsNullOrEmpty(CStr([property].Value)) Then
                Return defaultValue
            End If

            Return [property].Value
        End Function 'LookupCustomProperty
    End Class 'PolygonsCustomReportItem
End Namespace 'Microsoft.Samples.ReportingServices