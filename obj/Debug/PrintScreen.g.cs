﻿#pragma checksum "..\..\PrintScreen.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DF9A1594EE2B6A877C848C40116693CA2E40E434"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using ScreenCaptureDemo;
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


namespace ScreenCaptureDemo {
    
    
    /// <summary>
    /// PrintScreen
    /// </summary>
    public partial class PrintScreen : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\PrintScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ScreenCaptureDemo.PrintScreen windows;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\PrintScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Container;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\PrintScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImageContainer;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\PrintScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas MainGrid;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\PrintScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ToolPanel;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\PrintScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OkClick;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\PrintScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExitClick;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\PrintScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveClick;
        
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
            System.Uri resourceLocater = new System.Uri("/ScreenCaptureDemo;component/printscreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PrintScreen.xaml"
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
            this.windows = ((ScreenCaptureDemo.PrintScreen)(target));
            
            #line 10 "..\..\PrintScreen.xaml"
            this.windows.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.Windows_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 10 "..\..\PrintScreen.xaml"
            this.windows.MouseRightButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Windows_MouseRightButtonUp);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 19 "..\..\PrintScreen.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.Exit);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Container = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.ImageContainer = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.MainGrid = ((System.Windows.Controls.Canvas)(target));
            
            #line 23 "..\..\PrintScreen.xaml"
            this.MainGrid.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            
            #line 23 "..\..\PrintScreen.xaml"
            this.MainGrid.MouseMove += new System.Windows.Input.MouseEventHandler(this.Window_MouseMove);
            
            #line default
            #line hidden
            
            #line 23 "..\..\PrintScreen.xaml"
            this.MainGrid.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Window_MouseLeave);
            
            #line default
            #line hidden
            
            #line 23 "..\..\PrintScreen.xaml"
            this.MainGrid.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseUp);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ToolPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.OkClick = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\PrintScreen.xaml"
            this.OkClick.Click += new System.Windows.RoutedEventHandler(this.Ok_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ExitClick = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\PrintScreen.xaml"
            this.ExitClick.Click += new System.Windows.RoutedEventHandler(this.Exit_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.SaveClick = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\PrintScreen.xaml"
            this.SaveClick.Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

