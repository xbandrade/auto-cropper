namespace ImgAutoCropper
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new AutoCropper());
        }
    }
}