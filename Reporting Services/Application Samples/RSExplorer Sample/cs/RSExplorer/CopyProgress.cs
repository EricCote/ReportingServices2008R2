#region "Copyright © Microsoft Corporation. All rights reserved."

/*=============================================================================
  File:      CopyProgress.cs

  Summary:  Simple dialog for displaying the progress of copying items
         in the report server database.

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
=============================================================================*/

#endregion

#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#endregion

namespace Microsoft.Samples.ReportingServices.RSExplorer
{
    partial class CopyProgress : Form
    {
        public CopyProgress()
        {
            InitializeComponent();
        }

        public ProgressBar ProgressBar
        {
            get
            {
                return progressBar1;
            }
        }
    }
}