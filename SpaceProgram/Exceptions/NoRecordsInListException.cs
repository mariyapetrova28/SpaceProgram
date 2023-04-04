using SpaceProgram.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Exceptions
{
    internal class NoRecordsInListException:Exception
    {
        public NoRecordsInListException(string message) : base(message) 
        {
            Console.WriteLine($"{LanguageHelper.GetString("recordsList")}");
        }
    }
}
