using Microsoft.AspNetCore.Mvc;
using Rookies_ASP.NETCoreMVC.BusinessLogic.BusinessLogic;
using Rookies_ASP.NETCoreMVC.Models.Models;
using Rookies_ASP.NETCoreMVC.Models.DTOs;
using ClosedXML.Excel;

namespace Rookies_ASP.NETCoreMVC.WebApp.Controllers
{
    public class RookiesController : Controller
    {
        private readonly IPersonBusinessLogic _personBusinessLogic;
        public RookiesController(IPersonBusinessLogic personBusinessLogic)
        {
            _personBusinessLogic = personBusinessLogic;
        }
        public IActionResult Index(int? birthYear)
        {
            IEnumerable<Person> people = new List<Person>();
            if (birthYear.HasValue)
            {
                people = _personBusinessLogic.GetPeople(new FilterPersonDto { YearOfBirth = birthYear.Value });
            }
            else
            {
                people = _personBusinessLogic.GetPeople(null);
            }
            ViewData["people"] = people;
            return View();
        }
        [HttpGet]
        public IActionResult GetMaleStudents()
        {
            var people = _personBusinessLogic.GetPeople(new FilterPersonDto { Gender = TypeGender.Male });
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
        [HttpPost]
        public IActionResult ExportExcel()
        {
            XLWorkbook wb = _personBusinessLogic.GetExcelFile();
            MemoryStream ms = new MemoryStream();
            wb.SaveAs(ms);
            // Reset the stream position before returning
            ms.Position = 0;
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ListPeople.xlsx");
        }
    }
}
