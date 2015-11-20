#region "Copyright (C) Microsoft Corporation. All rights reserved."

/*=====================================================================
  File:      ItemProperties.cs

  Summary:  Demonstrates a WinForm which helps giving permissions
            to an added user on the selected report item

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
    public partial class ItemPropertiesDialog : Form
    {

        # region "Variable"
        
            string m_strPath;
            private ReportingService2010 m_rs;
            const string m_wsdl = "/ReportService2010.asmx";
            private string m_soapUrl;
            Policy[] m_reportPolicy;
            Role[] m_rsRoles;

        #endregion

        #region Constructors

        public ItemPropertiesDialog()
        {
            InitializeComponent();
        }

        public ItemPropertiesDialog(String rsUrl, String path)
        {
            InitializeComponent();
            GetExistingPermissions(rsUrl, path);
        }

        #endregion

        #region Miscellaneous Functions

        //This method connects to the RS instance based on the RS instance url, and get the permissions for the
        //specified path.
        public void GetExistingPermissions(String rsUrl, String path)
        {
            try
            {
                if (path == "Home")
                {
                    m_strPath = "/";
                }
                else
                {
                    m_strPath = path;
                }

                //Connects the RS Instance
                m_rs = new ReportingService2010();
                m_rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
                m_soapUrl = rsUrl + m_wsdl;
                m_rs.Url = m_soapUrl;

                //Add the columns based on the Roles defined
                AddColumnsToGrid();
                //Displays the permissions for the Users
                ShowPermissions();

                this.Text = "Permissions on the Report item :  " + m_strPath;
            }
            catch (UriFormatException ex)
            {
                MessageBox.Show("Not connected to any Reportserver instance.");
                this.Close();
            }
        }



        //This method displays the Roles defined for the Users
        public void ShowPermissions()
        {
            if (m_strPath != "")
            {
                m_reportPolicy = ReturnPolicy(m_strPath);


                if (m_reportPolicy.Length != 0)
                {

                    foreach (Policy pol in m_reportPolicy)
                    {

                        DataGridViewRow dr = new DataGridViewRow();
                        userpermissionsGridview.Rows.Add(dr);

                        userpermissionsGridview.Rows[userpermissionsGridview.RowCount - 1].Cells[0].Value = pol.GroupUserName;

                        foreach (Role currentRole in pol.Roles)
                        {
                            for (int i = 0; i < m_rsRoles.Length; i++)
                            {
                                if (currentRole.Name == userpermissionsGridview.Columns[i + 1].Name)
                                {
                                    userpermissionsGridview.Rows[userpermissionsGridview.RowCount - 1].Cells[i + 1].Value = true;
                                }
                            }
                        }

                    }

                }
            }
        }

        //Returns the Policies defined on the Item path
        public Policy[] ReturnPolicy(string itemPath)
        {
            Policy[] pols = new Policy[1];
            Boolean val = true;
            pols = m_rs.GetPolicies(itemPath, out val);

            return pols;
        }


        //Add the columns based the roles defined in the RS instance
        private void AddColumnsToGrid()
        {
            m_rsRoles = ReturnRoles();
            
            //Adding Columns
            DataGridViewColumn userRole = new DataGridViewTextBoxColumn();
            userRole.Name = "Group/User";
            userpermissionsGridview.Columns.Add(userRole);

            foreach (Role currentRole in m_rsRoles)
            {
                DataGridViewColumn roleName = new DataGridViewCheckBoxColumn();
                roleName.Name = currentRole.Name;
                userpermissionsGridview.Columns.Add(roleName);
            }

        }


        //Returns Roles specified in the ReportServer DB
        public Role[] ReturnRoles()
        {
            Role[] roles = new Role[1];
            roles = m_rs.ListRoles("Catalog",null);
            return roles;
        }

        #endregion

        #region Events

        private void removeuserButton_Click(object sender, EventArgs e)
        {
            Int32 rowToDelete = this.userpermissionsGridview.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (rowToDelete > -1)
            {
                this.userpermissionsGridview.Rows.RemoveAt(rowToDelete);
            }
        }

        private void adduserButton_Click(object sender, EventArgs e)
        {
            AddUserDialog addUser = new AddUserDialog();
            addUser.ShowDialog();

            String newUser;

            DialogResult dr;
            dr = addUser.DialogResult;


            if (dr.ToString() == "OK")
            {
                newUser = addUser.addUserProperty;
                DataGridViewRow newRow = new DataGridViewRow();
                userpermissionsGridview.Rows.Add(newRow);
                userpermissionsGridview.Rows[userpermissionsGridview.RowCount - 1].Cells[0].Value = newUser;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveUsers();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        #endregion

        #region How To : Save Users and the Roles
        //Event to Save Users and the corresponding roles
        //There is no way we can append the current user to the existing users list, we have to save them all once again
        private void SaveUsers()
        {
          
                Policy[] eachPolicy = new Policy[userpermissionsGridview.Rows.Count];
                int i = 0;
                int j = 0;


                foreach (DataGridViewRow dgvr in userpermissionsGridview.Rows)
                {
                    Policy currentPolicy = new Policy();
                    currentPolicy.GroupUserName = dgvr.Cells[0].Value.ToString();


                    j = 0;
                    foreach (DataGridViewCell dgvc in dgvr.Cells)
                    {
                        if (dgvc.FormattedValue.ToString() == "True")
                        {
                            j++;
                        }
                    }

                    Role[] roles = new Role[j];
                    j = 0;
                    foreach (DataGridViewCell dgvc in dgvr.Cells)
                    {
                        Role currentRole = new Role();
                        if (dgvc.FormattedValue.ToString() == "True")
                        {
                            currentRole.Name = dgvc.OwningColumn.Name;
                            roles[j] = currentRole;
                            j++;
                        }
                    }
                   
                    currentPolicy.Roles = roles;
                    eachPolicy[i] = currentPolicy;
                    i++;
                }


            //This method call with save all the users and the corresponding roles to the current report item.
                m_rs.SetPolicies(m_strPath, eachPolicy);

        }

        #endregion

    }
}
