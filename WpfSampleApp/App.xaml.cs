using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfSampleApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += new StartupEventHandler(App_Startup); // Can be called from XAML 

            DispatcherUnhandledException += App_DispatcherUnhandledException; //Example 2 

            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException; //Example 4 

            System.Windows.Forms.Application.ThreadException += WinFormApplication_ThreadException; //Example 5 
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            //Here if called from XAML, otherwise, this code can be in App() 
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException; // Example 1 
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; // Example 3 
        }

        void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.StackTrace, e.Exception.Message);
            e.Handled = true;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (e.IsTerminating)
            {
                //Now is a good time to write that critical error file! 
                MessageBox.Show("Goodbye world!");
            }
        }

        void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
        }

        void WinFormApplication_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
        }
    }
}
