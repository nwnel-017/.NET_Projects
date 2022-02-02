using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CanHazFunny.Tests
{
    [TestClass]
    public class JesterTests
    {
        [TestMethod]
        public void WorkingJokeService_ReturnsJoke()
        {
            JokeService jokeService = new();
            string joke = jokeService.GetJoke();
            Assert.IsNotNull(joke);
        }
        
        [TestMethod]
        public void InitializeJesterClass_Success()
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void Jester_TellJoke_BadJokeServiceField_Fail()
        {
            Jester testJester = new(new Output(), null);
            Assert.IsNotNull(testJester);
            string joke = testJester.TellJoke();
            Assert.IsFalse(String.IsNullOrEmpty(joke));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Jester_TellJoke_BadOutputField_Fail()
        {
            Jester testJester = new(null, new JokeService());
            Assert.IsNotNull(testJester);
            string joke = testJester.TellJoke();
            Assert.IsFalse(String.IsNullOrEmpty(joke));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Jester_TellJoke_BothFieldsAreBad_Fail()
        {
            Jester testJester = new(null, null);
            Assert.IsNotNull(testJester);
            string joke = testJester.TellJoke();
            Assert.IsFalse(String.IsNullOrEmpty(joke));
        }

        [TestMethod]
        public void Jester_TellJoke_Successful()
        {
            Jester testJester = new(new Output(), new JokeService());
            Assert.IsNotNull(testJester);
            string joke = testJester.TellJoke();
            Assert.IsFalse(String.IsNullOrEmpty(joke));
        }
    }
}
