Namespace ReportExecution2005

    '''<remarks/>
    <System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="ReportExecutionServiceSoap", [Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices"), _
     System.Xml.Serialization.XmlIncludeAttribute(GetType(ParameterValueOrFieldReference))> _
    Partial Public Class ReportExecutionService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol

        Private serverInfoHeaderValueField As ServerInfoHeader

        Private ListSecureMethodsOperationCompleted As System.Threading.SendOrPostCallback

        Private executionHeaderValueField As ExecutionHeader

        Private LoadReportOperationCompleted As System.Threading.SendOrPostCallback

        Private LoadReportDefinitionOperationCompleted As System.Threading.SendOrPostCallback

        Private SetExecutionCredentialsOperationCompleted As System.Threading.SendOrPostCallback

        Private SetExecutionParametersOperationCompleted As System.Threading.SendOrPostCallback

        Private ResetExecutionOperationCompleted As System.Threading.SendOrPostCallback

        Private RenderOperationCompleted As System.Threading.SendOrPostCallback

        Private RenderStreamOperationCompleted As System.Threading.SendOrPostCallback

        Private GetExecutionInfoOperationCompleted As System.Threading.SendOrPostCallback

        Private GetDocumentMapOperationCompleted As System.Threading.SendOrPostCallback

        Private LoadDrillthroughTargetOperationCompleted As System.Threading.SendOrPostCallback

        Private ToggleItemOperationCompleted As System.Threading.SendOrPostCallback

        Private NavigateDocumentMapOperationCompleted As System.Threading.SendOrPostCallback

        Private NavigateBookmarkOperationCompleted As System.Threading.SendOrPostCallback

        Private FindStringOperationCompleted As System.Threading.SendOrPostCallback

        Private SortOperationCompleted As System.Threading.SendOrPostCallback

        Private GetRenderResourceOperationCompleted As System.Threading.SendOrPostCallback

        Private ListRenderingExtensionsOperationCompleted As System.Threading.SendOrPostCallback

        Private LogonUserOperationCompleted As System.Threading.SendOrPostCallback

        Private LogoffOperationCompleted As System.Threading.SendOrPostCallback

        Private useDefaultCredentialsSetExplicitly As Boolean

        '''<remarks/>
        Public Sub New()
            MyBase.New()
            If (Me.IsLocalFileSystemWebService(Me.Url) = True) Then
                Me.UseDefaultCredentials = True
                Me.useDefaultCredentialsSetExplicitly = False
            Else
                Me.useDefaultCredentialsSetExplicitly = True
            End If
        End Sub

        Public Property ServerInfoHeaderValue() As ServerInfoHeader
            Get
                Return Me.serverInfoHeaderValueField
            End Get
            Set(ByVal value As ServerInfoHeader)
                Me.serverInfoHeaderValueField = value
            End Set
        End Property

        Public Property ExecutionHeaderValue() As ExecutionHeader
            Get
                Return Me.executionHeaderValueField
            End Get
            Set(ByVal value As ExecutionHeader)
                Me.executionHeaderValueField = value
            End Set
        End Property

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")> Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set(ByVal value As String)
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = True) _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = False)) _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = False)) Then
                    MyBase.UseDefaultCredentials = False
                End If
                MyBase.Url = value
            End Set
        End Property

        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set(ByVal value As Boolean)
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = True
            End Set
        End Property

        '''<remarks/>
        Public Event ListSecureMethodsCompleted As ListSecureMethodsCompletedEventHandler

        '''<remarks/>
        Public Event LoadReportCompleted As LoadReportCompletedEventHandler

        '''<remarks/>
        Public Event LoadReportDefinitionCompleted As LoadReportDefinitionCompletedEventHandler

        '''<remarks/>
        Public Event SetExecutionCredentialsCompleted As SetExecutionCredentialsCompletedEventHandler

        '''<remarks/>
        Public Event SetExecutionParametersCompleted As SetExecutionParametersCompletedEventHandler

        '''<remarks/>
        Public Event ResetExecutionCompleted As ResetExecutionCompletedEventHandler

        '''<remarks/>
        Public Event RenderCompleted As RenderCompletedEventHandler

        '''<remarks/>
        Public Event RenderStreamCompleted As RenderStreamCompletedEventHandler

        '''<remarks/>
        Public Event GetExecutionInfoCompleted As GetExecutionInfoCompletedEventHandler

        '''<remarks/>
        Public Event GetDocumentMapCompleted As GetDocumentMapCompletedEventHandler

        '''<remarks/>
        Public Event LoadDrillthroughTargetCompleted As LoadDrillthroughTargetCompletedEventHandler

        '''<remarks/>
        Public Event ToggleItemCompleted As ToggleItemCompletedEventHandler

        '''<remarks/>
        Public Event NavigateDocumentMapCompleted As NavigateDocumentMapCompletedEventHandler

        '''<remarks/>
        Public Event NavigateBookmarkCompleted As NavigateBookmarkCompletedEventHandler

        '''<remarks/>
        Public Event FindStringCompleted As FindStringCompletedEventHandler

        '''<remarks/>
        Public Event SortCompleted As SortCompletedEventHandler

        '''<remarks/>
        Public Event GetRenderResourceCompleted As GetRenderResourceCompletedEventHandler

        '''<remarks/>
        Public Event ListRenderingExtensionsCompleted As ListRenderingExtensionsCompletedEventHandler

        '''<remarks/>
        Public Event LogonUserCompleted As LogonUserCompletedEventHandler

        '''<remarks/>
        Public Event LogoffCompleted As LogoffCompletedEventHandler

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Lis" & _
            "tSecureMethods", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ListSecureMethods() As String()
            Dim results() As Object = Me.Invoke("ListSecureMethods", New Object(-1) {})
            Return CType(results(0), String())
        End Function

        '''<remarks/>
        Public Overloads Sub ListSecureMethodsAsync()
            Me.ListSecureMethodsAsync(Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub ListSecureMethodsAsync(ByVal userState As Object)
            If (Me.ListSecureMethodsOperationCompleted Is Nothing) Then
                Me.ListSecureMethodsOperationCompleted = AddressOf Me.OnListSecureMethodsOperationCompleted
            End If
            Me.InvokeAsync("ListSecureMethods", New Object(-1) {}, Me.ListSecureMethodsOperationCompleted, userState)
        End Sub

        Private Sub OnListSecureMethodsOperationCompleted(ByVal arg As Object)
            If (Not (Me.ListSecureMethodsCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ListSecureMethodsCompleted(Me, New ListSecureMethodsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId:="1#")> <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Loa" & _
            "dReport", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function LoadReport(ByVal Report As String, ByVal HistoryID As String) As <System.Xml.Serialization.XmlElementAttribute("executionInfo")> ExecutionInfo
            Dim results() As Object = Me.Invoke("LoadReport", New Object() {Report, HistoryID})
            Return CType(results(0), ExecutionInfo)
        End Function

        '''<remarks/>
        Public Overloads Sub LoadReportAsync(ByVal Report As String, ByVal HistoryID As String)
            Me.LoadReportAsync(Report, HistoryID, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub LoadReportAsync(ByVal Report As String, ByVal HistoryID As String, ByVal userState As Object)
            If (Me.LoadReportOperationCompleted Is Nothing) Then
                Me.LoadReportOperationCompleted = AddressOf Me.OnLoadReportOperationCompleted
            End If
            Me.InvokeAsync("LoadReport", New Object() {Report, HistoryID}, Me.LoadReportOperationCompleted, userState)
        End Sub

        Private Sub OnLoadReportOperationCompleted(ByVal arg As Object)
            If (Not (Me.LoadReportCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent LoadReportCompleted(Me, New LoadReportCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId:="1#")> <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Loa" & _
            "dReportDefinition", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function LoadReportDefinition(<System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> ByVal Definition() As Byte, ByRef warnings() As Warning) As <System.Xml.Serialization.XmlElementAttribute("executionInfo")> ExecutionInfo
            Dim results() As Object = Me.Invoke("LoadReportDefinition", New Object() {Definition})
            warnings = CType(results(1), Warning())
            Return CType(results(0), ExecutionInfo)
        End Function

        '''<remarks/>
        Public Overloads Sub LoadReportDefinitionAsync(ByVal Definition() As Byte)
            Me.LoadReportDefinitionAsync(Definition, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub LoadReportDefinitionAsync(ByVal Definition() As Byte, ByVal userState As Object)
            If (Me.LoadReportDefinitionOperationCompleted Is Nothing) Then
                Me.LoadReportDefinitionOperationCompleted = AddressOf Me.OnLoadReportDefinitionOperationCompleted
            End If
            Me.InvokeAsync("LoadReportDefinition", New Object() {Definition}, Me.LoadReportDefinitionOperationCompleted, userState)
        End Sub

        Private Sub OnLoadReportDefinitionOperationCompleted(ByVal arg As Object)
            If (Not (Me.LoadReportDefinitionCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent LoadReportDefinitionCompleted(Me, New LoadReportDefinitionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Set" & _
            "ExecutionCredentials", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function SetExecutionCredentials(ByVal Credentials() As DataSourceCredentials) As <System.Xml.Serialization.XmlElementAttribute("executionInfo")> ExecutionInfo
            Dim results() As Object = Me.Invoke("SetExecutionCredentials", New Object() {Credentials})
            Return CType(results(0), ExecutionInfo)
        End Function

        '''<remarks/>
        Public Overloads Sub SetExecutionCredentialsAsync(ByVal Credentials() As DataSourceCredentials)
            Me.SetExecutionCredentialsAsync(Credentials, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub SetExecutionCredentialsAsync(ByVal Credentials() As DataSourceCredentials, ByVal userState As Object)
            If (Me.SetExecutionCredentialsOperationCompleted Is Nothing) Then
                Me.SetExecutionCredentialsOperationCompleted = AddressOf Me.OnSetExecutionCredentialsOperationCompleted
            End If
            Me.InvokeAsync("SetExecutionCredentials", New Object() {Credentials}, Me.SetExecutionCredentialsOperationCompleted, userState)
        End Sub

        Private Sub OnSetExecutionCredentialsOperationCompleted(ByVal arg As Object)
            If (Not (Me.SetExecutionCredentialsCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent SetExecutionCredentialsCompleted(Me, New SetExecutionCredentialsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Set" & _
            "ExecutionParameters", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function SetExecutionParameters(ByVal Parameters() As ParameterValue, ByVal ParameterLanguage As String) As <System.Xml.Serialization.XmlElementAttribute("executionInfo")> ExecutionInfo
            Dim results() As Object = Me.Invoke("SetExecutionParameters", New Object() {Parameters, ParameterLanguage})
            Return CType(results(0), ExecutionInfo)
        End Function

        '''<remarks/>
        Public Overloads Sub SetExecutionParametersAsync(ByVal Parameters() As ParameterValue, ByVal ParameterLanguage As String)
            Me.SetExecutionParametersAsync(Parameters, ParameterLanguage, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub SetExecutionParametersAsync(ByVal Parameters() As ParameterValue, ByVal ParameterLanguage As String, ByVal userState As Object)
            If (Me.SetExecutionParametersOperationCompleted Is Nothing) Then
                Me.SetExecutionParametersOperationCompleted = AddressOf Me.OnSetExecutionParametersOperationCompleted
            End If
            Me.InvokeAsync("SetExecutionParameters", New Object() {Parameters, ParameterLanguage}, Me.SetExecutionParametersOperationCompleted, userState)
        End Sub

        Private Sub OnSetExecutionParametersOperationCompleted(ByVal arg As Object)
            If (Not (Me.SetExecutionParametersCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent SetExecutionParametersCompleted(Me, New SetExecutionParametersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Res" & _
            "etExecution", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ResetExecution() As <System.Xml.Serialization.XmlElementAttribute("executionInfo")> ExecutionInfo
            Dim results() As Object = Me.Invoke("ResetExecution", New Object(-1) {})
            Return CType(results(0), ExecutionInfo)
        End Function

        '''<remarks/>
        Public Overloads Sub ResetExecutionAsync()
            Me.ResetExecutionAsync(Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub ResetExecutionAsync(ByVal userState As Object)
            If (Me.ResetExecutionOperationCompleted Is Nothing) Then
                Me.ResetExecutionOperationCompleted = AddressOf Me.OnResetExecutionOperationCompleted
            End If
            Me.InvokeAsync("ResetExecution", New Object(-1) {}, Me.ResetExecutionOperationCompleted, userState)
        End Sub

        Private Sub OnResetExecutionOperationCompleted(ByVal arg As Object)
            If (Not (Me.ResetExecutionCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ResetExecutionCompleted(Me, New ResetExecutionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId:="5#")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId:="4#")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId:="3#")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId:="2#")> <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Ren" & _
            "der", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Render(ByVal Format As String, ByVal DeviceInfo As String, ByRef MimeType As String, ByRef Encoding As String, ByRef Warnings() As Warning, ByRef StreamIds() As String) As <System.Xml.Serialization.XmlElementAttribute("Result", DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("Render", New Object() {Format, DeviceInfo})
            MimeType = CType(results(1), String)
            Encoding = CType(results(2), String)
            Warnings = CType(results(3), Warning())
            StreamIds = CType(results(4), String())
            Return CType(results(0), Byte())
        End Function

        '''<remarks/>
        Public Overloads Sub RenderAsync(ByVal Format As String, ByVal DeviceInfo As String)
            Me.RenderAsync(Format, DeviceInfo, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub RenderAsync(ByVal Format As String, ByVal DeviceInfo As String, ByVal userState As Object)
            If (Me.RenderOperationCompleted Is Nothing) Then
                Me.RenderOperationCompleted = AddressOf Me.OnRenderOperationCompleted
            End If
            Me.InvokeAsync("Render", New Object() {Format, DeviceInfo}, Me.RenderOperationCompleted, userState)
        End Sub

        Private Sub OnRenderOperationCompleted(ByVal arg As Object)
            If (Not (Me.RenderCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent RenderCompleted(Me, New RenderCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId:="1#")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId:="3#")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId:="2#")> <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Ren" & _
            "derStream", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RenderStream(ByVal Format As String, ByVal StreamID As String, ByRef Encoding As String, ByRef MimeType As String) As <System.Xml.Serialization.XmlElementAttribute("Result", DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("RenderStream", New Object() {Format, StreamID})
            Encoding = CType(results(1), String)
            MimeType = CType(results(2), String)
            Return CType(results(0), Byte())
        End Function

        '''<remarks/>
        Public Overloads Sub RenderStreamAsync(ByVal Format As String, ByVal StreamID As String)
            Me.RenderStreamAsync(Format, StreamID, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub RenderStreamAsync(ByVal Format As String, ByVal StreamID As String, ByVal userState As Object)
            If (Me.RenderStreamOperationCompleted Is Nothing) Then
                Me.RenderStreamOperationCompleted = AddressOf Me.OnRenderStreamOperationCompleted
            End If
            Me.InvokeAsync("RenderStream", New Object() {Format, StreamID}, Me.RenderStreamOperationCompleted, userState)
        End Sub

        Private Sub OnRenderStreamOperationCompleted(ByVal arg As Object)
            If (Not (Me.RenderStreamCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent RenderStreamCompleted(Me, New RenderStreamCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")> <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Get" & _
            "ExecutionInfo", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GetExecutionInfo() As <System.Xml.Serialization.XmlElementAttribute("executionInfo")> ExecutionInfo
            Dim results() As Object = Me.Invoke("GetExecutionInfo", New Object(-1) {})
            Return CType(results(0), ExecutionInfo)
        End Function

        '''<remarks/>
        Public Overloads Sub GetExecutionInfoAsync()
            Me.GetExecutionInfoAsync(Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub GetExecutionInfoAsync(ByVal userState As Object)
            If (Me.GetExecutionInfoOperationCompleted Is Nothing) Then
                Me.GetExecutionInfoOperationCompleted = AddressOf Me.OnGetExecutionInfoOperationCompleted
            End If
            Me.InvokeAsync("GetExecutionInfo", New Object(-1) {}, Me.GetExecutionInfoOperationCompleted, userState)
        End Sub

        Private Sub OnGetExecutionInfoOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetExecutionInfoCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetExecutionInfoCompleted(Me, New GetExecutionInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")> <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Get" & _
            "DocumentMap", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GetDocumentMap() As <System.Xml.Serialization.XmlElementAttribute("result")> DocumentMapNode
            Dim results() As Object = Me.Invoke("GetDocumentMap", New Object(-1) {})
            Return CType(results(0), DocumentMapNode)
        End Function

        '''<remarks/>
        Public Overloads Sub GetDocumentMapAsync()
            Me.GetDocumentMapAsync(Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub GetDocumentMapAsync(ByVal userState As Object)
            If (Me.GetDocumentMapOperationCompleted Is Nothing) Then
                Me.GetDocumentMapOperationCompleted = AddressOf Me.OnGetDocumentMapOperationCompleted
            End If
            Me.InvokeAsync("GetDocumentMap", New Object(-1) {}, Me.GetDocumentMapOperationCompleted, userState)
        End Sub

        Private Sub OnGetDocumentMapOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetDocumentMapCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetDocumentMapCompleted(Me, New GetDocumentMapCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId:="0#")> <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.InOut), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Loa" & _
            "dDrillthroughTarget", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function LoadDrillthroughTarget(ByVal DrillthroughID As String) As <System.Xml.Serialization.XmlElementAttribute("ExecutionInfo")> ExecutionInfo
            Dim results() As Object = Me.Invoke("LoadDrillthroughTarget", New Object() {DrillthroughID})
            Return CType(results(0), ExecutionInfo)
        End Function

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId:="0#")> Public Overloads Sub LoadDrillthroughTargetAsync(ByVal DrillthroughID As String)
            Me.LoadDrillthroughTargetAsync(DrillthroughID, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub LoadDrillthroughTargetAsync(ByVal DrillthroughID As String, ByVal userState As Object)
            If (Me.LoadDrillthroughTargetOperationCompleted Is Nothing) Then
                Me.LoadDrillthroughTargetOperationCompleted = AddressOf Me.OnLoadDrillthroughTargetOperationCompleted
            End If
            Me.InvokeAsync("LoadDrillthroughTarget", New Object() {DrillthroughID}, Me.LoadDrillthroughTargetOperationCompleted, userState)
        End Sub

        Private Sub OnLoadDrillthroughTargetOperationCompleted(ByVal arg As Object)
            If (Not (Me.LoadDrillthroughTargetCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent LoadDrillthroughTargetCompleted(Me, New LoadDrillthroughTargetCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId:="0#")> <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Tog" & _
            "gleItem", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ToggleItem(ByVal ToggleID As String) As <System.Xml.Serialization.XmlElementAttribute("Found")> Boolean
            Dim results() As Object = Me.Invoke("ToggleItem", New Object() {ToggleID})
            Return CType(results(0), Boolean)
        End Function

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId:="0#")> Public Overloads Sub ToggleItemAsync(ByVal ToggleID As String)
            Me.ToggleItemAsync(ToggleID, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub ToggleItemAsync(ByVal ToggleID As String, ByVal userState As Object)
            If (Me.ToggleItemOperationCompleted Is Nothing) Then
                Me.ToggleItemOperationCompleted = AddressOf Me.OnToggleItemOperationCompleted
            End If
            Me.InvokeAsync("ToggleItem", New Object() {ToggleID}, Me.ToggleItemOperationCompleted, userState)
        End Sub

        Private Sub OnToggleItemOperationCompleted(ByVal arg As Object)
            If (Not (Me.ToggleItemCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ToggleItemCompleted(Me, New ToggleItemCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId:="0#")> <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Nav" & _
            "igateDocumentMap", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function NavigateDocumentMap(ByVal DocMapID As String) As <System.Xml.Serialization.XmlElementAttribute("PageNumber")> Integer
            Dim results() As Object = Me.Invoke("NavigateDocumentMap", New Object() {DocMapID})
            Return CType(results(0), Integer)
        End Function

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId:="0#")> Public Overloads Sub NavigateDocumentMapAsync(ByVal DocMapID As String)
            Me.NavigateDocumentMapAsync(DocMapID, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub NavigateDocumentMapAsync(ByVal DocMapID As String, ByVal userState As Object)
            If (Me.NavigateDocumentMapOperationCompleted Is Nothing) Then
                Me.NavigateDocumentMapOperationCompleted = AddressOf Me.OnNavigateDocumentMapOperationCompleted
            End If
            Me.InvokeAsync("NavigateDocumentMap", New Object() {DocMapID}, Me.NavigateDocumentMapOperationCompleted, userState)
        End Sub

        Private Sub OnNavigateDocumentMapOperationCompleted(ByVal arg As Object)
            If (Not (Me.NavigateDocumentMapCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent NavigateDocumentMapCompleted(Me, New NavigateDocumentMapCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId:="0#")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId:="1#")> <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Nav" & _
            "igateBookmark", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function NavigateBookmark(ByVal BookmarkID As String, ByRef UniqueName As String) As <System.Xml.Serialization.XmlElementAttribute("PageNumber")> Integer
            Dim results() As Object = Me.Invoke("NavigateBookmark", New Object() {BookmarkID})
            UniqueName = CType(results(1), String)
            Return CType(results(0), Integer)
        End Function

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId:="0#")> Public Overloads Sub NavigateBookmarkAsync(ByVal BookmarkID As String)
            Me.NavigateBookmarkAsync(BookmarkID, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub NavigateBookmarkAsync(ByVal BookmarkID As String, ByVal userState As Object)
            If (Me.NavigateBookmarkOperationCompleted Is Nothing) Then
                Me.NavigateBookmarkOperationCompleted = AddressOf Me.OnNavigateBookmarkOperationCompleted
            End If
            Me.InvokeAsync("NavigateBookmark", New Object() {BookmarkID}, Me.NavigateBookmarkOperationCompleted, userState)
        End Sub

        Private Sub OnNavigateBookmarkOperationCompleted(ByVal arg As Object)
            If (Not (Me.NavigateBookmarkCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent NavigateBookmarkCompleted(Me, New NavigateBookmarkCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Fin" & _
            "dString", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function FindString(ByVal StartPage As Integer, ByVal EndPage As Integer, ByVal FindValue As String) As <System.Xml.Serialization.XmlElementAttribute("PageNumber")> Integer
            Dim results() As Object = Me.Invoke("FindString", New Object() {StartPage, EndPage, FindValue})
            Return CType(results(0), Integer)
        End Function

        '''<remarks/>
        Public Overloads Sub FindStringAsync(ByVal StartPage As Integer, ByVal EndPage As Integer, ByVal FindValue As String)
            Me.FindStringAsync(StartPage, EndPage, FindValue, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub FindStringAsync(ByVal StartPage As Integer, ByVal EndPage As Integer, ByVal FindValue As String, ByVal userState As Object)
            If (Me.FindStringOperationCompleted Is Nothing) Then
                Me.FindStringOperationCompleted = AddressOf Me.OnFindStringOperationCompleted
            End If
            Me.InvokeAsync("FindString", New Object() {StartPage, EndPage, FindValue}, Me.FindStringOperationCompleted, userState)
        End Sub

        Private Sub OnFindStringOperationCompleted(ByVal arg As Object)
            If (Not (Me.FindStringCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent FindStringCompleted(Me, New FindStringCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId:="3#")> <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapHeaderAttribute("ExecutionHeaderValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Sor" & _
            "t", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Sort(ByVal SortItem As String, ByVal Direction As SortDirectionEnum, ByVal Clear As Boolean, ByRef ReportItem As String, ByRef NumPages As Integer) As <System.Xml.Serialization.XmlElementAttribute("PageNumber")> Integer
            Dim results() As Object = Me.Invoke("Sort", New Object() {SortItem, Direction, Clear})
            ReportItem = CType(results(1), String)
            NumPages = CType(results(2), Integer)
            Return CType(results(0), Integer)
        End Function

        '''<remarks/>
        Public Overloads Sub SortAsync(ByVal SortItem As String, ByVal Direction As SortDirectionEnum, ByVal Clear As Boolean)
            Me.SortAsync(SortItem, Direction, Clear, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub SortAsync(ByVal SortItem As String, ByVal Direction As SortDirectionEnum, ByVal Clear As Boolean, ByVal userState As Object)
            If (Me.SortOperationCompleted Is Nothing) Then
                Me.SortOperationCompleted = AddressOf Me.OnSortOperationCompleted
            End If
            Me.InvokeAsync("Sort", New Object() {SortItem, Direction, Clear}, Me.SortOperationCompleted, userState)
        End Sub

        Private Sub OnSortOperationCompleted(ByVal arg As Object)
            If (Not (Me.SortCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent SortCompleted(Me, New SortCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId:="2#")> <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Get" & _
            "RenderResource", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GetRenderResource(ByVal Format As String, ByVal DeviceInfo As String, ByRef MimeType As String) As <System.Xml.Serialization.XmlElementAttribute("Result", DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("GetRenderResource", New Object() {Format, DeviceInfo})
            MimeType = CType(results(1), String)
            Return CType(results(0), Byte())
        End Function

        '''<remarks/>
        Public Overloads Sub GetRenderResourceAsync(ByVal Format As String, ByVal DeviceInfo As String)
            Me.GetRenderResourceAsync(Format, DeviceInfo, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub GetRenderResourceAsync(ByVal Format As String, ByVal DeviceInfo As String, ByVal userState As Object)
            If (Me.GetRenderResourceOperationCompleted Is Nothing) Then
                Me.GetRenderResourceOperationCompleted = AddressOf Me.OnGetRenderResourceOperationCompleted
            End If
            Me.InvokeAsync("GetRenderResource", New Object() {Format, DeviceInfo}, Me.GetRenderResourceOperationCompleted, userState)
        End Sub

        Private Sub OnGetRenderResourceOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetRenderResourceCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetRenderResourceCompleted(Me, New GetRenderResourceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Lis" & _
            "tRenderingExtensions", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ListRenderingExtensions() As <System.Xml.Serialization.XmlArrayAttribute("Extensions")> Extension()
            Dim results() As Object = Me.Invoke("ListRenderingExtensions", New Object(-1) {})
            Return CType(results(0), Extension())
        End Function

        '''<remarks/>
        Public Overloads Sub ListRenderingExtensionsAsync()
            Me.ListRenderingExtensionsAsync(Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub ListRenderingExtensionsAsync(ByVal userState As Object)
            If (Me.ListRenderingExtensionsOperationCompleted Is Nothing) Then
                Me.ListRenderingExtensionsOperationCompleted = AddressOf Me.OnListRenderingExtensionsOperationCompleted
            End If
            Me.InvokeAsync("ListRenderingExtensions", New Object(-1) {}, Me.ListRenderingExtensionsOperationCompleted, userState)
        End Sub

        Private Sub OnListRenderingExtensionsOperationCompleted(ByVal arg As Object)
            If (Not (Me.ListRenderingExtensionsCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ListRenderingExtensionsCompleted(Me, New ListRenderingExtensionsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Log" & _
            "onUser", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Sub LogonUser(ByVal userName As String, ByVal password As String, ByVal authority As String)
            Me.Invoke("LogonUser", New Object() {userName, password, authority})
        End Sub

        '''<remarks/>
        Public Overloads Sub LogonUserAsync(ByVal userName As String, ByVal password As String, ByVal authority As String)
            Me.LogonUserAsync(userName, password, authority, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub LogonUserAsync(ByVal userName As String, ByVal password As String, ByVal authority As String, ByVal userState As Object)
            If (Me.LogonUserOperationCompleted Is Nothing) Then
                Me.LogonUserOperationCompleted = AddressOf Me.OnLogonUserOperationCompleted
            End If
            Me.InvokeAsync("LogonUser", New Object() {userName, password, authority}, Me.LogonUserOperationCompleted, userState)
        End Sub

        Private Sub OnLogonUserOperationCompleted(ByVal arg As Object)
            If (Not (Me.LogonUserCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent LogonUserCompleted(Me, New System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("ServerInfoHeaderValue", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.Out), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices/Log" & _
            "off", RequestNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", ResponseNamespace:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Sub Logoff()
            Me.Invoke("Logoff", New Object(-1) {})
        End Sub

        '''<remarks/>
        Public Overloads Sub LogoffAsync()
            Me.LogoffAsync(Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub LogoffAsync(ByVal userState As Object)
            If (Me.LogoffOperationCompleted Is Nothing) Then
                Me.LogoffOperationCompleted = AddressOf Me.OnLogoffOperationCompleted
            End If
            Me.InvokeAsync("Logoff", New Object(-1) {}, Me.LogoffOperationCompleted, userState)
        End Sub

        Private Sub OnLogoffOperationCompleted(ByVal arg As Object)
            If (Not (Me.LogoffCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent LogoffCompleted(Me, New System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId:="System.String.Compare(System.String,System.String,System.Boolean)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")> Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing) _
                        OrElse (url Is String.Empty)) Then
                Return False
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If (((wsUri.Port >= 1024) _
                        AndAlso (wsUri.Port <= 5000)) _
                        AndAlso (String.Compare(wsUri.Host, "localHost", True) = 0)) Then
                Return True
            End If
            Return False
        End Function
    End Class

    '''<remarks/>
    <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices"), _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", IsNullable:=False)> _
    Partial Public Class ServerInfoHeader
        Inherits System.Web.Services.Protocols.SoapHeader

        Private reportServerVersionNumberField As String

        Private reportServerEditionField As String

        Private reportServerVersionField As String

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2235:MarkAllNonSerializableFields")> Private anyAttrField() As System.Xml.XmlAttribute

        '''<remarks/>
        Public Property ReportServerVersionNumber() As String
            Get
                Return Me.reportServerVersionNumberField
            End Get
            Set(ByVal value As String)
                Me.reportServerVersionNumberField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ReportServerEdition() As String
            Get
                Return Me.reportServerEditionField
            End Get
            Set(ByVal value As String)
                Me.reportServerEditionField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ReportServerVersion() As String
            Get
                Return Me.reportServerVersionField
            End Get
            Set(ByVal value As String)
                Me.reportServerVersionField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlAnyAttributeAttribute()> _
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set(ByVal value As System.Xml.XmlAttribute())
                Me.anyAttrField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")> _
    Partial Public Class Extension

        Private extensionTypeField As ExtensionTypeEnum

        Private nameField As String

        Private localizedNameField As String

        Private visibleField As Boolean

        '''<remarks/>
        Public Property ExtensionType() As ExtensionTypeEnum
            Get
                Return Me.extensionTypeField
            End Get
            Set(ByVal value As ExtensionTypeEnum)
                Me.extensionTypeField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Name() As String
            Get
                Return Me.nameField
            End Get
            Set(ByVal value As String)
                Me.nameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property LocalizedName() As String
            Get
                Return Me.localizedNameField
            End Get
            Set(ByVal value As String)
                Me.localizedNameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Visible() As Boolean
            Get
                Return Me.visibleField
            End Get
            Set(ByVal value As Boolean)
                Me.visibleField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")> <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")> _
    Public Enum ExtensionTypeEnum

        '''<remarks/>
        Delivery

        '''<remarks/>
        Render

        '''<remarks/>
        Data

        '''<remarks/>
        All
    End Enum

    '''<remarks/>
    <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")> _
    Partial Public Class DocumentMapNode

        Private labelField As String

        Private uniqueNameField As String

        Private childrenField() As DocumentMapNode

        '''<remarks/>
        Public Property Label() As String
            Get
                Return Me.labelField
            End Get
            Set(ByVal value As String)
                Me.labelField = value
            End Set
        End Property

        '''<remarks/>
        Public Property UniqueName() As String
            Get
                Return Me.uniqueNameField
            End Get
            Set(ByVal value As String)
                Me.uniqueNameField = value
            End Set
        End Property

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")> Public Property Children() As DocumentMapNode()
            Get
                Return Me.childrenField
            End Get
            Set(ByVal value As DocumentMapNode())
                Me.childrenField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlIncludeAttribute(GetType(ParameterValue)), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")> _
    Partial Public Class ParameterValueOrFieldReference
    End Class

    '''<remarks/>
    <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")> _
    Partial Public Class ParameterValue
        Inherits ParameterValueOrFieldReference

        Private nameField As String

        Private valueField As String

        Private labelField As String

        '''<remarks/>
        Public Property Name() As String
            Get
                Return Me.nameField
            End Get
            Set(ByVal value As String)
                Me.nameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Value() As String
            Get
                Return Me.valueField
            End Get
            Set(ByVal value As String)
                Me.valueField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Label() As String
            Get
                Return Me.labelField
            End Get
            Set(ByVal value As String)
                Me.labelField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")> _
    Partial Public Class DataSourceCredentials

        Private dataSourceNameField As String

        Private userNameField As String

        Private passwordField As String

        '''<remarks/>
        Public Property DataSourceName() As String
            Get
                Return Me.dataSourceNameField
            End Get
            Set(ByVal value As String)
                Me.dataSourceNameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property UserName() As String
            Get
                Return Me.userNameField
            End Get
            Set(ByVal value As String)
                Me.userNameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Password() As String
            Get
                Return Me.passwordField
            End Get
            Set(ByVal value As String)
                Me.passwordField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")> _
    Partial Public Class Warning

        Private codeField As String

        Private severityField As String

        Private objectNameField As String

        Private objectTypeField As String

        Private messageField As String

        '''<remarks/>
        Public Property Code() As String
            Get
                Return Me.codeField
            End Get
            Set(ByVal value As String)
                Me.codeField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Severity() As String
            Get
                Return Me.severityField
            End Get
            Set(ByVal value As String)
                Me.severityField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ObjectName() As String
            Get
                Return Me.objectNameField
            End Get
            Set(ByVal value As String)
                Me.objectNameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ObjectType() As String
            Get
                Return Me.objectTypeField
            End Get
            Set(ByVal value As String)
                Me.objectTypeField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Message() As String
            Get
                Return Me.messageField
            End Get
            Set(ByVal value As String)
                Me.messageField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")> _
    Partial Public Class DataSourcePrompt

        Private nameField As String

        Private dataSourceIDField As String

        Private promptField As String

        '''<remarks/>
        Public Property Name() As String
            Get
                Return Me.nameField
            End Get
            Set(ByVal value As String)
                Me.nameField = value
            End Set
        End Property

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId:="Member")> Public Property DataSourceID() As String
            Get
                Return Me.dataSourceIDField
            End Get
            Set(ByVal value As String)
                Me.dataSourceIDField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Prompt() As String
            Get
                Return Me.promptField
            End Get
            Set(ByVal value As String)
                Me.promptField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")> _
    Partial Public Class ValidValue

        Private labelField As String

        Private valueField As String

        '''<remarks/>
        Public Property Label() As String
            Get
                Return Me.labelField
            End Get
            Set(ByVal value As String)
                Me.labelField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Value() As String
            Get
                Return Me.valueField
            End Get
            Set(ByVal value As String)
                Me.valueField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")> _
    Partial Public Class ReportParameter

        Private nameField As String

        Private typeField As ParameterTypeEnum

        Private typeFieldSpecified As Boolean

        Private nullableField As Boolean

        Private nullableFieldSpecified As Boolean

        Private allowBlankField As Boolean

        Private allowBlankFieldSpecified As Boolean

        Private multiValueField As Boolean

        Private multiValueFieldSpecified As Boolean

        Private queryParameterField As Boolean

        Private queryParameterFieldSpecified As Boolean

        Private promptField As String

        Private promptUserField As Boolean

        Private promptUserFieldSpecified As Boolean

        Private dependenciesField() As String

        Private validValuesQueryBasedField As Boolean

        Private validValuesQueryBasedFieldSpecified As Boolean

        Private validValuesField() As ValidValue

        Private defaultValuesQueryBasedField As Boolean

        Private defaultValuesQueryBasedFieldSpecified As Boolean

        Private defaultValuesField() As String

        Private stateField As ParameterStateEnum

        Private stateFieldSpecified As Boolean

        Private errorMessageField As String

        '''<remarks/>
        Public Property Name() As String
            Get
                Return Me.nameField
            End Get
            Set(ByVal value As String)
                Me.nameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Type() As ParameterTypeEnum
            Get
                Return Me.typeField
            End Get
            Set(ByVal value As ParameterTypeEnum)
                Me.typeField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property TypeSpecified() As Boolean
            Get
                Return Me.typeFieldSpecified
            End Get
            Set(ByVal value As Boolean)
                Me.typeFieldSpecified = value
            End Set
        End Property

        '''<remarks/>
        Public Property Nullable() As Boolean
            Get
                Return Me.nullableField
            End Get
            Set(ByVal value As Boolean)
                Me.nullableField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property NullableSpecified() As Boolean
            Get
                Return Me.nullableFieldSpecified
            End Get
            Set(ByVal value As Boolean)
                Me.nullableFieldSpecified = value
            End Set
        End Property

        '''<remarks/>
        Public Property AllowBlank() As Boolean
            Get
                Return Me.allowBlankField
            End Get
            Set(ByVal value As Boolean)
                Me.allowBlankField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property AllowBlankSpecified() As Boolean
            Get
                Return Me.allowBlankFieldSpecified
            End Get
            Set(ByVal value As Boolean)
                Me.allowBlankFieldSpecified = value
            End Set
        End Property

        '''<remarks/>
        Public Property MultiValue() As Boolean
            Get
                Return Me.multiValueField
            End Get
            Set(ByVal value As Boolean)
                Me.multiValueField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property MultiValueSpecified() As Boolean
            Get
                Return Me.multiValueFieldSpecified
            End Get
            Set(ByVal value As Boolean)
                Me.multiValueFieldSpecified = value
            End Set
        End Property

        '''<remarks/>
        Public Property QueryParameter() As Boolean
            Get
                Return Me.queryParameterField
            End Get
            Set(ByVal value As Boolean)
                Me.queryParameterField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property QueryParameterSpecified() As Boolean
            Get
                Return Me.queryParameterFieldSpecified
            End Get
            Set(ByVal value As Boolean)
                Me.queryParameterFieldSpecified = value
            End Set
        End Property

        '''<remarks/>
        Public Property Prompt() As String
            Get
                Return Me.promptField
            End Get
            Set(ByVal value As String)
                Me.promptField = value
            End Set
        End Property

        '''<remarks/>
        Public Property PromptUser() As Boolean
            Get
                Return Me.promptUserField
            End Get
            Set(ByVal value As Boolean)
                Me.promptUserField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property PromptUserSpecified() As Boolean
            Get
                Return Me.promptUserFieldSpecified
            End Get
            Set(ByVal value As Boolean)
                Me.promptUserFieldSpecified = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlArrayItemAttribute("Dependency")> _
        Public Property Dependencies() As String()
            Get
                Return Me.dependenciesField
            End Get
            Set(ByVal value As String())
                Me.dependenciesField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ValidValuesQueryBased() As Boolean
            Get
                Return Me.validValuesQueryBasedField
            End Get
            Set(ByVal value As Boolean)
                Me.validValuesQueryBasedField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property ValidValuesQueryBasedSpecified() As Boolean
            Get
                Return Me.validValuesQueryBasedFieldSpecified
            End Get
            Set(ByVal value As Boolean)
                Me.validValuesQueryBasedFieldSpecified = value
            End Set
        End Property

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")> Public Property ValidValues() As ValidValue()
            Get
                Return Me.validValuesField
            End Get
            Set(ByVal value As ValidValue())
                Me.validValuesField = value
            End Set
        End Property

        '''<remarks/>
        Public Property DefaultValuesQueryBased() As Boolean
            Get
                Return Me.defaultValuesQueryBasedField
            End Get
            Set(ByVal value As Boolean)
                Me.defaultValuesQueryBasedField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property DefaultValuesQueryBasedSpecified() As Boolean
            Get
                Return Me.defaultValuesQueryBasedFieldSpecified
            End Get
            Set(ByVal value As Boolean)
                Me.defaultValuesQueryBasedFieldSpecified = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlArrayItemAttribute("Value")> _
        Public Property DefaultValues() As String()
            Get
                Return Me.defaultValuesField
            End Get
            Set(ByVal value As String())
                Me.defaultValuesField = value
            End Set
        End Property

        '''<remarks/>
        Public Property State() As ParameterStateEnum
            Get
                Return Me.stateField
            End Get
            Set(ByVal value As ParameterStateEnum)
                Me.stateField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property StateSpecified() As Boolean
            Get
                Return Me.stateFieldSpecified
            End Get
            Set(ByVal value As Boolean)
                Me.stateFieldSpecified = value
            End Set
        End Property

        '''<remarks/>
        Public Property ErrorMessage() As String
            Get
                Return Me.errorMessageField
            End Get
            Set(ByVal value As String)
                Me.errorMessageField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")> <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")> _
    Public Enum ParameterTypeEnum

        '''<remarks/>
        [Boolean]

        '''<remarks/>
        DateTime

        '''<remarks/>
        [Integer]

        '''<remarks/>
        Float

        '''<remarks/>
        [String]
    End Enum

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")> <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")> _
    Public Enum ParameterStateEnum

        '''<remarks/>
        HasValidValue

        '''<remarks/>
        MissingValidValue

        '''<remarks/>
        HasOutstandingDependencies

        '''<remarks/>
        DynamicValuesUnavailable
    End Enum

    '''<remarks/>
    <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")> _
    Partial Public Class ExecutionInfo

        Private hasSnapshotField As Boolean

        Private needsProcessingField As Boolean

        Private allowQueryExecutionField As Boolean

        Private credentialsRequiredField As Boolean

        Private parametersRequiredField As Boolean

        Private expirationDateTimeField As Date

        Private executionDateTimeField As Date

        Private numPagesField As Integer

        Private parametersField() As ReportParameter

        Private dataSourcePromptsField() As DataSourcePrompt

        Private hasDocumentMapField As Boolean

        Private executionIDField As String

        Private reportPathField As String

        Private historyIDField As String

        '''<remarks/>
        Public Property HasSnapshot() As Boolean
            Get
                Return Me.hasSnapshotField
            End Get
            Set(ByVal value As Boolean)
                Me.hasSnapshotField = value
            End Set
        End Property

        '''<remarks/>
        Public Property NeedsProcessing() As Boolean
            Get
                Return Me.needsProcessingField
            End Get
            Set(ByVal value As Boolean)
                Me.needsProcessingField = value
            End Set
        End Property

        '''<remarks/>
        Public Property AllowQueryExecution() As Boolean
            Get
                Return Me.allowQueryExecutionField
            End Get
            Set(ByVal value As Boolean)
                Me.allowQueryExecutionField = value
            End Set
        End Property

        '''<remarks/>
        Public Property CredentialsRequired() As Boolean
            Get
                Return Me.credentialsRequiredField
            End Get
            Set(ByVal value As Boolean)
                Me.credentialsRequiredField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ParametersRequired() As Boolean
            Get
                Return Me.parametersRequiredField
            End Get
            Set(ByVal value As Boolean)
                Me.parametersRequiredField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ExpirationDateTime() As Date
            Get
                Return Me.expirationDateTimeField
            End Get
            Set(ByVal value As Date)
                Me.expirationDateTimeField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ExecutionDateTime() As Date
            Get
                Return Me.executionDateTimeField
            End Get
            Set(ByVal value As Date)
                Me.executionDateTimeField = value
            End Set
        End Property

        '''<remarks/>
        Public Property NumPages() As Integer
            Get
                Return Me.numPagesField
            End Get
            Set(ByVal value As Integer)
                Me.numPagesField = value
            End Set
        End Property

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")> Public Property Parameters() As ReportParameter()
            Get
                Return Me.parametersField
            End Get
            Set(ByVal value As ReportParameter())
                Me.parametersField = value
            End Set
        End Property

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")> Public Property DataSourcePrompts() As DataSourcePrompt()
            Get
                Return Me.dataSourcePromptsField
            End Get
            Set(ByVal value As DataSourcePrompt())
                Me.dataSourcePromptsField = value
            End Set
        End Property

        '''<remarks/>
        Public Property HasDocumentMap() As Boolean
            Get
                Return Me.hasDocumentMapField
            End Get
            Set(ByVal value As Boolean)
                Me.hasDocumentMapField = value
            End Set
        End Property

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId:="Member")> Public Property ExecutionID() As String
            Get
                Return Me.executionIDField
            End Get
            Set(ByVal value As String)
                Me.executionIDField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ReportPath() As String
            Get
                Return Me.reportPathField
            End Get
            Set(ByVal value As String)
                Me.reportPathField = value
            End Set
        End Property

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId:="Member")> Public Property HistoryID() As String
            Get
                Return Me.historyIDField
            End Get
            Set(ByVal value As String)
                Me.historyIDField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices"), _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", IsNullable:=False)> _
    Partial Public Class ExecutionHeader
        Inherits System.Web.Services.Protocols.SoapHeader

        Private executionIDField As String

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2235:MarkAllNonSerializableFields")> Private anyAttrField() As System.Xml.XmlAttribute

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId:="Member")> Public Property ExecutionID() As String
            Get
                Return Me.executionIDField
            End Get
            Set(ByVal value As String)
                Me.executionIDField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlAnyAttributeAttribute()> _
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set(ByVal value As System.Xml.XmlAttribute())
                Me.anyAttrField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")> <System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")> _
    Public Enum SortDirectionEnum

        '''<remarks/>
        None

        '''<remarks/>
        Ascending

        '''<remarks/>
        Descending
    End Enum

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub ListSecureMethodsCompletedEventHandler(ByVal sender As Object, ByVal e As ListSecureMethodsCompletedEventArgs)

    '''<remarks/>
    Partial Public Class ListSecureMethodsCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")> Public ReadOnly Property Result() As String()
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), String())
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub LoadReportCompletedEventHandler(ByVal sender As Object, ByVal e As LoadReportCompletedEventArgs)

    '''<remarks/>
    Partial Public Class LoadReportCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As ExecutionInfo
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), ExecutionInfo)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub LoadReportDefinitionCompletedEventHandler(ByVal sender As Object, ByVal e As LoadReportDefinitionCompletedEventArgs)

    '''<remarks/>
    Partial Public Class LoadReportDefinitionCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As ExecutionInfo
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), ExecutionInfo)
            End Get
        End Property

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId:="Member")> Public ReadOnly Property warnings() As Warning()
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(1), Warning())
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub SetExecutionCredentialsCompletedEventHandler(ByVal sender As Object, ByVal e As SetExecutionCredentialsCompletedEventArgs)

    '''<remarks/>
    Partial Public Class SetExecutionCredentialsCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As ExecutionInfo
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), ExecutionInfo)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub SetExecutionParametersCompletedEventHandler(ByVal sender As Object, ByVal e As SetExecutionParametersCompletedEventArgs)

    '''<remarks/>
    Partial Public Class SetExecutionParametersCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As ExecutionInfo
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), ExecutionInfo)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub ResetExecutionCompletedEventHandler(ByVal sender As Object, ByVal e As ResetExecutionCompletedEventArgs)

    '''<remarks/>
    Partial Public Class ResetExecutionCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As ExecutionInfo
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), ExecutionInfo)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub RenderCompletedEventHandler(ByVal sender As Object, ByVal e As RenderCompletedEventArgs)

    '''<remarks/>
    Partial Public Class RenderCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")> Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), Byte())
            End Get
        End Property

        '''<remarks/>
        Public ReadOnly Property MimeType() As String
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(1), String)
            End Get
        End Property

        '''<remarks/>
        Public ReadOnly Property Encoding() As String
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(2), String)
            End Get
        End Property

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")> Public ReadOnly Property Warnings() As Warning()
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(3), Warning())
            End Get
        End Property

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")> Public ReadOnly Property StreamIds() As String()
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(4), String())
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub RenderStreamCompletedEventHandler(ByVal sender As Object, ByVal e As RenderStreamCompletedEventArgs)

    '''<remarks/>
    Partial Public Class RenderStreamCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")> Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), Byte())
            End Get
        End Property

        '''<remarks/>
        Public ReadOnly Property Encoding() As String
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(1), String)
            End Get
        End Property

        '''<remarks/>
        Public ReadOnly Property MimeType() As String
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(2), String)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub GetExecutionInfoCompletedEventHandler(ByVal sender As Object, ByVal e As GetExecutionInfoCompletedEventArgs)

    '''<remarks/>
    Partial Public Class GetExecutionInfoCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As ExecutionInfo
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), ExecutionInfo)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub GetDocumentMapCompletedEventHandler(ByVal sender As Object, ByVal e As GetDocumentMapCompletedEventArgs)

    '''<remarks/>
    Partial Public Class GetDocumentMapCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As DocumentMapNode
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), DocumentMapNode)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub LoadDrillthroughTargetCompletedEventHandler(ByVal sender As Object, ByVal e As LoadDrillthroughTargetCompletedEventArgs)

    '''<remarks/>
    Partial Public Class LoadDrillthroughTargetCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As ExecutionInfo
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), ExecutionInfo)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub ToggleItemCompletedEventHandler(ByVal sender As Object, ByVal e As ToggleItemCompletedEventArgs)

    '''<remarks/>
    Partial Public Class ToggleItemCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), Boolean)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub NavigateDocumentMapCompletedEventHandler(ByVal sender As Object, ByVal e As NavigateDocumentMapCompletedEventArgs)

    '''<remarks/>
    Partial Public Class NavigateDocumentMapCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), Integer)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub NavigateBookmarkCompletedEventHandler(ByVal sender As Object, ByVal e As NavigateBookmarkCompletedEventArgs)

    '''<remarks/>
    Partial Public Class NavigateBookmarkCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), Integer)
            End Get
        End Property

        '''<remarks/>
        Public ReadOnly Property UniqueName() As String
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(1), String)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub FindStringCompletedEventHandler(ByVal sender As Object, ByVal e As FindStringCompletedEventArgs)

    '''<remarks/>
    Partial Public Class FindStringCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), Integer)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub SortCompletedEventHandler(ByVal sender As Object, ByVal e As SortCompletedEventArgs)

    '''<remarks/>
    Partial Public Class SortCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), Integer)
            End Get
        End Property

        '''<remarks/>
        Public ReadOnly Property ReportItem() As String
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(1), String)
            End Get
        End Property

        '''<remarks/>
        Public ReadOnly Property NumPages() As Integer
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(2), Integer)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub GetRenderResourceCompletedEventHandler(ByVal sender As Object, ByVal e As GetRenderResourceCompletedEventArgs)

    '''<remarks/>
    Partial Public Class GetRenderResourceCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")> Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), Byte())
            End Get
        End Property

        '''<remarks/>
        Public ReadOnly Property MimeType() As String
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(1), String)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub ListRenderingExtensionsCompletedEventHandler(ByVal sender As Object, ByVal e As ListRenderingExtensionsCompletedEventArgs)

    '''<remarks/>
    Partial Public Class ListRenderingExtensionsCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")> Public ReadOnly Property Result() As Extension()
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), Extension())
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub LogonUserCompletedEventHandler(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)

    '''<remarks/>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances")> Public Delegate Sub LogoffCompletedEventHandler(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
End Namespace