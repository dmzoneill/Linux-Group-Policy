﻿#pragma checksum "..\..\..\Splash.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9F70A9E786C178137AE4CD7D89475899"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace LGP {
    
    
    /// <summary>
    /// Splash
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class Splash : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\Splash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LGP.Splash SplashWindow;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\Splash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Animation.Storyboard FormFadeOut;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\Splash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Animation.DoubleAnimation FormFadeOutAnimation;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Splash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlockComponentName;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Splash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label1;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Splash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar progressBar1;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Splash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlockDllPath;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Splash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlockComponentVersion;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Splash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlockLoading;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Splash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image1;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Splash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlockCopyright;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Linux Group Policy;component/splash.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Splash.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.SplashWindow = ((LGP.Splash)(target));
            
            #line 3 "..\..\..\Splash.xaml"
            this.SplashWindow.Loaded += new System.Windows.RoutedEventHandler(this.WindowLoaded);
            
            #line default
            #line hidden
            
            #line 4 "..\..\..\Splash.xaml"
            this.SplashWindow.Closing += new System.ComponentModel.CancelEventHandler(this.WindowClosing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.FormFadeOut = ((System.Windows.Media.Animation.Storyboard)(target));
            
            #line 8 "..\..\..\Splash.xaml"
            this.FormFadeOut.Completed += new System.EventHandler(this.FormFadeOutCompleted);
            
            #line default
            #line hidden
            return;
            case 3:
            this.FormFadeOutAnimation = ((System.Windows.Media.Animation.DoubleAnimation)(target));
            return;
            case 4:
            this.textBlockComponentName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.label1 = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.progressBar1 = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 7:
            this.textBlockDllPath = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.textBlockComponentVersion = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.textBlockLoading = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.image1 = ((System.Windows.Controls.Image)(target));
            return;
            case 11:
            this.textBlockCopyright = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
