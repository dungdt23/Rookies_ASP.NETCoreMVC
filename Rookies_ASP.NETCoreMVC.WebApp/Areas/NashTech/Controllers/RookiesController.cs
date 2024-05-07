using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Rookies_ASP.NETCoreMVC.BusinessLogic.BusinessLogic;
using Rookies_ASP.NETCoreMVC.Models.DTOs;
using Rookies_ASP.NETCoreMVC.Models.Models;
using Rookies_ASP.NETCoreMVC.Shared.Constants;
using Rookies_ASP.NETCoreMVC.Shared.DTOs;
using Person = Rookies_ASP.NETCoreMVC.Models.Models.Person;

namespace Rookies_ASP.NETCoreMVC.WebApp.Areas.NashTech.Controllers
{
    [Area("NashTech")]
    public class RookiesController : Controller
    {
        private readonly IPersonBusinessLogic _personBusinessLogic;
        private readonly ILogger<RookiesController> _logger;

        public RookiesController(IPersonBusinessLogic personBusinessLogic, ILogger<RookiesController> logger)
        {
            _personBusinessLogic = personBusinessLogic;
            _logger = logger;
        }
        //using LazZiya Tag Helpers to paginate, P and S is default query string, P is PageIndex and S is Size
        public IActionResult Index(string? birthYear, int p = 1, int s = 4)
        {
            IEnumerable<Person> people = new List<Person>();
            //variable to count total number of people in list
            int totalCount = 0;
            if (!string.IsNullOrEmpty(birthYear))
            {
                try
                {
                    int parsedBirthYear = int.Parse(birthYear);
                    people = _personBusinessLogic.GetPeople(new FilterPersonDto { YearOfBirth = parsedBirthYear }, p, s);
                    totalCount = _personBusinessLogic.GetPeople(new FilterPersonDto { YearOfBirth = parsedBirthYear }, null, null).Count();
                }
                catch (Exception ex)
                {
                    ViewData["errorMessage"] = "Invalid paramter";
                    _logger.LogInformation(ex.Message);
                }
            }
            else
            {
                people = _personBusinessLogic.GetPeople(null, p, s);
                //get number of elements in people list
                totalCount = _personBusinessLogic.GetAllPeople().Count();
            }
            //paging model: pass value like total count, page size, page index to view
            PagingModel pagingModel = new PagingModel { TotalCount = totalCount, PageIndex = p, Size = s };
            ViewData["people"] = people;
            return View(pagingModel);
        }
        [HttpGet]
        public IActionResult Detail(Guid id)
        {
            var person = _personBusinessLogic.GetPersonById(id);
            if (person == null) return RedirectToAction("Index");
            else return View(person);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Person person)
        {
            if (ModelState.IsValid)
            {
                int addResult = _personBusinessLogic.Add(person);
                if (addResult == ConstantsStatus.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["errorMessage"] = "Phone Number is not valid!";
                    return View(person);
                }
            }
            return View(person);
        }
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _personBusinessLogic.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var person = _personBusinessLogic.GetPersonById(id);
            if (person == null) return RedirectToAction("Index");
            else return View(person);
        }
        [HttpPost]
        public IActionResult Update(Person person)
        {
            if (ModelState.IsValid)
            {
                int updateResult = _personBusinessLogic.Update(person.Id, person);
                if (updateResult == ConstantsStatus.Success)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewData["errorMessage"] = "Phone Number is not valid!";
                    return View(person);
                }
            }
            return View(person);
        }
        [HttpGet]
        public IActionResult GetMaleStudents()
        {
            var people = _personBusinessLogic.GetPeople(new FilterPersonDto { Gender = TypeGender.Male }, null, null);
            ViewData["people"] = people;
            return View("Index");
        }
        [HttpGet]
        public IActionResult GetTheOldest()
        {
            var oldestPerson = _personBusinessLogic.GetTheOldest();
            ViewData["peoples"] = null;
            ViewData["fullNames"] = null;
            ViewData["oldestPerson"] = oldestPerson;
            return View("Index");
        }
        [HttpGet]
        public IActionResult GetFullName()
        {
            var fullNames = _personBusinessLogic.GetFullNames();
            ViewData["people"] = null;
            ViewData["fullNames"] = fullNames;
            return View("Index");

        }
        [HttpGet]
        public IActionResult GetPeopleBasedOnAge(int selectCondition, int p = 1, int s = 4)
        {
            IEnumerable<Person> people = new List<Person>();
            //set default select condition
            Func<Person, bool> condition = s => s.DateOfBirth.Year > 0;
            switch (selectCondition)
            {
                case -1:
                    condition = s => s.DateOfBirth.Year < 2000;
                    break;
                case 0:
                    condition = s => s.DateOfBirth.Year == 2000;
                    break;
                case 1:
                    condition = s => s.DateOfBirth.Year > 2000;
                    break;
            }
            people = _personBusinessLogic.GetPeopleBaseOnAge(condition, p, s);
            int totalCount = _personBusinessLogic.GetPeopleBaseOnAge(condition, null, null).Count();
            //paging model: pass value like total count, page size, page index to view
            PagingModel pagingModel = new PagingModel { TotalCount = totalCount, PageIndex = p, Size = s };
            ViewData["people"] = people;
            return View("Index", pagingModel);

        }
        [HttpPost]
        public IActionResult ExportExcel()
        {
            XLWorkbook wb = _personBusinessLogic.GetExcelFile();
            MemoryStream ms = new MemoryStream();
            wb.SaveAs(ms);
            // Reset the stream position 
            ms.Position = 0;
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ListPeople.xlsx");
        }
    }
}
