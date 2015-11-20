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

 * Summary:  Demonstrates a Microsoft Office 2003 Research Pane for Microsoft 
 * SQL Server Reporting Service. The Query web service is required for
 * a Research Pane. The Query web service process the queries 
 * you receive from your users and returns an XML stream comforming to the
 * urn:Microsoft.Search namespace. Please see the Microsoft Research Library
 * SDK for more details.
---------------------------------------------------------------------*/

using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Net;
using System.Web.Services.Protocols;
using System.Xml;
using System.IO;
using System.Text;
using System.Configuration;
using System.Globalization;
using Microsoft.SqlServer.ReportingServices2010;

namespace Microsoft.Samples.SqlServer
{
    /// <summary>
    /// The web service must have the urn:Microsoft.Search namespace attribute.
    /// </summary>
    [WebService(Namespace = "urn:Microsoft.Search")]
    public class QueryService : System.Web.Services.WebService
    {
        private XmlWriter xmlQueryResponse;
        private MemoryStream ioMemoryStream;
        private CatalogItem[] catalogItemArray;
        private CatalogManager catalogManager;
        private string HTTPPath = String.Empty;
        private string reportItemPath = String.Empty;
        private string queryText = String.Empty;
        private Hashtable queryValues;

        public QueryService()
        {
        }


        /// <summary>
        /// Called by Microsoft Office research pane API to process user query string and return appropriate
        /// xml back to Microsoft Office.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), WebMethod]
        public String Query(String queryXml)
        {
            try
            {
                reportItemPath = System.Configuration.ConfigurationManager.AppSettings["ReportItemPath"];
                string reportServer = System.Configuration.ConfigurationManager.AppSettings["ReportServer"];
                HTTPPath = System.Configuration.ConfigurationManager.AppSettings["SearchServerPath"];
                if (!HTTPPath.EndsWith("/"))
                    HTTPPath += "/";

                //The response requires a research pane specific header. See the method for details.
                //Create starting elements for urn:Microsoft.Search.Response header. If there is an exception, the 
                //required response header stream is created outside of the try/catch.
                WriteStartResponseHeader();

                queryValues = QueryValuesFromXml(queryXml);
      
                Connect(reportServer, CredentialCache.DefaultCredentials);

                string startChar = queryText.Substring(0, 1);
                bool isSearchAction = queryValues.ContainsKey("FormAction");
                int searchType = 0;
                if (startChar != "?" || isSearchAction)
                {
                    WriteContentHeader();

                    if (isSearchAction)
                        searchType = 2;
                    else if (startChar == "/")
                        searchType = 1;
                    //Set the CatalogItems. A "/" will call ListChildren. Any other query will call FindItems.

                    CatalogItems(searchType);

                    //Create elements for Folders with a folder icon
                    Folders();
                    //Create elements for Reports
                    Reports();
                    
                }
                else
                {
                    SearchForm();
                }

            }
            catch (Exception ex)
            {
                NoResult(ex);
            }
            finally
            {
                WriteGoTo();

                WriteEndResponseHeader();
            }

            //Return a response representing Reporting Services catalogs or the exception message.
            return WriteResponse();
        }

        /// <summary>
        /// Set the CatalogItems to render based on the user query.
        /// </summary>
        private void CatalogItems(int searchType)
        {
            BooleanOperatorEnum boolOperator = BooleanOperatorEnum.Or;
            string searchFolder;
            //Default searchType = 0
            if (searchType == 1)
            {
                catalogItemArray = catalogManager.ListChildren(queryText);
            }
            else if (searchType == 2)
            {
                
                if (queryValues["AndOr"].ToString() == "And"){
                    boolOperator = BooleanOperatorEnum.And;}
                else if (queryValues["AndOr"].ToString() == "Or"){
                    boolOperator = BooleanOperatorEnum.Or;}
                
                searchFolder = queryValues["SearchFolder"].ToString();
                if (searchFolder == "RootFolder")
                    searchFolder = "/";
                
                catalogItemArray = catalogManager.AdvancedSearch(searchFolder, boolOperator, SearchConditions());
            }
            else
            {               
                catalogItemArray = catalogManager.FindItems(queryText);
            }
            if (catalogItemArray.Length == 0)
                NoResult(null);
        }
        /// <summary>
        /// Create single condition sample based on "?" in queryText.
        /// Supports Folder to Search and Name Contains Or Description contains
        /// </summary>
        private SearchCondition[] SearchConditions()
        {   
            SearchCondition searchCondition = null;
            Hashtable searchValues = new Hashtable();

            foreach (DictionaryEntry item in queryValues)
            {
                string key = item.Key.ToString();

                if (key.StartsWith("SearchCondition."))
                {
                    searchValues.Add(key, item.Value.ToString());
                }

            }

            SearchCondition[] conditions = new SearchCondition[searchValues.Count];
            int i = -1;
            foreach (DictionaryEntry searchItem in searchValues)
            {
                i++;
                string delimStr = ".";
                char[] delimiter = delimStr.ToCharArray();
                string searchName = searchItem.Key.ToString().Split(delimiter)[1];

                searchCondition = CreateSearchCondition(searchName, searchItem.Value.ToString());
                conditions[i] = searchCondition;
            }

            return conditions;
        }

        private SearchCondition CreateSearchCondition(string name, string value)
        {
            SearchCondition searchCondition = new SearchCondition();
            searchCondition.Condition = ConditionEnum.Contains;
            searchCondition.ConditionSpecified = true;
            searchCondition.Name = name;
            searchCondition.Values[0] = value;
            
            return searchCondition;
        }

        /// <summary>
        /// Write the final response to Office research pane
        /// </summary>
        private String WriteResponse()
        {
            xmlQueryResponse.Flush();
            ioMemoryStream.Flush();
            ioMemoryStream.Position = 0;
            string response = String.Empty;

            using (StreamReader iostReader = new StreamReader(ioMemoryStream))
            {
                using (xmlQueryResponse)
                {
                    response = iostReader.ReadToEnd().ToString();
                }
                ioMemoryStream.Close();
                return response;
            }
        }

        /// <summary>
        /// Get the query text and form values from xml.
        /// </summary>
        private Hashtable QueryValuesFromXml(string queryXml)
        {
            Hashtable values = new Hashtable();
            XmlDocument requestXml = new XmlDocument();
            XmlNamespaceManager nsmRequest = null;
            //Extact Query Text
            if (queryXml.Length > 0)
            {
                requestXml.LoadXml(queryXml);
                nsmRequest = new XmlNamespaceManager(requestXml.NameTable);
                nsmRequest.AddNamespace("ns", "urn:Microsoft.Search.Query");
                nsmRequest.AddNamespace("sp", "urn:Microsoft.Search.Office.ServiceParameters");
                nsmRequest.AddNamespace("oc", "urn:Microsoft.Search.Query.Office.Context");
            }

            queryText = requestXml.SelectSingleNode("//ns:QueryText", nsmRequest).InnerText;

            //Get ServiceParameters
            XmlNode serviceParameterNode = requestXml.SelectSingleNode("//sp:ServiceParameters/sp:Parameters", nsmRequest);
            if (serviceParameterNode != null)
            {
                values.Add("FormAction", 
                    requestXml.SelectSingleNode("//sp:ServiceParameters/sp:Action", nsmRequest).InnerText);
                
                foreach (XmlNode param in serviceParameterNode.ChildNodes)
                {
                    values.Add(param.Name, param.InnerText);
                }
            }

            return values;
        }

        /// <summary>
        /// Response header for registration domain
        /// </summary>
        private void WriteStartResponseHeader()
        {
            
            //Create response MemoryStream and xmlQueryResponse for response string
            ioMemoryStream = new MemoryStream();
            xmlQueryResponse = XmlWriter.Create(ioMemoryStream);

            //Start of XML document
            xmlQueryResponse.WriteStartDocument();
            xmlQueryResponse.WriteStartElement("ResponsePacket", "urn:Microsoft.Search.Response");
            xmlQueryResponse.WriteAttributeString("revision", "1");
            xmlQueryResponse.WriteStartElement("Response");

            //The domain GUID must match the Registration Service ID
            xmlQueryResponse.WriteAttributeString("domain", Resources.Research.ServiceId);
            xmlQueryResponse.WriteElementString("QueryID",	Resources.Research.QueryId);
            xmlQueryResponse.WriteStartElement("Range");
            xmlQueryResponse.WriteStartElement("Results");
        }
        private void WriteContentHeader()
        {
            xmlQueryResponse.WriteStartElement("Content", "urn:Microsoft.Search.Response.Content");
        }

        private void WriteFormHeader()
        {
            xmlQueryResponse.WriteStartElement("Form", "urn:Microsoft.Search.Response.Form");
        }


        /// <summary>
        /// Close response start elements.
        /// </summary>
        private void WriteEndResponseHeader()
        {
            xmlQueryResponse.WriteEndElement(); //Content
            xmlQueryResponse.WriteEndElement(); //Results
            xmlQueryResponse.WriteEndElement(); //Range

            xmlQueryResponse.WriteElementString("Status", string.Format(CultureInfo.InvariantCulture,
                    Resources.Resources.Status));

            xmlQueryResponse.WriteEndElement(); //Response
            xmlQueryResponse.WriteEndElement(); //ResponsePacket

            xmlQueryResponse.WriteEndDocument();
        }

        /// <summary>
        /// Write default xml for no search result.
        /// </summary>
        private void NoResult(Exception ex)
        {   
            //Close any open resources since response creates a new XmlWriter
            ioMemoryStream.Close();
            xmlQueryResponse.Close();

            WriteStartResponseHeader();
            WriteContentHeader();

            xmlQueryResponse.WriteStartElement("Line");

            xmlQueryResponse.WriteStartElement("Image");

            xmlQueryResponse.WriteAttributeString("source", HTTPPath +
                 System.Configuration.ConfigurationManager.AppSettings["InfoIcon"]);
            xmlQueryResponse.WriteEndElement();
            xmlQueryResponse.WriteElementString("Char", string.Format(CultureInfo.InvariantCulture,
                    Resources.Resources.NoResults));
            xmlQueryResponse.WriteEndElement();

            xmlQueryResponse.WriteRaw("<HorizontalRule/>");

            if (ex != null)
            {
                //For testing - Create P elements to return the exception message to the research pane. 
                xmlQueryResponse.WriteElementString("P", "Exception:");
                xmlQueryResponse.WriteElementString("P", ex.Message);
                xmlQueryResponse.WriteRaw("<HorizontalRule/>");
            }
        }

        /// <summary>
        /// Write the Go To navigation line.
        /// </summary>
        private void WriteGoTo()
        {
            xmlQueryResponse.WriteStartElement("Line");
            xmlQueryResponse.WriteElementString("Char", string.Format(CultureInfo.InvariantCulture,
                    Resources.Resources.GoTo));
            //NewQuery
            xmlQueryResponse.WriteStartElement("NewQuery");
            xmlQueryResponse.WriteAttributeString("query", "/");
            xmlQueryResponse.WriteAttributeString("tooltip", string.Format(CultureInfo.InvariantCulture,
                    Resources.Resources.GoToRootFolder));
            xmlQueryResponse.WriteElementString("Text", string.Format(CultureInfo.InvariantCulture,
                    Resources.Resources.ShowRootFolder));
            xmlQueryResponse.WriteEndElement();

            xmlQueryResponse.WriteElementString("Char", "|");
            //NewQuery
            xmlQueryResponse.WriteStartElement("NewQuery");
            xmlQueryResponse.WriteAttributeString("query", "?");
            xmlQueryResponse.WriteAttributeString("tooltip", string.Format(CultureInfo.InvariantCulture,
                    Resources.Resources.ShowSearch));
            xmlQueryResponse.WriteElementString("Text", string.Format(CultureInfo.InvariantCulture,
                    Resources.Resources.ShowSearch));

            xmlQueryResponse.WriteEndElement();
            xmlQueryResponse.WriteEndElement();
        }

        /// <summary>
        /// Create line elements with folder image and subfolder hyperlinks. The hyperlink contains
        /// a new query for a subfolder such as "/Products"
        /// </summary>
        /// <param name="folderImage"></param>
        private void Folders()
        {
            foreach (CatalogItem catalogItem in catalogItemArray)
            {
                if (catalogItem.TypeName == "Folder" && catalogItem.TypeName != "Data Sources")
                {
                    xmlQueryResponse.WriteStartElement("Line");
                    //Folder Image
                    xmlQueryResponse.WriteStartElement("Image");
                    
                    xmlQueryResponse.WriteAttributeString("source", HTTPPath + 
                        System.Configuration.ConfigurationManager.AppSettings["FolderIcon"]);
                    xmlQueryResponse.WriteEndElement();
                    //NewQuery
                    xmlQueryResponse.WriteStartElement("NewQuery");
                    xmlQueryResponse.WriteAttributeString("query", catalogItem.Path);
                    xmlQueryResponse.WriteElementString("Text", catalogItem.Name);
                    xmlQueryResponse.WriteEndElement();
                    xmlQueryResponse.WriteEndElement();
                }
            }            
        }

        /// <summary>
        /// Write the simple report search form.
        /// </summary>
        private void SearchForm()
        {
            //Get all folders
            catalogItemArray = catalogManager.AllFolders();

            WriteFormHeader();

            xmlQueryResponse.WriteElementString("Label", string.Format(CultureInfo.InvariantCulture,
                    Resources.Resources.SearchPage));

            //Search Folders List
            xmlQueryResponse.WriteElementString("Label", string.Format(CultureInfo.InvariantCulture,
                Resources.Resources.SearchWithinFolder));
            xmlQueryResponse.WriteStartElement("Listbox");
                xmlQueryResponse.WriteAttributeString("id", "SearchFolder");
                
                xmlQueryResponse.WriteStartElement("Option");
                xmlQueryResponse.WriteAttributeString("id", "RootFolder");
                xmlQueryResponse.WriteAttributeString("selected", "true");
                xmlQueryResponse.WriteElementString("Text", string.Format(CultureInfo.InvariantCulture,
                    Resources.Resources.RootFolder));
                xmlQueryResponse.WriteEndElement();

                foreach (CatalogItem catalogItem in catalogItemArray)
                {
                    if (catalogItem.TypeName == "Folder" && catalogItem.TypeName != "Data Sources")
                    {

                        xmlQueryResponse.WriteStartElement("Option");
                        xmlQueryResponse.WriteAttributeString("id", catalogItem.Path);
                        xmlQueryResponse.WriteElementString("Text", string.Format(CultureInfo.InvariantCulture,
                            catalogItem.Name));
                        xmlQueryResponse.WriteEndElement();

                    }
                }        
            xmlQueryResponse.WriteEndElement();

            //Name Contains Option            
            xmlQueryResponse.WriteElementString("Label", string.Format(CultureInfo.InvariantCulture,
                    Resources.Resources.NameContains));

            xmlQueryResponse.WriteStartElement("Edit");
            xmlQueryResponse.WriteAttributeString("id", "SearchCondition.Name");
            xmlQueryResponse.WriteEndElement();


            //AndOr Combobox
            xmlQueryResponse.WriteStartElement("Listbox");
            xmlQueryResponse.WriteAttributeString("id", "AndOr");
            xmlQueryResponse.WriteAttributeString("width", "4");
            //xmlQueryResponse.WriteElementString("Text", "Boolean Operator");

                xmlQueryResponse.WriteStartElement("Option");
                xmlQueryResponse.WriteAttributeString("id", "And");
                xmlQueryResponse.WriteElementString("Text", "And");
                xmlQueryResponse.WriteEndElement();

                xmlQueryResponse.WriteStartElement("Option");
                xmlQueryResponse.WriteAttributeString("id", "Or");
                xmlQueryResponse.WriteAttributeString("selected", "true");
                xmlQueryResponse.WriteElementString("Text", "Or");
                xmlQueryResponse.WriteEndElement();

            xmlQueryResponse.WriteEndElement();

            //Description Contains Option            
            xmlQueryResponse.WriteElementString("Label", string.Format(CultureInfo.InvariantCulture,
                    Resources.Resources.DescriptionContains));

            xmlQueryResponse.WriteStartElement("Edit");
            xmlQueryResponse.WriteAttributeString("id", "SearchCondition.Description");
            xmlQueryResponse.WriteEndElement();
            
            //Requery Button
            xmlQueryResponse.WriteStartElement("Button");
            xmlQueryResponse.WriteAttributeString("id", "SearchForm");
            xmlQueryResponse.WriteAttributeString("action", "requery");
            xmlQueryResponse.WriteElementString("Text", string.Format(CultureInfo.InvariantCulture,
                    Resources.Resources.Search));
            xmlQueryResponse.WriteEndElement();
                
        }
        /// <summary>
        /// Create report line items. Earch report is a heading. The heading contains a hyperlink to
        /// the report (report will be display in IE), a description, and another heading which contains
        /// a report summary. If the report contains a table, then the table field names will be listed.
        /// </summary>
        private void Reports()
        {
            string catalogItemPath = String.Empty;

            foreach (CatalogItem catalogItem in catalogItemArray)
            {
                if (catalogItem.TypeName == "Report")
                {
                    //Collapsed Heading
                    xmlQueryResponse.WriteStartElement("Heading");
                    xmlQueryResponse.WriteAttributeString("collapsed", "true");
                    xmlQueryResponse.WriteElementString("Text", catalogItem.Name);

                    xmlQueryResponse.WriteStartElement("Line");
                    xmlQueryResponse.WriteStartElement("Image");
                    xmlQueryResponse.WriteAttributeString("source", HTTPPath +
                        System.Configuration.ConfigurationManager.AppSettings["NavLinkIcon"]);
                    xmlQueryResponse.WriteEndElement();
                    xmlQueryResponse.WriteStartElement("Hyperlink");
                    xmlQueryResponse.WriteAttributeString("url",
                        reportItemPath + catalogItem.Path);
                    xmlQueryResponse.WriteAttributeString("tooltip", string.Format(CultureInfo.InvariantCulture,
                        Resources.Resources.GoToReport + catalogItem.Name));
                    xmlQueryResponse.WriteElementString("Text",
                         string.Format(CultureInfo.InvariantCulture, catalogItem.Name));
                    xmlQueryResponse.WriteEndElement();
                    xmlQueryResponse.WriteEndElement();

                    //Description
                    if (catalogItem.Description != null)
                    {
                        xmlQueryResponse.WriteElementString("P", string.Format(CultureInfo.InvariantCulture,
                            catalogItem.Description));
                    }

                    //More details Heading
                    xmlQueryResponse.WriteStartElement("Heading");
                    xmlQueryResponse.WriteAttributeString("collapsed", "true");
                    xmlQueryResponse.WriteElementString("Text", string.Format(CultureInfo.InvariantCulture,
                            Resources.Resources.ReportSummary));
                    //Report Preview Table
                    TabularPreview(catalogItem);
                    xmlQueryResponse.WriteEndElement();
                    xmlQueryResponse.WriteEndElement();
                }
            }
        }

        /// <summary>
        /// Render a Tabular element containing Name/Value pair for CreatedBy and CreationDate properties.
        /// </summary>
        /// <param name="catalogItem"></param>
        private void TabularPreview(CatalogItem catalogItem)
        {
            //Start Tabular
            xmlQueryResponse.WriteStartElement("Tabular");
            //CreatedBy Record
            xmlQueryResponse.WriteStartElement("Record");
            xmlQueryResponse.WriteElementString("Name",
                string.Format(CultureInfo.InvariantCulture, Resources.Resources.CreatedBy));
            xmlQueryResponse.WriteElementString("Value", 
                string.Format(CultureInfo.InvariantCulture, catalogItem.CreatedBy));
            xmlQueryResponse.WriteEndElement();
            //CreationDate Record
            xmlQueryResponse.WriteStartElement("Record");
            xmlQueryResponse.WriteElementString("Name",
                string.Format(CultureInfo.InvariantCulture, Resources.Resources.CreationDate));
            xmlQueryResponse.WriteElementString("Value",
                string.Format(CultureInfo.InvariantCulture, catalogItem.CreationDate.ToString()));
            xmlQueryResponse.WriteEndElement();

            xmlQueryResponse.WriteEndElement();
        }


        /// <summary>
        /// Connect to Reporting Services web service.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="credentials"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
        private void Connect(string url, ICredentials credentials)
        {
            catalogManager = new CatalogManager();
            catalogManager.Credentials = credentials;
            
            catalogManager.Connect(url);

        }
    }
}