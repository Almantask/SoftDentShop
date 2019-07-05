using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftdentShop.Presentation.Desktop.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            label1.Content = Thread.CurrentThread.ManagedThreadId.ToString();
            // wtf #1: Updated UI from another thread...
            await Task.Run(() =>
            {
                label1.Content = "Started";
                Thread.Sleep(5000);
                label1.Content = "Done";
                label2.Content = Thread.CurrentThread.ManagedThreadId.ToString();
            }
            );
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            label1.Content = ((Exception)e.ExceptionObject).Message;
        }

        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            label1.Content = e.Exception.Message;
        }

    }
}
