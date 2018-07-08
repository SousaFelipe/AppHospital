using View.Adapter;


namespace View
{
    public abstract class Single
    {
        public static Window                MainWindow      { get; private set; }



        public static void SetMainWindow(Window mainWindow)
        {
            if (MainWindow == null)
            {
                MainWindow = mainWindow;
            }
        }
    }
}
