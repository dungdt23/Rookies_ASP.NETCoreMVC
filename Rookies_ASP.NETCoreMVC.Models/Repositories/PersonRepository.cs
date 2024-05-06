using Rookies_ASP.NETCoreMVC.Models.DTOs;
using Rookies_ASP.NETCoreMVC.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookies_ASP.NETCoreMVC.BusinessLogic.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public static IEnumerable<Person> _people;
        public PersonRepository()
        {
            _people = GetPeopleData();
        }
        public IEnumerable<string> GetFullNames()
        {
            var fullNames = _people.Select(student => student.LastName + " " + student.FirstName).ToList();
            return fullNames;
        }
        public Person? GetTheOldest()
        {
            if (_people.Count() == 0) return null;
            var oldestPerson = _people.OrderBy(student => student.DateOfBirth).FirstOrDefault();
            return oldestPerson;
        }
        public IEnumerable<Person> GetPeople(FilterPersonDto? filterPersonDto)
        {
            IEnumerable<Person> resultPeople = _people;
            if (filterPersonDto == null) return resultPeople;
            if (filterPersonDto.Gender.HasValue)
                resultPeople = resultPeople.Where(person => person.Gender == filterPersonDto.Gender.Value);
            if (filterPersonDto.YearOfBirth.HasValue)
                resultPeople = resultPeople.Where(person => person.DateOfBirth.Year == filterPersonDto.YearOfBirth);
            return resultPeople;
        }
        public IEnumerable<Person> GetPeopleData()
        {
            IEnumerable<Person> people = new List<Person>
            {
                new Person
                {
                    FirstName = "Hoa",
                    LastName = "Truong",
                    Gender = TypeGender.Male,
                    Age = 22,
                    DateOfBirth = new DateTime(2002, 11, 11),
                    PhoneNumber = "0983327119",
                    Birthplace = "Vinh Phuc",
                    IsGraduated = false
                },
                new Person
                {
                    FirstName = "Anh",
                    LastName = "Nguyen",
                    Gender = TypeGender.Male,
                    Age = 24,
                    DateOfBirth = new DateTime(2000, 11, 11),
                    PhoneNumber = "0983327119",
                    Birthplace = "Ha Noi",
                    IsGraduated = true
                },
                new Person
                {
                    FirstName = "Minh",
                    LastName = "Hoang",
                    Gender = TypeGender.Male,
                    Age = 23,
                    DateOfBirth = new DateTime(2001, 11, 11),
                    PhoneNumber = "0983327119",
                    Birthplace = "Ha Noi",
                    IsGraduated = true
                },
                new Person
                {
                    FirstName = "Hieu",
                    LastName = "Mai",
                    Gender = TypeGender.Male,
                    Age = 22,
                    DateOfBirth = new DateTime(2002, 11, 11),
                    PhoneNumber = "0983327119",
                    Birthplace = "Ha Noi",
                    IsGraduated = false
                },
                new Person
                {
                    FirstName = "Vu",
                    LastName = "La",
                    Gender = TypeGender.Male,
                    Age = 25,
                    DateOfBirth = new DateTime(1999, 11, 11),
                    PhoneNumber = "0983327119",
                    Birthplace = "Ha Noi",
                    IsGraduated = true
                },
                new Person
                {
                    FirstName = "Phuong",
                    LastName = "Nguyen",
                    Gender = TypeGender.Male,
                    Age = 22,
                    DateOfBirth = new DateTime(2002, 11, 11),
                    PhoneNumber = "0983327119",
                    Birthplace = "Sai Gon",
                    IsGraduated = false
                },
                new Person
                {
                    FirstName = "Quang",
                    LastName = "Nguyen",
                    Gender = TypeGender.Male,
                    Age = 22,
                    DateOfBirth = new DateTime(2002, 1, 1),
                    PhoneNumber = "0983327119",
                    Birthplace = "Phu Tho",
                    IsGraduated = false
                },
                new Person
                {
                    FirstName = "Hoang",
                    LastName = "Le",
                    Gender = TypeGender.Male,
                    Age = 23,
                    DateOfBirth = new DateTime(2001, 11, 11),
                    PhoneNumber = "0983327119",
                    Birthplace = "Ha Noi",
                    IsGraduated = true
                },
                new Person
                {
                    FirstName = "Manh",
                    LastName = "Phan",
                    Gender = TypeGender.Male,
                    Age = 24,
                    DateOfBirth = new DateTime(2000, 11, 11),
                    PhoneNumber = "0983327119",
                    Birthplace = "Ha Noi",
                    IsGraduated = true
                },
                new Person
                {
                    FirstName = "Hoang",
                    LastName = "Thai",
                    Gender = TypeGender.Male,
                    Age = 27,
                    DateOfBirth = new DateTime(1997, 11, 11),
                    PhoneNumber = "0983327119",
                    Birthplace = "Hai Phong",
                    IsGraduated = false
                },
                new Person
                {
                    FirstName = "Nguyen",
                    LastName = "Mai",
                    Gender = TypeGender.Female,
                    Age = 27,
                    DateOfBirth = new DateTime(1997, 11, 11),
                    PhoneNumber = "0983327119",
                    Birthplace = "Hai Phong",
                    IsGraduated = false
                }
            };
            return people;
        }
    }
}
