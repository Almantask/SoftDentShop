using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        Stopwatch sw = new Stopwatch();
        BlockingCollection<int> _fibonaccis;

        public Form1()
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            InitializeComponent();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            label1.Text = ((Exception)e.ExceptionObject).Message;
            Thread.Sleep(2000);
        }

        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            label1.Text = e.Exception.Message;
            Thread.Sleep(2000);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            label2.Text = Thread.CurrentThread.ManagedThreadId.ToString();
            label1.Text = "---------";
            // wtf #1: Updated UI from another thread...

            var t1 = Task.Run(() =>
            {
                for (var i = 0; i < 1000; i++)
                {
                    label1.Text = $"#{i} Task 11111";
                }
                Thread.Sleep(5000);
                label1.Text = "Done 1111111";
                label2.Text = Thread.CurrentThread.ManagedThreadId.ToString();
            }
            );
            var t2 = Task.Run(() =>
            {
                for (var i = 0; i < 1000; i++)
                {
                    label1.Text = $"#{i} Task 222222";
                }
                Thread.Sleep(5000);
                label1.Text = "Done 22222";
                label2.Text = Thread.CurrentThread.ManagedThreadId.ToString();
            }
            );
            await Task.WhenAll(t1, t2);
        }

        async Task Test()
        {
            //wtf?
            await Task.Delay(5000);
        }

        async Task Bar()
        {
            await GetIntAsync();
        }

        async Task<int> GetIntAsync()
        {
            richTextBox1.Text += $"Thread:{Thread.CurrentThread.ManagedThreadId}, Started: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} \r\n";
            await Task.Delay(500);
            richTextBox1.Text += $"Thread:{Thread.CurrentThread.ManagedThreadId}, Ended {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} \r\n";
            return 5;
        }

        int GetInt()
        {
            richTextBox1.Text += $"Thread:{Thread.CurrentThread.ManagedThreadId}, Started: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} \r\n";
            Thread.Sleep(500);
            richTextBox1.Text += $"Thread:{Thread.CurrentThread.ManagedThreadId}, Ended {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} \r\n";
            return 5;
        }

        async Task<int> OnButtonClickedAsync()
        {
            var t1 = GetIntAsync();
            var t2 = GetIntAsync();
            var t3 = GetIntAsync();

            //var t4 = Task.Run(() => GetInt());
            //var t5 = Task.Run(() => GetInt());
            //var t6 = Task.Run(() => GetInt());

            var n1 = await t1;
            var n2 = await t2;
            var n3 = await t3;

            //var n4 = await t4;
            //var n5 = await t5;
            //var n6 = await t6;

            var result = n1 + n2 + n3;// + n4 + n5 + n6;
            return result;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var sum = await OnButtonClickedAsync();
                label3.Text = sum.ToString();
                MessageBox.Show(sum.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FibonacciTestParallel();
        }

        private void FibonacciTestParallel()
        {
            sw.Reset();
            sw.Start();
            _fibonaccis = new BlockingCollection<int>();
            var obj = new object();
            var sum = 0;
            Parallel.Invoke(

                () =>
                {
                    var fibo = Fibonnaci(40);
                    lock (obj)
                    {
                        sum += fibo;
                    }
                },
                                () =>
                                {
                                    var fibo = Fibonnaci(40);
                                    lock (obj)
                                    {
                                        sum += fibo;
                                    }
                                },
                                () =>
                                {
                                    var fibo = Fibonnaci(40);
                                    lock (obj)
                                    {
                                        sum += fibo;
                                    }
                                },
                                () =>
                                {
                                    var fibo = Fibonnaci(40);
                                    lock (obj)
                                    {
                                        sum += fibo;
                                    }
                                },
                                () =>
                                {
                                    var fibo = Fibonnaci(40);
                                    lock (obj)
                                    {
                                        sum += fibo;
                                    }
                                },
                                () =>
                                {
                                    var fibo = Fibonnaci(40);
                                    lock (obj)
                                    {
                                        sum += fibo;
                                    }
                                }
               );

            sw.Stop();
            richTextBox1.Text += "Parallel.Invoke(): \r\n";
            richTextBox1.Text += $"Total time elapsed: {sw.ElapsedMilliseconds}ms \r\n";
            richTextBox1.Text += $"Sum = {sum} \r\n";
        }

        private async Task<int> FibonnaciTaskTestAsync()
        {
            sw.Reset();
            sw.Start();
            _fibonaccis = new BlockingCollection<int>();

            var t1 = Task.Factory.StartNew(
                () => _fibonaccis.Add(Fibonnaci(40))
                );
            var t2 = Task.Factory.StartNew(
                 () => _fibonaccis.Add(Fibonnaci(40))
                 );
            var t3 = Task.Factory.StartNew(
                 () => _fibonaccis.Add(Fibonnaci(40))
                 );
            var t4 = Task.Factory.StartNew(
                 () => _fibonaccis.Add(Fibonnaci(40))
                 );
            var t5 = Task.Factory.StartNew(
                 () => _fibonaccis.Add(Fibonnaci(40))
                 );
            var t6 = Task.Factory.StartNew(
             () => _fibonaccis.Add(Fibonnaci(40))
             );

            await Task.WhenAll(t1, t2, t3, t4, t5, t6);
            var sum = _fibonaccis.Sum();
            sw.Stop();


            return sum;
        }

        // 1 1 2 3 5 
        public int Fibonnaci(int n)
        {
            if (n <= 2) return 1;
            return Fibonnaci(n - 1) + Fibonnaci(n - 2);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var sum = await FibonnaciTaskTestAsync();
            richTextBox1.Text += "Task.Factory.StartNew: \r\n";
            richTextBox1.Text += $"Total time elapsed: {sw.ElapsedMilliseconds}ms \r\n";
            richTextBox1.Text += $"Sum = {sum} \r\n";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FibonacciTestSync();
        }

        private void FibonacciTestSync()
        {
            long sum = 0;
            sw.Reset();
            sw.Start();
            sum += Fibonnaci(40);
            sum += Fibonnaci(40);
            sum += Fibonnaci(40);
            sum += Fibonnaci(40);
            sum += Fibonnaci(40);
            sum += Fibonnaci(40);
            sw.Stop();
            richTextBox1.Text += "Sync: \r\n";
            richTextBox1.Text += $"Total time elapsed: {sw.ElapsedMilliseconds}ms \r\n";
            richTextBox1.Text += $"Sum = {sum} \r\n";
        }
    }
}
