﻿#pragma checksum "..\..\Popup.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "02B4C9CD623DD85B978D814F38AC920F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Basic_Financial_Tracker;
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


namespace Basic_Financial_Tracker {
    
    
    /// <summary>
    /// Popup
    /// </summary>
    public partial class Popup : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\Popup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Basic_Financial_Tracker.Popup PopupWindow;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\Popup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label popupLabel;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Popup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox popupTextBox;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Popup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button aPopupButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Popup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bPopupButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Basic Financial Tracker;component/popup.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Popup.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.PopupWindow = ((Basic_Financial_Tracker.Popup)(target));
            return;
            case 2:
            this.popupLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.popupTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 17 "..\..\Popup.xaml"
            this.popupTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.popupTextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 17 "..\..\Popup.xaml"
            this.popupTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.popupTextBox_LostFocus);
            
            #line default
            #line hidden
            
            #line 17 "..\..\Popup.xaml"
            this.popupTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.popupTextBox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 17 "..\..\Popup.xaml"
            this.popupTextBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.popupTextBox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.aPopupButton = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\Popup.xaml"
            this.aPopupButton.Click += new System.Windows.RoutedEventHandler(this.aPopupButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.bPopupButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\Popup.xaml"
            this.bPopupButton.Click += new System.Windows.RoutedEventHandler(this.bPopupButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

