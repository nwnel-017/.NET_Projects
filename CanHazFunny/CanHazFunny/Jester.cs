using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanHazFunny
{
    public class Jester
    {
        public Output? Output { get; set; }
        public JokeService? JokeService { get; set; }

        public Jester(Output? output, JokeService? jokeService)
        {
            if(output != null)
            {
                Output = output;
            }
            if(jokeService != null)
            {
                JokeService = jokeService;
            }
        }

        public bool TellJoke()
        {
            if (JokeService == null || Output == null) { return false; }
            
            
            string joke = JokeService.GetJoke();


            while (joke.Contains("Chuck Norris"))
            {
                joke = JokeService.GetJoke();
            }

            Output.WriteToConsole(joke);
            return true;
                

        }


    }
}
