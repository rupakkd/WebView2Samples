using System.Windows;

namespace WebView2WpfBrowser
{
    /// <summary>
    /// Interaction logic for TextInputDialog.xaml
    /// </summary>
    public partial class TextInputDialog : Window
    {
        public TextInputDialog(
            string title = null,
            string description = null,
            string defaultInput = null)
        {
            InitializeComponent();
            if (title != null)
            {
                Title = title;
            }

            if (description != null)
            {
                Description.Text = description;
            }

            if (defaultInput != null)
            {
                Input.Text = defaultInput;
            }

            Input.Focus();
            Input.SelectAll();
        }

        void Ok_Clicked(object sender, RoutedEventArgs args)
        {
            this.DialogResult = true;
        }
    }
}
