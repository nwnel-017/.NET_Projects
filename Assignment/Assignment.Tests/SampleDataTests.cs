using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;

namespace Assignment.Tests
{
    //All of my tests were running but out of nowhere half of them stopped working because my "file doesnt exist"\
    //i wasnt sure what to do because a lot of my tests still ran even though others were saying my filepath was invalid
    //i used a global variable for filepath so im thinking there is something wrong with my machine
    [TestClass]
    public class SampleDataTests
    {
        public string FilePath = @"N:\\Assignment5+6\\Assignment\\Assignment\\People.csv";//I understand this isn't the right way
        //because the filepath on your machine is different--> i tried using the statement below but it crashed on my system
        //every time--> my machine was being unpredictable and doing different things every time i ran the program, i think it was
        //a problem with the computer


        //public string FilePath = AppDomain.CurrentDomain.BaseDirectory + "People.csv";//--> Why isnt this working

        [TestMethod]//Test passed
        [ExpectedException(typeof(FileNotFoundException))]
        public void InitializeSampleDataClass_WithBadPath_Failure()
        {
            SampleData sampleData = new("this is not a valid path");
            Assert.IsNull(sampleData);
        }

        [TestMethod]//Test passed
        [ExpectedException(typeof(ArgumentNullException))]
        public void InitializeSampleDataClass_WithEmptyFile_Failure()
        {
            string path = "blankfile.csv";
            File.WriteAllText(path, "");
            SampleData sampleData = new(path);
            File.Delete(path);
        }

        [TestMethod]//Test passed
        public void InitializeSampleDataClass_Success()
        {
            SampleData sampleData = new(FilePath);
            Assert.IsNotNull(sampleData);
            Assert.IsNotNull(sampleData.CsvRows);
        }

        [TestMethod]//Test passed
        public void GetCsvRows_ReturnsSuccessfully()
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
        public void Part2_GetUniqueSortedListOfState_StatesAreUnique_Success()
        {
            SampleData sampleData = new(FilePath);
            IEnumerable<string> states = sampleData.GetUniqueSortedListOfStatesGivenCsvRows();

            Assert.IsNotNull(states);//make sure list returned properly
            Assert.IsTrue(states.Distinct().Count() == states.Count());//make sure states list is unique
        }

        [TestMethod]//Test passed
        public void Part2_GetUniqueSortedListOfState_StatesAreSorted_Success()
        {
            SampleData sampleData = new(FilePath);
            IEnumerable<string> result = sampleData.GetUniqueSortedListOfStatesGivenCsvRows();
            IEnumerable<string> expected = sampleData.CsvRows.Select(item => item.Split(',')).Select(column => column[6]).OrderBy(state => state).Distinct();

            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]//Test passes
        public void Part2_GetUniqueSortedListOfStatesGivenCsvRows_UsingHardCodedAddressed_Success()
        {
            string[] hardCodedData = {
                "",
                "1,Nate,Nelson,natenelson@gmail.com,1111 N. Eastern St., Spokane,WA,99205",
                "1,Danny,Allen,dannyallen@gmail.com,2222 N. Eastern St., Spokane,WA,99205",
                "1,Bryan,Wais,bryanwais@gmail.com,3333 N. Eastern St., Spokane,WA,99205",
            };

            string tempFilePath = "TemporaryFile.csv";
            File.WriteAllLines(tempFilePath, hardCodedData);

            SampleData sampleData = new(tempFilePath);

            IEnumerable<string> spokaneAddresses = sampleData.GetUniqueSortedListOfStatesGivenCsvRows();
            Assert.AreEqual(1, spokaneAddresses.Count());
            File.Delete(tempFilePath);
        }

        [TestMethod]//Test passed
        public void Part3_GetAggregatedSortedListOfStatesUsingCsvRows_ReturnsActuallist_Success()
        {
            SampleData sampleData = new(FilePath);
            string? listOfStates = sampleData.GetAggregateSortedListOfStatesUsingCsvRows();

            Assert.IsNotNull(listOfStates);
        }

        [TestMethod]//Test passed
        public void Part3_GetAggregateSortedListOfStatesUsingCsvRows_IsCorrectLength_Success()
        {
            SampleData sampleData = new(FilePath);
            string listOfStates = sampleData.GetAggregateSortedListOfStatesUsingCsvRows();//method to test
            IEnumerable<string> states = sampleData.GetUniqueSortedListOfStatesGivenCsvRows();

            Assert.AreEqual(listOfStates.Split(", ").Length, states.Count());//successfully puts every state into string
        }

        [TestMethod]//Test passed
        public void Part3_GetAggregatedSortedListOfStatesUsingCsvRows_StatesAreUnique_Success()
        {
            SampleData sampleData = new(FilePath);
            string listOfStates = sampleData.GetAggregateSortedListOfStatesUsingCsvRows();//method to test
            Assert.AreEqual(listOfStates.Split(", ").ToList().Distinct().Count(), listOfStates.Split(", ").Length);
        }

        [TestMethod]//Test passed
        public void Part4_GetPeople_ReturnsList_Success()
        {
            SampleData sampleData = new(FilePath);

            Assert.IsNotNull(sampleData);
            Assert.IsNotNull(sampleData.People);
        }

        [TestMethod]//Why is test not running
        public void Part4_GetPeople_ReturnsListOfCorrectLength_Success()
        {
            SampleData sampleData = new(FilePath);

            Assert.IsNotNull(sampleData);
            Assert.IsNotNull(sampleData.People);

            Assert.IsTrue(sampleData.People.Count() == sampleData.CsvRows.Count());
        }


        [TestMethod]//Test finished-> but not running for some reason
        public void Part5_FilterByEmailAddress_CorrectFilter_Success() //Fix this
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