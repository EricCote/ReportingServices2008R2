#region "Copyright (C) Microsoft Corporation. All rights reserved."

/*=====================================================================
  File:      Explorer.cs

  Summary:  Demonstrates a WinForm that you can use to view and 
         navigate items in a report server database. This form is
         the startup form for the RSExplorer sample application.

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

#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using rs2010 = Microsoft.SqlServer.ReportingServices2010; 
using System.Runtime.InteropServices;
using Microsoft.SqlServer;
using Microsoft.SqlServer.MessageBox;
using System.Globalization;
#endregion

namespace Microsoft.Samples.ReportingServices.RSExplorer
{
    partial class Explorer : Form
    {
        #region Constants
        
        const string wsdl = "/ReportService2010.asmx";
        private const string Root = "/";
        private const int _MaxFileUploadSize = 4096;

        #endregion

        #region Private Member Variables

        private string soapUrl;
		private rs2010.ReportingService2010 rs;
        private string m_path;
        private string m_lastShortName;
        private ArrayList m_copyList;

        //Classes related to UI
        CopyProgress copyProgress;
        //Forms
        AddFolder addFolder = new AddFolder();
        EditItem editItem = new EditItem();

        #endregion

        public Explorer()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            // Event handlers for cross form communication
            addFolder.OnAdd += new AddFolder.AddEventHandler(this.CreateFolder);
            editItem.OnEdit += new EditItem.EditEventHandler(this.EditItem);
        }

        #region How To: Reporting Services
        #region How To: Connect to ReportingServices
        private void goButton_Click(object sender, System.EventArgs e)
        {
            this.ConnectToServer();
        }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void ConnectToServer()
        {
            // Init UI
            EnableButtons(true);
            this.Path = Root;
            string serverPath = serverPathTextbox.Text;
            try
            {
                // Connect to Reporting Services
                Cursor.Current = Cursors.WaitCursor;
                rs = new rs2010.ReportingService2010(); 
                // A production application would perform a complete check on the url path
                this.Connect(serverPath);
                // Display root items
                DisplayItems(Path);
            }
            catch (Exception ex)
            {
                EnableButtons(false);
                this.HandleGeneralException(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        public void Connect(string url)
        {
            // Create an instance of the Web service proxy and set the Url property
            rs = new rs2010.ReportingService2010();
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            soapUrl = url + wsdl;

            rs.Url = soapUrl;
        }

        #endregion

        #region How To: Display CatalogItems in a Listview

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void DisplayItems(string path)
        {
			rs2010.CatalogItem[] catalogItems = null;

            // Change UI state
            Cursor.Current = Cursors.WaitCursor;
            explorerListView.Items.Clear();
            upButton.Enabled = true;
            if (this.Path == "/")
                upButton.Enabled = false;

            // Call RS ListChildren
            catalogItems = rs.ListChildren(path, false);

            try
            {
                // Main part of method 
                if (catalogItems != null)
                {
					foreach (rs2010.CatalogItem ci in catalogItems)
                    {
                        // Create a ListView item containing a CatalogItem
                        CatalogListViewItem newItem = new CatalogListViewItem(ci);
                        newItem.ImageIndex = GetTypeIndex(newItem.Item.TypeName);
                        explorerListView.Items.Add(newItem);
                    }
                }
            }
            catch (Exception ex)
            {
                this.HandleGeneralException(ex);
            }
            finally
            {
                // Update and restore UI status 
                SetFormText();
                Cursor.Current = Cursors.Default;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public int GetTypeIndex(string TypeName)
        {
            int typeIndex= 0;
            switch (TypeName)
            {
				case "Folder":
                    typeIndex = 0;
                    break;
				case "Report":
                    typeIndex = 1;
                    break;
				case "Resource":
                    typeIndex = 2;
                    break;
				case "LinkedReport":
                    typeIndex = 3;
                    break;
				case "DataSource":
                    typeIndex = 4;
                    break;
            }
            return typeIndex;
        }

        #endregion

        #region How To: Get CatalogItem Properties from a path
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		private void FillPropertiesListview(rs2010.CatalogItem selItem)
        {
			rs2010.Property[] property = null;
            // Clear Listview
            propertiesListview.Items.Clear();

            try
            {
                // Properties parameter is null so all properties for the specified item are returned.
                property = rs.GetProperties(selItem.Path, null);

                // Display properties in a Listview 
				foreach (rs2010.Property prop in property)
                {
                    ListViewItem lstItem = propertiesListview.Items.Add(prop.Name);
                    lstItem.SubItems.Add(prop.Value);
                }
            }
            catch (Exception ex)
            {
                // Note: In a production application you would want to use a specific exception type 
                this.HandleGeneralException(ex);
            }
        }
        #endregion

        #region How To: Create a Folder with a description property

        /// <summary>
        /// Create a Folder in CatalogManager
        /// <seealso cref=""/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void CreateFolder(string name, string description)
        {
            try
            {
				rs2010.Property[] props = CreateDescriptionProperty(description);
                // See Create a Folder in CatalogManager
                rs.CreateFolder(name, this.Path, props);
            }
            catch (Exception ex)
            {
                this.HandleGeneralException(ex);
            }
            finally
            {
                // Restore UI
                DisplayItems(this.Path);
				addFolder.NameTextBox.Text = string.Format(CultureInfo.InvariantCulture,
					Properties.Resources.NewFolder);
                addFolder.Hide();
            }
        }

        /// <summary>
        /// Helper method for sample purposes only. A more robust application would create a general
        /// purpose property method.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private rs2010.Property[] CreateDescriptionProperty(string description)
        {
			rs2010.Property[] rsProperties = new rs2010.Property[1];
			rs2010.Property rsProperty = new rs2010.Property();
			rsProperty.Name = "Description";
            rsProperty.Value = description;
            rsProperties[0] = rsProperty;

            return rsProperties;
        }

        #endregion

        #region How To: Delete multiple items from a Listview
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void DeleteItems()
        {
            // Only execute method if there are selected items
            if (explorerListView.SelectedItems.Count > 0)
            {
                DialogResult answer =
				MessageBox.Show(string.Format(CultureInfo.InvariantCulture,
					Properties.Resources.DeleteItem),
                string.Format(CultureInfo.InvariantCulture,
					Properties.Resources.ConfirmDelete), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (answer == DialogResult.Yes)
                {
                    ArrayList selectedItems = new ArrayList();
                    Cursor.Current = Cursors.WaitCursor;

                    // Create ArrayList to pass to DeleteItems
                    foreach (ListViewItem item in explorerListView.SelectedItems)
                    {
                        selectedItems.Add(item.Text);
                    }
                    // Call DeleteItems
                    try
                    {
                        string itemPath;
                                            

                        // Call DeleteItem methods
                        foreach (string item in selectedItems)
                        {
                            itemPath = GetCleanPath(this.Path, item);
                            rs.DeleteItem(itemPath);
                        }

                    }
                    catch (Exception e)
                    {
                        HandleGeneralException(e);
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                    // Restore UI
                    DisplayItems(Path);
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public string GetCleanPath(string path, string item)
        {
            // If the item is in the root don't include the path.
            // This avoids double forward slash "//Pathname" problems
            string itemPath;
            if (path != Root)
            {
                itemPath = path + Root + item;
            }
            else
            {
                itemPath = Root + item;
            }

            return itemPath;
        }
        #endregion

        #region How To: Edit a CatalogItem
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public void EditItem(string name, string description)
        {
            string item, target;
            try
            {
                // Get a clean path to pass to EditItem
                item = GetCleanPath(this.Path, m_lastShortName);
                target = GetCleanPath(this.Path, name);

				rs2010.Property[] rsProperties = CreateDescriptionProperty(description);

                // Rename Item and Create new Properties based on old properties
               
                rs.MoveItem(item, target);
                rs.SetProperties(target, rsProperties);
                

            }
            catch (Exception ex)
            {
                HandleGeneralException(ex);
            }

            finally
            {
                // Restore UI
                DisplayItems(Path);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public void EditItem(string name)
		{
			string item, target;
			try
			{
				// Get a clean path to pass to EditItem
				item = GetCleanPath(this.Path, m_lastShortName);
				target = GetCleanPath(this.Path, name);

				// Rename Item and Create new Properties based on old properties
				
				rs.MoveItem(item, target);
				// Test code. 
				
			}
			catch (Exception ex)
			{
				HandleGeneralException(ex);
			}

			finally
			{
				// Restore UI
				DisplayItems(Path);
			}
		}
        #endregion

        #region How To: Copy and paste a report or resource

        #region Copy selected CatalogItems to an array
        private void copyMenuItem_Click(object sender, System.EventArgs e)
        {
            m_copyList = new ArrayList();

            if (explorerListView.SelectedItems.Count > 0)
            {
                // Add CatalogItems to ArrayList
                foreach (CatalogListViewItem clvi in explorerListView.SelectedItems)
                {
                    m_copyList.Add(clvi);
                }

                pasteMenuItem.Enabled = true;
                pasteCntxtMenuItem.Enabled = true;
            }
        }
        #endregion

        #region Paste CatalogItems within m_copyList array to new path
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void pasteMenuItem_Click(object sender, System.EventArgs e)
        {
            bool doSkip = false;
            // Do not execute any further if m_copyList array is zero length
            if (m_copyList.Count > 0)
            {
                this.ShowProgressDialog(true);
                try
                {
					// Loop copy ArrayList
                    foreach (CatalogListViewItem clvi in m_copyList)
                    {
                        // Test if Item is in Listview.Lists
                        doSkip = this.IsInList(clvi);
                        if (!doSkip)
                        {
                            // Copy datasource first
							if (clvi.Item.TypeName == "DataSource")
                            {
                                this.CopyDataSource(clvi.Item.Name, clvi.Item.Path, Path);
                            }

                                // Test if we need to copy a report or resource
							else if (clvi.Item.TypeName == "Report")
                            {
                                // Handling warning in CopyReport() method below
                                this.CopyReport(clvi.Item.Name, clvi.Item.Path, Path);
                            }
							else if (clvi.Item.TypeName == "Resource")
                            {
                                this.CopyResource(clvi.Item.Name, clvi.Item.Path, Path);
                            }
                            
                            // Refresh CopyProgress Form
                            copyProgress.Refresh();
                            copyProgress.itemNameLabel.Text = clvi.Item.Name;
                            copyProgress.ProgressBar.PerformStep();
                        }

                        else
                            doSkip = false;
                    }
                }

                catch (Exception ex)
                {
                    this.HandleGeneralException(ex);
                }

                finally
                {
                    this.ShowProgressDialog(false);
                }
            }
        }

        public void CopyResource(string name, string oldPath, string newPath)
        {
            byte[] resourceContents = null;
            rs2010.Warning[] warnings = null;
            
            resourceContents = this.rs.GetItemDefinition(oldPath);
            this.rs.CreateCatalogItem("Resource", name, newPath, true, resourceContents, null, out warnings);            
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		private rs2010.Warning[] CopyReport(string name, string oldPath, string newPath)
        {
			rs2010.Warning[] warning = null;
            try
            {
                // GetReportDefinition()
                // CreateReport
                byte[] reportDefinition = null;
                 rs2010.Warning[] warnings = null;

                 reportDefinition = this.rs.GetItemDefinition(oldPath);
                rs2010.CatalogItem report = rs.CreateCatalogItem("Report",name, newPath, true, reportDefinition, null,out warnings);

                if (warning != null && warning[0].ObjectType.ToString() == "DataSource")
                {
                    // Show warning details.
					MessageBox.Show(warning[0].Message + Properties.Resources.CopyDataSource, string.Format(CultureInfo.InvariantCulture,
					Properties.Resources.DataSourceWarning),
					MessageBoxButtons.OK, MessageBoxIcon.Information);
					string itemPath = GetCleanPath(this.Path, name);
					rs.DeleteItem(itemPath);
                }
            }
            catch (Exception ex)
            {
                this.HandleGeneralException(ex);
            }
            return warning;
        }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		private void CopyDataSource(string name, string oldPath, string newPath)
        {
            try
            {
				rs2010.DataSourceDefinition dsDefinition = null;

                dsDefinition = this.rs.GetDataSourceContents(oldPath);
                rs.CreateDataSource(name, newPath, false, dsDefinition, null);
            }
            catch (Exception ex)
            {
                this.HandleGeneralException(ex);
            }
        }


        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
        private bool IsInList(CatalogListViewItem clvi)
        {
            bool skip = false;
            DialogResult answer;

            foreach (ListViewItem lvi in explorerListView.Items)
            {

                if ((clvi.Item.Name == lvi.Text) & clvi.Item.TypeName != "Folder")
                {
					answer = MessageBox.Show(clvi.Item.Name, string.Format(CultureInfo.InvariantCulture,
					Properties.Resources.ItemExists),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (answer == DialogResult.Yes)
                        //Note: This sample code does not account for existing "Copy of" CatalogItems
                        // Test for "Copy of ..." CatalogItems within a production application
						clvi.Item.Name = string.Format(CultureInfo.InvariantCulture,
							Properties.Resources.CopyOf) + clvi.Item.Name;
                    else
                        skip = true;
                }
            }
            return skip;
        }

        private void ShowProgressDialog(bool visible)
        {
            if (visible)
            {
                copyProgress = new CopyProgress();
                copyProgress.ProgressBar.Minimum = 1;
                copyProgress.ProgressBar.Maximum = m_copyList.Count;
                copyProgress.ProgressBar.Value = 1;
                copyProgress.ProgressBar.Step = 1;

                Cursor.Current = Cursors.WaitCursor;

                copyProgress.Show();
                copyProgress.Refresh();

                Cursor.Current = Cursors.Default;
            }
            else
            {
                copyProgress.Hide();
                DisplayItems(Path);
            }
        }


        #endregion

        #region How To: Render a report in the ReportViewer WinForms control
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public void RenderReport(rs2010.CatalogItem report)
        {
            string currentPath;

            // If the path of the item is the root of the catalog
            // set path equal to /,
            if (this.Path == "/")
                currentPath = this.Path;
            else
                currentPath = this.Path + "/";

            // Build string for url access
            string url = serverPathTextbox.Text + "?" + currentPath + report.Name;

            try
            {
                ReportViewer viewer = new ReportViewer();
                viewer.Url = url;
                viewer.Show();
            }

            catch (Exception ex)
            {
                this.HandleGeneralException(ex);
            }

        }
        #endregion
        #endregion

        #region General Event Handlers

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            switch (toolBar1.Buttons.IndexOf(e.Button))
            {
                case 0: // New Folder button
                    addFolder.Show();
                    break;
                case 1: // Edit button   
                    this.ShowEditForm();
                    break;
                case 2: // Delete button
                    this.DeleteItems();
                    break;
                case 3: // Separator
                    break;
                case 4: // Up button
                    Path = GetParentPath(this.Path);
                    DisplayItems(Path);
                    break;
                case 5: // Refresh button
                    DisplayItems(Path);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Fill Properties Listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explorerListView_Click(object sender, System.EventArgs e)
        {
            this.RefreshProperties(true);
        }

        private void renameCntxtMenuItem_Click(object sender, System.EventArgs e)
        {
            this.ShowEditForm();
        }

        private void fileExitMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
        private void explorerListView_DoubleClick(object sender, System.EventArgs e)
        {
            CatalogListViewItem selItem = (CatalogListViewItem)explorerListView.SelectedItems[0];

            switch (selItem.Item.TypeName)
            {
                // If the item is a folder, display children
				case "Folder":
                    Path = GetFolderPath(selItem.Item);
                    DisplayItems(Path);
                    break;

                // If the item is a report, launch the Win32 viewer form
				case "Report":
                    RenderReport(selItem.Item);
                    break;

                // If the item is a resource, do nothing
				case "Resource":
                    break;

                // If the item is a linked report, render it to the viewer
				case "LinkedReport":
                    RenderReport(selItem.Item);
                    break;

                // If it is a data source, do nothing
				case "DataSource":
                    break;

                default:
                    MessageBox.Show(selItem.Text, string.Format(CultureInfo.InvariantCulture,
							Properties.Resources.UnexplainedError), MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    break;
            }
        }

        // Used to support vitual paths of folders.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private string GetFolderPath(rs2010.CatalogItem item)
        {
            string delimiter = "/";

            // Split the path string into the folder names as parts
            Regex rx = new Regex(delimiter);
            string[] pathParts = rx.Split(item.Path);

            // Check to see if the item has a virtual path and return the
            // virtual path for items that support it, like My Reports
            if (item.VirtualPath != null && pathParts[1] != "Users Folders")
                return item.VirtualPath;
            else
                return item.Path;
        }

        private void viewMenuItem_Click(object sender, System.EventArgs e)
        {
            if (sender == viewIconsMenuItem)
            {
                SetView("ICONS");
            }

            else if (sender == viewListMenuItem)
            {
                SetView("LIST");
            }

            else if (sender == viewDetailsMenuItem)
            {
                SetView("DETAILS");
            }
        }

        private void viewCntxtMenuItem_Click(object sender, System.EventArgs e)
        {
            if (sender == iconsCntxtMenuItem)
            {
                SetView("ICONS");
            }
            else if (sender == listCntxtMenuItem)
            {
                SetView("LIST");
            }
            else if (sender == detailsCntxtMenuItem)
            {
                SetView("DETAILS");
            }
        }

        private void viewRefreshMenuItem_Click(object sender, System.EventArgs e)
        {
			DisplayItems(Path);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
        private void aboutMenuItem_Click(object sender, System.EventArgs e)
        {
			MessageBox.Show(Application.ProductVersion, string.Format(CultureInfo.InvariantCulture,
					Properties.Resources.AboutRSExplorer), MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void fileNewFolderMenuItem_Click(object sender, System.EventArgs e)
        {
            addFolder.Show();
        }

        private void deleteMenuItem_Click(object sender, System.EventArgs e)
        {
            if (explorerListView.Focused)
                this.DeleteItems();
        }

        private void serverPathTextbox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.ConnectToServer();
        }

        private void explorerListView_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue == 46)
                this.DeleteItems();
        }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void explorerListView_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    defaultContextMenu.Show(explorerListView, new Point(e.X, e.Y));
                }
                catch (Exception) { }
            }
        }

        private void propertiesntxtMenuItem_Click(object sender, System.EventArgs e)
        {
            this.RefreshProperties(false);

        }

        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            this.RefreshProperties(false);
        }

        // Method supports single click editing of item names in the list view
        private void explorerListView_BeforeLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
        {
			this.refreshButton.Enabled = false;
			this.refreshCntxtMenuItem.Enabled = false;
			this.viewRefreshMenuItem.Enabled = false;

			// Need to store the last known name of an item as a member variable
            m_lastShortName = explorerListView.Items[e.Item].Text;
        }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void explorerListView_AfterLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
        {
			this.refreshButton.Enabled = true;
			this.refreshCntxtMenuItem.Enabled = true;
			this.viewRefreshMenuItem.Enabled = true;

			if (e.Label != null && e.Label != m_lastShortName)
            {
                Cursor.Current = Cursors.WaitCursor;

                try
                {
                    // Use the EditItem method, which ultimately calls the SOAP API
                    // MoveItem method to make the change.
					this.EditItem(e.Label);
                }

                catch (Exception ex)
                {
                    e.CancelEdit = true;
                    HandleGeneralException(ex);
                }

                finally
                {
                    DisplayItems(Path);
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void menuItemImport_Click(object sender, System.EventArgs e)
        {
            Stream reportStream;
            System.Byte[] reportDefinition;
            string[] parsedPath = null;

            if (openReportFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get Report Name and Report Extension
                string delimStr = @"\";
                char[] delimiter = delimStr.ToCharArray();
                parsedPath = openReportFileDialog.FileName.Split(delimiter);
                string reportName = parsedPath[parsedPath.Length - 1];
                string reportExt = reportName.Substring(reportName.Length - 3, 3);
                reportName = reportName.Substring(0, reportName.Length - 4);

                // Don't go any further if the stream is null or ext is not rdl.
                // In a production application you would want to test for an rdl file type
                if ((reportStream = openReportFileDialog.OpenFile()) != null && reportExt == "rdl")
                {
                    //Code to read the stream here.
                    try
                    {
                        rs2010.Warning[] warnings = null;
                        Cursor.Current = Cursors.WaitCursor;
                        reportDefinition = new Byte[reportStream.Length];
                        reportStream.Read(reportDefinition, 0, (int)reportStream.Length);
                        reportStream.Close();

                        rs.CreateCatalogItem("Report",reportName, this.Path, false, reportDefinition, null,out warnings);
                    }
                    catch (Exception ex)
                    {
						HandleGeneralException(ex);
                    }
                    finally
                    {
                        DisplayItems(Path);
                        Cursor.Current = Cursors.Default;
                    }
                }
                else
                {
					MessageBox.Show(string.Format(CultureInfo.InvariantCulture,
					Properties.Resources.NotFormat), string.Format(CultureInfo.InvariantCulture,
					Properties.Resources.GeneralError));
                }

            }
        }

        #endregion // Event Handlers

        #region General Methods

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void ShowEditForm()
        {
            if (explorerListView.SelectedItems.Count > 0)
            {
                // For the Edit dialog, capture the current name of the item
                string description = String.Empty;
				rs2010.CatalogItem item = ((CatalogListViewItem)explorerListView.SelectedItems[0]).Item;
                editItem.nameTextBox.Text = item.Name;
                m_lastShortName = item.Name;

                try
                {
                    // Retrieve the description of the item for the Edit dialog
                    description = GetProperty(item.Path, "Description");
                    editItem.descriptionTextBox.Text = description;
                    editItem.ShowDialog();
                }
                catch (Exception ex)
                {
                    HandleGeneralException(ex);
                }

                finally
                {
                    DisplayItems(Path);
                }
            }
        }

        public void HandleGeneralException(Exception ex)
        {
            /* Note: This error handling is for sample purposes only
            *  A production application would require more robust exception handling
            */
			ExceptionMessageBox emb = new ExceptionMessageBox(ex);
			emb.Show(this);

            Cursor.Current = Cursors.Default;
        }

        public void EnableButtons(bool enable)
        {
            upButton.Enabled = enable;
            newFolderButton.Enabled = enable;
            deleteButton.Enabled = deleteMenuItem.Enabled = enable;
            refreshButton.Enabled = viewRefreshMenuItem.Enabled = refreshCntxtMenuItem.Enabled = enable;
            editButton.Enabled = enable;
            fileNewFolderMenuItem.Enabled = enable;
            //Context Menus
            refreshCntxtMenuItem.Enabled = enable;
            copyCntxtMenuItem.Enabled = enable;
            
            deleteCntxtMenuItem.Enabled = enable;
            renameCntxtMenuItem.Enabled = enable;
            propertiesCntxtMenuItem.Enabled = enable;
            menuItemImport.Enabled = enable;
        }

        public void SetView(string viewType)
        {
            switch (viewType)
            {
                case "ICONS":
                    viewIconsMenuItem.Checked = iconsCntxtMenuItem.Checked = true;
                    viewListMenuItem.Checked = listCntxtMenuItem.Checked = false;
                    viewDetailsMenuItem.Checked = detailsCntxtMenuItem.Checked = false;
                    explorerListView.View = View.LargeIcon;
                    break;

                case "LIST":
                    viewIconsMenuItem.Checked = iconsCntxtMenuItem.Checked = false;
                    viewListMenuItem.Checked = listCntxtMenuItem.Checked = true;
                    viewDetailsMenuItem.Checked = detailsCntxtMenuItem.Checked = false;
                    explorerListView.View = View.List;
                    break;

                case "DETAILS":
                    viewIconsMenuItem.Checked = iconsCntxtMenuItem.Checked = false;
                    viewListMenuItem.Checked = listCntxtMenuItem.Checked = false;
                    viewDetailsMenuItem.Checked = detailsCntxtMenuItem.Checked = true;
                    explorerListView.View = View.Details;
                    break;

                default:
                    break;
            }
        }

        public void SetFormText()
        {
            titleLabel.Text = Path;
        }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void RefreshProperties(bool clearList)
        {
            if (clearList)
            {
                propertiesListview.Items.Clear();
            }
            else
            {
                try
                {
					rs2010.CatalogItem selItem = ((CatalogListViewItem)explorerListView.SelectedItems[0]).Item;
                    this.FillPropertiesListview(selItem);
                }
                catch (Exception)
                {
                    //Do nothing
                }
            }
        }

        #endregion

        #region Properties
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

        #region Utility Members
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public string GetParentPath(string currentPath)
        {
            string delimiter = "/";
            Regex rx = new Regex(delimiter);
            string[] childPath = rx.Split(currentPath);

            int parentLength = childPath.Length - 1;
            string[] parentPath = new string[parentLength];

            for (int i = 0; i < parentLength; i++)
                parentPath[i] = childPath[i];

            if (parentPath.Length == 1)
                return "/";
            else
                return String.Join("/", parentPath);
        }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public string GetProperty(string path, string name)
        {
            string propValue = String.Empty;
            try
            {
				rs2010.Property[] propArray = CreatePropertesArray(name);
				rs2010.Property[] properties = rs.GetProperties(path, propArray);
				foreach (rs2010.Property prop in properties)
                {
                    if (prop.Name == name)
                    {
                        propValue = prop.Value;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                
            }
            return propValue;
        }

        /// <summary>
        /// Create a Property Array 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public rs2010.Property[] CreatePropertesArray(string name)
        {
			rs2010.Property rsProperty = new rs2010.Property();
            rsProperty.Name = name;
			rs2010.Property[] rsProperties = new rs2010.Property[1];
            rsProperties[0] = rsProperty;

            return rsProperties;
        }
        #endregion
    }
}