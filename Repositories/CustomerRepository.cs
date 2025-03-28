using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class CustomerRepository
    {
        private readonly VaccineTrackingContext _context;

        public CustomerRepository()
        {
            _context = new VaccineTrackingContext();
        }

        public List<Customer> GetAll()
        {
            return _context.Customers.Include( x => x.Children ).Where( x => x.Status == 1).ToList();
        }

        public List<Customer> GetAllByEmailAddress( string email )
        {
            return _context.Customers.Include( x => x.Children ).Where( x => x.Email.Contains( email ) && x.Status == 1).ToList();
        }

        public Customer Login(string username, string password)
        {
            return _context.Customers.FirstOrDefault(c => c.Username == username && c.Password == password);
        }

        public List<Customer> SearchCustomer(string search)
        {
            return _context.Customers.Where(x => x.Username.Contains(search) || x.Email.Contains(search)).ToList();
        }

        public void Add( Customer customer )
        {
            _context.Customers.Add( customer );
            _context.SaveChanges();
        }

        public void Update( Customer customer )
        {
            _context.Customers.Update( customer );
            _context.SaveChanges();
        }

        public void Delete( Customer customer )
        {
            _context.Customers.Remove( customer );
            _context.SaveChanges();
        }
    }
}
