using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class CustomerServices
    {
        CustomerRepository customerRepository;

        public List<Customer> GetAll()
        {
            customerRepository = new CustomerRepository();
            return customerRepository.GetAll();
        }

        public List<Customer> GetAllByMail( string email )
        {
            customerRepository = new CustomerRepository();
            return customerRepository.GetAllByEmailAddress( email );
        }
    }
}
