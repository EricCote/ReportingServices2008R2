#region "Copyright (C) Microsoft Corporation. All rights reserved."

/*=====================================================================
  File:      ConnectToServerDialog.cs

  Summary:  Demonstrates a WinForm which helps connecting to an RS
            Instance through the RS URL entered

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
=====================================================================*/

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.ReportingServices.ReportService2010;

#endregion

namespace RSPermissionsCS
{
    public partial class ConnectToServerDialog : Form
    {

        #region Private Variables
        string m_rsInstanceURL;
        #endregion

        #region Constructors
        public ConnectToServerDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        //The button click event which passes over the RS Instance URL to the Permissions Winform
        private void reportserverurlButton_Click(object sender, EventArgs e)
        {
            if (reportserverurlTextbox.Text == "")
            {
                MessageBox.Show("Enter a valid Report server URL", "!Error");
                return;
            }
            m_rsInstanceURL = reportserverurlTextbox.Text;

            this.Close();
        }

        #endregion

        #region Properties
        public string RSInstanceUrl
        {
            get
            {
                return m_rsInstanceURL;
            }
        }
        #endregion

    }
}
