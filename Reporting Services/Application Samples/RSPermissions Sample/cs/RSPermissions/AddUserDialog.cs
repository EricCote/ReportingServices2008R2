#region "Copyright (C) Microsoft Corporation. All rights reserved."

/*=====================================================================
  File:      AddUser.cs

  Summary:  Demonstrates a WinForm that you can add a user roles to the
            selected report item

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
    public partial class AddUserDialog : Form
    {

        #region Constructors

        public AddUserDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Private variables
        String m_propUser;
        #endregion

        #region Properties
        public string addUserProperty
        {
            get
            {
                return m_propUser;
            }
        }
        #endregion

        #region Events

        //The button click event which get the Username we want to give permissions on the report items
        private void okButton_Click(object sender, EventArgs e)
        {
            if (userTextbox.Text == "")
            {
                MessageBox.Show("Enter a security group/user name");
            }
            else
            {
                m_propUser = userTextbox.Text;
                this.Close();
            }
        }

        // The button click event which closes the win form
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
