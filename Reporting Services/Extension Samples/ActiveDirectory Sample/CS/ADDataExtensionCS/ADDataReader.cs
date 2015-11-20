
/*======================================================================
  
  File:      ADDataReader.cs
  
  Summary:   Provides a means of reading one or more forward-only streams 
    of result sets obtained by executing a command at a data source, 
    and is implemented by AD Data Processing Extensions 
    that access Active Directory.

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
using System.Data.OleDb;

namespace Microsoft.Samples.ReportingServices.ADDataExtension
{
    public sealed class ADDataReader : IDataReader
    {

        #region Variables

        private ADConnection m_connection;
        internal System.Data.DataTable m_DataTable = new System.Data.DataTable();
        internal int m_currentRow = -1;

        #endregion


        #region Constructors
        /*
        * Because the user should not be able to directly create a 
        * DataReader object, the constructors are
        * marked as internal.
        */
        internal ADDataReader()
        {
            // TODO: Implement default constructor
        }

        internal ADDataReader(string cmdText)
        {
            // TODO: Implement command text only construtor
        }

        internal ADDataReader(string cmdText, ADConnection connection)
        {
            m_connection = connection;
        }

        #endregion


        #region IDataReader members
        /****
        * METHODS / PROPERTIES FROM IDataReader.
        ****/


    //Reads one row after the other. Advances the IDataReader to the next record.
    //The DataReader object enables a client to retrieve a read-only, forward-only stream of data 
    //from a data source. Results are returned as the query executes and are stored in the network buffer 
    //on the client until you request them using the Read method of the DataReader class
        bool IDataReader.Read()
        {
            m_currentRow = m_currentRow + 1;

            if (m_currentRow >= m_DataTable.Rows.Count)

                return false;

            else
                return true;
        }

        //Returns the total number of columns. Gets the number of fields in the data reader.
        int IDataReader.FieldCount
        {
            get { return m_DataTable.Columns.Count; }
        }

        //Returns the Datatype of the current column.
        Type IDataReader.GetFieldType(int fieldIndex)
        {
            return m_DataTable.Columns[fieldIndex].DataType;
        }

        //Gets the name of the field to find.
        string IDataReader.GetName(int fieldIndex)
        {
            return m_DataTable.Columns[fieldIndex].ColumnName;
        }

        //Return the index of the named field.
        int IDataReader.GetOrdinal(string fieldName)
        {
            return m_DataTable.Columns[fieldName].Ordinal;
        }

        //Return the value of the specified field.
        object IDataReader.GetValue(int fieldIndex)
        {
            return m_DataTable.Rows[m_currentRow][fieldIndex];
        }

        #endregion

        
        #region Dispose

        void IDisposable.Dispose()
        {
            // TODO: Displose implementation
        }

        #endregion


        /*
        * Implementation specific methods.
        */


        /*
        We are executing the command using the connection string that connects to the Active Directory.
        Hard coding of the connection string is because it is the same for all the ADs.
        Once we read the data using a DataReader, we place the same in a DataTable so that can be used for
        Other processings.
         */
                  
        public void GetData(string _commandText)
        {
            using (OleDbConnection adCon = new OleDbConnection("Provider=ADSDSOObject"))
            {
                OleDbCommand adCmd = new OleDbCommand(_commandText);
                OleDbDataReader adDataReader;

                try
                {
                    adCon.Open();
                    adCmd.Connection = adCon;
                    adDataReader = adCmd.ExecuteReader();
                    m_DataTable.Load(adDataReader);
                }
                catch
                {
                }
                finally
                {
                    adCon.Close();
                }
            }
         } 


    }
}
