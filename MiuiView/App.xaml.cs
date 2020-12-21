using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MiuiView
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void App_OnDispatcherUnhandledException(object sender,
            DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                Console.WriteLine(e.Exception.Message);
            }
            catch { }

            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender,
            UnhandledExceptionEventArgs e)
        {
            try
            {
                var ex = e.ExceptionObject as Exception;
                Console.WriteLine(ex.Message);
            }
            catch { }
        }
    }
}
