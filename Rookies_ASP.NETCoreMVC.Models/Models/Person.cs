using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookies_ASP.NETCoreMVC.Models.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TypeGender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Birthplace { get; set; }
        public int Age { get; set; }
        public bool IsGraduated { get; set; }
    }
}
