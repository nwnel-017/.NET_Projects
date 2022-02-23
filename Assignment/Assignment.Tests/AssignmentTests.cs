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
        public string FilePath = "N:\\Assignment5+6\\Assignment\\Assignment\\People.csv";
        //public string FilePath = AppDomain.CurrentDomain.BaseDirectory;

        [TestMethod]//Test passed
        [ExpectedException(typeof(FileNotFoundException))]
        public void InitializeSampleDataClass_WithBadPath_Failure()//Works as expected
        {
            SampleData sampleData = new("this is not a valid path");
        }

        [TestMethod]//Test not passing 
        [ExpectedException(typeof(Exception))]
        public void InitializeSampleDataClass_WithEmptyFile_Failure()
        {
            string path = ".\\blankfile.csv";
            File.WriteAllText(path, "");
            SampleData sampleData = new("EmptyFile.csv");
            File.Delete("EmptyFile.csv");
        }

        [TestMethod]//Test passed
        public void InitializeSampleDataClass_Success()
        {
            SampleData sampleData = new(FilePath);
            Assert.IsNotNull(sampleData);
            Assert.IsNotNull(sampleData.CsvRows);
        }

        [TestMethod]//Test passed
        public void GetCsvRows_ReturnsSuccessfully()//Test passed
        {
            SampleData sampleData = new(FilePath);
            string csvFile = FilePath;
            string[] lines = System.IO.File.ReadAllLines(csvFile);
            int length = lines.Length - 1;

            Assert.IsNotNull(sampleData.CsvRows);
            Assert.AreEqual(sampleData.CsvRows.Count(), length);
        }

        [TestMethod]//Test passed
        public void GetCsvRows_SkippedFirstLine_Success()
        {
            SampleData sampleData = new(FilePath);
            IEnumerable<string> rows = sampleData.CsvRows;

            Assert.IsFalse(rows.First().Contains("Id"));
            Assert.IsFalse(rows.First().Contains("FirstName"));
            Assert.IsFalse(rows.First().Contains("LastName"));
            Assert.IsFalse(rows.First().Contains("Email"));
            Assert.IsFalse(rows.First().Contains("StreetAddress"));
            Assert.IsFalse(rows.First().Contains("City"));
            Assert.IsFalse(rows.First().Contains("State"));
            Assert.IsFalse(rows.First().Contains("Zip"));
        }

        [TestMethod]//Test passed
        public void GetUniqueSortedListOfState_StatesAreUnique_Success()
        {
            SampleData sampleData = new(FilePath);
            IEnumerable<string> states = sampleData.GetUniqueSortedListOfStatesGivenCsvRows();

            Assert.IsNotNull(states);//make sure list returned properly
            Assert.IsTrue(states.Distinct().Count() == states.Count());//make sure states list is unique
        }

        /*[TestMethod]//Not done
        public void GetUniqueSortedListOfState_StatesAreSorted_Success()
        {
            SampleData sampleData = new(FilePath);
            IEnumerable<string> states = sampleData.GetUniqueSortedListOfStatesGivenCsvRows();
            bool result = true;
            result = states.ForEach((state1, state2) => string.Compare(state1, state2) <= 0);
            Assert.IsTrue(result);
        }*/

        [TestMethod]
        public void GetUniqueSortedListOfStatesGivenCsvRows_UsingHardCodedAddressed_Success()
        {
            SampleData sampleData = new(FilePath);
            Assert.IsNotNull(sampleData);
        }

        [TestMethod]//Test passed
        public void GetAggregatedSortedListOfStatesUsingCsvRows_ReturnsActuallist_Success()
        {
            SampleData sampleData = new(FilePath);
            string? listOfStates = sampleData.GetAggregateSortedListOfStatesUsingCsvRows();//method to test

            Assert.IsNotNull(listOfStates);//states are not null
        }

        [TestMethod]//Test passed
        public void GetAggregateSortedListOfStatesUsingCsvRows_IsCorrectLength_Success() //Fix this test---> line 49
        {
            SampleData sampleData = new(FilePath);
            string listOfStates = sampleData.GetAggregateSortedListOfStatesUsingCsvRows();//method to test
            IEnumerable<string> states = sampleData.GetUniqueSortedListOfStatesGivenCsvRows();

            Assert.AreEqual(listOfStates.Split(", ").Length, states.Count());//successfully puts every state into string
        }

        [TestMethod]//Test passed
        public void GetAggregatedSortedListOfStatesUsingCsvRows_StatesAreUnique_Success()
        {
            SampleData sampleData = new(FilePath);
            string listOfStates = sampleData.GetAggregateSortedListOfStatesUsingCsvRows();//method to test
            Assert.AreEqual(listOfStates.Split(", ").ToList().Distinct().Count(), listOfStates.Split(", ").Length);//states are not unique??
        }

        [TestMethod]//Test passed
        public void GetPeople_ReturnsList_Success()
        {
            SampleData sampleData = new(FilePath);

            Assert.IsNotNull(sampleData);
            Assert.IsNotNull(sampleData.People);
        }

        [TestMethod]//Why is test not running
        public void GetPeople_ReturnsListOfCorrectLength_Success()
        {
            SampleData sampleData = new(FilePath);

            Assert.IsNotNull(sampleData);
            Assert.IsNotNull(sampleData.People);

            Assert.IsTrue(sampleData.People.Count() == sampleData.CsvRows.Count());
        }

        [TestMethod]//Test finished-> but not running for some reason
        public void FilterByEmailAddress_CorrectFilter_Success() //Fix this
        {
            SampleData sampleData = new(FilePath);
            Predicate<string> email = email => email.Contains("stanford");
            IEnumerable<(string FirstName, string LastName)> result = sampleData.FilterByEmailAddress(email);
            Assert.IsNotNull(result);

            Assert.AreEqual("Sancho", result.First().FirstName);
            Assert.AreEqual("Mahony", result.First().LastName);

            Assert.AreEqual("Fayette", result.Last().FirstName);
            Assert.AreEqual("Dougherty", result.Last().LastName);
        }


    }
}