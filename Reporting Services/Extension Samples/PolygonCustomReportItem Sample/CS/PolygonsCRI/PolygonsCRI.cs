//-----------------------------------------------------------------------
//  This file is part of the Microsoft Code Samples.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// 
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
// 
//  THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//-----------------------------------------------------------------------

namespace Microsoft.Samples.ReportingServices
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;
    using Microsoft.ReportingServices.OnDemandReportRendering;

    /// <summary>
    /// The main class for the custom report item design-time component.
    /// The report processor first calls the GenerateReportItemDefinition method 
    /// and then calls the EvaluateReportItemInstance method to get the rendered 
    /// report item.
    /// </summary>
    public class PolygonsCustomReportItem : ICustomReportItem
    {
        #region ICustomReportItem Members

        public void GenerateReportItemDefinition(CustomReportItem cri)
        {
            // Create the Image Definition object that will be 
            // used to render the custom report item
            cri.CreateCriImageDefinition();
            Image polygonImage = (Image)cri.GeneratedReportItem;
        }

        public void EvaluateReportItemInstance(CustomReportItem cri)
        {
            // Get the Image definition
            Image polygonImage = (Image)cri.GeneratedReportItem;

            // Render the image for the custom report item
            polygonImage.ImageInstance.ImageData = DrawImage(cri);
        }

        #endregion

        // Accesses the data saved with the custom report item in the design environment and
        // creates the custom report items image that will be rendered on the report.
        private byte[] DrawImage(CustomReportItem customReportItem)
        {
            int dpi = 96;
            int imageWidth = (int)(customReportItem.Width.ToInches() * dpi);
            int imageHeight = (int)(customReportItem.Height.ToInches() * dpi);

            System.Drawing.Bitmap image = new System.Drawing.Bitmap(imageWidth, imageHeight);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(image);
            System.Drawing.Color backgroundColor = customReportItem.Style.BackgroundColor.Value.ToColor();
            if (backgroundColor == System.Drawing.Color.Transparent)
            {
                backgroundColor = System.Drawing.Color.White;
            }

            graphics.Clear(backgroundColor);

            int maxX = (int)LookupCustomProperty(customReportItem.CustomProperties, "poly:MaxX", 100);
            int maxY = (int)LookupCustomProperty(customReportItem.CustomProperties, "poly:MaxY", 100);
            int minX = (int)LookupCustomProperty(customReportItem.CustomProperties, "poly:MinX", 0);
            int minY = (int)LookupCustomProperty(customReportItem.CustomProperties, "poly:MinY", 0);

            float scaleX = imageWidth / (float)(maxX - minX);
            float scaleY = imageHeight / (float)(maxY - minY);

            string proportional = (string)LookupCustomProperty(customReportItem.CustomProperties, "poly:Proportional", bool.FalseString);

            if ((string)proportional == bool.TrueString)
            {
                if (scaleX > scaleY)
                    scaleX = scaleY;
                else
                    scaleY = scaleX;
            }

            string transString = (string)LookupCustomProperty(customReportItem.CustomProperties, "poly:Translucency", "Opaque");
            int translucency = 255;

            switch (transString)
            {
                case "Opaque":
                    translucency = 255;
                    break;

                case "Translucent":
                    translucency = 128;
                    break;

                case "Transparent":
                    translucency = 32;
                    break;
            }

            CustomData customData = customReportItem.CustomData;

            // Iterate over the Shapes
            foreach (DataMember shape in customData.DataRowHierarchy.MemberCollection)
            {
                // Get the Points for drawing the shapes
                List<System.Drawing.Point> points = new List<System.Drawing.Point>();
                System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

                DataDynamicMemberInstance shapeInstance = (DataDynamicMemberInstance)shape.Instance;

                while (shapeInstance.MoveNext())
                {
                    points.Clear();

                    // Get the Color for the Shape
                    string colorname = (string)LookupCustomProperty(shape.CustomProperties, "poly:Color", "Black");
                    ReportColor rptColor = new ReportColor(colorname);
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(translucency, rptColor.ToColor());
                    brush = new System.Drawing.SolidBrush(color);

                    // Get the Hyperlink value if one has been assigned
                    string hyperlink = (string)LookupCustomProperty(shape.CustomProperties, "poly:Hyperlink", "");

                    // Iterate over the Points
                    foreach (DataMember point in shape.Children)
                    {
                        DataDynamicMemberInstance pointInstance = (DataDynamicMemberInstance)point.Instance;

                        while (pointInstance.MoveNext())
                        {
                            for (int i = 0; i < customData.RowCollection.Count; i++)
                            {
                                DataRow row = customData.RowCollection[i];

                                int x = (int)LookupDataValue(row, "X", 0);
                                int y = (int)LookupDataValue(row, "Y", 0);

                                x = (int)((x - minX) * scaleX);
                                y = (int)((y - minY) * scaleY);

                                points.Add(new System.Drawing.Point(x, y));
                            }
                        }
                    }

                    // Create the Hyperlink Actions
                    if (!string.IsNullOrEmpty(hyperlink))
                    {
                        // Build the coordinates for the Hyperlink Actions
                        float[] coordinates = new float[points.Count * 2];
                        for (int i = 0; i < points.Count; i++)
                        {
                            System.Drawing.Point point = points[i];

                            coordinates[i * 2] = (100 * point.X) / imageWidth;
                            coordinates[i * 2 + 1] = (100 * point.Y) / imageHeight;
                        }

                        Image polygonImage = (Image)customReportItem.GeneratedReportItem;
                        ActionInfoWithDynamicImageMap imageMap = polygonImage.ImageInstance.CreateActionInfoWithDynamicImageMap();
                        Action action = imageMap.CreateHyperlinkAction();
                        action.Instance.HyperlinkText = hyperlink;
                        imageMap.CreateImageMapAreaInstance(ImageMapArea.ImageMapAreaShape.Polygon, coordinates);
                    }

                    // Draw the image
                    graphics.FillPolygon(brush, points.ToArray());
                }
            }

            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Bmp);

            byte[] imageData = new byte[(int)stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(imageData, 0, (int)stream.Length);

            return imageData;
        }

        // Utility method to return custom report item properties from the collection
        private object LookupCustomProperty(CustomPropertyCollection customProperties, string name, object defaultValue)
        {
            object customPropertyValue = defaultValue;

            if (customProperties == null || customProperties.Count == 0)
                return defaultValue;

            CustomProperty customProperty = customProperties[name];

            if (customProperty != null)
            {
                // If the property exists check to see if it is an expression
                // if it is an expression return the evaluated instance
                // otherwise the definition value can be returned
                if (customProperty.Value.IsExpression)
                    customPropertyValue = customProperty.Instance.Value;
                else
                    customPropertyValue = customProperty.Value.Value;
            }

            return customPropertyValue;
        }

        // Utility method to return a data rows values
        private object LookupDataValue(DataRow dataRow, string name, object defaultValue)
        {
            object dataValue = defaultValue;

            if (dataRow == null || dataRow[0].DataValues.Count == 0)
                return defaultValue;

            DataValue dataValueObject = dataRow[0].DataValues[name];

            // If the property exists check to see if it is an expression
            // if it is an expression return the evaluated instance
            // otherwise the definition value can be returned
            if (dataValueObject.Value.IsExpression)
                dataValue = dataValueObject.Instance.Value;
            else
                dataValue = dataValueObject.Value.Value;

            return dataValue;
        }
    }
}