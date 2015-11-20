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
    #region Using directives

    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using Microsoft.ReportDesigner;
    using Microsoft.ReportDesigner.Design;
    using Microsoft.ReportingServices.Interfaces;
    using Microsoft.ReportingServices.RdlObjectModel;
    using System.Xml;
    using System.Xml.Xsl;
    using System.Xml.XPath;

    #endregion

    [LocalizedName("Polygons")]
    [Editor(typeof(CustomEditor), typeof(ComponentEditor))]
    [ToolboxBitmap(typeof(PolygonsDesigner), "Polygons.ico")]

    // this CRI-specific attribute sets the name of the 
    // custom report item which is referenced by the config
    // files and saved in the report definition language 
    [CustomReportItem("Polygons")]

    // the main class for our CRI design-time component
    [System.CLSCompliant(false)]
    public class PolygonsDesigner : CustomReportItemDesigner
    {
        private PolygonsDesignWindow adornment;
        private DesignerVerbCollection verbs;
        private IComponentChangeService changeService;

        // adds a designer verb and context menu handler to the collection 
        // this item will display in a context menu when the control is right-clicked
        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (this.verbs == null)
                {
                    this.verbs = new DesignerVerbCollection();
                    this.verbs.Add(new DesignerVerb("Proportional Scaling", new EventHandler(this.OnProportionalScaling)));
                    this.verbs[0].Checked = (this.GetCustomProperty("poly:Proportional") == bool.TrueString);
                }

                return this.verbs;
            }
        }

        [Browsable(true), Category("Data")]
        public string DataSetName
        {
            get
            {
                return CustomData.DataSetName;
            }

            set
            {
                CustomData.DataSetName = value;
            }
        }

        [Browsable(true), Category("Data")]
        public string XExpression
        {
            get
            {
                if (CustomData.DataRows.Count > 0 && CustomData.DataRows[0].Count > 0)
                {
                    return GetDataValue(CustomData.DataRows[0][0], "X");
                }
                else
                {
                    return "X Coordinate";
                }
            }

            set
            {
                SetDataValue(CustomData.DataRows[0][0], "X", value);
            }
        }

        [Browsable(true), Category("Data")]
        public string YExpression
        {
            get
            {
                if (CustomData.DataRows.Count > 0 && CustomData.DataRows[0].Count > 0)
                {
                    return GetDataValue(CustomData.DataRows[0][0], "Y");
                }
                else
                {
                    return "Y Coordinate";
                }
            }

            set
            {
                SetDataValue(CustomData.DataRows[0][0], "Y", value);
            }
        }

        [Browsable(true), Category("Data")]
        public string ShapeExpression
        {
            get
            {
                if (CustomData.DataRowHierarchy.DataMembers.Count > 0)
                {
                    return GetGroupLabel(CustomData.DataRowHierarchy.DataMembers[0].Group);
                }
                else
                {
                    return "Shape";
                }
            }

            set
            {
                CustomData.DataRowHierarchy.DataMembers[0].Group.GroupExpressions[0] = value;
            }
        }

        [Browsable(true), Category("Data")]
        public string PointExpression
        {
            get
            {
                if (CustomData.DataRowHierarchy.DataMembers.Count > 0)
                {
                    return GetGroupLabel(CustomData.DataRowHierarchy.DataMembers[0].DataMembers[0].Group);
                }
                else
                {
                    return "Point";
                }
            }

            set
            {
                CustomData.DataRowHierarchy.DataMembers[0].DataMembers[0].Group.GroupExpressions[0] = value;
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue(false)]
        public String Translucency
        {
            get
            {
                return this.GetCustomProperty("poly:Translucency");
            }

            set
            {
                this.SetCustomProperty("poly:Translucency", value);
                Invalidate();
            }
        }

        [Browsable(true), Category("Data")]
        public string ShapeHyperlink
        {
            get
            {
                return PolygonsDesigner.GetCustomProperty(
                    CustomData.DataRowHierarchy.DataMembers[0].CustomProperties,
                    "poly:Hyperlink");
            }

            set
            {
                if (CustomData.DataRowHierarchy.DataMembers[0].CustomProperties == null)
                {
                    CustomData.DataRowHierarchy.DataMembers[0].CustomProperties = new RdlCollection<CustomProperty>();
                }

                PolygonsDesigner.SetCustomProperty(
                    CustomData.DataRowHierarchy.DataMembers[0].CustomProperties,
                    "poly:Hyperlink",
                    value);
            }
        }

        [Browsable(true), Category("Data")]
        public string ShapeColor
        {
            get
            {
                return PolygonsDesigner.GetCustomProperty(
                    CustomData.DataRowHierarchy.DataMembers[0].CustomProperties,
                    "poly:Color");
            }

            set
            {
                if (CustomData.DataRowHierarchy.DataMembers[0].CustomProperties == null)
                {
                    CustomData.DataRowHierarchy.DataMembers[0].CustomProperties = new RdlCollection<CustomProperty>();
                }

                PolygonsDesigner.SetCustomProperty(
                    CustomData.DataRowHierarchy.DataMembers[0].CustomProperties,
                    "poly:Color",
                    value);
            }
        }

        // this returns an adornment class that is used to draw outside
        // of the main design area rectangle in the host environment
        [System.CLSCompliant(false)]
        public override Adornment Adornment
        {
            get
            {
                if (this.adornment == null)
                {
                    this.adornment = new PolygonsDesignWindow(this);
                }

                return this.adornment;
            }
        }

        public IComponentChangeService ChangeService()
        {
            if (this.changeService == null)
            {
                this.changeService = (IComponentChangeService)Site.GetService(typeof(IComponentChangeService));
            }

            return this.changeService;
        }

        public string GetCustomProperty(string propertyname)
        {
            foreach (KeyValuePair<string, string> property in CustomProperties)
            {
                if (property.Key == propertyname)
                    return (string)property.Value;
            }
            return null;
        }

        public void SetCustomProperty(string propertyname, string value)
        {
            if (!this.CustomProperties.ContainsKey(propertyname))
                this.CustomProperties.Add(propertyname, value);
            else
                this.CustomProperties[propertyname] = value;
        }


        private static string GetCustomProperty(IList<CustomProperty> prop, string propertyname)
        {
            if (prop == null)
            {
                return null;
            }

            for (int i = 0; i < prop.Count; i++)
            {
                if (prop[i].Name == propertyname)
                {
                    return prop[i].Value.Value;
                }
            }

            return null;
        }

        private static void SetCustomProperty(IList<CustomProperty> prop, string propertyname, string value)
        {
            bool found = false;
            for (int i = 0; i < prop.Count; i++)
            {
                if (prop[i].Name == propertyname)
                {
                    prop[i].Value = value;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                CustomProperty property = new CustomProperty();
                property.Name = propertyname;
                property.Value = value;
                prop.Add(property);
            }
        }

        public override void Draw(Graphics gr, ReportItemDrawParams dp)
        {
            if (gr == null)
            {
                throw new ArgumentNullException("gr");
            }

            int pixelWidth = (int)Math.Round(Width);
            int pixelHeight = (int)Math.Round(Height);
            if (this.GetCustomProperty("poly:Proportional") == bool.TrueString)
            {
                if (pixelWidth > pixelHeight)
                {
                    pixelWidth = pixelHeight;
                }
                else
                {
                    pixelHeight = pixelWidth;
                }
            }

            int alpha;
            if (this.Translucency == "Transparent")
            {
                alpha = 32;
            }
            else
            {
                if (this.Translucency == "Translucent")
                {
                    alpha = 128;
                }
                else
                {
                    alpha = 255;
                }
            }

            Color color = Color.FromArgb(alpha, Style.Color.Value.Color);
            Color borderColor = Style.Border.Color.Value.Color;
            Pen borderPen = new Pen(borderColor);
            SolidBrush colorBrush = new SolidBrush(color);
            SolidBrush backgroundColorBrush = new SolidBrush(Style.BackgroundColor.Value.Color);
            gr.FillRectangle(backgroundColorBrush, 0, 0, pixelWidth, pixelHeight);
            gr.FillRectangle(colorBrush, 3 * pixelWidth / 8, 3 * pixelHeight / 8, pixelWidth / 2, pixelHeight / 2);
            gr.DrawRectangle(borderPen, 3 * pixelWidth / 8, 3 * pixelHeight / 8, pixelWidth / 2, pixelHeight / 2);
            Point[] points = new Point[3];
            points[0] = new Point(3 * pixelWidth / 8, pixelHeight / 8);
            points[1] = new Point(pixelWidth / 8, 3 * pixelHeight / 5);
            points[2] = new Point(5 * pixelWidth / 8, 3 * pixelHeight / 5);
            gr.FillPolygon(colorBrush, points);
            gr.DrawPolygon(borderPen, points);
            borderPen.Dispose();
            colorBrush.Dispose();
            backgroundColorBrush.Dispose();
        }

        // initialize our CustomData structure with default values
        public override void InitializeNewComponent()
        {
            CustomData = new CustomData();
            CustomData.DataRowHierarchy = new DataHierarchy();

            // Shape grouping
            CustomData.DataRowHierarchy.DataMembers.Add(new DataMember());
            CustomData.DataRowHierarchy.DataMembers[0].Group = new Group();
            CustomData.DataRowHierarchy.DataMembers[0].Group.Name = Name + "_Shape";
            CustomData.DataRowHierarchy.DataMembers[0].Group.GroupExpressions.Add(new ReportExpression());

            // Point grouping
            CustomData.DataRowHierarchy.DataMembers[0].DataMembers.Add(new DataMember());
            CustomData.DataRowHierarchy.DataMembers[0].DataMembers[0].Group = new Group();
            CustomData.DataRowHierarchy.DataMembers[0].DataMembers[0].Group.Name = Name + "_Point";
            CustomData.DataRowHierarchy.DataMembers[0].DataMembers[0].Group.GroupExpressions.Add(new ReportExpression());

            // Static column
            CustomData.DataColumnHierarchy = new DataHierarchy();
            CustomData.DataColumnHierarchy.DataMembers.Add(new DataMember());

            // Points
            CustomData.DataRows.Add(new DataRow());
            CustomData.DataRows[0].Add(new DataCell());
            CustomData.DataRows[0][0].Add(NewDataValue("X", ""));
            CustomData.DataRows[0][0].Add(NewDataValue("Y", ""));
        }

        private static string GetGroupLabel(Group group)
        {
            if (group.GroupExpressions != null && group.GroupExpressions.Count > 0)
            {
                return group.GroupExpressions[0].Value;
            }

            return null;
        }

        private static string GetDataValue(IList<DataValue> cell, string name)
        {
            foreach (DataValue value in cell)
            {
                if (value.Name == name)
                {
                    return value.Value.Value;
                }
            }

            return null;
        }

        private static void SetDataValue(IList<DataValue> cell, string name, string expression)
        {
            foreach (DataValue value in cell)
            {
                if (value.Name == name)
                {
                    value.Value = expression;
                    return;
                }
            }

            DataValue datavalue = NewDataValue(name, expression);
            cell.Add(datavalue);
        }

        // method to handle the context menu designer verb
        private void OnProportionalScaling(object sender, EventArgs e)
        {
            bool proportional = !(this.GetCustomProperty("poly:Proportional") == bool.TrueString);
            this.verbs[0].Checked = proportional;
            this.SetCustomProperty("poly:Proportional", proportional.ToString());
            this.ChangeService().OnComponentChanged(this, null, null, null);
            Invalidate();
        }

        private static DataValue NewDataValue(string name, string value)
        {
            DataValue dv = new DataValue();
            dv.Name = name;
            dv.Value = value;
            return dv;
        }
    }
}
