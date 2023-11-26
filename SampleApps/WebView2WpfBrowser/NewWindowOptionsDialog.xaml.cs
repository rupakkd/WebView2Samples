// Copyright (C) Microsoft Corporation. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using System.Windows;
using Microsoft.Web.WebView2.Wpf;

namespace WebView2WpfBrowser
{
    /// <summary>
    /// Interaction logic for NewWindowOptionsDialog.xaml
    /// </summary>
    public partial class NewWindowOptionsDialog : Window
    {
        public NewWindowOptionsDialog()
        {
            InitializeComponent();

            CreationProperties = new CoreWebView2CreationProperties();
            BrowserExecutableFolder.Focus();
            BrowserExecutableFolder.SelectAll();
        }

        private CoreWebView2CreationProperties _creationProperties = null;
        public CoreWebView2CreationProperties CreationProperties
        {
            get
            {
                return _creationProperties;
            }
            set
            {
                _creationProperties = value;
                if (_creationProperties == null)
                {
                    // Reset the controls to defaults.
                    BrowserExecutableFolder.Text = null;
                    UserDataFolder.Text = null;
                    EnvLanguage.Text = null;
                    ProfileName.Text = null;
                    comboBox_IsInPrivateModeEnabled.SelectedIndex = 2;
                    ScriptLocale.Text = null;
                }
                else
                {
                    // Copy the values to the controls.
                    BrowserExecutableFolder.Text = _creationProperties.BrowserExecutableFolder;
                    UserDataFolder.Text = _creationProperties.UserDataFolder;
                    EnvLanguage.Text = _creationProperties.Language;
                    ProfileName.Text = _creationProperties.ProfileName;
                    ScriptLocale.Text = _creationProperties.ScriptLocale;
                    switch (_creationProperties.IsInPrivateModeEnabled)
                    {
                        case null:
                            comboBox_IsInPrivateModeEnabled.SelectedIndex = 2;
                            break;
                        case true:
                            comboBox_IsInPrivateModeEnabled.SelectedIndex = 0;
                            break;
                        case false:
                            comboBox_IsInPrivateModeEnabled.SelectedIndex = 1;
                            break;
                    }
                }
            }
        }

        void OK_Clicked(object sender, RoutedEventArgs args)
        {
            CreationProperties.BrowserExecutableFolder = BrowserExecutableFolder.Text == "" ? null : BrowserExecutableFolder.Text;
            CreationProperties.UserDataFolder = UserDataFolder.Text == "" ? null : UserDataFolder.Text;
            CreationProperties.Language = EnvLanguage.Text == "" ? null : EnvLanguage.Text;
            CreationProperties.ProfileName = ProfileName.Text == "" ? null : ProfileName.Text;
            CreationProperties.ScriptLocale = ScriptLocale.Text == "" ? null : ScriptLocale.Text;
            switch (comboBox_IsInPrivateModeEnabled.SelectedIndex)
            {
                case 0:
                    CreationProperties.IsInPrivateModeEnabled = true;
                    break;
                case 1:
                    CreationProperties.IsInPrivateModeEnabled = false;
                    break;
                case 2:
                    CreationProperties.IsInPrivateModeEnabled = null;
                    break;
            }

            this.DialogResult = true;
        }
    }
}
