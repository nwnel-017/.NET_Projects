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

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Error, file does not exist");
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
                IEnumerable<Person> people = CsvRows.Select(item => item.Split(",")).Select(item => new Person(item[1], item[2], new Address(item[4], item[5], item[6], item[7]), item[3])).ToList();
                return People;
            }
            
        }
        // 5. -> Finished?
        public IEnumerable<(string FirstName, string LastName)> FilterByEmailAddress( 
            Predicate<string> filter)
        {
            string? filterString = filter.ToString();
            IEnumerable<(string firstName, string lastName)>? people = null;
            if (filterString == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }
            
            people =
                (IEnumerable<(string firstName, string lastName)>)_CsvRows.
                Select(item => item.Split(",")).
                OrderBy(item => item[3]).
                Where(item => item[3].
                Contains(char.Parse(filterString))).
                Select(item => item[1] + " " + item[2]).ToList();

            if(people == null)
            {
                throw new ArgumentNullException(nameof(people));
            }
                      
            return people;
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
