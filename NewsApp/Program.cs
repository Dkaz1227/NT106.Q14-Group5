using NewsApp.UI; 
using System;
using System.Windows.Forms;

namespace NewsApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Bắt đầu ứng dụng bằng cách chạy LoginForm
            Application.Run(new LoginForm());
        }
    }
}