﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.225
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module MyResources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Microsoft.Samples.ReportingServices.FindRenderSave.MyResources", GetType(MyResources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to an error occurred:.
        '''</summary>
        Friend ReadOnly Property genericErrorMessage() As String
            Get
                Return ResourceManager.GetString("genericErrorMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Application Error.
        '''</summary>
        Friend ReadOnly Property genericErrorMessageBoxTitle() As String
            Get
                Return ResourceManager.GetString("genericErrorMessageBoxTitle", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Please enter a valid search string..
        '''</summary>
        Friend ReadOnly Property invalidSearchStringErrorMessage() As String
            Get
                Return ResourceManager.GetString("invalidSearchStringErrorMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Invalid Search String.
        '''</summary>
        Friend ReadOnly Property invalidSearchStringMessageBoxTitle() As String
            Get
                Return ResourceManager.GetString("invalidSearchStringMessageBoxTitle", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Unable to save the report from this application because one or more parameters for the report are missing default values..
        '''</summary>
        Friend ReadOnly Property missingDefaultParametersErrorMessage() As String
            Get
                Return ResourceManager.GetString("missingDefaultParametersErrorMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Missing Default Parameters.
        '''</summary>
        Friend ReadOnly Property missingDefaultParametersMessageBoxTitle() As String
            Get
                Return ResourceManager.GetString("missingDefaultParametersMessageBoxTitle", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to No items matching your search criteria were found..
        '''</summary>
        Friend ReadOnly Property noItemsFoundInfoMessage() As String
            Get
                Return ResourceManager.GetString("noItemsFoundInfoMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Items Not Found.
        '''</summary>
        Friend ReadOnly Property noItemsFoundMessageBoxTitle() As String
            Get
                Return ResourceManager.GetString("noItemsFoundMessageBoxTitle", resourceCulture)
            End Get
        End Property
    End Module
End Namespace