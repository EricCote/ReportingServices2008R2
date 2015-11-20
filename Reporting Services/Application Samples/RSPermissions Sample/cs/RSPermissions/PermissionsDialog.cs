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


# region Using Directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer;
using Microsoft.SqlServer.ReportingServices.ReportService2010;

# endregion

namespace RSPermissionsCS
{
    public partial class PermissionsDialog : Form
    {

        #region Constants

       const string m_wsdl = "/ReportService2010.asmx";
        
        #endregion

        #region "Private Variables"

        private ReportingService2010 m_rs;
        private string m_soapUrl;
        String m_completefullPath;
        String m_path;
        string m_selectedItemPath = "";
        
        #endregion

        #region Properties

        //Gets are sets the Report Server URL
        public string Path
        {
            get
            {
                return m_path;
            }

            set
            {
                m_path = value;
            }
        }

        #endregion

        #region Constructor
        
        public PermissionsDialog()
        {
            //Calling the Windows Designer
            InitializeComponent();

            //Calling a Win form that connects to a ReportServer Instance
            callConnect();
        }

        #endregion

        #region Miscellaneous Functions

        //The Function is called to connect to the specific RS instance
        private void callConnect()
        {
            //The winform ConnectToServerDialog gets the RS instance URL we need to connect
            ConnectToServerDialog RsInstance = new ConnectToServerDialog();
            RsInstance.ShowDialog();

            //This method call actually connect to the RS instance
            InvokeRSInstance(RsInstance.RSInstanceUrl);
        }

        //The method used to connect to an RS instance specified in the strReportserverurl
        private void InvokeRSInstance(String strReportserverurl)
        {
            try
            {
                if (strReportserverurl == string.Empty || strReportserverurl == null)
                {
                    return;
                }
                m_path = strReportserverurl;
                m_rs = new ReportingService2010();
                m_rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
                m_soapUrl = m_path + m_wsdl;
                m_rs.Url = m_soapUrl;
                

                //This method loads the Catalog contents to the Treeview
                LoadObjectExplorer();

                lvChildren.Columns.Clear();
                lvChildren.Columns.Add("Name");

                lblMessage.Text = "Report Server instance : " + strReportserverurl.ToString();
                lblMessage.TextAlign = ContentAlignment.BottomCenter;
            }
            //Catch the invalid URI format exception
            catch (UriFormatException uriEx)
            {
                MessageBox.Show(uriEx.Message.ToString() + " Please check the Reportserver URL");
            }

            //Catch the report server connectivity exception
            catch (System.Net.WebException webEx)
            {
                MessageBox.Show(webEx.Message.ToString());
            }

        }

        #endregion

        #region How To : Connect to Reporting Services Instance

        //// The below stated functions connect to the Report Server specified
        //private void ConnectToServer()
        //{
        //    string serverPath = m_path;
        //    this.Connect(serverPath);
        //}

        //public void Connect(string url)
        //{
        //    try
        //    {
        //        // Create an instance of the Web service proxy and set the Url property
        //        m_rs = new ReportingService2005();
        //        m_rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
        //        m_soapUrl = url + m_wsdl;
        //        m_rs.Url = m_soapUrl;
        //    }
        //    catch (UriFormatException ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString() + " Please check the Reportserver URL");
        //    }
        //}

        #endregion

        #region How To : Load Catalog Items in a Tree view
        

        //Load the Treeview control with the Report Items
        private void LoadObjectExplorer()
        {
            try
            {
                tvReportitems.Nodes.Clear();
                //Defining the images for the various report items.
                tvReportitems.ImageList = smallImageList;
               
                //Adding a root node
                TreeNode rootNode = new TreeNode("Home");
                rootNode.Name = "Home";
                rootNode.ImageIndex = 1;
                rootNode.SelectedImageIndex = 1;
                tvReportitems.Nodes.Add(rootNode);

                //This routine builds the treeview for the report items as per the hierarchy
                buildTreeview(rootNode, "/");
            }
            catch(Exception ex)
            {
                tvReportitems.Nodes.Clear();
                lvChildren.Items.Clear();
                lblReportItemDetails.Text = "";
                MessageBox.Show(ex.Message.ToString(), "Connect To Server");

            }
     }


        //Builds the tree view for the Report Items
        private void buildTreeview(TreeNode rootNode, string strPath)
        {
                CatalogItem[] items;
                items = listChildItems(strPath);

                if (items != null)
                {
                    //Recursively calling the nodes to add them to the Treeview
                    foreach (CatalogItem ci in items)
                    {
                        TreeNode childNode = new TreeNode(ci.Name.ToString());
                        childNode.ImageIndex = GetItemTypeImage(ci.TypeName);
                        childNode.SelectedImageIndex = GetItemTypeImage(ci.TypeName);
                        rootNode.Nodes.Add(childNode);

                        //Recursive call for building tree view
                        buildTreeview(childNode, ci.Path.ToString());
                    }

                }
        }


        #endregion
        
        #region How To : Return roles in a specified instance of Reporting Services
        
        ////Returns Roles specified in the ReportServer DB
        //public Role[] ReturnRoles()
        //{
        //    Role[] roles = new Role[1];
        //    roles = m_rs.ListRoles(SecurityScopeEnum.Catalog);
        //    return roles;
        //}

        #endregion

        #region How To : Return policies in for a specified report item
        

        //// Returns Policies specified for a path in the RS Database
        //public Policy[] ReturnPolicy(string ItemPath)
        //{
        //    Policy[] pols = new Policy[1];
        //    Boolean val = true;
        //    pols = m_rs.GetPolicies(ItemPath, out val);

        //    return pols;
        //}

        #endregion

        #region How To : Get child report items under a specified path
        
        // Get a list of Child Items
        private CatalogItem[] listChildItems(string itemPath)
        {
            CatalogItem[] items = null;
            try
            {
                items = m_rs.ListChildren(itemPath, false);
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {

                //Ignoring the exception, The ListChildren when executed on 
                //the root level Report items will throw an exception.
            }
            return items;
        }

        #endregion
        
        #region How To : Get Images for correpsonding ReportServer Items
        
        //returns the image index for the corresponding Items.
        int GetItemTypeImage(string itemType)
        {
            switch (itemType)
            {
                case "DataSource":
                    return 0;

                case "Folder":
                    return 1;

                case "LinkedReport":
                    return 2;

                case "Model":
                    return 3;

                case "Report":
                    return 4;

                case "Resource":
                    return 5;

                default:
                    return 5;

            }
        }

        #endregion
        
        #region How To : Load Catalog Items in a Tree view


        //Based on the path, the child items would be displayed in the Listview control
        private void DisplayChildren(String path)
        {
            CatalogItem[] childrenItems;
                   
            lblReportItemDetails.Text = "The folders under the path : " + path.ToString();
            lvChildren.Items.Clear();

            //Get the list of children
            childrenItems = listChildItems(path);
            if (childrenItems != null)
            {
                            //Iterate through list of children, and add those items to the Listview control
                            for (int i = 0; i < childrenItems.Length; i++)
                            {
                                CatalogItem item = new CatalogItem();
                                item = childrenItems[i];

                                String childPath = item.Path.Substring(1);
                                String[] pathNodes = childPath.Split(new char[] { '/' });

                                lvChildren.SmallImageList = smallImageList;

                                ListViewItem childItem = new ListViewItem();
                                childItem.ImageIndex = GetItemTypeImage(item.TypeName);
                                childItem.Text = item.Name;

                                if (path == "/")
                                {
                                    childItem.ToolTipText = path + item.Name;
                                }
                                else
                                {
                                    childItem.ToolTipText = path + "/" + item.Name;
                                }

                                lvChildren.Items.Add(childItem);

                            }
                        }
                
        }

        #endregion

        #region How To : Render a report in a ReportViewer Win Form control

        //This method will render the report in the ReportViewer Winform
        private void RenderReport(string path)
        {
                string url = m_path + "?" + path;
                ReportViewerDialog viewer = new ReportViewerDialog(url);
                viewer.ReportName = path;
                viewer.Show();
        }

        #endregion

        #region Event Handlers

        //This event is invoked once we select an reportserver item in the treeview
        //The event will call Display children method to display the child items in 
        //the listview for the selected item in treeview
        private void tvReportitems_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (e.Node.FullPath == "Home")
            {
                m_completefullPath = "/";
            }
            else
            {
                String fullPath = e.Node.FullPath.Substring(4);
                m_completefullPath = fullPath;
            }
            //Method to display child items in the listview
            DisplayChildren(m_completefullPath);
            m_selectedItemPath = m_completefullPath;
          
        }

        //The event is invoked if we select Permissions options from the Context Menu, either in treeview or in listview
        private void mnuUserPermissions_Click(object sender, EventArgs e)
        {
            ItemPropertiesDialog itmProp;
            string itemPath;

            //Based on the selection from either Treeview or Listview
            if (lvChildren.SelectedItems.Count > 0)
            {
                itemPath = lvChildren.SelectedItems[0].ToolTipText;
                itmProp = new ItemPropertiesDialog(m_path, itemPath);
            }
            else
            {
                itmProp = new ItemPropertiesDialog(m_path, m_selectedItemPath);
            }

            itmProp.ShowDialog();

        }
        
        //This event is invoked when we click the right button over the mouse on any report item in the treeview control        
        private void tvReportitems_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnuTextFile.Show(tvReportitems, new Point(e.X,e.Y));
            }
        }


        //This event is invoked when we click the right button over the mouse on any report item in the Listview control
        private void lvChildren_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnuTextFile.Show(lvChildren, new Point(e.X, e.Y));
            }
        }


        //This event is invoked when we double click on any of the reportserver item in the listview control
        private void lvChildren_DoubleClick(object sender, EventArgs e)
        {
            //Gets the selected item in the listview control
            ListViewItem selItem = (ListViewItem)lvChildren.SelectedItems[0];

            if (selItem.ImageIndex == 1)  // If the item is a folder, display children
            {
                DisplayChildren(selItem.ToolTipText);
            }
            else if (selItem.ImageIndex == 4) //Calls the Render report method if we double click on a report item.
            {
                RenderReport(selItem.ToolTipText);
            }
        }

      
        //This event is invoked once we double click on any ReportServer Item in the treeview control
        private void tvReportitems_DoubleClick(object sender, EventArgs e)
        {
            TreeNode tr = tvReportitems.SelectedNode;

            if (tr.SelectedImageIndex == 4)
            {
                //If the selected item is Report, then call the render method.
                RenderReport(m_selectedItemPath);
            }
        }


        //This event is invoked when we hit on the Connect button
        //This invokes the ConnectToServer winform, and helps connect to a ReportServer instance.
        private void btnConnect_Click(object sender, EventArgs e)
        {
            //Creates an instance of the ConnectToServer winform and opens the Dialog
            ConnectToServerDialog RsInstance = new ConnectToServerDialog();
            RsInstance.ShowDialog();

            //Connects to the ReportServer instance specified through URL, and calls all the corresponding events
            //to display the report items of the corresponding RS instance.
            InvokeRSInstance(RsInstance.RSInstanceUrl);

        }

        //This event brings up the permissions and the users added to the selected item.
        private void btnProperties_Click(object sender, EventArgs e)
        {
            ItemPropertiesDialog itmProp;
            string itemPath;

            if (m_selectedItemPath == "") return;
            
            //Based on the selection from either Treeview or Listview
            if (lvChildren.SelectedItems.Count > 0)
            {
                itemPath = lvChildren.SelectedItems[0].ToolTipText;
                itmProp = new ItemPropertiesDialog(m_path, itemPath);
            }
            else 
            {
                itmProp = new ItemPropertiesDialog(m_path, m_selectedItemPath);
            }

            itmProp.ShowDialog();
        }
       
         
        #endregion
       
    }
}
