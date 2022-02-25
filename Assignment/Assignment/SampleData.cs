using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Assignment
{
    public class SampleData : ISampleData
    {
        private List<string> _CsvRows;

        public SampleData(string filePath)
        {

            if (!File.Exists(filePath))//case if file does not exist
            {
                throw new FileNotFoundException("Error, file does not exist");
            }
            if (new FileInfo(filePath).Length == 0)//case if file passed in is empty
            {
                throw new ArgumentNullException("Error, file is empty");
            }

            try
            {
                _CsvRows = File.ReadAllLines(filePath).Skip(1).ToList();
            }catch (Exception)
            {
                throw new ArgumentNullException("could not read from file");
            }
            
            if (_CsvRows is null)
            {
                throw new ArgumentNullException("Error, reading lines from file went bad"); ;
            }
        }

        // 1.->Finished
        public IEnumerable<string> CsvRows { get { return _CsvRows; } }

        // 2.->Finished
        public IEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows()
        {
            List<string> states = new List<string>();
            
            states = CsvRows.Select(item => item.Split(",")).OrderBy(item => item[6]).Select(item => item[6]).Distinct().ToList();
            states.Zip(states, (current, next) => string.Compare(current, next) <= 0).All(x => x);
            return states;
        }

        // 3.->Finished
        public string GetAggregateSortedListOfStatesUsingCsvRows() 
        {
            string statesString = "";
            IEnumerable<string> statesList = GetUniqueSortedListOfStatesGivenCsvRows();
            statesString = statesList.Aggregate((state1, state2) => state1 + ", " + state2);
            return statesString;
        }

        // 4.->Finished
        public IEnumerable<IPerson> People { get {
                IEnumerable<IPerson> people =
                    CsvRows.Select(line => line.Split(",")).OrderBy(line => line[5]).ThenBy(line => line[6]).ThenBy(line => line[7])
                    .Select(item => new Person(item[1], item[2], new Address(item[4], item[5], item[6], item[7]), item[3])).ToList();
                return people;
            }
            
        }
        // 5. -> Finished?
        public IEnumerable<(string FirstName, string LastName)> FilterByEmailAddress( 
            Predicate<string> filter)
        {
            List<IPerson> people = People.ToList();
            IEnumerable<(string FirstName, string LastName)> results = 
                People.ToList().FindAll(person => filter(person.EmailAddress)).
                Select(person => (person.FirstName, person.LastName));

            return results;
        }

        // 6.->Finished
        public string GetAggregateListOfStatesGivenPeopleCollection(
            IEnumerable<IPerson> people)
        {
            if(people == null)
            {
                throw new ArgumentNullException("error, bad people list");
            }

            IEnumerable<string> statesList = people.Select(person => (person.Address.State)).Distinct().ToList();
            return statesList.Aggregate((state1, state2) => state1 + ", " + state2);
        }
    }
}
