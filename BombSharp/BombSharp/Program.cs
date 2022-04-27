namespace BombSharp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {   
            ApplicationConfiguration.Initialize();
            Menu menu = new Menu();

            menu.KeyPreview = true;
            
            Application.Run(menu);
        }
    }
}