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
        private readonly CustomerRepository _customerRepository;
        private readonly ChildRepository _childRepository;

        public CustomerServices()
        {
            _customerRepository = new CustomerRepository();
            _childRepository = new ChildRepository();
        }

        public List<Customer> GetAllByMail(string search)
        {
            return _customerRepository.GetAll().Where(x => x.Email.Contains(search)).ToList();
        }

        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll().Where(x => x.Status == 1).ToList();
        }

        public List<Customer> GetAllForAdmin()
        {
            return _customerRepository.GetAll();
        }

        public Customer GetById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public Customer GetByIdForAdmin(int id)
        {
            return _customerRepository.GetByIdForAdmin(id);
        }

        public Customer Login(string email, string password)
        {
            return _customerRepository.Login(email, password);
        }

        public List<Customer> SearchCustomer(string search)
        {
            return _customerRepository.SearchCustomer(search);
        }

        public void AddCustomer(Customer customer)
        {
            _customerRepository.AddCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);
        }

        public void DeleteCustomer(int id)
        {
            var customer = _customerRepository.GetByIdForAdmin(id);
            var check = _customerRepository.HasChildTracking(id);
            if (!check)
            {
                
                var children = _childRepository.GetByCustomerIdForAdmin(customer.Id);
                if(children != null)
                {
                    foreach(var child in children)
                    {
                        _childRepository.DeleteChild(child);
                    }
                }
                _customerRepository.DeleteCustomer(customer);
            }
            else
            {
                throw new ArgumentException("Customer has children are in vaccine trackings, can not delete this customer");
            }
        }
    
    }
}
