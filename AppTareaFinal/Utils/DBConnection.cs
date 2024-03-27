

namespace AppTareaFinal.Utils
{
    public class DBConnection
    {
        public static string  getPath(string dataBaseName)
        {
            string path = string.Empty;
            if(DeviceInfo.Platform == DevicePlatform.Android)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                path = Path.Combine(path, dataBaseName);
            }

            return path;
        }
    }
}
