/*======================================================================
  
  File:      ADConnection.cs
  
  Summary:   Describes the attributes of a Connection, and the implementation
  of Active Directory Connection class for the Active Directory data 
  processing extension

------------------------------------------------------------------------
  This file is part of Microsoft SQL Server Code Samples.
  
  Copyright (C) Microsoft Corporation.  All rights reserved.

  This source code is intended only as a supplement to Microsoft
  Development Tools and/or on-line documentation.  See these other
  materials for detailed information regarding Microsoft code samples.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
======================================================================== */

using System;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Interfaces;
using System.Security.Permissions;
using System.Security.Principal;

namespace Microsoft.Samples.ReportingServices.ADDataExtension
{
    public sealed class ADConnection : IDbConnectionExtension
    {

        #region Variables

        private string m_conn;
        private System.Data.ConnectionState m_state
            = System.Data.ConnectionState.Closed;
        private string m_locName = "Active Directory Information";
        private string m_impersonate;
        private string m_username;
        private string m_password;

        private bool m_connectionOpened = false;
        private WindowsIdentity m_connectionUser = null;
        private bool m_integratedSecurity = false;

        #endregion

        #region Constructor

        public ADConnection()
        {
            // TODO: Implement default constructor
        }

        // Have a constructor that takes a connection string.
        public ADConnection(string conn)
        {
            // TODO: Implement constructor with a connection string
        }

        #endregion

        #region IDbConnection members


        //A public property which Gets or Sets the string used to open a database.
        string IDbConnection.ConnectionString
        {
            get
            {
                // Always return exactly what the user set.
                // Security-sensitive information may be removed.
                return m_conn;
            }
            set
            {
                m_conn = value;
            }
        }

        //Gets the time to wait, while trying to establish a connection, before terminating the attempt and generating an error.
        int IDbConnection.ConnectionTimeout
        {
            get
            {
                // Returns the connection time-out value set in the connection
                // string. Zero indicates an indefinite time-out period.
                return 0;
            }
        }


        public System.Data.ConnectionState State
        {
            get
            {
                return m_state;
            }
        }


        IDbTransaction IDbConnection.BeginTransaction()
        {
            throw new NotSupportedException();
        }

        //Opens a database connection with the settings specified by the ConnectionString property of the provider-specific Connection object.
        void IDbConnection.Open()
        {
            /*
            * Open the database connection and set the ConnectionState
            * property.
            */

            if (!m_connectionOpened)
            {
                if (m_integratedSecurity)
                {
                    m_connectionUser = WindowsIdentity.GetCurrent();
                }
                else
                {
                    m_connectionUser = null;
                }

                m_connectionOpened = true;
            }
        }

        void IDbConnection.Close()
        {
            /*
            * Close the database connection and set the ConnectionState
            * property. 
            */
            if (m_connectionUser != null)
            {
                m_connectionUser.Dispose();
            }
            m_connectionOpened = false;
        }

        IDbCommand IDbConnection.CreateCommand()
        {
            // Return a new instance of a command object.
            return new ADCommand(this);
        }


        #endregion

        #region IExtension Members

        string IExtension.LocalizedName
        {
            get
            {
                // Always return exactly what the user set.
                // Security-sensitive information may be removed.
                return m_locName;
            }
        }

        void IExtension.SetConfiguration(string configuration)
        {
            // Used to retrieve configuration data from the config file
            //TODO: Implement the Set configuration method based on the requirements
        }

        #endregion

        #region IDbConnectionExtension members

        /****
        * REQUIRED METHODS / PROPERTIES FROM IDbConnectionExtension.
        ****/
        /*
        * For data sources that require credentials, these properties
        * add support for storing secure credentials while designing
        * reports with Report Designer. The Data Source dialog will
        * include support for the Integrated checkbox as well as
        * text boxes for username and password.
        */
        bool IDbConnectionExtension.IntegratedSecurity
        {
            get
            {
                return m_integratedSecurity;
            }
            set
            {
                m_integratedSecurity = value;
            }
        }

        string IDbConnectionExtension.UserName
        {
            set
            {
                m_username = value;
            }
        }

        string IDbConnectionExtension.Password
        {
            set
            {
                m_password = value;
            }
        }

        string IDbConnectionExtension.Impersonate
        {
            set
            {
                m_impersonate = value;
            }
        }

        #endregion

        #region Dispose

        void IDisposable.Dispose()
        {
            // TODO: Displose implementation
        }

        #endregion


        internal WindowsIdentity ConnectionUser
        {
            get
            {
                // m_connectionUser is valid only during open connection
                if (!m_connectionOpened)
                {
                    throw new Exception();
                    //When the connection is not opened, throw an exception
                }
                return m_connectionUser;
            }
        }
    }
}
