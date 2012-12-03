﻿#pragma checksum "D:\Studies\Jyväskylän ammattikorkeakoulu\Windows Phone Programming\Project\endProject\SeizeDay\SeizeDay\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "83927F599E1A868F1CE909582D03DBB4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace SeizeDay {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentGrid;
        
        internal System.Windows.Shapes.Ellipse ClockFaceEllipse;
        
        internal System.Windows.Controls.Canvas ClockHandsCanvas;
        
        internal System.Windows.Media.Animation.Storyboard ClockStoryboard;
        
        internal System.Windows.Media.Animation.DoubleAnimation HourAnimation;
        
        internal System.Windows.Media.Animation.DoubleAnimation MinuteAnimation;
        
        internal System.Windows.Media.Animation.DoubleAnimation SecondAnimation;
        
        internal System.Windows.Media.RotateTransform SecondHandTransform;
        
        internal System.Windows.Media.RotateTransform MinuteHandTransform;
        
        internal System.Windows.Media.RotateTransform HourHandTransform;
        
        internal System.Windows.Controls.ProgressBar myProgressBar;
        
        internal System.Windows.Controls.ListBox alarmList;
        
        internal System.Windows.Controls.Button btnAddAlarm;
        
        internal System.Windows.Controls.Button btndeleteAlarm;
        
        internal System.Windows.Controls.ListBox FirstListBox;
        
        internal System.Windows.Controls.Button btnAddComponent;
        
        internal System.Windows.Controls.Button btndeleteComponent;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/SeizeDay;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentGrid = ((System.Windows.Controls.Grid)(this.FindName("ContentGrid")));
            this.ClockFaceEllipse = ((System.Windows.Shapes.Ellipse)(this.FindName("ClockFaceEllipse")));
            this.ClockHandsCanvas = ((System.Windows.Controls.Canvas)(this.FindName("ClockHandsCanvas")));
            this.ClockStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("ClockStoryboard")));
            this.HourAnimation = ((System.Windows.Media.Animation.DoubleAnimation)(this.FindName("HourAnimation")));
            this.MinuteAnimation = ((System.Windows.Media.Animation.DoubleAnimation)(this.FindName("MinuteAnimation")));
            this.SecondAnimation = ((System.Windows.Media.Animation.DoubleAnimation)(this.FindName("SecondAnimation")));
            this.SecondHandTransform = ((System.Windows.Media.RotateTransform)(this.FindName("SecondHandTransform")));
            this.MinuteHandTransform = ((System.Windows.Media.RotateTransform)(this.FindName("MinuteHandTransform")));
            this.HourHandTransform = ((System.Windows.Media.RotateTransform)(this.FindName("HourHandTransform")));
            this.myProgressBar = ((System.Windows.Controls.ProgressBar)(this.FindName("myProgressBar")));
            this.alarmList = ((System.Windows.Controls.ListBox)(this.FindName("alarmList")));
            this.btnAddAlarm = ((System.Windows.Controls.Button)(this.FindName("btnAddAlarm")));
            this.btndeleteAlarm = ((System.Windows.Controls.Button)(this.FindName("btndeleteAlarm")));
            this.FirstListBox = ((System.Windows.Controls.ListBox)(this.FindName("FirstListBox")));
            this.btnAddComponent = ((System.Windows.Controls.Button)(this.FindName("btnAddComponent")));
            this.btndeleteComponent = ((System.Windows.Controls.Button)(this.FindName("btndeleteComponent")));
        }
    }
}

