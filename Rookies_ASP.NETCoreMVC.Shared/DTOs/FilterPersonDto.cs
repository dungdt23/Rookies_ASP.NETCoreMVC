using Rookies_ASP.NETCoreMVC.Models.Models;

namespace Rookies_ASP.NETCoreMVC.Models.DTOs
{
    public class FilterPersonDto
    {
        public TypeGender? Gender { get; set; }
        public int? YearOfBirth { get; set; }
    }
}
