using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Assignment
{
    public class SampleData : ISampleData
    {
        private IEnumerable<string> _CsvRows;
        private IEnumerable<Person> _People;

        public SampleData()
        {
            List<string> rows = new();
            List<Person> people = new();
            using (var reader = new StreamReader("People.csv"))
            {
                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    if(line == null)
                    {
                        throw new ArgumentNullException(nameof(line));
                    }
                    rows.Add(line);
                }
                _CsvRows = rows;

                foreach(string row in _CsvRows)
                {
                    string [] cols = row.Split(',');
                    string firstName = cols[1];
                    string lastName = cols[2];
                    string email = cols[3];
                    string address = cols[4];
                    string city = cols[5];
                    string state = cols[6];
                    string zip = cols[7];

                    Person person = new(firstName, lastName, new Address(address, city, state, zip), email);
                    people.Add(person);
                }

                people.OrderBy(x => x.Address);
                _People = people;
            }
        }

        // 1.
        public IEnumerable<string> CsvRows { get { return _CsvRows; } }

        // 2.
        public IEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows()
        {
            List<string> states = new List<string>();
            foreach(string row in _CsvRows)
            {
                string[] array = row.Split(',');
                states.Add(array[6]);
            }
            if(states is null || states.Count == 0)
            {
                throw new ArgumentNullException();
            }
            states.OrderBy(x => x).Distinct().ToList();          
            
            return states;
        }

        // 3.
        public string GetAggregateSortedListOfStatesUsingCsvRows() //This seems easier than what the instructions said to do?????
        {
            IEnumerable<string> states = GetUniqueSortedListOfStatesGivenCsvRows();

            string statesString = "";

            foreach(string state in states){
                statesString += state + " ";
            }
            
            return statesString;
        }

        // 4.
        public IEnumerable<IPerson> People { get { return _People; } }
        // 5.
        public IEnumerable<(string FirstName, string LastName)> FilterByEmailAddress(
            Predicate<string> filter) => throw new NotImplementedException();

        // 6.
        public string GetAggregateListOfStatesGivenPeopleCollection(
            IEnumerable<IPerson> people) => throw new NotImplementedException();
    }
}
