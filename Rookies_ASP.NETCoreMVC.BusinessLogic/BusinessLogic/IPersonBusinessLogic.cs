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
        IEnumerable<Person> GetPeople(FilterPersonDto? filterPersonDto);
        Person? GetTheOldest();

        IEnumerable<string> GetFullNames();

        XLWorkbook GetExcelFile();
    }
}
