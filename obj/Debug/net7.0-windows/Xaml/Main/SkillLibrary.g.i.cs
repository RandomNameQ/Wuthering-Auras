﻿#pragma checksum "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "98A97338C5F3B3A618C69BA3562BBD9B486186FA"
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
    /// SkillLibrary
    /// </summary>
    public partial class SkillLibrary : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox_Skill;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameSkill;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DescriptionSkill;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TypeSkill;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ActionTypeSkill;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DurationSkill;
        
        #line default
        #line hidden
        
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
            System.Uri resourceLocater = new System.Uri("/Wuthering Waves comfort vision;component/xaml/main/skilllibrary.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
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
            
            #line 33 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_AddNewSkill);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ComboBox_Skill = ((System.Windows.Controls.ComboBox)(target));
            
            #line 37 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
            this.ComboBox_Skill.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_Skill_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 40 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_DeleteSkill);
            
            #line default
            #line hidden
            return;
            case 4:
            this.NameSkill = ((System.Windows.Controls.TextBox)(target));
            
            #line 54 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
            this.NameSkill.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.NameSkill_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.DescriptionSkill = ((System.Windows.Controls.TextBox)(target));
            
            #line 57 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
            this.DescriptionSkill.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.DescriptionSkill_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TypeSkill = ((System.Windows.Controls.ComboBox)(target));
            
            #line 61 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
            this.TypeSkill.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SkillType_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ActionTypeSkill = ((System.Windows.Controls.ComboBox)(target));
            
            #line 66 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
            this.ActionTypeSkill.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SkillActionType_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DurationSkill = ((System.Windows.Controls.TextBox)(target));
            
            #line 72 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
            this.DurationSkill.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.DurationSkill_TextChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 75 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_ImagePath);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 104 "..\..\..\..\..\Xaml\Main\SkillLibrary.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_SaveSkill);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

