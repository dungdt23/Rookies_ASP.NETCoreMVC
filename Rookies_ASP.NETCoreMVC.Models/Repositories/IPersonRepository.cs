using Rookies_ASP.NETCoreMVC.Models.Models;
using Rookies_ASP.NETCoreMVC.Models.DTOs;

namespace Rookies_ASP.NETCoreMVC.BusinessLogic.Repositories
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPeople(FilterPersonDto? filterPersonDto);
        Person? GetTheOldest();
        IEnumerable<string> GetFullNames();
        IEnumerable<Person> GetPeopleData();


    }
}
