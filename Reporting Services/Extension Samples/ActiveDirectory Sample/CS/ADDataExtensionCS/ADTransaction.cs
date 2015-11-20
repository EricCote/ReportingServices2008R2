
/*======================================================================
  
  File:      ADTransaction.cs
  
  Summary:   Represents a transaction to be performed at a data source.
    This is not applicable for the current Data Extension

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
    public sealed class ADTransaction : IDbTransaction
    {
        void IDbTransaction.Commit()
        {
            /*
            * Implement Commit here. Although the template does
            * not provide an implementation, it should never be 
            * a no-op because data corruption could result.
            */
        }

        void IDbTransaction.Rollback()
        {
            /*
            * Implement Rollback here. Although the template does
            * not provide an implementation, it should never be
            * a no-op because data corruption could result.
            */
        }

        void IDisposable.Dispose()
        {
            // TODO: Displose implementation
        }
    }
}

