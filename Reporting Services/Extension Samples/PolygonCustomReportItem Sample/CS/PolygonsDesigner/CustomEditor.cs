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
    using System.Xml;
    using System.Xml.Xsl;
    using System.Xml.XPath;

    #endregion
    // show our custom properties editor window
    internal sealed class CustomEditor : ComponentEditor
    {
        public override bool EditComponent(ITypeDescriptorContext context, object component)
        {
            PolygonsDesigner designer = (PolygonsDesigner)component;
            PolygonProperties dialog = new PolygonProperties();
            dialog.DesignerComponent = designer;
            DialogResult result = dialog.ShowDialog();
            dialog.Dispose();
            if (result == DialogResult.OK)
            {
                designer.Invalidate();
                designer.ChangeService().OnComponentChanged(designer, null, null, null);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
