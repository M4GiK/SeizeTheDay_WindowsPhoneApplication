﻿#pragma checksum "C:\studia\project\repo6\endProject\SeizeDay\SeizeDay\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2835F11CF47892D02C29E693F2058D5B"
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
        
        internal System.Windows.Controls.ProgressBar myProgressBar;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock EmptyTextBlock;
        
        internal System.Windows.Controls.Button btnAddAlarm;
        
        internal System.Windows.Controls.ListBox NotificationListBox;
        
        internal System.Windows.Controls.ListBox FirstListBox;
        
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
            this.myProgressBar = ((System.Windows.Controls.ProgressBar)(this.FindName("myProgressBar")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.EmptyTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("EmptyTextBlock")));
            this.btnAddAlarm = ((System.Windows.Controls.Button)(this.FindName("btnAddAlarm")));
            this.NotificationListBox = ((System.Windows.Controls.ListBox)(this.FindName("NotificationListBox")));
            this.FirstListBox = ((System.Windows.Controls.ListBox)(this.FindName("FirstListBox")));
        }
    }
}

