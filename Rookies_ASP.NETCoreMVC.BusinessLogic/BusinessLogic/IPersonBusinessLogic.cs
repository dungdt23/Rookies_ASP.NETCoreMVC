using ClosedXML.Excel;
using Rookies_ASP.NETCoreMVC.Models.DTOs;
using Rookies_ASP.NETCoreMVC.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookies_ASP.NETCoreMVC.BusinessLogic.BusinessLogic
{
    public interface IPersonBusinessLogic
    {
        IEnumerable<Person> GetPeople(FilterPersonDto? filterPersonDto, int? startIndex, int? size);
        Person? GetTheOldest();

        IEnumerable<Person> GetAllPeople();

        IEnumerable<string> GetFullNames();
        int Add(Person person);
        int Delete(Guid id);
        int Update(Guid id, Person person);

        XLWorkbook GetExcelFile();
        Person? GetPersonById(Guid id);
        IEnumerable<Person> GetPeopleBaseOnAge(Func<Person, bool> condition, int? startIndex, int? size);
    }
}
