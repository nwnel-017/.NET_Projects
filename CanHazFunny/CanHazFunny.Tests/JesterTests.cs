using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanHazFunny.Tests
{
    [TestClass]
    public class JesterTests
    {
        [TestMethod]
        public void InitializeJesterClassSuccess()
        {
            Output output = new();
            JokeService jokeService = new();
            Assert.IsNotNull(jokeService);
            Assert.IsNotNull(output);

            Jester jester = new(output, jokeService);
            Assert.IsNotNull(jester);
            Assert.AreEqual(jokeService, jester.JokeService);
            Assert.AreEqual(output, jester.Output);
        }

        [TestMethod]
        public void JesterTellJoke_TellJoke_Success()
        {
            Jester testJester = new(new Output(), new JokeService());
            Assert.IsNotNull(testJester);   

        }

        [TestMethod]
/*        [ExpectedException(typeof(ArgumentException), "error, bad output or jokeservice fields")]
*/        public void Jester_TellJoke_BadFields_Unsuccessful()
        {
            Jester testJester = new(null, null);
            Assert.IsNotNull(testJester);
            bool tellJokeResult = testJester.TellJoke();
            Assert.IsFalse(tellJokeResult);
        }

        [TestMethod]
        public void Jester_TellJoke_Successful()
        {
            Jester testJester = new(new Output(), new JokeService());
            Assert.IsNotNull(testJester);
            bool tellJokeResult = testJester.TellJoke();
            Assert.IsTrue(tellJokeResult);
        }
    }
}
