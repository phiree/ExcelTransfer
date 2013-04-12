using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace ETBiz
{
    public class IOHelper
    {

        public static string[] ReadAllLinesFromFile(string filePath)
        {

            FileInfo file = EnsureFile(filePath);
            string[] result = File.ReadAllLines(filePath);

            return result;
        }
        public static FileInfo EnsureFile(string filepath)
        {
            FileInfo fi = new FileInfo(filepath);
            if (fi.Exists)
            {
                return fi;
            }
            string directory = Path.GetDirectoryName(filepath);
            if (!Directory.Exists(directory))
            {
                DirectoryInfo di = Directory.CreateDirectory(directory);
            }
            FileStream fs = fi.Create();
            fs.Close();
            return fi;


        }
    }

    public class TextHelper
    { 
     
    }
}
