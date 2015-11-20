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
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.ReportDesigner;
    using Microsoft.ReportDesigner.Design;
    using Microsoft.ReportingServices.Interfaces;
    using Rdl = Microsoft.ReportingServices.RdlObjectModel;

    public partial class PolygonProperties : Form
    {
        private PolygonsDesigner designerComponent;
        private Rdl.DataSet reportDataSet;
        private String oldComboValue;
        private bool launchEditor;

        public PolygonProperties()
        {
            InitializeComponent();
        }

        [System.CLSCompliant(false)]
        public PolygonsDesigner DesignerComponent
        {
            get
            {
                return this.designerComponent;
            }

            set
            {
                this.designerComponent = value;
            }
        }

        private void PolygonProperties_Load(object sender, EventArgs e)
        {
            this.DataSetName.Text = this.designerComponent.DataSetName;
            IList<Rdl.DataSet> datasets = this.designerComponent.Report.DataSets;
            for (int i = 0; i < datasets.Count; i++)
            {
                this.DataSetName.Items.Add(datasets[i].Name);
            }
            
            this.UpdateDataSet();
            this.ShapeColor.Text = this.designerComponent.ShapeColor;
            ProportionalScaling.Checked = (
                this.designerComponent.GetCustomProperty("poly:Proportional") == bool.TrueString);
            Translucency.Text = this.designerComponent.Translucency;
            this.MinX.Text = this.designerComponent.GetCustomProperty("poly:MinX");
            this.MaxX.Text = this.designerComponent.GetCustomProperty("poly:MaxX");
            this.MinY.Text = this.designerComponent.GetCustomProperty("poly:MinY");
            this.MaxY.Text = this.designerComponent.GetCustomProperty("poly:MaxY");
            this.PointID.Text = this.designerComponent.PointExpression;
            this.ShapeID.Text = this.designerComponent.ShapeExpression;
            this.XValue.Text = this.designerComponent.XExpression;
            this.YValue.Text = this.designerComponent.YExpression;
            this.Hyperlink.Text = this.designerComponent.ShapeHyperlink;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.designerComponent.DataSetName = this.DataSetName.Text;
            this.designerComponent.ShapeColor = this.ShapeColor.Text;
            this.designerComponent.SetCustomProperty(
                "poly:Proportional", 
                ProportionalScaling.Checked ? bool.TrueString : bool.FalseString);
            this.designerComponent.Translucency = Translucency.Text;
            this.designerComponent.SetCustomProperty("poly:MinX", MinX.Text);
            this.designerComponent.SetCustomProperty("poly:MaxX", MaxX.Text);
            this.designerComponent.SetCustomProperty("poly:MinY", MinY.Text);
            this.designerComponent.SetCustomProperty("poly:MaxY", MaxY.Text);
            this.designerComponent.PointExpression = PointID.Text;
            this.designerComponent.ShapeExpression = ShapeID.Text;
            this.designerComponent.XExpression = XValue.Text;
            this.designerComponent.YExpression = YValue.Text;
            this.designerComponent.ShapeHyperlink = this.Hyperlink.Text;
        }

        private void DataSetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateDataSet();
        }

        private void UpdateDataSet()
        {
            this.reportDataSet = null;
            IList<Rdl.DataSet> datasets = this.designerComponent.Report.DataSets;
            for (int i = 0; i < datasets.Count; i++)
            {
                if (datasets[i].Name == this.DataSetName.Text)
                {
                    this.reportDataSet = datasets[i];
                    break;
                }
            }

            this.PopulateFieldSelector(this.ShapeID);
            this.PopulateFieldSelector(this.ShapeColor);
            this.PopulateFieldSelector(this.Hyperlink);
            this.PopulateFieldSelector(this.PointID);
            this.PopulateFieldSelector(this.XValue);
            this.PopulateFieldSelector(this.YValue);
        }

        private void PopulateFieldSelector(ComboBox combo)
        {
            combo.Items.Clear();
            combo.Items.Add("<Expression...>");
            if (this.reportDataSet == null)
            {
                return;
            }

            for (int i = 0; i < this.reportDataSet.Fields.Count; i++)
            {
                combo.Items.Add("=Fields!" + this.reportDataSet.Fields[i].Name + ".Value");
            }
        }

        private void EditableCombo_DropDown(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            if (combo.SelectedIndex == 0)
            {
                string currentValue = combo.Text;
                combo.SelectedIndex = -1;
                combo.Items[0] = "<Expression...>";
                combo.Text = currentValue;
            }
            else
            {
                combo.Items[0] = "<Expression...>";
            }
        }

        private void EditableCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            this.oldComboValue = combo.Text;
            this.launchEditor = true;
        }

        private void EditableCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            if (combo.SelectedIndex == 0 && this.launchEditor)
            {
                this.launchEditor = false;

                // if <Expression...> is selected in the combo box,
                // invoke the report builder expression editor
                ExpressionEditor editor = new ExpressionEditor();
                string newValue;
                newValue = (string)editor.EditValue(null, this.designerComponent.Site, new Rdl.ReportExpression(this.oldComboValue));
                combo.Items[0] = newValue;
            }
        }
    }
}