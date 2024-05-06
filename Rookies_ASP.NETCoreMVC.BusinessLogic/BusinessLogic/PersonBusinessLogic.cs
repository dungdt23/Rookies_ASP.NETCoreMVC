using ClosedXML.Excel;
using Rookies_ASP.NETCoreMVC.BusinessLogic.Repositories;
using Rookies_ASP.NETCoreMVC.Models.DTOs;
using Rookies_ASP.NETCoreMVC.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Rookies_ASP.NETCoreMVC.BusinessLogic.BusinessLogic
{
    public class PersonBusinessLogic : IPersonBusinessLogic
    {
        private readonly IPersonRepository _personRepository;
        public PersonBusinessLogic(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public XLWorkbook GetExcelFile()
        {
            XLWorkbook wb = new XLWorkbook();

            var exportData = ConvertListToDataTable();
            var worksheet = wb.AddWorksheet(exportData, "List People");

            //style for header of excel file
            string colorHex = "#c6e0b4";
            XLColor fillColor = XLColor.FromHtml(colorHex);
            var headerRow = worksheet.Row(1);
            headerRow.Style.Fill.BackgroundColor = fillColor;
            headerRow.Style.Font.FontColor = XLColor.SmokyBlack;
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            headerRow.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            headerRow.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            headerRow.Style.Border.OutsideBorderColor = XLColor.Black;

            //edit width of cell to display clearly
            var columnA = worksheet.Column("A");
            columnA.Width = 12;
            var columnB = worksheet.Column("B");
            columnB.Width = 12;
            var columnC = worksheet.Column("C");
            columnC.Width = 10;
            var columnD = worksheet.Column("D");
            columnD.Width = 20;
            var columnE = worksheet.Column("E");
            columnE.Width = 20;
            var columnF = worksheet.Column("F");
            columnF.Width = 15;
            var columnH = worksheet.Column("H");
            columnH.Width = 15;

            return wb;

        }
        private DataTable ConvertListToDataTable()
        {
            var people = _personRepository.GetPeopleData();
            DataTable dataTable = new DataTable();
            dataTable.TableName = "List People";
            dataTable.Columns.Add("First Name", typeof(string));
            dataTable.Columns.Add("Last Name", typeof(string));
            dataTable.Columns.Add("Gender", typeof(string));
            dataTable.Columns.Add("Date Of Birth", typeof(string));
            dataTable.Columns.Add("Phone Number", typeof(string));
            dataTable.Columns.Add("Birth Place", typeof(string));
            dataTable.Columns.Add("Age", typeof(string));
            dataTable.Columns.Add("Is Graduated", typeof(string));
            if (people.Count() > 0)
            {
                foreach (var person in people)
                {
                    DataRow row = dataTable.NewRow();
                    row["First Name"] = person.FirstName;
                    row["Last Name"] = person.LastName;
                    row["Gender"] = person.Gender;
                    row["Date Of Birth"] = person.DateOfBirth.ToString("yyyy-MM-dd");
                    row["Phone Number"] = person.PhoneNumber;
                    row["Birth Place"] = person.Birthplace;
                    row["Age"] = person.Age;
                    row["Is Graduated"] = person.IsGraduated;
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }
        public IEnumerable<string> GetFullNames()
        {
            return _personRepository.GetFullNames();
        }

        public IEnumerable<Person> GetPeople(FilterPersonDto? filterPersonDto)
        {
            return _personRepository.GetPeople(filterPersonDto);
        }

        public Person? GetTheOldest()
        {
            return _personRepository.GetTheOldest();
        }
    }
}
