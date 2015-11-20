
#Region "Copyright Microsoft Corporation. All rights reserved."
'============================================================================
'  File:      AuthenticationExtension.vb
'
'  Summary:  Demonstrates an implementation of an authentication 
'            extension.
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

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Principal
Imports System.Web
Imports Microsoft.ReportingServices.Interfaces
Imports System.Globalization

Public Class AuthenticationExtension
    Implements IAuthenticationExtension

    '<summary>
    'You must implement SetConfiguration as required by IExtension
    '</summary>
    '<param name="configuration">Configuration data as an XML
    'string that is stored along with the Extension element in
    'the configuration file.</param>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")> Public Sub SetConfiguration(ByVal configuration As String) Implements IAuthenticationExtension.SetConfiguration

    End Sub
    ' No configuration data is needed for this extension

    '<summary>
    'You must implement LocalizedName as required by IExtension
    '</summary>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")> Public ReadOnly Property LocalizedName() As String Implements IAuthenticationExtension.LocalizedName
        Get
            Return Nothing
        End Get
    End Property

    '<summary>
    'Indicates whether a supplied username and password are valid.
    '</summary>
    '<param name="userName">The supplied username</param>
    '<param name="password">The supplied password</param>
    '<param name="authority">Optional. The specific authority to use to
    'authenticate a user. For example, in Windows it would be a Windows 
    'Domain</param>
    '<returns>true when the username and password are valid</returns>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")> Public Function LogonUser(ByVal userName As String, ByVal password As String, ByVal authority As String) As Boolean Implements IAuthenticationExtension.LogonUser
        Return AuthenticationUtilities.VerifyPassword(userName, password)
    End Function


    '<summary>
    'Required by IAuthenticationExtension. The report server calls the 
    'GetUserInfo methodfor each request to retrieve the current user 
    'identity.
    '</summary>
    '<param name="userIdentity">represents the identity of the current 
    'user. The value of IIdentity may appear in a user interface and 
    'should be human readable</param>
    '<param name="userId">represents a pointer to a unique user identity
    '</param>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")> Public Sub GetUserInfo(ByRef userIdentity As IIdentity, ByRef userId As IntPtr) Implements IAuthenticationExtension.GetUserInfo
        ' If the current user identity is not null,
        ' set the userIdentity parameter to that of the current user 
        If Not (HttpContext.Current Is Nothing) AndAlso Not (HttpContext.Current.User Is Nothing) Then
            userIdentity = HttpContext.Current.User.Identity
        Else
            ' The current user identity is null. This happens when the user attempts an anonymous logon.
            ' Although it is ok to return userIdentity as a null reference, it is best to throw an appropriate
            ' exception for debugging purposes.
            ' To configure for anonymous logon, return a Gener
            System.Diagnostics.Debug.Assert(False, "Warning: userIdentity is null! Modify your code if you wish to support anonymous logon.")
            Throw New NullReferenceException("Anonymous logon is not configured. userIdentity should not be null!")
        End If
        ' initialize a pointer to the current user id to zero
        userId = IntPtr.Zero
    End Sub

    '<summary>
    'The IsValidPrincipalName method is called by the report server when 
    'the report server sets security on an item. This method validates 
    'that the user name is valid for Windows.The principal name needs to 
    'be a user, group, or builtin account name.
    '</summary>
    '<param name="principalName">A user, group, or built-in account name
    '</param>
    '<returns>true when the principle name is valid</returns>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")> Public Function IsValidPrincipalName(ByVal principalName As String) As Boolean Implements IAuthenticationExtension.IsValidPrincipalName
        Return VerifyUser(principalName)
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")> Public Shared Function VerifyUser(ByVal userName As String) As Boolean
        Dim isValid As Boolean = False
        Dim conn As New SqlConnection("Server=localhost;" + "Integrated Security=SSPI;" + "database=UserAccounts")
        Try
            Dim cmd As New SqlCommand("LookupUser", conn)
            cmd.CommandType = CommandType.StoredProcedure

            Dim sqlParam As SqlParameter = cmd.Parameters.Add("@userName", SqlDbType.VarChar, 255)
            sqlParam.Value = userName
            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                Try
                    ' If a row exists for the user, then the user is valid.
                    If reader.Read() Then
                        isValid = True
                    End If
                Finally
                    reader.Dispose()
                End Try
            Catch ex As Exception
                Throw New Exception(String.Format(CultureInfo.InvariantCulture, My.Resources.CustomSecurity.VerifyError + ex.Message))
            End Try
        Finally
            conn.Dispose()
        End Try

        Return isValid

    End Function
End Class