/*======================================================================
  
  File:      ADDataParameterCollection.cs
  
  Summary:   Represents a collection of parameters.

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
using System.Collections;
using System.Globalization;

namespace Microsoft.Samples.ReportingServices.ADDataExtension
{
    /*
    * Because IDataParameterCollection is primarily an IList,
    * the sample can use an existing class for most of the implementation.
    * In this sample, we use ArrayList. 
    */

    public sealed class ADDataParameterCollection : ArrayList, IDataParameterCollection
    {
        public object this[string index]
        {
            get
            {
                return this[IndexOf(index)];
            }
            set
            {
                this[IndexOf(index)] = value;
            }
        }

        public override int Add(object value)
        {
            return Add((IDataParameter)value);
        }

        int IDataParameterCollection.Add(IDataParameter parameter)
        {
            if (parameter.ParameterName != null)
            {
                return base.Add(parameter);
            }
            else
            {
                throw new ArgumentException("parameter must be named");
            }
        }
    }
}
