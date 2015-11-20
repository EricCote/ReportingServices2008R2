
#Region "Copyright ? Microsoft Corporation. All rights reserved."
'============================================================================
'  File:      AssemblyInfo.vb
'--------------------------------------------------------------------
'  This file is part of Microsoft SQL Server Code Samples.
'    
' This source code is intended only as a supplement to Microsoft
' Development Tools and/or on-line documentation. See these other
' materials for detailed information regarding Microsoft code 
' samples.
'
' THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF 
' ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
' THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'===========================================================================
#End Region

Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices


'
' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.
'

<assembly: AssemblyTitle("Forms Authentication Sample")>

<assembly: AssemblyDescription("Forms Authentication Sample")> '[assembly: AssemblyConfiguration("")]
'[assembly: AssemblyCompany("")]
'[assembly: AssemblyProduct("")]
'[assembly: AssemblyCopyright("")]
'[assembly: AssemblyTrademark("")]
'[assembly: AssemblyCulture("")]		
'
' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Revision and Build Numbers 
' by using the '*' as shown below:
<Assembly: ComVisible(False)> 
<Assembly: AssemblyVersion("1.0.*")> 
<Assembly: AssemblyFileVersion("1.0.0.0")> 
<Assembly: System.CLSCompliant(True)> 
<Assembly: System.Security.Permissions.SecurityPermissionAttribute(System.Security.Permissions.SecurityAction.RequestMinimum)> 
<Assembly: System.Runtime.ConstrainedExecution.ReliabilityContract(System.Runtime.ConstrainedExecution.Consistency.MayCorruptProcess, Runtime.ConstrainedExecution.Cer.None)> 

<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.ResourceOperation[]):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.ResourceOperation[]):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.ResourceOperation):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.ResourceOperation):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.ReportOperation):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.ReportOperation):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.ModelOperation):System.Boolean", MessageId:="3#")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.ModelOperation):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.ModelOperation):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.ModelItemOperation):System.Boolean", MessageId:="3#")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.ModelItemOperation):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.ModelItemOperation):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.FolderOperation[]):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.FolderOperation[]):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.FolderOperation):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.FolderOperation):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.DatasourceOperation):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.DatasourceOperation):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.CatalogOperation[]):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.CatalogOperation[]):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.CatalogOperation):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization.CheckAccess(System.String,System.IntPtr,System.Byte[],Microsoft.ReportingServices.Interfaces.CatalogOperation):System.Boolean")> 
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Scope:="member", Target:="Microsoft.Samples.ReportingServices.CustomSecurity.Authorization..cctor()")> 
