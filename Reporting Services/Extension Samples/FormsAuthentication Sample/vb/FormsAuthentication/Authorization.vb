
#Region "Copyright Microsoft Corporation. All rights reserved."
'============================================================================
'  File:     Authorization.vb
'
'  Summary:  Demonstrates an implementation of an authorization 
'            extension.
'------------------------------------------------------------------------------
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

Imports System
Imports System.IO
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Globalization
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports Microsoft.ReportingServices.Interfaces
Imports System.Xml

Public Class Authorization
    Implements IAuthorizationExtension
    Private Shared m_adminUserName As String
    
    Shared Sub New() 
        InitializeMaps()  
    End Sub
       
    '<summary>
    'Returns a security descriptor that is stored with an individual 
    'item in the report server database. 
    '</summary>
    '<param name="acl">The access code list (ACL) created by the report 
    'server for the item. It contains a collection of access code entry 
    '(ACE) structures.</param>
    '<param name="itemType">The type of item for which the security 
    'descriptor is created.</param>
    '<param name="stringSecDesc">Optional. A user-friendly description 
    'of the security descriptor, used for debugging. This is not stored
    'by the report server.</param>
    '<returns>Should be implemented to return a serialized access code 
    'list for the item.</returns>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")> Public Function CreateSecurityDescriptor(ByVal acl As AceCollection, ByVal itemType As SecurityItemType, ByRef stringSecDesc As String) As Byte() Implements IAuthorizationExtension.CreateSecurityDescriptor
        ' Creates a memory stream and serializes the ACL for storage.
        Dim bf As New BinaryFormatter()
        Dim result As New MemoryStream()
        Try
            bf.Serialize(result, acl)
            stringSecDesc = Nothing
            Return result.GetBuffer()
        Finally
            result.Close()
        End Try
    End Function

    Public Overloads Function CheckAccess(ByVal userName As String, ByVal userToken As IntPtr, ByVal secDesc() As Byte, ByVal modelItemOperation As ModelItemOperation) As Boolean Implements IAuthorizationExtension.CheckAccess
        ' If the user is the administrator, allow unrestricted access.
        ' Because SQL Server defaults to case-insensitive, we have to
        ' perform a case insensitive comparison. Ideally you would check
        ' the SQL Server instance CaseSensitivity property before making
        ' a case-insensitive comparison.
        If 0 = String.Compare(userName, m_adminUserName, True, CultureInfo.CurrentCulture) Then
            Return True
        End If
        Dim acl As AceCollection = DeserializeAcl(secDesc)
        Dim ace As AceStruct
        For Each ace In acl
            ' First check to see if the user or group has an access control 
            '  entry for the item
            If 0 = String.Compare(userName, ace.PrincipalName, True, CultureInfo.CurrentCulture) Then
                ' If an entry is found, 
                ' return true if the given required operation
                ' is contained in the ACE structure
                Dim aclOperation As ModelItemOperation
                For Each aclOperation In ace.ModelItemOperations
                    If aclOperation = modelItemOperation Then
                        Return True
                    End If
                Next aclOperation
            End If
        Next ace
        Return False
    End Function
    
    Public Overloads Function CheckAccess(ByVal userName As String, ByVal userToken As IntPtr, ByVal secDesc() As Byte, ByVal modelOperation As ModelOperation) As Boolean Implements IAuthorizationExtension.CheckAccess
        ' If the user is the administrator, allow unrestricted access.
        ' Because SQL Server defaults to case-insensitive, we have to
        ' perform a case insensitive comparison. Ideally you would check
        ' the SQL Server instance CaseSensitivity property before making
        ' a case-insensitive comparison.
        If 0 = String.Compare(userName, m_adminUserName, True, CultureInfo.CurrentCulture) Then
            Return True
        End If
        Dim acl As AceCollection = DeserializeAcl(secDesc)
        Dim ace As AceStruct
        For Each ace In acl
            ' First check to see if the user or group has an access control 
            '  entry for the item
            If 0 = String.Compare(userName, ace.PrincipalName, True, CultureInfo.CurrentCulture) Then
                ' If an entry is found, 
                ' return true if the given required operation
                ' is contained in the ACE structure
                Dim aclOperation As ModelOperation
                For Each aclOperation In ace.ModelOperations
                    If aclOperation = modelOperation Then
                        Return True
                    End If
                Next aclOperation
            End If
        Next ace
        Return False
    End Function
    
    '<summary>
    'Indicates whether a given user is authorized to access the item 
    'for a given catalog operation.
    '</summary>
    '<param name="userName">The name of the user as returned by the 
    'GetUserInfo method.</param>
    '<param name="userToken">Pointer to the user ID returned by 
    'GetUserInfo.</param>
    '<param name="secDesc">The security descriptor returned by 
    'CreateSecurityDescriptor.</param>
    '<param name="requiredOperation">The operation being requested by 
    'the report server for a given user.</param>
    '<returns>True if the user is authorized.</returns>
    Public Overloads Function CheckAccess(ByVal userName As String, ByVal userToken As IntPtr, ByVal secDesc() As Byte, ByVal requiredOperation As CatalogOperation) As Boolean Implements IAuthorizationExtension.CheckAccess
        ' If the user is the administrator, allow unrestricted access.
        ' Because SQL Server defaults to case-insensitive, we have to
        ' perform a case insensitive comparison. Ideally you would check
        ' the SQL Server instance CaseSensitivity property before making
        ' a case-insensitive comparison.
        If 0 = String.Compare(userName, m_adminUserName, True, CultureInfo.CurrentCulture) Then
            Return True
        End If
        Dim acl As AceCollection = DeserializeAcl(secDesc)
        Dim ace As AceStruct
        For Each ace In acl
            ' First check to see if the user or group has an access control 
            '  entry for the item
            If 0 = String.Compare(userName, ace.PrincipalName, True, CultureInfo.CurrentCulture) Then
                ' If an entry is found, 
                ' return true if the given required operation
                ' is contained in the ACE structure
                Dim aclOperation As CatalogOperation
                For Each aclOperation In ace.CatalogOperations
                    If aclOperation = requiredOperation Then
                        Return True
                    End If
                Next aclOperation
            End If
        Next ace
        Return False
    End Function

    ' Overload for array of catalog operations
    Public Overloads Function CheckAccess(ByVal userName As String, ByVal userToken As IntPtr, ByVal secDesc() As Byte, ByVal requiredOperations() As CatalogOperation) As Boolean Implements IAuthorizationExtension.CheckAccess
        Dim operation As CatalogOperation
        For Each operation In requiredOperations
            If Not CheckAccess(userName, userToken, secDesc, operation) Then
                Return False
            End If
        Next operation
        Return True
    End Function
       
    ' Overload for Report operations
    Public Overloads Function CheckAccess(ByVal userName As String, ByVal userToken As IntPtr, ByVal secDesc() As Byte, ByVal requiredOperation As ReportOperation) As Boolean Implements IAuthorizationExtension.CheckAccess
        ' If the user is the administrator, allow unrestricted access.
        If 0 = String.Compare(userName, m_adminUserName, True, CultureInfo.CurrentCulture) Then
            Return True
        End If
        Dim acl As AceCollection = DeserializeAcl(secDesc)
        Dim ace As AceStruct
        For Each ace In acl
            If 0 = String.Compare(userName, ace.PrincipalName, True, CultureInfo.CurrentCulture) Then
                Dim aclOperation As ReportOperation
                For Each aclOperation In ace.ReportOperations
                    If aclOperation = requiredOperation Then
                        Return True
                    End If
                Next aclOperation
            End If
        Next ace
        Return False
    End Function

    ' Overload for Folder operations
    Public Overloads Function CheckAccess(ByVal userName As String, ByVal userToken As IntPtr, ByVal secDesc() As Byte, ByVal requiredOperation As FolderOperation) As Boolean Implements IAuthorizationExtension.CheckAccess
        ' If the user is the administrator, allow unrestricted access.
        If 0 = String.Compare(userName, m_adminUserName, True, CultureInfo.CurrentCulture) Then
            Return True
        End If
        Dim acl As AceCollection = DeserializeAcl(secDesc)
        Dim ace As AceStruct
        For Each ace In acl
            If 0 = String.Compare(userName, ace.PrincipalName, True, CultureInfo.CurrentCulture) Then
                Dim aclOperation As FolderOperation
                For Each aclOperation In ace.FolderOperations
                    If aclOperation = requiredOperation Then
                        Return True
                    End If
                Next aclOperation
            End If
        Next ace
        Return False
    End Function
    
    ' Overload for an array of Folder operations
    Public Overloads Function CheckAccess(ByVal userName As String, ByVal userToken As IntPtr, ByVal secDesc() As Byte, ByVal requiredOperations() As FolderOperation) As Boolean Implements IAuthorizationExtension.CheckAccess
        Dim operation As FolderOperation
        For Each operation In requiredOperations
            If Not CheckAccess(userName, userToken, secDesc, operation) Then
                Return False
            End If
        Next operation
        Return True
    End Function
     
    ' Overload for Resource operations
    Public Overloads Function CheckAccess(ByVal userName As String, ByVal userToken As IntPtr, ByVal secDesc() As Byte, ByVal requiredOperation As ResourceOperation) As Boolean Implements IAuthorizationExtension.CheckAccess
        ' If the user is the administrator, allow unrestricted access.
        If 0 = String.Compare(userName, m_adminUserName, True, CultureInfo.CurrentCulture) Then
            Return True
        End If
        Dim acl As AceCollection = DeserializeAcl(secDesc)
        Dim ace As AceStruct
        For Each ace In acl
            If 0 = String.Compare(userName, ace.PrincipalName, True, CultureInfo.CurrentCulture) Then
                Dim aclOperation As ResourceOperation
                For Each aclOperation In ace.ResourceOperations
                    If aclOperation = requiredOperation Then
                        Return True
                    End If
                Next aclOperation
            End If
        Next ace
        Return False
    End Function
    
    ' Overload for an array of Resource operations
    Public Overloads Function CheckAccess(ByVal userName As String, ByVal userToken As IntPtr, ByVal secDesc() As Byte, ByVal requiredOperations() As ResourceOperation) As Boolean Implements IAuthorizationExtension.CheckAccess
        ' If the user is the administrator, allow unrestricted access.
        If 0 = String.Compare(userName, m_adminUserName, True, CultureInfo.CurrentCulture) Then
            Return True
        End If
        Dim operation As ResourceOperation
        For Each operation In requiredOperations
            If Not CheckAccess(userName, userToken, secDesc, operation) Then
                Return False
            End If
        Next operation
        Return True

    End Function
    
    ' Overload for Datasource operations
    Public Overloads Function CheckAccess(ByVal userName As String, ByVal userToken As IntPtr, ByVal secDesc() As Byte, ByVal requiredOperation As DatasourceOperation) As Boolean Implements IAuthorizationExtension.CheckAccess
        ' If the user is the administrator, allow unrestricted access.
        If 0 = String.Compare(userName, m_adminUserName, True, CultureInfo.CurrentCulture) Then
            Return True
        End If
        Dim acl As AceCollection = DeserializeAcl(secDesc)
        Dim ace As AceStruct
        For Each ace In acl
            If 0 = String.Compare(userName, ace.PrincipalName, True, CultureInfo.CurrentCulture) Then
                Dim aclOperation As DatasourceOperation
                For Each aclOperation In ace.DatasourceOperations
                    If aclOperation = requiredOperation Then
                        Return True
                    End If
                Next aclOperation
            End If
        Next ace
        Return False
    End Function
      
    '<summary>
    'Returns the set of permissions a specific user has for a specific 
    'item managed in the report server database. This provides underlying 
    'support for the Web service method GetPermissions().
    '</summary>
    '<param name="userName">The name of the user as returned by the 
    'GetUserInfo method.</param>
    '<param name="userToken">Pointer to the user ID returned by 
    'GetUserInfo.</param>
    '<param name="itemType">The type of item for which the permissions 
    'are returned.</param>
    '<param name="secDesc">The security descriptor associated with the 
    'item.</param>
    '<returns></returns>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")> _
    Public Function GetPermissions(ByVal userName As String, ByVal userToken As IntPtr, ByVal itemType As SecurityItemType, ByVal secDesc() As Byte) As StringCollection Implements IAuthorizationExtension.GetPermissions
        Dim permissions As New StringCollection()
        If 0 = String.Compare(userName, m_adminUserName, True, CultureInfo.CurrentCulture) Then
            For Each oper As CatalogOperation In m_CatOperNames.Keys
                If Not permissions.Contains(CStr(m_CatOperNames(oper))) Then
                    permissions.Add(CStr(m_CatOperNames(oper)))
                End If
            Next oper

            For Each oper As ModelItemOperation In m_ModelItemOperNames.Keys
                If Not permissions.Contains(CStr(m_ModelItemOperNames(oper))) Then
                    permissions.Add(CStr(m_ModelItemOperNames(oper)))
                End If
            Next oper

            For Each oper As ModelOperation In m_ModelOperNames.Keys
                If Not permissions.Contains(CStr(m_ModelOperNames(oper))) Then
                    permissions.Add(CStr(m_ModelOperNames(oper)))
                End If
            Next oper

            For Each oper As CatalogOperation In m_CatOperNames.Keys
                If Not permissions.Contains(CStr(m_CatOperNames(oper))) Then
                    permissions.Add(CStr(m_CatOperNames(oper)))
                End If
            Next oper

            For Each oper As ReportOperation In m_RptOperNames.Keys
                If Not permissions.Contains(CStr(m_RptOperNames(oper))) Then
                    permissions.Add(CStr(m_RptOperNames(oper)))
                End If
            Next oper

            For Each oper As FolderOperation In m_FldOperNames.Keys
                If Not permissions.Contains(CStr(m_FldOperNames(oper))) Then
                    permissions.Add(CStr(m_FldOperNames(oper)))
                End If
            Next oper

            For Each oper As ResourceOperation In m_ResOperNames.Keys
                If Not permissions.Contains(CStr(m_ResOperNames(oper))) Then
                    permissions.Add(CStr(m_ResOperNames(oper)))
                End If
            Next oper

            For Each oper As DatasourceOperation In m_DSOperNames.Keys
                If Not permissions.Contains(CStr(m_DSOperNames(oper))) Then
                    permissions.Add(CStr(m_DSOperNames(oper)))
                End If
            Next oper
        Else
            Dim acl As AceCollection = DeserializeAcl(secDesc)
            Dim ace As AceStruct
            For Each ace In acl
                If 0 = String.Compare(userName, ace.PrincipalName, True, CultureInfo.CurrentCulture) Then

                    For Each aclOperation As ModelItemOperation In ace.ModelItemOperations
                        If Not permissions.Contains(CStr(m_ModelItemOperNames(aclOperation))) Then
                            permissions.Add(CStr(m_ModelItemOperNames(aclOperation)))
                        End If
                    Next aclOperation

                    For Each aclOperation As ModelOperation In ace.ModelOperations
                        If Not permissions.Contains(CStr(m_ModelOperNames(aclOperation))) Then
                            permissions.Add(CStr(m_ModelOperNames(aclOperation)))
                        End If
                    Next aclOperation

                    For Each aclOperation As CatalogOperation In ace.CatalogOperations
                        If Not permissions.Contains(CStr(m_CatOperNames(aclOperation))) Then
                            permissions.Add(CStr(m_CatOperNames(aclOperation)))
                        End If
                    Next aclOperation

                    For Each aclOperation As ReportOperation In ace.ReportOperations
                        If Not permissions.Contains(CStr(m_RptOperNames(aclOperation))) Then
                            permissions.Add(CStr(m_RptOperNames(aclOperation)))
                        End If
                    Next aclOperation

                    For Each aclOperation As FolderOperation In ace.FolderOperations
                        If Not permissions.Contains(CStr(m_FldOperNames(aclOperation))) Then
                            permissions.Add(CStr(m_FldOperNames(aclOperation)))
                        End If
                    Next aclOperation

                    For Each aclOperation As ResourceOperation In ace.ResourceOperations
                        If Not permissions.Contains(CStr(m_ResOperNames(aclOperation))) Then
                            permissions.Add(CStr(m_ResOperNames(aclOperation)))
                        End If
                    Next aclOperation

                    For Each aclOperation As DatasourceOperation In ace.DatasourceOperations
                        If Not permissions.Contains(CStr(m_DSOperNames(aclOperation))) Then
                            permissions.Add(CStr(m_DSOperNames(aclOperation)))
                        End If
                    Next aclOperation
                End If
            Next ace
        End If
        Return permissions
    End Function
      
    ' Used to deserialize the ACL that is stored by the report server.
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")> Private Function DeserializeAcl(ByVal secDesc() As Byte) As AceCollection
        Dim acl As New AceCollection()
        If Not (secDesc Is Nothing) Then
            Dim bf As New BinaryFormatter()
            Dim sdStream As New MemoryStream(secDesc)
            Try
                acl = CType(bf.Deserialize(sdStream), AceCollection)
            Finally
                sdStream.Close()
            End Try
        End If
        Return acl
    End Function
    
    Private Shared m_ModelItemOperNames As Hashtable
    Private Shared m_ModelOperNames As Hashtable
    Private Shared m_CatOperNames As Hashtable
    Private Shared m_FldOperNames As Hashtable
    Private Shared m_RptOperNames As Hashtable
    Private Shared m_ResOperNames As Hashtable
    Private Shared m_DSOperNames As Hashtable

    Private Const NrRptOperations As Integer = 27
    Private Const NrFldOperations As Integer = 10
    Private Const NrResOperations As Integer = 7
    Private Const NrDSOperations As Integer = 7
    Private Const NrCatOperations As Integer = 16
    Private Const NrModelOperations As Integer = 11
    Private Const NrModelItemOperations As Integer = 1
    
    
    ' Utility method used to create mappings to the various
    ' operations in Reporting Services. These mappings support
    ' the implementation of the GetPermissions method.
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> Private Shared Sub InitializeMaps()
        ' create model operation names data
        m_ModelItemOperNames = New Hashtable()
        m_ModelItemOperNames.Add(ModelItemOperation.ReadProperties, OperationNames.OperReadProperties)

        If m_ModelItemOperNames.Count <> NrModelItemOperations Then
            'Model item name mismatch
            Throw New Exception(String.Format(CultureInfo.InvariantCulture, My.Resources.CustomSecurity.OperationNameError))
        End If

        ' create model operation names data
        m_ModelOperNames = New Hashtable()
        m_ModelOperNames.Add(ModelOperation.Delete, OperationNames.OperDelete)
        m_ModelOperNames.Add(ModelOperation.ReadAuthorizationPolicy, OperationNames.OperReadAuthorizationPolicy)
        m_ModelOperNames.Add(ModelOperation.ReadContent, OperationNames.OperReadContent)
        m_ModelOperNames.Add(ModelOperation.ReadDatasource, OperationNames.OperReadDatasources)
        m_ModelOperNames.Add(ModelOperation.ReadModelItemAuthorizationPolicies, OperationNames.OperReadModelItemSecurityPolicies)
        m_ModelOperNames.Add(ModelOperation.ReadProperties, OperationNames.OperReadProperties)
        m_ModelOperNames.Add(ModelOperation.UpdateContent, OperationNames.OperUpdateContent)
        m_ModelOperNames.Add(ModelOperation.UpdateDatasource, OperationNames.OperUpdateDatasources)
        m_ModelOperNames.Add(ModelOperation.UpdateDeleteAuthorizationPolicy, OperationNames.OperUpdateDeleteAuthorizationPolicy)
        m_ModelOperNames.Add(ModelOperation.UpdateModelItemAuthorizationPolicies, OperationNames.OperUpdateModelItemSecurityPolicies)
        m_ModelOperNames.Add(ModelOperation.UpdateProperties, OperationNames.OperUpdatePolicy)

        If m_ModelOperNames.Count <> NrModelOperations Then
            'Model name mismatch
            Throw New Exception(String.Format(CultureInfo.InvariantCulture, My.Resources.CustomSecurity.OperationNameError))
        End If

        ' create operation names data
        m_CatOperNames = New Hashtable()
        m_CatOperNames.Add(CatalogOperation.CreateRoles, OperationNames.OperCreateRoles)
        m_CatOperNames.Add(CatalogOperation.DeleteRoles, OperationNames.OperDeleteRoles)
        m_CatOperNames.Add(CatalogOperation.ReadRoleProperties, OperationNames.OperReadRoleProperties)
        m_CatOperNames.Add(CatalogOperation.UpdateRoleProperties, OperationNames.OperUpdateRoleProperties)
        m_CatOperNames.Add(CatalogOperation.ReadSystemProperties, OperationNames.OperReadSystemProperties)
        m_CatOperNames.Add(CatalogOperation.UpdateSystemProperties, OperationNames.OperUpdateSystemProperties)
        m_CatOperNames.Add(CatalogOperation.GenerateEvents, OperationNames.OperGenerateEvents)
        m_CatOperNames.Add(CatalogOperation.ReadSystemSecurityPolicy, OperationNames.OperReadSystemSecurityPolicy)
        m_CatOperNames.Add(CatalogOperation.UpdateSystemSecurityPolicy, OperationNames.OperUpdateSystemSecurityPolicy)
        m_CatOperNames.Add(CatalogOperation.CreateSchedules, OperationNames.OperCreateSchedules)
        m_CatOperNames.Add(CatalogOperation.DeleteSchedules, OperationNames.OperDeleteSchedules)
        m_CatOperNames.Add(CatalogOperation.ReadSchedules, OperationNames.OperReadSchedules)
        m_CatOperNames.Add(CatalogOperation.UpdateSchedules, OperationNames.OperUpdateSchedules)
        m_CatOperNames.Add(CatalogOperation.ListJobs, OperationNames.OperListJobs)
        m_CatOperNames.Add(CatalogOperation.CancelJobs, OperationNames.OperCancelJobs)
        m_CatOperNames.Add(CatalogOperation.ExecuteReportDefinition, OperationNames.ExecuteReportDefinition) 

        If m_CatOperNames.Count <> NrCatOperations Then
            'Catalog name mismatch
            Throw New Exception(String.Format(CultureInfo.InvariantCulture, My.Resources.CustomSecurity.OperationNameError))
        End If

        m_FldOperNames = New Hashtable()
        m_FldOperNames.Add(FolderOperation.CreateFolder, OperationNames.OperCreateFolder)
        m_FldOperNames.Add(FolderOperation.Delete, OperationNames.OperDelete)
        m_FldOperNames.Add(FolderOperation.ReadProperties, OperationNames.OperReadProperties)
        m_FldOperNames.Add(FolderOperation.UpdateProperties, OperationNames.OperUpdateProperties)
        m_FldOperNames.Add(FolderOperation.CreateReport, OperationNames.OperCreateReport)
        m_FldOperNames.Add(FolderOperation.CreateResource, OperationNames.OperCreateResource)
        m_FldOperNames.Add(FolderOperation.ReadAuthorizationPolicy, OperationNames.OperReadAuthorizationPolicy)
        m_FldOperNames.Add(FolderOperation.UpdateDeleteAuthorizationPolicy, OperationNames.OperUpdateDeleteAuthorizationPolicy)
        m_FldOperNames.Add(FolderOperation.CreateDatasource, OperationNames.OperCreateDatasource)
        m_FldOperNames.Add(FolderOperation.CreateModel, OperationNames.OperCreateModel) 

        If m_FldOperNames.Count <> NrFldOperations Then
            'Folder name mismatch
            Throw New Exception(String.Format(CultureInfo.InvariantCulture, My.Resources.CustomSecurity.OperationNameError))
        End If

        m_RptOperNames = New Hashtable()
        m_RptOperNames.Add(ReportOperation.Delete, OperationNames.OperDelete)
        m_RptOperNames.Add(ReportOperation.ReadProperties, OperationNames.OperReadProperties)
        m_RptOperNames.Add(ReportOperation.UpdateProperties, OperationNames.OperUpdateProperties)
        m_RptOperNames.Add(ReportOperation.UpdateParameters, OperationNames.OperUpdateParameters)
        m_RptOperNames.Add(ReportOperation.ReadDatasource, OperationNames.OperReadDatasources)
        m_RptOperNames.Add(ReportOperation.UpdateDatasource, OperationNames.OperUpdateDatasources)
        m_RptOperNames.Add(ReportOperation.ReadReportDefinition, OperationNames.OperReadReportDefinition)
        m_RptOperNames.Add(ReportOperation.UpdateReportDefinition, OperationNames.OperUpdateReportDefinition)
        m_RptOperNames.Add(ReportOperation.CreateSubscription, OperationNames.OperCreateSubscription)
        m_RptOperNames.Add(ReportOperation.DeleteSubscription, OperationNames.OperDeleteSubscription)
        m_RptOperNames.Add(ReportOperation.ReadSubscription, OperationNames.OperReadSubscription)
        m_RptOperNames.Add(ReportOperation.UpdateSubscription, OperationNames.OperUpdateSubscription)
        m_RptOperNames.Add(ReportOperation.CreateAnySubscription, OperationNames.OperCreateAnySubscription)
        m_RptOperNames.Add(ReportOperation.DeleteAnySubscription, OperationNames.OperDeleteAnySubscription)
        m_RptOperNames.Add(ReportOperation.ReadAnySubscription, OperationNames.OperReadAnySubscription)
        m_RptOperNames.Add(ReportOperation.UpdateAnySubscription, OperationNames.OperUpdateAnySubscription)
        m_RptOperNames.Add(ReportOperation.UpdatePolicy, OperationNames.OperUpdatePolicy)
        m_RptOperNames.Add(ReportOperation.ReadPolicy, OperationNames.OperReadPolicy)
        m_RptOperNames.Add(ReportOperation.DeleteHistory, OperationNames.OperDeleteHistory)
        m_RptOperNames.Add(ReportOperation.ListHistory, OperationNames.OperListHistory)
        m_RptOperNames.Add(ReportOperation.ExecuteAndView, OperationNames.OperExecuteAndView)
        m_RptOperNames.Add(ReportOperation.CreateResource, OperationNames.OperCreateResource)
        m_RptOperNames.Add(ReportOperation.CreateSnapshot, OperationNames.OperCreateSnapshot)
        m_RptOperNames.Add(ReportOperation.ReadAuthorizationPolicy, OperationNames.OperReadAuthorizationPolicy)
        m_RptOperNames.Add(ReportOperation.UpdateDeleteAuthorizationPolicy, OperationNames.OperUpdateDeleteAuthorizationPolicy)
        m_RptOperNames.Add(ReportOperation.Execute, OperationNames.OperExecute)
        m_RptOperNames.Add(ReportOperation.CreateLink, OperationNames.OperCreateLink)

        If m_RptOperNames.Count <> NrRptOperations Then
            'Report name mismatch
            Throw New Exception(String.Format(CultureInfo.InvariantCulture, My.Resources.CustomSecurity.OperationNameError))
        End If

        m_ResOperNames = New Hashtable()
        m_ResOperNames.Add(ResourceOperation.Delete, OperationNames.OperDelete)
        m_ResOperNames.Add(ResourceOperation.ReadProperties, OperationNames.OperReadProperties)
        m_ResOperNames.Add(ResourceOperation.UpdateProperties, OperationNames.OperUpdateProperties)
        m_ResOperNames.Add(ResourceOperation.ReadContent, OperationNames.OperReadContent)
        m_ResOperNames.Add(ResourceOperation.UpdateContent, OperationNames.OperUpdateContent)
        m_ResOperNames.Add(ResourceOperation.ReadAuthorizationPolicy, OperationNames.OperReadAuthorizationPolicy)
        m_ResOperNames.Add(ResourceOperation.UpdateDeleteAuthorizationPolicy, OperationNames.OperUpdateDeleteAuthorizationPolicy)

        If m_ResOperNames.Count <> NrResOperations Then
            'Resource name mismatch
            Throw New Exception(String.Format(CultureInfo.InvariantCulture, My.Resources.CustomSecurity.OperationNameError))
        End If

        m_DSOperNames = New Hashtable()
        m_DSOperNames.Add(DatasourceOperation.Delete, OperationNames.OperDelete)
        m_DSOperNames.Add(DatasourceOperation.ReadProperties, OperationNames.OperReadProperties)
        m_DSOperNames.Add(DatasourceOperation.UpdateProperties, OperationNames.OperUpdateProperties)
        m_DSOperNames.Add(DatasourceOperation.ReadContent, OperationNames.OperReadContent)
        m_DSOperNames.Add(DatasourceOperation.UpdateContent, OperationNames.OperUpdateContent)
        m_DSOperNames.Add(DatasourceOperation.ReadAuthorizationPolicy, OperationNames.OperReadAuthorizationPolicy)
        m_DSOperNames.Add(DatasourceOperation.UpdateDeleteAuthorizationPolicy, OperationNames.OperUpdateDeleteAuthorizationPolicy)

        If m_DSOperNames.Count <> NrDSOperations Then
            'Datasource name mismatch
            Throw New Exception(String.Format(CultureInfo.InvariantCulture, My.Resources.CustomSecurity.OperationNameError))
        End If
    End Sub
    
    '/ <summary>
    '/ You must implement SetConfiguration as required by IExtension
    '/ </summary>
    '/ <param name="configuration">Configuration data as an XML
    '/ string that is stored along with the Extension element in
    '/ the configuration file.</param>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> _
    Public Overloads Sub SetConfiguration(ByVal configuration As String) Implements IAuthorizationExtension.SetConfiguration
        ' Retrieve admin user and password from the config settings
        ' and verify
        Dim doc As New XmlDocument()
        doc.LoadXml(configuration)
        If doc.DocumentElement.Name = "AdminConfiguration" Then
            Dim child As XmlNode
            For Each child In doc.DocumentElement.ChildNodes
                If child.Name = "UserName" Then
                    m_adminUserName = child.InnerText
                Else
                    Throw New Exception(String.Format(CultureInfo.InvariantCulture, My.Resources.CustomSecurity.UnrecognizedElement))
                End If
            Next child
        Else
            Throw New Exception(String.Format(CultureInfo.InvariantCulture, My.Resources.CustomSecurity.AdminConfiguration))
        End If
    End Sub
    
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")> Public ReadOnly Property LocalizedName() As String Implements IAuthorizationExtension.LocalizedName
        Get
            ' Return a localized name for this extension
            Return Nothing
        End Get
    End Property
End Class