using System;
using System.Windows.Forms;


namespace OldPhoneApp
{
    static class Program
    {
        [STAThread] // Specifies single-threaded apartment model for UI
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); 
            Application.Run(new Form1()); // Starts the application with Form1
        }
    }
}
