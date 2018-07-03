using System.IO;
using System.Reflection;


namespace View
{
    public abstract class Path
    {
        public static string Resources
        {
            get
            {
                string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                return System.IO.Path.Combine(Directory.GetParent(path).Parent.FullName, "Resources") + "\\";
            }
        }
    }
}
