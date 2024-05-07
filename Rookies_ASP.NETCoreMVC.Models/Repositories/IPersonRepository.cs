using Rookies_ASP.NETCoreMVC.Models.Models;
using Rookies_ASP.NETCoreMVC.Models.DTOs;

namespace Rookies_ASP.NETCoreMVC.BusinessLogic.Repositories
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPeople(FilterPersonDto? filterPersonDto, int? startIndex, int? size);
        Person? GetTheOldest();
        IEnumerable<string> GetFullNames();
        void Add(Person person);
        int Delete(Guid id);
        int Update(Guid id, Person person);
        Person? GetPersonById(Guid id);
        IEnumerable<Person> GetPeopleBaseOnAge(Func<Person, bool> condition, int? startIndex, int? size);
        IEnumerable<Person> GetAllPeople();
    }
}
