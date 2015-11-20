﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.ReportingServices.AsynchronousRender.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microsoft.Samples.ReportingServices.AsynchronousRender.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Callback! Would you like to save the report to disk?.
        /// </summary>
        internal static string callbackQuestionMessage {
            get {
                return ResourceManager.GetString("callbackQuestionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Report Rendered.
        /// </summary>
        internal static string callbackQuestionMessageBoxTitle {
            get {
                return ResourceManager.GetString("callbackQuestionMessageBoxTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred:.
        /// </summary>
        internal static string genericErrorMessage {
            get {
                return ResourceManager.GetString("genericErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error.
        /// </summary>
        internal static string genericErrorMessageBoxTitle {
            get {
                return ResourceManager.GetString("genericErrorMessageBoxTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sample does not support rendering a report with a ReportParameter..
        /// </summary>
        internal static string RenderingNotSupported {
            get {
                return ResourceManager.GetString("RenderingNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rendering Report.
        /// </summary>
        internal static string renderingReportText {
            get {
                return ResourceManager.GetString("renderingReportText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The report was saved successfully..
        /// </summary>
        internal static string reportSavedMessage {
            get {
                return ResourceManager.GetString("reportSavedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please select a report..
        /// </summary>
        internal static string SelectReport {
            get {
                return ResourceManager.GetString("SelectReport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ready..
        /// </summary>
        internal static string statusReadyText {
            get {
                return ResourceManager.GetString("statusReadyText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Web Page, single file (*.mhtml)|*.mhtml.
        /// </summary>
        internal static string WebPageFormat {
            get {
                return ResourceManager.GetString("WebPageFormat", resourceCulture);
            }
        }
    }
}