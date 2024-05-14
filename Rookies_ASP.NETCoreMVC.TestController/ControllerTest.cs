using Castle.Core.Logging;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Rookies_ASP.NETCoreMVC.BusinessLogic.BusinessLogic;
using Rookies_ASP.NETCoreMVC.Models.DTOs;
using Rookies_ASP.NETCoreMVC.Models.Models;
using Rookies_ASP.NETCoreMVC.Shared.Constants;
using Rookies_ASP.NETCoreMVC.WebApp.Areas.NashTech.Controllers;
using System.Security.Cryptography.Xml;

namespace Rookies_ASP.NETCoreMVC.TestController
{
    public class ControllerTest
    {
        Mock<IPersonBusinessLogic> mockService;
        Mock<ILogger<RookiesController>> mockLogger;
        [OneTimeSetUp]
        public void ClassInit()
        {
            mockService = new Mock<IPersonBusinessLogic>();
            mockLogger = new Mock<ILogger<RookiesController>>();
        }
        [Test]
        public void Detail_ById_RetrieveNull()
        {
            // Arrange
            Person? person = null;
            mockService.Setup(m => m.GetPersonById(Guid.NewGuid())).Returns(person);
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            RedirectToActionResult actionResult = (RedirectToActionResult)rookiesController.Detail(Guid.NewGuid());

            // Assert
            Assert.AreEqual("Index", actionResult.ActionName);
        }

        [Test]
        public void Detail_ById_RetrieveValidPerson()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Person? person = new Person
            {
                Id = id,
                FirstName = "John",
                LastName = "Max",
                Gender = TypeGender.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "0983311009",
                Birthplace = "Ha Noi"
            };
            mockService.Setup(m => m.GetPersonById(id)).Returns(person);
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            ViewResult viewResult = (ViewResult)rookiesController.Detail(id);

            // Assert
            Assert.AreEqual(person, viewResult.Model);
        }
        [Test]
        public void Add_NoInput_ReturnAddView()
        {
            // Arrange
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.Add() as ViewResult;

            // Assert
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }
        [Test]
        public void Add_PersonModel_ReturnIndexAction()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Person? person = new Person
            {
                Id = id,
                FirstName = "John",
                LastName = "Max",
                Gender = TypeGender.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "0983311009",
                Birthplace = "Ha Noi"
            };
            mockService.Setup(m => m.Add(person)).Returns(ConstantsStatus.Success);
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = rookiesController.Add(person) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Index", actionResult.ActionName);
        }
        [Test]
        public void Add_PersonModel_ReturnViewWithErrorMessage()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Person? person = new Person
            {
                Id = id,
                FirstName = "John",
                LastName = "Max",
                Gender = TypeGender.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "098331x009",
                Birthplace = "Ha Noi"
            };
            mockService.Setup(m => m.Add(person)).Returns(ConstantsStatus.Failed);
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.Add(person) as ViewResult;
            var errorMessage = viewResult.ViewData["errorMessage"] as string;

            // Assert
            Assert.AreEqual("Phone Number is not valid!", errorMessage);
        }
        [Test]
        public void Add_PersonModel_ReturnViewWithInvalidModel()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Person? person = new Person
            {
                Id = id,
                FirstName = "",
                LastName = "Max",
                Gender = TypeGender.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "098331x009",
                Birthplace = string.Empty
            };
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.Add(person) as ViewResult;

            // Assert
            Assert.AreEqual(person, viewResult.Model);
        }
        [Test]
        public void Delete_ById_ReturnIndexAction()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            mockService.Setup(m => m.Delete(id)).Returns(ConstantsStatus.Success);
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = rookiesController.Delete(id) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Index", actionResult.ActionName);
        }
        [Test]
        public void Update_ById_ReturnIndexAction()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Person? person = null;
            mockService.Setup(m => m.GetPersonById(id)).Returns(person);
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = rookiesController.Update(id) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Index", actionResult.ActionName);

        }
        [Test]
        public void Update_ById_ReturnView()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Person? person = new Person
            {
                Id = id,
                FirstName = "John",
                LastName = "Max",
                Gender = TypeGender.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "0983311009",
                Birthplace = "Ha Noi"
            };
            mockService.Setup(m => m.GetPersonById(id)).Returns(person);
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.Update(id) as ViewResult;

            // Assert
            Assert.AreEqual(person, viewResult.Model);

        }
        [Test]
        public void Update_ByPersonModel_ReturnIndexAction()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Person? person = new Person
            {
                Id = id,
                FirstName = "John",
                LastName = "Max",
                Gender = TypeGender.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "0983311009",
                Birthplace = "Ha Noi"
            };
            mockService.Setup(m => m.Update(id, person)).Returns(ConstantsStatus.Success);
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var actionResult = rookiesController.Update(person) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Index", actionResult.ActionName);

        }
        [Test]
        public void Update_ByPersonModel_ReturnViewWithInvalidModel()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Person? person = new Person
            {
                Id = id,
                FirstName = "John",
                LastName = "",
                Gender = TypeGender.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "0983311009",
                Birthplace = ""
            };
            mockService.Setup(m => m.Update(id, person)).Returns(ConstantsStatus.Failed);
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.Update(person) as ViewResult;

            // Assert
            Assert.AreEqual(person, viewResult.Model);
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }
        [Test]
        public void Update_ByPersonModel_ReturnViewWithErrorMessage()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Person? person = new Person
            {
                Id = id,
                FirstName = "John",
                LastName = "",
                Gender = TypeGender.Male,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "0983311111009",
                Birthplace = ""
            };
            mockService.Setup(m => m.Update(id, person)).Returns(ConstantsStatus.Failed);
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.Update(person) as ViewResult;
            var errorMessage = viewResult.ViewData["errorMessage"] as string;

            // Assert
            Assert.AreEqual("Phone Number is not valid!", errorMessage);
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }
        [Test]
        public void GetMaleStudent_ReturnViewWithListPeople()
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
            mockService.Setup(m => m.GetPeople(It.IsAny<FilterPersonDto>(), null, null))
            .Returns(people);
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.GetMaleStudents() as ViewResult;
            var result = viewResult.ViewData["people"] as IEnumerable<Person>;

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }
        [Test]
        public void GetTheOldest_ReturnViewWithPersonModel()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            mockService.Setup(m => m.GetTheOldest())
                .Returns(new Person
                {
                    Id = id,
                    FirstName = "John",
                    LastName = "Max",
                    Gender = TypeGender.Male,
                    DateOfBirth = DateTime.Now,
                    PhoneNumber = "0983311009",
                    Birthplace = "Ha Noi"
                });
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.GetTheOldest() as ViewResult;
            var oldest = viewResult.ViewData["oldestPerson"] as Person;

            // Assert
            Assert.IsNotNull(oldest);
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }
        [Test]
        public void GetFullName_ReturnViewWithListPeople()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            mockService.Setup(m => m.GetFullNames())
                .Returns(new List<string> { "John Wick", "Nick James" });
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.GetFullName() as ViewResult;
            var fullNames = viewResult.ViewData["fullNames"] as IEnumerable<string>;

            // Assert
            Assert.AreEqual(2, fullNames.Count());
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }
        [Test]
        public void Index_InputBirthYearPageIndexPageSize_ReturnViewWithoutBirthYearFilter()
        {
            mockService.Setup(m => m.GetPeople(null, 1, 2))
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
            mockService.Setup(m => m.GetAllPeople())
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
                            },
                            new Person
                            {
                                Id = Guid.NewGuid(),
                                FirstName = "John",
                                LastName = "Max",
                                Gender = TypeGender.Male,
                                DateOfBirth = DateTime.Now,
                                PhoneNumber = "0983311009",
                                Birthplace = "Ha Noi"
                            },
                            new Person
                            {
                                Id = Guid.NewGuid(),
                                FirstName = "Nick",
                                LastName = "Max",
                                Gender = TypeGender.Male,
                                DateOfBirth = DateTime.Now,
                                PhoneNumber = "0983311009",
                                Birthplace = "Ha Noi"
                            }
           });
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.Index(null, 1, 2) as ViewResult;
            var people = viewResult.ViewData["people"] as IEnumerable<Person>;

            // Assert
            Assert.AreEqual(2, people.Count());
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }
        [Test]
        public void Index_InputBirthYearPageIndexPageSize_ReturnViewWithBirthYearFilter()
        {
            mockService.Setup(m => m.GetPeople(It.IsAny<FilterPersonDto>(), 1, 2))
                       .Returns(new List<Person>
                       {
                            new Person
                            {
                                Id = Guid.NewGuid(),
                                FirstName = "Hoa",
                                LastName = "Truong",
                                Gender = TypeGender.Male,
                                Age = 22,
                                DateOfBirth = new DateTime(2000, 11, 11),
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
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.Index("2002", 1, 2) as ViewResult;
            var people = viewResult.ViewData["people"] as IEnumerable<Person>;

            // Assert
            Assert.AreEqual(2, people.Count());
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }
        [Test]
        public void Index_InputBirthYearPageIndexPageSize_ReturnViewWithErrorMessage()
        {
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.Index("2002x", 1, 2) as ViewResult;
            var message = viewResult.ViewData["errorMessage"] as string;

            // Assert
            Assert.AreEqual("Invalid paramter", message);
        }
        [Test]
        public void GetPeopleBasedOnAge_GreaterThan2000_ReturnViewWithListPeople()
        {
            mockService.Setup(m => m.GetPeopleBaseOnAge(It.IsAny<Func<Person, bool>>(), 1, 2))
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
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.GetPeopleBasedOnAge(1, 1, 2) as ViewResult;
            var people = viewResult.ViewData["people"] as IEnumerable<Person>;

            // Assert
            Assert.AreEqual(2, people.Count());
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }
        [Test]
        public void GetPeopleBasedOnAge_Equal2000_ReturnViewWithListPeople()
        {
            mockService.Setup(m => m.GetPeopleBaseOnAge(It.IsAny<Func<Person, bool>>(), 1, 2))
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
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.GetPeopleBasedOnAge(0, 1, 2) as ViewResult;

            // Assert
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }
        [Test]
        public void GetPeopleBasedOnAge_LessThan2000_ReturnViewWithListPeople()
        {
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);

            // Act
            var viewResult = rookiesController.GetPeopleBasedOnAge(-1, 1, 2) as ViewResult;

            // Assert
            Assert.IsInstanceOf<ViewResult>(viewResult);
        }
        [Test]
        public void ExportExcel_ReturnsFileResult()
        {
            // Arrange
            var wb = new XLWorkbook();
            wb.Worksheets.Add("Sheet1");
            wb.Worksheets.Add("Sheet2");
            mockService.Setup(b => b.GetExcelFile()).Returns(wb);
            RookiesController rookiesController = new RookiesController(mockService.Object, mockLogger.Object);


            // Act
            var result = rookiesController.ExportExcel() as FileResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.ContentType);
        }
    }
}