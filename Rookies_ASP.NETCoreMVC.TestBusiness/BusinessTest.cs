using ClosedXML.Excel;
using Moq;
using Rookies_ASP.NETCoreMVC.BusinessLogic.BusinessLogic;
using Rookies_ASP.NETCoreMVC.BusinessLogic.Repositories;
using Rookies_ASP.NETCoreMVC.Models.DTOs;
using Rookies_ASP.NETCoreMVC.Models.Models;
using Rookies_ASP.NETCoreMVC.Shared.Constants;
using System.Data;

namespace Rookies_ASP.NETCoreMVC.TestBusiness
{
    public class BusinessTest
    {
        Mock<IPersonRepository> mockRepository;
        [OneTimeSetUp]
        public void Setup()
        {
            mockRepository = new Mock<IPersonRepository>();
        }

        [Test]
        public void GetExcelFile_ReturnWorkBook()
        {
            // Arrange
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
                }
            };
            mockRepository.Setup(m => m.GetAllPeople()).Returns(people);
            IPersonBusinessLogic personBusinessLogic = new PersonBusinessLogic(mockRepository.Object);

            // Act
            var wb = personBusinessLogic.GetExcelFile();

            // Assert
            Assert.IsInstanceOf<XLWorkbook>(wb);

        }
        [Test]
        public void GetFullNames_ReturnListFullNames()
        {
            // Arrange
            mockRepository.Setup(m => m.GetFullNames()).Returns(new List<string> { "John Wick", "Adam Warlock" });
            IPersonBusinessLogic personBusinessLogic = new PersonBusinessLogic(mockRepository.Object);

            // Act
            var fullNames = personBusinessLogic.GetFullNames();

            // Assert
            Assert.AreEqual(2, fullNames.Count());
        }
        [Test]
        public void GetPeople_ReturnListPeople()
        {
            // Arrange
            mockRepository.Setup(m => m.GetPeople(It.IsAny<FilterPersonDto>(), null, null))
                     .Returns(new List<Person>
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
                            }
                       });
            IPersonBusinessLogic personBusinessLogic = new PersonBusinessLogic(mockRepository.Object);

            // Act
            var people = personBusinessLogic.GetPeople(It.IsAny<FilterPersonDto>(), null, null);

            // Assert
            Assert.AreEqual(2, people.Count());
        }
        [Test]
        public void GetTheOldest_ReturnPersonModel()
        {
            // Arrange
            mockRepository.Setup(m => m.GetTheOldest())
                .Returns(new Person
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
                });
            IPersonBusinessLogic personBusinessLogic = new PersonBusinessLogic(mockRepository.Object);

            // Act
            var oldest = personBusinessLogic.GetTheOldest();

            // Assert
            Assert.NotNull(oldest);
        }
        [Test]
        public void Add_Person_ValidRecord()
        {
            //Arrange
            var person = new Person
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
            };
            mockRepository.Setup(m => m.Add(person));
            IPersonBusinessLogic personBusinessLogic = new PersonBusinessLogic(mockRepository.Object);

            // Act
            var status = personBusinessLogic.Add(person);


            // Assert
            Assert.AreEqual(ConstantsStatus.Success, status);
        }
        [Test]
        public void Add_Person_InvalidRecord()
        {
            //Arrange
            var person = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "Anh",
                LastName = "Nguyen",
                Gender = TypeGender.Male,
                Age = 24,
                DateOfBirth = new DateTime(2000, 11, 11),
                PhoneNumber = "09833271",
                Birthplace = "Ha Noi",
                IsGraduated = true
            };
            mockRepository.Setup(m => m.Add(person));
            IPersonBusinessLogic personBusinessLogic = new PersonBusinessLogic(mockRepository.Object);

            // Act
            var status = personBusinessLogic.Add(person);


            // Assert
            Assert.AreEqual(ConstantsStatus.Failed, status);
        }
        [Test]
        public void Update_ByIdAndPerson_ValidRecord()
        {
            //Arrange
            var person = new Person
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
            };
            mockRepository.Setup(m => m.Update(person.Id, person))
                          .Returns(ConstantsStatus.Success);
            IPersonBusinessLogic personBusinessLogic = new PersonBusinessLogic(mockRepository.Object);

            // Act
            var status = personBusinessLogic.Update(person.Id, person);


            // Assert
            Assert.AreEqual(ConstantsStatus.Success, status);
        }
        [Test]
        public void Update_ByIdAndPerson_InvalidRecord()
        {
            //Arrange
            var person = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "",
                LastName = "Nguyen",
                Gender = TypeGender.Male,
                Age = 24,
                DateOfBirth = new DateTime(2000, 11, 11),
                PhoneNumber = "098371",
                Birthplace = "Ha Noi",
                IsGraduated = true
            };
            mockRepository.Setup(m => m.Update(person.Id, person))
                          .Returns(ConstantsStatus.Failed);
            IPersonBusinessLogic personBusinessLogic = new PersonBusinessLogic(mockRepository.Object);

            // Act
            var status = personBusinessLogic.Update(person.Id, person);


            // Assert
            Assert.AreEqual(ConstantsStatus.Failed, status);
        }
        [Test]
        public void Delete_ById_ValidRecord()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            mockRepository.Setup(m => m.Delete(id))
                          .Returns(ConstantsStatus.Success);
            IPersonBusinessLogic personBusinessLogic = new PersonBusinessLogic(mockRepository.Object);

            // Act
            var status = personBusinessLogic.Delete(id);


            // Assert
            Assert.AreEqual(ConstantsStatus.Success, status);
        }
        [Test]
        public void GetPersonById_Id_ReturnNullablePersonModel()
        {
            //Arrange
            Guid id = Guid.NewGuid();

            mockRepository.Setup(m => m.GetPersonById(id))
                          .Returns(new Person { Id = id });
            IPersonBusinessLogic personBusinessLogic = new PersonBusinessLogic(mockRepository.Object);

            // Act
            var person = personBusinessLogic.GetPersonById(id);


            // Assert
            Assert.IsInstanceOf<Person?>(person);
        }
        [Test]
        public void GetAllPeople_Id_ReturnListPeople()
        {
            //Arrange

            mockRepository.Setup(m => m.GetAllPeople())
                       .Returns(new List<Person>
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
                            }
                       });
            IPersonBusinessLogic personBusinessLogic = new PersonBusinessLogic(mockRepository.Object);

            // Act
            var people = personBusinessLogic.GetAllPeople();


            // Assert
            Assert.AreEqual(2, people.Count());
        }
        [Test]
        public void GetAllPeopleBaseOnAge_ByExpressionPageIndexPageSize_ReturnListPeople()
        {
            //Arrange
            mockRepository.Setup(m => m.GetPeopleBaseOnAge(It.IsAny<Func<Person, bool>>(), 1, 2))
           .Returns(new List<Person>
           {
                            new Person
                            {
                                Id = Guid.NewGuid(),
                                FirstName = "Hoa",
                                LastName = "Truong",
                                Gender = TypeGender.Male,
                                Age = 23,
                                DateOfBirth = new DateTime(2001, 11, 11),
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
                                Age = 21,
                                DateOfBirth = new DateTime(2003, 11, 11),
                                PhoneNumber = "0983327119",
                                Birthplace = "Ha Noi",
                                IsGraduated = true
                            }
           });
            IPersonBusinessLogic personBusinessLogic = new PersonBusinessLogic(mockRepository.Object);

            // Act
            Func<Person, bool> condition = s => s.DateOfBirth.Year > 200;
            var people = personBusinessLogic.GetPeopleBaseOnAge(condition, 1, 2);


            // Assert
            Assert.AreEqual(2, people.Count());
        }
    }
}