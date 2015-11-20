#region Copyright Microsoft Corporation. All rights reserved.
/*=============================================================================
  File:      FindRenderSave.cs

  Summary:  Demonstrates an implementation of a Form that can
            be used to search for reports, render them, and
            save them to disk.
      
---------------------------------------------------------------------
  This file is part of Microsoft SQL Server Code Samples.
  
 This source code is intended only as a supplement to Microsoft
 Development Tools and/or on-line documentation.  See these other
 materials for detailed information regarding Microsoft code samples.

 THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
 KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 PARTICULAR PURPOSE.
==============================================================================*/
#endregion
#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using rs2010 = Microsoft.SqlServer.ReportingServices2010;
using rsExecService = Microsoft.SqlServer.ReportingServices.ReportExecutionService;
using Microsoft.Samples.ReportingServices.FindRenderSave.Properties;
using System.Configuration;

#endregion

namespace Microsoft.Samples.ReportingServices.FindRenderSave
{
    partial class FindRenderSave : Form
    {
        // User defined variables.
		public rs2010.CatalogItem selItem;
		private rs2010.ReportingService2010 rs;
		private rsExecService.ReportExecutionService rsExec;

		private rs2010.CatalogItem[] returnedItems;
        private const int NAME = 0;
        private const int DESC = 1;
        private const int BOTH = 2;

        // Variables used for save dialog filters
        private const string PDF = "PDF file (*.pdf)|*.pdf";
        private const string IMAGE = "Tiff file (*.tif)|*.tif";
        private const string MHTML = "Web Page, single file (*.mhtml)|*.mhtml";
        private const string EXCEL = "Microsoft Excel Workbook (*.xls)|*.xls";

        public FindRenderSave()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Once the search has returned a list of reports, users can select
        /// reports to display the description and path properties.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "selItem")]
        private void reportListView_SelectedIndexChanged(object sender, 
		    System.EventArgs e)
        {
            rs2010.CatalogItem selItem;

            // Once a report is selected, enable the save button.
            saveReportButton.Enabled = true;
             
            descriptionTextBox.Clear();
            pathTextBox.Clear();
            if (reportListView.SelectedItems.Count > 0)
            {
			    selItem = ((CatalogListViewItem)reportListView.SelectedItems[0]).Item;         

                //Show the description
                if ( selItem.Description != null)
                    descriptionTextBox.Text = selItem.Description;

                // Show the path
                pathTextBox.Text = selItem.Path;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "format"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void saveReportButton_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // Define variables needed for the Render() method.
            string historyID = null;
            string format = formatComboBox.Text;
			rs2010.DataSourceCredentials[] credentials = null;
			rs2010.ParameterValue[] reportHistoryParameters = null;
             
            // Define variables needed for GetParameters() method
            bool forRendering = false;
			rs2010.ItemParameter[] parameters = null;
            bool noDefault = false;

            // Create a variable containing the selected item
            selItem = ((CatalogListViewItem)reportListView.SelectedItems[0]).Item; 

            try
            {
                // If the report uses parameters for which there is no default
                // value, then the report cannot be rendered and saved by this
                // application
                parameters = rs.GetItemParameters(selItem.Path, historyID, 
				    forRendering, reportHistoryParameters, credentials);

				foreach (rs2010.ItemParameter parameter in parameters)
                {
                    if (parameter.DefaultValues == null)
                    {
                        noDefault = true;
                        break;
                    }
                }

                if (noDefault)
                {
                    MessageBox.Show(
				        Resources.missingDefaultParametersErrorMessage, 
				        Resources.missingDefaultParametersMessageBoxTitle,
				        MessageBoxButtons.OK,
				        MessageBoxIcon.Error);
                }
                else
                {
                    SaveAs();
                }
            }

            catch (Exception exception)
            {
                HandleException(exception); 
            }

            finally 
            {
                Cursor.Current = Cursors.Default;
            }
        }
          
        // Utility method that is used to simplify populating the Save Dialog 
        // with the appropriate file filters.
        private string GetFilterString()
        {
            switch (formatComboBox.Text)
            {
                case "MHTML":
                return MHTML;
                case "PDF":
                return PDF;
                case "IMAGE":
                return IMAGE;
                case "EXCEL":
                return EXCEL;
                default:
                return "";
            }
        }

        /// <summary>
        /// When the search button is selected, connect to the Web service
        /// and return a list of reports that meets the search conditions
        /// defined by the user.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void searchButton_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // Clear out the current descripton and path fields
            descriptionTextBox.Clear();
            pathTextBox.Clear();

            // Disable save button on new search
            saveReportButton.Enabled = false;

            // Check to see if the 'Search By' string is valid.
            if (conditionComboBox.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select a valid 'Search By' string by clicking the drop down arrow!",
                    "Invalid 'Search By' String",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Check to see if a search string is entered
            if (searchTextBox.Text == null || searchTextBox.Text == "")
            {
			    MessageBox.Show(
				    Resources.invalidSearchStringErrorMessage,
				    Resources.invalidSearchStringMessageBoxTitle, 
				    MessageBoxButtons.OK,
				    MessageBoxIcon.Error);
		    }
		    else
            {
                reportListView.Items.Clear();

                // Create a new proxy to the web service
                rs = new rs2010.ReportingService2010();
				rsExec = new rsExecService.ReportExecutionService();

                // Authenticate to the Web service using Windows credentials
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
                rsExec.Credentials = System.Net.CredentialCache.DefaultCredentials;

                // Assign the URL of the Web service
				rs.Url = ConfigurationManager.AppSettings["ReportingService2010"];
				rsExec.Url = ConfigurationManager.AppSettings["ReportExecutionService"];

				rs2010.SearchCondition[] conditions;
             
                if (conditionComboBox.SelectedIndex == NAME)
                {
                    // Create Name search condition
					rs2010.SearchCondition condition = new rs2010.SearchCondition();
					condition.Condition = rs2010.ConditionEnum.Contains;
                    condition.ConditionSpecified = true;
                    condition.Name = "Name";
                    string[] val = {searchTextBox.Text};
                    condition.Values = val; 
                      

					conditions = new rs2010.SearchCondition[1];
                    conditions[0] = condition;
                }
                else if (conditionComboBox.SelectedIndex == DESC)
                {
                    // Create Description search condition
					rs2010.SearchCondition condition = new rs2010.SearchCondition();
					condition.Condition = rs2010.ConditionEnum.Contains;
                    condition.ConditionSpecified = true;
                    condition.Name = "Description";
                    condition.Values[0] = searchTextBox.Text;

                    // Add conditions to the conditions argument to be used for 
			        // FindItems
					conditions = new rs2010.SearchCondition[1];
                    conditions[0] = condition;
                }
                else 
                {
                    // Create Name
					rs2010.SearchCondition nameCondition = new rs2010.SearchCondition();
					nameCondition.Condition = rs2010.ConditionEnum.Contains;
                    nameCondition.ConditionSpecified = true;
                    nameCondition.Name = "Name";
                    nameCondition.Values[0] = searchTextBox.Text;

                    // Create Desription
					rs2010.SearchCondition descCondition = new rs2010.SearchCondition();
					descCondition.Condition = rs2010.ConditionEnum.Contains;
                    descCondition.ConditionSpecified = true;
                    descCondition.Name = "Description";
                    descCondition.Values[0] = searchTextBox.Text;

                    // Add conditions to the conditions argument to be used for 
                    // FindItems
					conditions = new rs2010.SearchCondition[2];
                    conditions[0] = nameCondition;
                    conditions[1] = descCondition;
                }

                try
                {
                    // Return a list of items based on the search conditions that 
                    // apply
                    rs2010.Property[] SearchOptions = new rs2010.Property[1];
                    rs2010.Property SearchOption = new rs2010.Property();
                    SearchOption.Name = "Recursive";
                    SearchOption.Value = "True";
                    SearchOptions[0] = SearchOption;


                    returnedItems = rs.FindItems("/", rs2010.BooleanOperatorEnum.Or, SearchOptions, conditions);
                    

			        if (returnedItems != null && returnedItems.Length != 0)
			        {
						foreach (rs2010.CatalogItem ci in returnedItems)
				        {
					        //Create a ListView item containing a report catalog item
							if (ci.TypeName == "Report")
					        {
						        // Add the items to the list view
								CatalogListViewItem newItem = new CatalogListViewItem(ci);
						        reportListView.Items.Add(newItem);
					        }
				        }
			        }
			        else
				        MessageBox.Show(
                            Resources.noItemsFoundInfoMessage,
					        Resources.noItemsFoundMessageBoxTitle,
					        MessageBoxButtons.OK, MessageBoxIcon.Information);
		        }

                catch (Exception exception)
                {
                    HandleException(exception); 
                }

                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void closeButton_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        // Method to save the selected item
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "ei"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void SaveAs()
        {
            // Create instance of save dialog and set default
            // filename and filter
            saveReportDialog = new SaveFileDialog();
            saveReportDialog.Filter = GetFilterString();
            saveReportDialog.FileName = selItem.Name;
             
            // Open the save file dialog
            DialogResult dr = saveReportDialog.ShowDialog();
             
            // If the user selects a valid item and clicks OK
            if (dr == DialogResult.OK) 
            {
                // Store the filename of the report
                string fileName = saveReportDialog.FileName;

                // Prepare Render arguments
                string historyID = null;
				string deviceInfo = null;
                string format = formatComboBox.Text;
                Byte[] results;
				string encoding = String.Empty;
				string mimeType = String.Empty;
				string extension = String.Empty;
				rsExecService.Warning[] warnings = null;
                
                string[] streamIDs = null;

				rsExecService.ExecutionInfo ei = rsExec.LoadReport(selItem.Path, historyID);
            
                //Execute the report and save it into a file.
                try
                {
					results = rsExec.Render(format, deviceInfo, out extension,
                       out encoding, out mimeType, 
                        out warnings, out streamIDs);

                    // Create a file stream and write the report to it
					using (FileStream stream = File.OpenWrite(fileName))
					{
						stream.Write(results, 0, results.Length);
					}
                }

                catch (Exception exception)
                {
                    HandleException(exception); 
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        private void HandleException(Exception exception)
        {
            string exceptionText;
            // Find out if the exception is a SOAP exception and make use of the
            // SOAP exception Detail property.
            if (exception is SoapException)
                exceptionText = 
                ((SoapException)exception).Detail["Message"].InnerXml;
            else
                exceptionText = exception.Message;

		    MessageBox.Show(
                exceptionText,
			    Resources.genericErrorMessageBoxTitle, 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Error);
	    }
    }
}