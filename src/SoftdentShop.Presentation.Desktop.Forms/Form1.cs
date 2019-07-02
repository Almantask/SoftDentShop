using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftdentShop.Presentation.Desktop.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            InitializeComponent();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            label1.Text = ((Exception)e.ExceptionObject).Message;
        }

        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            label1.Text = e.Exception.Message;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            label2.Text = Thread.CurrentThread.ManagedThreadId.ToString();
            //label1.Text = "Started";
            //await Task.Delay(5000);
            //label2.Text = Thread.CurrentThread.ManagedThreadId.ToString();
            //label1.Text = "Done";
            //// wtf
            //var thread = new Thread(new ThreadStart(
            //    () =>
            //    {
            //        label1.Text = "Started";
            //        Thread.Sleep(5000);
            //        label1.Text = "Done";
            //    }
            //    )
            //);
            //thread.Start();

            await Task.Run(() =>
            {
                label1.Text = "Started";
                Thread.Sleep(5000);
                label1.Text = "Done";
                label2.Text = Thread.CurrentThread.ManagedThreadId.ToString();
            }
            );
            ////Thread.Sleep(5000);

        }

        Task Test()
        {
            //v wtf?
            Thread.Sleep(5000);
            return Task.CompletedTask;
        }
    }
}
