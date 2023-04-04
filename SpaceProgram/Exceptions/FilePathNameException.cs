using SpaceProgram.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Exceptions
{
    internal class FilePathNameException:Exception
    {
        public FilePathNameException(string message):base(message)
        {
            Console.WriteLine($"{LanguageHelper.GetString("fileNotFound")}");
        }
    }
}
