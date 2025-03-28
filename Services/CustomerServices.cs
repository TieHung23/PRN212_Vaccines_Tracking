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
        private readonly CustomerRepository customerRepository;
        private readonly ChildRepository childRepository;

        public CustomerServices()
        {
            customerRepository = new CustomerRepository();
            childRepository = new ChildRepository();
        }

        public List<Customer> GetAll()
        {
            return customerRepository.GetAll();
        }

        public List<Customer> GetAllByMail( string email )
        {
            return customerRepository.GetAllByEmailAddress( email );
        }

        public Customer Login( string username, string password )
        {
            return customerRepository.Login( username, password );
        }

        public List<Customer> SearchCustomer(string search)
        {
            return customerRepository.SearchCustomer( search );
        }

        public void Add( Customer customer )
        {
            customerRepository.Add( customer );
        }

        public void Update( Customer customer )
        {
            customerRepository.Update( customer );
        }

        public void Delete( int id )
        {
            var customer = customerRepository.GetAll().FirstOrDefault(x => x.Id == id);
            
            if (customer == null)
            {
                throw new Exception("Do not exist.");
            }


            if (childRepository.HasTrackingChildren(id))
            {
                throw new InvalidOperationException("This customer has child in the tracking, cannot delete");
            }

            customerRepository.Delete(customer);
        }
    }
}
