using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Rookies_ASP.NETCoreMVC.BusinessLogic.BusinessLogic;
using Rookies_ASP.NETCoreMVC.Models.DTOs;
using Rookies_ASP.NETCoreMVC.Models.Models;

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
        public IActionResult Index(string? birthYear)
        {
            IEnumerable<Person> people = new List<Person>();
            if (!string.IsNullOrEmpty(birthYear))
            {
                try
                {
                    int parsedBirthYear = int.Parse(birthYear);
                    people = _personBusinessLogic.GetPeople(new FilterPersonDto { YearOfBirth = parsedBirthYear });
                }
                catch (Exception ex)
                {
                    ViewData["errorMessage"] = "Invalid paramter";
                    _logger.LogInformation(ex.Message);
                }
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
            // Reset the stream position 
            ms.Position = 0;
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ListPeople.xlsx");
        }
    }
}
