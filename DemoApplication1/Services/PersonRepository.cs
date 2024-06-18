using DemoApplication1.DbContexts;
using DemoApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication1.Services
{
    public class PersonRepository
    {
        PersonContext _context;

        public PersonRepository(PersonContext context)
        {
            _context = context;
        }

        public async Task AddPersonAsync(Person person)
        {
            _context.Persons.Add(person);
        }

        public async Task<IEnumerable<Person>> GetPersonsAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public void DeletePersonByIdAsync(Person person)
        {
            _context.Persons.Remove(person);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
