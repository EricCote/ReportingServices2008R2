#region "Copyright © Microsoft Corporation. All rights reserved."

/*=============================================================================
  File:      EditItem.cs

  Summary:  Demonstrates a WinForm that you can use to edit items in
         the report server database.

---------------------------------------------------------------------
  This file is part of Microsoft SQL Server Code Samples.
  
  Copyright (C) Microsoft Corporation.  All rights reserved.

 This source code is intended only as a supplement to Microsoft
 Development Tools and/or on-line documentation.  See these other
 materials for detailed information regarding Microsoft code samples.

 THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
 KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 PARTICULAR PURPOSE.
=============================================================================*/

#endregion

#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#endregion

namespace Microsoft.Samples.ReportingServices.RSExplorer
{
    partial class EditItem : Form
    {
        // OnEdit Event for window integration
        // Note: In a production application use sender/EventArgs signature format      
        public delegate void EditEventHandler(string name, string desc);
        public event EditEventHandler OnEdit;
        
        public EditItem()
        {
            InitializeComponent();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.nameTextBox.Text = "";
            this.descriptionTextBox.Text = "";
            this.Hide();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void updateButton_Click(object sender, System.EventArgs e)
        {
            this.OnEdit(nameTextBox.Text, descriptionTextBox.Text);
            this.nameTextBox.Text = "";
            this.descriptionTextBox.Text = "";
            this.Hide();
        }
    }
}