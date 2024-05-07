using Rookies_ASP.NETCoreMVC.Models.DTOs;
using Rookies_ASP.NETCoreMVC.Models.Models;
using Rookies_ASP.NETCoreMVC.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Rookies_ASP.NETCoreMVC.BusinessLogic.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public static IEnumerable<Person> _people;
        public PersonRepository()
        {
            _people = InitialData();
        }
        public void Add(Person person)
        {
            //convert IEnumerable to List in order to modify elements
            List<Person> peopleList = _people.ToList();
            peopleList.Add(person);
            _people = peopleList;
        }
        public int Delete(Guid id)
        {
            //convert IEnumerable to List in order to modify elements
            List<Person> peopleList = _people.ToList();
            var deletePerson = peopleList.FirstOrDefault(x => x.Id == id);
            if (deletePerson != null) peopleList.Remove(deletePerson);
            else return ConstantsStatus.Failed;
            if (peopleList.Count < _people.Count())
            {
                _people = peopleList;
                return ConstantsStatus.Success;
            }
            else return ConstantsStatus.Failed;

        }
        public int Update(Guid id, Person person)
        {
            //convert IEnumerable to List in order to modify elements
            List<Person> peopleList = _people.ToList();
            var updatePerson = peopleList.FirstOrDefault(x => x.Id == id);
            if (updatePerson == null) return ConstantsStatus.Failed;
            else
            {
                updatePerson.Gender = person.Gender;
                updatePerson.FirstName = person.FirstName;
                updatePerson.LastName = person.LastName;
                updatePerson.DateOfBirth = person.DateOfBirth;
                updatePerson.PhoneNumber = person.PhoneNumber;
                updatePerson.Birthplace = person.Birthplace;
                updatePerson.IsGraduated = person.IsGraduated;
                updatePerson.Age = person.Age;
                return ConstantsStatus.Success;
            }

        }
        public IEnumerable<Person> GetPeopleBaseOnAge(Func<Person, bool> condition, int? startIndex, int? size)
        {
            var people = _people.Where(condition).ToList();
            if (startIndex.HasValue && size.HasValue)
                return people.Skip((startIndex.Value - 1) * size.Value).Take(size.Value);
            else
                return people;
        }
        public Person? GetPersonById(Guid id)
        {
            return _people.FirstOrDefault(person => person.Id == id);
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
        public IEnumerable<Person> GetPeople(FilterPersonDto? filterPersonDto, int? startIndex, int? size)
        {
            IEnumerable<Person> resultPeople = _people;
            if (filterPersonDto != null)
            {
                if (filterPersonDto.Gender.HasValue)
                    resultPeople = resultPeople.Where(person => person.Gender == filterPersonDto.Gender.Value);
                if (filterPersonDto.YearOfBirth.HasValue)
                    resultPeople = resultPeople.Where(person => person.DateOfBirth.Year == filterPersonDto.YearOfBirth);
                var x = resultPeople.ToList();
            }
            if (startIndex.HasValue && size.HasValue)
            {
                resultPeople = resultPeople.Skip((startIndex.Value - 1) * size.Value).Take(size.Value);
            }
            return resultPeople;
        }
        public IEnumerable<Person> GetAllPeople()
        {
            return _people;
        }
        private IEnumerable<Person> InitialData()
        {
            IEnumerable<Person> people = new List<Person>
            {
                new Person
                {
                    Id = Guid.NewGuid(),
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
                    Id = Guid.NewGuid(),
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
                {   Id = Guid.NewGuid(),
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
                {   Id = Guid.NewGuid(),
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
                {   Id = Guid.NewGuid(),
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
                {   Id = Guid.NewGuid(),
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
                {   Id = Guid.NewGuid(),
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
                {   Id = Guid.NewGuid(),
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
                {   Id = Guid.NewGuid(),
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
                {   Id = Guid.NewGuid(),
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
                {   Id = Guid.NewGuid(),
                    FirstName = "Nguyen",
                    LastName = "Mai",
                    Gender = TypeGender.Female,
                    Age = 27,
                    DateOfBirth = new DateTime(1997, 11, 11),
                    PhoneNumber = "0983327119",
                    Birthplace = "Hai Phong",
                    IsGraduated = false
                },
                new Person
                {   Id = Guid.NewGuid(),
                    FirstName = "Tran",
                    LastName = "Duong",
                    Gender = TypeGender.Female,
                    Age = 22,
                    DateOfBirth = new DateTime(2002, 11, 11),
                    PhoneNumber = "0983327119",
                    Birthplace = "Hai Phong",
                    IsGraduated = false
                }
            };
            return people;
        }
    }
}
