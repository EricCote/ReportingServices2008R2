
#Region "Copyright Microsoft Corporation. All rights reserved."
'============================================================================
'   File:      AuthenticationStore.vb
'
'  Summary:  Demonstrates how to create and maintain a user store for
'            a security extension. 
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
Imports System.Security.Cryptography
Imports System.Text.RegularExpressions
Imports System.Web.Security
Imports System.Management
Imports System.Xml
Imports System.Text
Imports System.Globalization



<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")> Friend NotInheritable Class AuthenticationUtilities
    ' The path of any item in the report server database 
    ' has a maximum character length of 260
    Private Const MaxItemPathLength As Integer = 260
    Private Const wmiNamespace As String = "\root\Microsoft\SqlServer\ReportServer\{0}\v10"
    Private Const rsAsmx As String = "/ReportService2010.asmx"


    ' Method used to create a cryptographic random number that is used to
    ' salt the user's password for added security
    Friend Shared Function CreateSalt(ByVal size As Integer) As String
        ' Generate a cryptographic random number using the cryptographic
        ' service provider
        Dim rng As New RNGCryptoServiceProvider()
        Dim buff(size) As Byte
        rng.GetBytes(buff)
        ' Return a Base64 string representation of the random number
        Return Convert.ToBase64String(buff)

    End Function


    ' Returns a hash of the combined password and salt value
    Friend Shared Function CreatePasswordHash(ByVal pwd As String, ByVal salt As String) As String
        ' Concat the raw password and salt value
        Dim saltAndPwd As String = String.Concat(pwd, salt)
        ' Hash the salted password
        Dim hashedPwd As String = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "SHA1")

        Return hashedPwd

    End Function



    ' Stores the account details in a SQL table named UserAccounts
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")> Friend Shared Sub StoreAccountDetails(ByVal userName As String, ByVal passwordHash As String, ByVal salt As String)
        ' See "How To Use DPAPI (Machine Store) from ASP.NET" for 
        ' information about securely storing connection strings.
        Dim conn As New SqlConnection("Server=localhost;" + "Integrated Security=SSPI;" + "database=UserAccounts")
        Try
            Dim cmd As New SqlCommand("RegisterUser", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter = Nothing

            sqlParam = cmd.Parameters.Add("@userName", SqlDbType.VarChar, 40)
            sqlParam.Value = userName

            sqlParam = cmd.Parameters.Add("@passwordHash", SqlDbType.VarChar, 50)
            sqlParam.Value = passwordHash

            sqlParam = cmd.Parameters.Add("@salt", SqlDbType.VarChar, 10)
            sqlParam.Value = salt

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                ' Code to check for primary key violation (duplicate account 
                ' name) or other database errors omitted for clarity
                Throw New Exception(String.Format(CultureInfo.InvariantCulture, My.Resources.CustomSecurity.AddAccountError + ex.Message))
            End Try
        Finally
            conn.Dispose()
        End Try

    End Sub


    ' Method that indicates whether 
    ' the supplied username and password are valid
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")> Friend Shared Function VerifyPassword(ByVal suppliedUserName As String, ByVal suppliedPassword As String) As Boolean
        Dim passwordMatch As Boolean = False
        ' Get the salt and pwd from the database based on the user name.
        ' See "How To: Use DPAPI (Machine Store) from ASP.NET," "How To:
        ' Use DPAPI (User Store) from Enterprise Services," and "How To:
        ' Create a DPAPI Library" on MSDN for more information about 
        ' how to use DPAPI to securely store connection strings.
        Dim conn As New SqlConnection("Server=localhost;" + "Integrated Security=SSPI;" + "database=UserAccounts")
        Try
            Dim cmd As New SqlCommand("LookupUser", conn)
            cmd.CommandType = CommandType.StoredProcedure

            Dim sqlParam As SqlParameter = cmd.Parameters.Add("@userName", SqlDbType.VarChar, 255)
            sqlParam.Value = suppliedUserName
            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                Try
                    reader.Read() ' Advance to the one and only row
                    ' Return output parameters from returned data stream
                    Dim dbPasswordHash As String = reader.GetString(0)
                    Dim salt As String = reader.GetString(1)
                    ' Now take the salt and the password entered by the user
                    ' and concatenate them together.
                    Dim passwordAndSalt As String = String.Concat(suppliedPassword, salt)
                    ' Now hash them
                    Dim hashedPasswordAndSalt As String = FormsAuthentication.HashPasswordForStoringInConfigFile(passwordAndSalt, "SHA1")
                    ' Now verify them. Returns true if they are equal
                    passwordMatch = hashedPasswordAndSalt.Equals(dbPasswordHash)
                Finally
                    reader.Dispose()
                End Try
            Catch ex As Exception
                Throw New Exception(String.Format(CultureInfo.InvariantCulture, My.Resources.CustomSecurity.VerifyUserException + ex.Message))
            End Try
        Finally
            conn.Dispose()
        End Try
        Return passwordMatch

    End Function


    ' Method to verify that the user name does not contain any
    ' illegal characters. If My Reports is enabled, illegal characters
    ' will invalidate the paths created in the \Users folder. Usernames
    ' should not contain the characters captured by this method.
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId:="System.Int32.ToString")> _
    Friend Shared Function ValidateUserName(ByVal input As String) As Boolean
        Dim r As New Regex("\A(\..*)?[^\. ](.*[^ ])?\z(?<=\A[^/;\?:@&=\+\$,\\\*<>\|""]{0," + MaxItemPathLength.ToString() + "}\z)", RegexOptions.Compiled Or RegexOptions.ExplicitCapture)
        Dim isValid As Boolean = r.IsMatch(input)
        Return isValid

    End Function


    'Method to get the report server url using WMI
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> _
    Friend Shared Function GetReportServerUrl(ByVal machineName As String, ByVal instanceName As String) As String
        Dim reportServerVirtualDirectory As String = String.Empty
        Dim fullWmiNamespace As String = "\\" + machineName + String.Format(wmiNamespace, instanceName)

        Dim scope As ManagementScope = Nothing

        Dim connOptions As New ConnectionOptions()
        connOptions.Authentication = AuthenticationLevel.PacketPrivacy

        'Get management scope
        Try
            scope = New ManagementScope(fullWmiNamespace, connOptions)
            scope.Connect()

            'Get management class
            Dim path As New ManagementPath("MSReportServer_Instance")
            Dim options As New ObjectGetOptions()
            Dim serverClass As New ManagementClass(scope, path, options)

            serverClass.Get()

            If serverClass Is Nothing Then
                Throw New Exception(String.Format(CultureInfo.InvariantCulture, My.Resources.CustomSecurity.WMIClassError))
            End If

            'Get instances
            Dim instances As ManagementObjectCollection = serverClass.GetInstances()

            Dim instance As ManagementObject
            For Each instance In instances
                instance.Get()

                Dim outParams As ManagementBaseObject = CType(instance.InvokeMethod("GetReportServerUrls", Nothing, Nothing), _
                    ManagementBaseObject)

                Dim appNames As String() = CType(outParams("ApplicationName"), String())
                Dim urls As String() = CType(outParams("URLs"), String())

                For i As Integer = 0 To appNames.Length - 1
                    If appNames(i) = "ReportServerWebService" Then
                        reportServerVirtualDirectory = urls(i)
                    End If
                Next

                If reportServerVirtualDirectory = String.Empty Then
                    Throw New Exception(String.Format(CultureInfo.InvariantCulture, _
                        My.Resources.CustomSecurity.MissingUrlReservation))
                End If
            Next instance
        Catch ex As Exception
            Throw New Exception(String.Format(CultureInfo.InvariantCulture, My.Resources.CustomSecurity.RSUrlError + ex.Message))
        End Try

        Return reportServerVirtualDirectory + rsAsmx

    End Function
End Class