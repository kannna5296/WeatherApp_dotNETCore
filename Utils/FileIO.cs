using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp_dotNETCore.Utils
{
    public class FileIO
    {
        public static string FileToStr(string file_in)
        {
            StreamReader fp_in = new StreamReader(file_in);

            string buff;

            StringBuilder stb = new StringBuilder();

            while ((buff = fp_in.ReadLine()) != null)
            {
                stb.Append(buff);
            }

            fp_in.Close();

            string str_in = stb.ToString();

            return str_in;
        }
    }
}
