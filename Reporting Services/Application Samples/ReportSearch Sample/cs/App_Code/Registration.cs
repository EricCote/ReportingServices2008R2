/*-----------------------------------------------------------------------
 This file is part of the Microsoft Code Samples.

 Copyright (C) Microsoft Corporation.  All rights reserved.
 
 This source code is intended only as a supplement to Microsoft
 Development Tools and/or on-line documentation.  See these other
 materials for detailed information regarding Microsoft code samples.

 THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
 KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 PARTICULAR PURPOSE.

 Summary:  Demonstrates a Microsoft Office 2003 Research Pane for Microsoft 
 SQL Server Reporting Service. The Registration web service is required for
 a Research Pane. The Registration web service is used by Microsoft Office 2003 to
 create a reference to the Query web service.
---------------------------------------------------------------------*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Xml;
using System.IO;
using System.Globalization;

namespace Microsoft.Samples.SqlServer
{
    /// <summary>
    ///  The web service must have the urn:Microsoft.Search namespace attribute.
    /// </summary>
    [WebService(Namespace = "urn:Microsoft.Search")]
    public class ReportServerPane : System.Web.Services.WebService
    {
        public ReportServerPane()
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor"), WebMethod()]
        public String Registration()
        {
            StringWriter output = new StringWriter();
            string regString = String.Empty;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Auto;

            using (XmlWriter writer = XmlWriter.Create(output, settings))
            {
                // Write XML data.
                writer.WriteRaw("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                writer.WriteRaw("<ProviderUpdate xmlns=\"urn:Microsoft.Search.Registration.Response\">");
                    writer.WriteStartElement("Status");
                    writer.WriteValue(Resources.Resources.Status);
                    writer.WriteEndElement();
                    writer.WriteStartElement("Providers");
                        writer.WriteStartElement("Provider");
                            writer.WriteStartElement("Message");
                            writer.WriteValue(Resources.Research.Message);
                            writer.WriteEndElement();
                            writer.WriteStartElement("Id");
                            writer.WriteValue(Resources.Research.RegistrationId);
                            writer.WriteEndElement();
                            writer.WriteStartElement("Name");
                            writer.WriteValue(Resources.Research.RegistrationName);
                            writer.WriteEndElement();

                            string HTTPPath = System.Configuration.ConfigurationManager.AppSettings["SearchServerPath"];
                            writer.WriteStartElement("QueryPath");
                            writer.WriteValue(HTTPPath + "/" + Resources.Research.QueryPath);
                            writer.WriteEndElement();
                            writer.WriteStartElement("RegistrationPath");
                            writer.WriteValue(Resources.Research.RegistrationPath);
                            writer.WriteEndElement();
                            writer.WriteRaw("<Type>SOAP</Type>");

                        writer.WriteStartElement("Services");
                            writer.WriteStartElement("Service");
                            writer.WriteStartElement("Id");
                            writer.WriteValue(Resources.Research.ServiceId);
                            writer.WriteEndElement();
                            writer.WriteStartElement("Name");
                            writer.WriteValue(Resources.Research.ServiceName);
                            writer.WriteEndElement();
                            writer.WriteStartElement("Description");
                            writer.WriteValue(Resources.Research.ServiceDescription);
                            writer.WriteEndElement();
                            writer.WriteStartElement("Copyright");
                            writer.WriteValue(Resources.Research.Copyright);
                            writer.WriteEndElement();
                            writer.WriteRaw("<Display>On</Display>");
                            writer.WriteRaw("<Category>INTRANET_GENERAL</Category>");
                            writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                     writer.WriteEndElement();
                writer.WriteRaw("</ProviderUpdate>");

                writer.Flush();
                using (output)
                {
                    regString = output.ToString();
                }
            }

            return regString;

        }

        [WebMethod()]
        public String Status()
        {
            return string.Format(CultureInfo.InvariantCulture, 
                Resources.Resources.Status);
        }
    }
}