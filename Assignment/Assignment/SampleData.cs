using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Assignment
{
    public class SampleData : ISampleData
    {
        private List<string> _CsvRows;

        public SampleData() //Path issue needs to be fixed
        {
            string currentPath = Directory.GetCurrentDirectory();
            _CsvRows = File.ReadAllLines("N:\\Assignment5+6\\Assignment\\Assignment\\People.csv").Skip(1).ToList();
            if (_CsvRows is null)
            {
                throw new ArgumentNullException("Error, reading lines from file went bad"); ;
            }
        }

        // 1.
        public IEnumerable<string> CsvRows { get { return _CsvRows; } }

        // 2.
        public IEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows()
        {
            List<string> states = new List<string>();
            
            states = CsvRows.Select(item => item.Split(",")).OrderBy(item => item[6]).Select(item => item[6]).Distinct().ToList();
            
            return states;
        }

        // 3.
        public string GetAggregateSortedListOfStatesUsingCsvRows() 
        {
            string statesString = "";
            IEnumerable<string> statesList = GetUniqueSortedListOfStatesGivenCsvRows();
            statesString = statesList.Aggregate((state1, state2) => state1 + ", " + state2);
            return statesString;
        }

        // 4.
        public IEnumerable<IPerson> People { get { 
                IEnumerable<Person> people = CsvRows.Select(x => x.Split(",")).Select(x => new Person(x[1], x[2], new Address(x[4], x[5], x[6], x[7]), x[3])).ToList();
                return People;
            }
            
        }
        // 5. ------> Finished?
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
            

/*            IEnumerable<(string firstName, string lastName)> people = People.Select(person => (person.FirstName, person.LastName));
*/            //var people = People.Contains(filter).Select(p => new { p.FirstName, p.LastName });
            //return people.Where(x => filter());
            return people;
        }

        // 6.
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
