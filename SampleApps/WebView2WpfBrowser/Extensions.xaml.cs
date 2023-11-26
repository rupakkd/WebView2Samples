using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Web.WebView2.Core;

namespace WebView2WpfBrowser
{
    /// <summary>
    /// Interaction logic for Extensions.xaml
    /// </summary>
    public partial class Extensions : Window
    {
        private readonly CoreWebView2 _coreWebView2;
        public Extensions(CoreWebView2 coreWebView2)
        {
            _coreWebView2 = coreWebView2;
            InitializeComponent();
#if USE_WEBVIEW2_EXPERIMENTAL
            _ = FillViewAsync();
#endif
        }

        public class ListEntry
        {
            public string Name;
            public string Id;
            public bool Enabled;

            public override string ToString()
            {
                return (Enabled ? "" : "Disabled ") + Name + " (" + Id + ")";
            }
        }

        readonly List<ListEntry> _listData = new List<ListEntry>();

#if USE_WEBVIEW2_EXPERIMENTAL
        private async Task FillViewAsync()
        {
            var extensionsList = await _coreWebView2.Profile.GetBrowserExtensionsAsync();

            _listData.Clear();
            for (var i = 0; i < extensionsList.Count; ++i)
            {
                var entry = 
                    new ListEntry
                {
                    Name = extensionsList[i].Name,
                    Id = extensionsList[i].Id,
                    Enabled = extensionsList[i].IsEnabled
                };
                _listData.Add(entry);
            }

            ExtensionsList.ItemsSource = _listData;
            ExtensionsList.Items.Refresh();
        }
#endif

        private void ExtensionsToggleEnabled(object sender, RoutedEventArgs e)
        {
            _ = ExtensionsToggleEnabledAsync(sender, e);
        }

        private async Task ExtensionsToggleEnabledAsync(object sender, RoutedEventArgs e)
        {
#if USE_WEBVIEW2_EXPERIMENTAL
            var entry = (ListEntry)ExtensionsList.SelectedItem;
            var extensionsList = await _coreWebView2.Profile.GetBrowserExtensionsAsync();
            var found = false;
            for (var i = 0; i < extensionsList.Count; ++i)
            {
                if (extensionsList[i].Id == entry.Id)
                {
                    try
                    {
                        await extensionsList[i].EnableAsync(!extensionsList[i].IsEnabled);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Failed to toggle extension enabled: " + exception);
                    }

                    found = true;
                    break;
                }
            }

            if (!found)
            {
                MessageBox.Show("Failed to find extension");
            }

            await FillViewAsync();
#else
            await Task.CompletedTask;
#endif
        }

        private void ExtensionsAdd(object sender, RoutedEventArgs e)
        {
            _ = ExtensionsAddAsync(sender, e);
        }

        private async Task ExtensionsAddAsync(object sender, RoutedEventArgs e)
        {
#if USE_WEBVIEW2_EXPERIMENTAL
            var dialog = new TextInputDialog(
                title: "Add extension",
                description: "Enter the absolute Windows file path to the unpackaged browser extension",
                defaultInput: "");
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var extension = await _coreWebView2.Profile.AddBrowserExtensionAsync(dialog.Input.Text);
                    MessageBox.Show("Added extension " + extension.Name + " (" + extension.Id + ")");
                    await FillViewAsync();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Failed to add extension: " + exception);
                }
            }
#else
            await Task.CompletedTask;
#endif
        }

        private void ExtensionsRemove(object sender, RoutedEventArgs e)
        {
            _ = ExtensionsRemoveAsync(sender, e);
        }

        private async Task ExtensionsRemoveAsync(object sender, RoutedEventArgs e)
        {
#if USE_WEBVIEW2_EXPERIMENTAL
            var entry = (ListEntry)ExtensionsList.SelectedItem;
            if (MessageBox.Show("Remove extension " + entry + "?", "Confirm removal", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                var extensionsList = await _coreWebView2.Profile.GetBrowserExtensionsAsync();
                var found = false;
                for (var i = 0; i < extensionsList.Count; ++i)
                {
                    if (extensionsList[i].Id == entry.Id)
                    {
                        try
                        {
                            await extensionsList[i].RemoveAsync();
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show("Failed to remove extension: " + exception);
                        }

                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    MessageBox.Show("Failed to find extension");
                }
            }

            await FillViewAsync();
#else
            await Task.CompletedTask;
#endif
        }
    }
}
