﻿#pragma checksum "..\..\..\..\..\Xaml\Main\OverlayScreenArea.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5EA0A3EBBB62878584F1D2428177088295C1FE7F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using System.Windows.Controls.Ribbon;
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
using Wuthering_Waves_comfort_vision.Xaml.Main;


namespace Wuthering_Waves_comfort_vision.Xaml.Main {
    
    
    /// <summary>
    /// OverlayScreenArea
    /// </summary>
    public partial class OverlayScreenArea : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Wuthering Waves comfort vision;component/xaml/main/overlayscreenarea.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Xaml\Main\OverlayScreenArea.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 41 "..\..\..\..\..\Xaml\Main\OverlayScreenArea.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Click_ChoseScreenArea);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 42 "..\..\..\..\..\Xaml\Main\OverlayScreenArea.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Click_SaveSizePosition);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 43 "..\..\..\..\..\Xaml\Main\OverlayScreenArea.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Click_StartShowOverlay);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 44 "..\..\..\..\..\Xaml\Main\OverlayScreenArea.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Click_SaveOverlay);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
