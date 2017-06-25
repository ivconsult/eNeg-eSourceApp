
#region → Usings   .

using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Browser;
using System.Threading;
using System.Globalization;
using citPOINT.eSourceApp.Common;

#endregion

#region → History  .

/* Date         User              Change
 * 20.01.12     Yousra Reda      Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eSourceApp.Client
{
    /// <summary>
    /// Mian Class Or Start Point Class
    /// </summary>
    public partial class App : Application
    {

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
          
            // Set the current UI culture.
            Thread.CurrentThread.CurrentCulture = new CultureInfo("En-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("En-US");

            //Register for most used event handlers of App
            this.Startup += this.Application_Startup;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        #endregion Constructor

        #region → Event Handlers .

        /// <summary>
        /// Handler for Application Start Up.
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of StartupEventArgs </param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Helper.Run();
            this.RootVisual = new MainPageView();
        }

        /// <summary>
        /// Handle Application Unhandled Exception
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of ApplicationUnhandledExceptionEventArgs </param>
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                e.Handled = true;

                if (!(e.ExceptionObject is System.InvalidOperationException))
                {
                    Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e.ExceptionObject); });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Used to Report Error To DOM (Mean Html Side)
        /// </summary>
        /// <param name="e">Value of ApplicationUnhandledExceptionEventArgs</param>
        private void ReportErrorToDOM(Exception e)
        {
            try
            {


                string errorMsg = e.Message + e.StackTrace;


                if (e.InnerException != null)
                {
                    errorMsg += "\r\n---------Inner-----------\r\n";
                    errorMsg += e.InnerException.Message + e.InnerException.StackTrace;


                    if (e.InnerException.InnerException != null)
                    {
                        errorMsg += "\r\n---------Inner Inner-----------\r\n";
                        errorMsg += e.InnerException.InnerException.Message + e.InnerException.InnerException.StackTrace;
                    }

                }

                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Alert("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {

            }
        }

        #endregion Event Handlers
    }
}
