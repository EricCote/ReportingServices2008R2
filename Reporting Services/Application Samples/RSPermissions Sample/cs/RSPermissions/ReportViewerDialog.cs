#region "Copyright (C) Microsoft Corporation. All rights reserved."

/*=====================================================================
  File:      Permissions.cs

  Summary:  Demonstrates a WinForm that you can use to view and 
            navigate items in a report server database and set  
            permissions for the users. This form is the startup  
            form for the RSPermissionsCS sample application.

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

#endregion

namespace RSPermissionsCS
{
    public partial class ReportViewerDialog : Form
    {

        #region Private Variables

        private string m_url;

        #endregion

        #region Constructors

        public ReportViewerDialog()
        {
            InitializeComponent();
        }

        public ReportViewerDialog(string reportUrl)
        {
            InitializeComponent();
            this.getURL = reportUrl;
        }

        #endregion

        #region Properties

        private string getURL
        {
            set
            {
                m_url = value;
            }
        }

        public string ReportName
        {
            set
            {
                this.Text = value;
            }
            get
            {
                return this.Text;
            }
        }

        #endregion

        #region Events

        //Loads the report in the Web browser control
        private void ReportViewer_Load(object sender, EventArgs e)
        {
            rptWebbrowser.Url = new Uri(m_url);
        }

        #endregion

    }
}
