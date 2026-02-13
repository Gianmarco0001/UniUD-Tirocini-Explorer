using System.Diagnostics;
using System.Windows;

namespace UniUdTirocini
{
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnLinkedIn_Click(object sender, RoutedEventArgs e)
        {
            ApriLink("https://www.linkedin.com/in/gianmarco-benedetti/");
        }

        private void BtnGitHub_Click(object sender, RoutedEventArgs e)
        {
            ApriLink("https://github.com/Gianmarco0001");
        }

        private void ApriLink(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch { }
        }
    }
}