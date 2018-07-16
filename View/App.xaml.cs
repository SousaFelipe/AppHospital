using System;
using System.Windows;


namespace View
{
    public partial class App : Application
    {
        private void Start(object sender, StartupEventArgs e)
        {
            try
            {
                Window window = new Window();
                window.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}