// Copyright (C) Microsoft Corporation. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using System.Windows;

namespace WebView2WpfBrowser
{
    /// <summary>
    /// Interaction logic for SaveAsDialog.xaml
    /// </summary>
    public partial class SaveAsDialog : Window
    {
        void OK_Clicked(object sender, RoutedEventArgs args) { this.DialogResult = true; }
    }
}
