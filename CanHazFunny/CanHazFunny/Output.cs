using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanHazFunny
{
    public class Output : IOutputable
    {
        public void WriteToConsole(string joke)
        { 
            Console.WriteLine(joke);
        }

    }
}
