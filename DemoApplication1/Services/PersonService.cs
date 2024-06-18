using DemoApplication1.Models;

namespace DemoApplication1.Services
{
    public class PersonService
    {
        public string GetPersonFullName(Person person)
        {
            return person.FirstName + " " + person.LastName;
        }

        public double? GetPersonBMI(Person person)
        {
            return person.Weight / (person.Height / 100) * 2; // not accurate
        }
    }
}
