#region "Copyright © Microsoft Corporation. All rights reserved."

/*=============================================================================
  File:      AddFolder.cs

  Summary:  Demonstrates a WinForm that you can use to add a folder to
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
using System.Globalization;

#endregion

namespace Microsoft.Samples.ReportingServices.RSExplorer
{
    partial class AddFolder : Form
    {
        // OnAdd Event for window integration
        // Note: In a production application use sender/EventArgs signature format
        public delegate void AddEventHandler(string name, string desc);
        public event AddEventHandler OnAdd;
        
        public AddFolder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method to handle when a user clicks the Add button in dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void addButton_Click(object sender, System.EventArgs e)
        {
            this.OnAdd(nameTextBox.Text, descriptionTextBox.Text);
            //Restore
			this.nameTextBox.Text = string.Format(CultureInfo.InvariantCulture,
					Properties.Resources.NewFolder);
            this.descriptionTextBox.Text = "";
            this.Hide();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            // When the dialog box is closed via the cancel button, set the default values.
            // The dialog remains in memory for the next use
			this.nameTextBox.Text = string.Format(CultureInfo.InvariantCulture,
					Properties.Resources.NewFolder);
            this.descriptionTextBox.Text = "";
            this.Hide();
        }

        public TextBox NameTextBox
        {
            get
            {
                return this.nameTextBox;
            }
        }
    }
}