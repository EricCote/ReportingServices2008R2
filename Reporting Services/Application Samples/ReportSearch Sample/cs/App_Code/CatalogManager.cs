/*-----------------------------------------------------------------------
 * This file is part of the Microsoft Code Samples.

 * Copyright (C) Microsoft Corporation.  All rights reserved.
 
 * This source code is intended only as a supplement to Microsoft
 * Development Tools and/or on-line documentation.  See these other
 * materials for detailed information regarding Microsoft code samples.

 * THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
 * KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 * PARTICULAR PURPOSE.
*/

using System;
using System.Net;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.Text.RegularExpressions;
using System.Web.Services.Protocols;
using System.Globalization;
using Microsoft.SqlServer.ReportingServices2010;

namespace Microsoft.Samples.SqlServer
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class CatalogManager
	{
		#region Constants
		const string wsdl = "/ReportService2010.asmx";
		private const string Root = "/";
		private const int m_MaxFileUploadSize = 4096;
		#endregion
		
		#region Private Member Variables	
	   
		private string m_url;
		private string m_soapUrl;
        private ReportingService2010 m_service;
		private ICredentials m_credentials;
			
		#endregion

		#region Constructors
		
		public CatalogManager()
		{
		}
		
		public CatalogManager( string url)
			: this()
		{
           
			this.m_url = url;
			this.m_soapUrl = this.m_url + wsdl;
			
			this.m_service.Url = this.m_soapUrl;
		}

		#endregion
		
		#region RSWebService
        public ReportingService2010 RSWebService
		{
			get
			{
				return m_service;
			}
		}
		
		#region How To: Connect to Reporting Services
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
        public void Connect(string url)
		{
            this.m_service = new ReportingService2010();
			this.m_service.Credentials = this.Credentials;
			this.m_url = url;
			this.m_soapUrl = this.m_url + wsdl;
			
			this.m_service.Url = this.m_soapUrl;
		}
		#endregion
		#endregion
		
		#region How To: RS Methods
		
        
		#region How To: Create a Folder with description property
		public void CreateFolder(string path, string name, string description )
		{
            if (String.IsNullOrEmpty(path) | String.IsNullOrEmpty(name))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
					Resources.Resources.InvalidParameter));
			}
			else
			{
				//Create Property array for description
				Property[] rsProperties = this.CreateDescriptionProperty(description);
				
				//Call RS CreateFolder() base method
				this.CreateFolder(name, path, rsProperties );
			}
		}
		
		
		/// <summary>
		/// RSWebService Wrapper Method called by CreateFolder(path, name, description )
		/// </summary>
		/// <param name="folderName"></param>
		/// <param name="parentPath"></param>
		/// <param name="properties"></param>
		public void CreateFolder(string folderName, string parentPath, Property[] properties)
		{
			this.RSWebService.CreateFolder(folderName, parentPath, properties);
		}
		
		
		#endregion

		#region How To: Get a list of child CatalogItems
		/// <summary>
		/// Returns CatalogItem
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public CatalogItem[] ListChildren( string path )
		{
            return this.RSWebService.ListChildren(path, false);
		}

        public CatalogItem[] AllFolders()
		{
            return this.RSWebService.ListChildren("/", true);
		}
		#endregion

		#region How To: Delete multiple CatalogItems within a Batch
		public void DeleteItems(string path, ArrayList catalogItems)
		{		
			string itemPath;

			//Create batch header - Decremented in RS2010 endpoint
			//BatchHeader singleBatchHeader = new BatchHeader();
			//Set EditItem batch id and top BatchHeaderValue
			//singleBatchHeader.BatchID = this.RSWebService.CreateBatch();
			//this.RSWebService.BatchHeaderValue = singleBatchHeader;     
			
			//Call DeleteItem methods
			foreach(string item in catalogItems)
			{
				itemPath = this.GetCleanPath(path, item);
				this.DeleteItem( itemPath );
			}
			//Execute DeleteItem methods in a batch
			//this.RSWebService.ExecuteBatch();
			
			//this.RSWebService.BatchHeaderValue = null;
		}
		
		public string GetCleanPath(string path, string item)
		{
			// If the item is in the root don't include the path.
			// This avoids double forward slash "//Pathname" problems
			string itemPath;
			if ( path != Root )
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

		#region How To: Delete an item
		public void DeleteItem(string path)
		{
			this.RSWebService.DeleteItem(path);
		}
		#endregion
		
		#region How To: Edit a CatalogItem using a CreateBatch/ExecuteBatch
		//How To: Excecute multiple methods with CreateBatch(), CancelBatch() and ExecuteBatch()
		//How To: Set CatalogItem Properties

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public void EditItem( string oldPath, string newPath, string description)
		{
			Property[] rsProperties = this.CreateDescriptionProperty(description);

			try
			{
				//Rename Item and Create new Properties based on old properties
				//Create batch header decremented in RS2010
				//BatchHeader singleBatchHeader = new BatchHeader();
				//Set EditItem batch id and top BatchHeaderValue
				//singleBatchHeader.BatchID = this.RSWebService.CreateBatch();
				//this.RSWebService.BatchHeaderValue = singleBatchHeader;     
				
				//Call methods to batch
				this.RSWebService.MoveItem(oldPath, newPath);
				this.RSWebService.SetProperties(newPath, rsProperties);
				//Test code. 
				// this.SetProperties( newPath, null ); causes a failed method call which results in
				// a rollback of the MoveItem changes

				//Rollback transaction if either method fails
				//this.RSWebService.ExecuteBatch();
				
				//this.RSWebService.BatchHeaderValue = null;
			}		
			catch (SoapException)
			{
                //BatchOperationFailed
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    Resources.Resources.BatchOperationFailed));
			}
		}
				
		#endregion
		
		#region How To: Copy a Report, Resource or DataSource to a new path	
		/*
        public Warning[] CopyReport(string name, string oldPath, string newPath)
		{
			//GetReportDefinition()
			//CreateReport
			Warning[] warning = null;
			byte[] reportDefinition = null;
			
			reportDefinition = this.RSWebService.GetItemDefinition(oldPath);
			this.RSWebService.CreateCatalogItem("Report",name, newPath, true, reportDefinition, null,out warning);
            


			return warning;
		}
         
		
		public void CopyResource(string name, string oldPath, string newPath)
		{
			byte[] resourceContents = null;
			string mimeType;

			resourceContents = this.RSWebService.GetResourceContents(oldPath, out mimeType);
			this.RSWebService.CreateResource(name, newPath, true, resourceContents, mimeType, null);
		}
		
		public void CopyDataSource(string dataSource, string oldPath, string newPath)
		{
			DataSourceDefinition dsDefinition = null;

			dsDefinition = this.RSWebService.GetDataSourceContents(oldPath);
			this.RSWebService.CreateDataSource(dataSource, newPath, true, dsDefinition, null);
		}
		
        */
		#endregion
	
		#region How To: Get CatalogItem properties from a path
		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <param name="properties"></param>
		/// <returns></returns>
		public Property[] GetProperties(string path, Property[] properties)
		{			
			return this.RSWebService.GetProperties(path, properties);
		}
		
		#endregion
		
		#region How To: Import a Report		
		/// <summary>
		/// The sample will not replace existing reports.
		/// </summary>
		/// <param name="reportName"></param>
		/// <param name="path"></param>
		/// <param name="reportDefinition"></param>
		/// <returns></returns>
		public Warning[] CreateReport(string reportName, string path, Byte[] reportDefinition)
		{
            Warning[] warnings = null;
            RSWebService.CreateCatalogItem("Report", reportName, path, false, reportDefinition, null, out warnings);

			return warnings;
		}
		
		#endregion
		
		#region How To: Find reports 
        public CatalogItem[] FindItems(string queryText)
		{
			//Create conditions
            SearchCondition[] conditions = new SearchCondition[1];
            

            SearchCondition condition = new SearchCondition();
			condition.Condition = ConditionEnum.Contains;
			condition.ConditionSpecified = true;
			condition.Name = "Name";
            string[] val = { queryText };
            condition.Values = val;
            
            conditions[0] = condition;

            Property[] SearchOptions = new Property[1];
            Property SearchOption = new Property();
            SearchOption.Name = "Recursive";
            SearchOption.Value = "True";
            SearchOptions[0] = SearchOption;
						
			return this.RSWebService.FindItems( "/", BooleanOperatorEnum.Or,SearchOptions, conditions );

		}

        public CatalogItem[] AdvancedSearch(string folder, BooleanOperatorEnum booleanOperator,
            SearchCondition[] searchConditions)
		{
            Property[] SearchOptions = new Property[1];
            Property SearchOption = new Property();
            SearchOption.Name = "Recursive";
            SearchOption.Value = "True";
            SearchOptions[0] = SearchOption;

            return this.RSWebService.FindItems(folder, booleanOperator,SearchOptions, searchConditions);
		}

		#endregion
		
		#endregion
		
		#region Properties
		
		public ICredentials Credentials
		{
			get
			{
				return this.m_credentials;
			}
			
			set
			{
				this.m_credentials = value;
			}
		}
		#endregion
		
		#region Utility Members
		public string GetParentPath(string currentPath)
		{
			string delimiter = "/";
			Regex rx = new Regex( delimiter );
			string[] childPath = rx.Split( currentPath );
		
			int parentLength = childPath.Length - 1;
			string[] parentPath = new string[parentLength];
		
			for ( int i = 0; i < parentLength; i++ )
				parentPath[i] = childPath[i];
 
			if ( parentPath.Length == 1 )
				return "/";
			else
				return String.Join( "/", parentPath );
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string ServerName()
		{
			Regex rx = new Regex("//");
			string[] tempArray = rx.Split(this.m_url );
			string path = tempArray[1];
			rx = new Regex( "/" );
			tempArray = rx.Split( path );
			string server = tempArray[0];
			
			return server;
		}

		/// <summary>
		/// Create a Property Array 
		/// </summary>
		/// <param name="Name"></param>
		/// <returns></returns>
		public Property[] CreatePropertesArray(string name)
		{
			Property rsProperty = new Property();
			rsProperty.Name = name;
			Property[] rsProperties = new Property[1];
			rsProperties[0] = rsProperty;
			
			return rsProperties;
		}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public string GetProperty(string path, string name)
		{
			string propValue = String.Empty;
			try
			{
				Property[] propArray = this.CreatePropertesArray(name);
				Property[] properties = this.m_service.GetProperties(path, propArray);
				foreach(Property prop in properties)
				{
					if(prop.Name == name)
					{
						propValue = prop.Value;
						break;
					}
				}
			}
			catch(SoapException)
			{
                throw new Exception(string.Format(CultureInfo.InvariantCulture,
                    Resources.Resources.GetPropertyError));
			}
			return propValue;
		}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public int GetTypeIndex(string ItemType)
		{
			int typeIndex;
            switch (ItemType)
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
				default:
                    throw new Exception(string.Format(CultureInfo.InvariantCulture,
                        Resources.Resources.NoTypeInformation + ItemType));
			}
			return typeIndex;
		}
		
		/// <summary>
		/// Helper method for sample purposes only. A more robust application would create a general
		/// purpose property method.
		/// </summary>
		/// <returns></returns>
		private Property[] CreateDescriptionProperty(string description)
		{
			Property[] rsProperties = new Property[1];
			Property rsProperty = new Property();
			rsProperty.Name = "Description";
			rsProperty.Value = description;
			rsProperties[0] = rsProperty;
			
			return rsProperties;
		}
		#endregion
		
	}
}

