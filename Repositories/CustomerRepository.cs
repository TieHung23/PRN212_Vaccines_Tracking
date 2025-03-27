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
        private readonly VaccineTrackingContext context;

        public CustomerRepository()
        {
            context = new VaccineTrackingContext();
        }

        public List<Customer> GetAll()
        {
            return context.Customers.Include( x => x.Children ).ToList();
        }

        public List<Customer> GetAllByEmailAddress( string email )
        {
            return context.Customers.Include( x => x.Children ).Where( x => x.Email.Contains( email ) ).ToList();
        }
    }
}
