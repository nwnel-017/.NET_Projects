using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;

namespace Assignment.Tests
{
    [TestClass]
    public class AssignmentTests
    {
        [TestMethod]
        public void InitializeSampleDataClass_Success()//Works as expected
        {
            SampleData sampleData = new();
            Assert.IsNotNull(sampleData);
        }

        [TestMethod]
        public void GetCsvRows_ReturnsSuccessfully()//Works as expected-> change path
        {
            SampleData sampleData = new();
            string csvFile = "N:\\Assignment5+6\\Assignment\\Assignment\\People.csv"; //This path needs to be changed at some point
            string[] lines = System.IO.File.ReadAllLines(csvFile);
            int length = lines.Length - 1;

            Assert.IsNotNull(sampleData.CsvRows);
            Assert.AreEqual(sampleData.CsvRows.Count(), length);
        }

        [TestMethod]
        public void GetUniqueSortedListOfState_ReturnsSuccessfully()//Need to check that list is alphabetical
        {
            SampleData sampleData = new();
            IEnumerable<string> states = sampleData.GetUniqueSortedListOfStatesGivenCsvRows();
            //still need to check if states are alphabetical

            Assert.IsNotNull(states);//make sure list returned properly
            Assert.IsTrue(states.Distinct().Count() == states.Count());//make sure states list is unique
        }

        [TestMethod]
        public void GetAggregateSortedListOfStatesUsingCsvRows_ReturnsSuccessfully() //Fix this test---> line 49
        {
            SampleData sampleData = new();
            string? listOfStates = sampleData.GetAggregateSortedListOfStatesUsingCsvRows();//method to test
            IEnumerable<string> states = sampleData.GetUniqueSortedListOfStatesGivenCsvRows();

            Assert.IsNotNull(listOfStates);//states are not null
            Assert.AreEqual(listOfStates.Split(", ").Length, states.Count());//successfully puts every state into string
            //Assert.IsTrue(listOfStates.Split(", ").ToList().Distinct().Count() == listOfStates.Count());//states are not unique??
        }

        [TestMethod]
        public void GetPeople_ReturnsSuccess()//Why isnt this method running
        {
            SampleData sampleData = new();

            Assert.IsNotNull(sampleData.People);//get people method is returning something
        }

        [TestMethod]
        public void FilterByEmailAddress_IncorrectFilter_Failure() //Fix this
        {
            SampleData sampleData = new();
            Predicate<string>? predicate = "bad";
            IEnumerable<(string FirstName, string LastName)> filteredList = sampleData.FilterByEmailAddress(predicate);
        }
    }
}