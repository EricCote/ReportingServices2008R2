/*======================================================================
  
  File:      ADCommand.cs
  
  Summary:   Describes the attributes of a command, and the implementation
  Active Directory Command class for the Active Directory data 
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


namespace Microsoft.Samples.ReportingServices.ADDataExtension
{
    public sealed class ADCommand : IDbCommand
    {

        #region Variables

        private ADConnection m_connection;
        private ADTransaction m_txn;
        private string m_cmdText;

        private ADDataParameterCollection parameters
            = new ADDataParameterCollection();

        #endregion

        #region Constructors

        public ADCommand()
        {
            // Implement the default constructor here
        }

        // Implement other constructors here
        public ADCommand(ADConnection connection)
        {
            m_connection = connection;
        }

        public ADCommand(string cmdText)
        {
            m_cmdText = cmdText;
        }

        public ADCommand(string cmdText, ADConnection connection)
        {
            m_cmdText = cmdText;
            m_connection = connection;
        }

        public ADCommand(string cmdText, ADConnection connection, ADTransaction txn)
        {
            m_cmdText = cmdText;
            m_connection = connection;
            m_txn = txn;
        }

        #endregion

        #region IDbCommand Members

        /****
        * IMPLEMENT THE REQUIRED PROPERTIES.
        ****/
        string IDbCommand.CommandText
        {
            get
            {
                return m_cmdText;
            }
            set
            {
                m_cmdText = value;
            }
        }


        //Gets or sets the wait time before terminating the attempt to execute a command and generating an error.
        int IDbCommand.CommandTimeout
        {
            /*
            * The sample does not support a command time-out. 
            */
            get
            {
                return 0;
            }
            set
            {
            }
        }

        CommandType IDbCommand.CommandType
        {
            /*
            * The sample only supports CommandType.Text.
            */
            get { return CommandType.Text; }
            set { if (value != CommandType.Text) throw new NotSupportedException(); }
        }


        IDataParameterCollection IDbCommand.Parameters
        {
            get { return parameters; }
        }


        IDbTransaction IDbCommand.Transaction
        {
            /*
            * Transactions are not supported with Active Directory.
            */
            get 
            {
                throw new NotSupportedException();
            }
            set 
            {
                throw new NotSupportedException();
            }
        }


        /****
        * IMPLEMENT THE REQUIRED METHODS.
        ****/
        void IDbCommand.Cancel()
        {
            // The sample does not support canceling a command
            // once it has been initiated.
            throw new NotSupportedException();
        }

        //Creates a new instance of an IDataParameter object.Here it is ADDataParameter.
        IDataParameter IDbCommand.CreateParameter()
        {
            return (IDataParameter)(new ADDataParameter());
        }


        //Executes the CommandText against the Connection and builds an IDataReader.
        IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
        {
            //We call the GetData method of the DataReader method 
            //This method basically connects to the Active Directory and reads the data into a DataTable
            ADDataReader reader = new ADDataReader();
            reader.GetData(m_cmdText);
            return reader;
        }

        #endregion

        #region Dispose
        void IDisposable.Dispose()
        {
            /*
            * Dispose of the object and perform any cleanup.
            */
            // TODO: Displose implementation
        }
        #endregion
    }
}

