﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shuttle.Sentinel {
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
    public class SentinelResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SentinelResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Shuttle.Sentinel.SentinelResources", typeof(SentinelResources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Permission &apos;{0}&apos; already exists on role &apos;{1}&apos;..
        /// </summary>
        public static string DuplicatePermissionException {
            get {
                return ResourceManager.GetString("DuplicatePermissionException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not enqueue message id &apos;{0}&apos; on inspection queue.  Exception: {1}.
        /// </summary>
        public static string EnqueueException {
            get {
                return ResourceManager.GetString("EnqueueException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The provided value of &apos;{0}&apos; is not a valid URI..
        /// </summary>
        public static string InvalidUri {
            get {
                return ResourceManager.GetString("InvalidUri", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Permission &apos;{0}&apos; does not exist on role &apos;{1}&apos;..
        /// </summary>
        public static string PermissionNotFoundException {
            get {
                return ResourceManager.GetString("PermissionNotFoundException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Role &apos;{0}&apos; does not exist on user &apos;{1}&apos;..
        /// </summary>
        public static string RoleNotFoundException {
            get {
                return ResourceManager.GetString("RoleNotFoundException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type &apos;{0}&apos; could not be loaded using value &apos;{1}&apos;.  Exception: {2}.
        /// </summary>
        public static string TypeNotFoundException {
            get {
                return ResourceManager.GetString("TypeNotFoundException", resourceCulture);
            }
        }
    }
}
