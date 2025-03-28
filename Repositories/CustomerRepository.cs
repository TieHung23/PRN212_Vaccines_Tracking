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
        private readonly Prn212VaccinesTracking2Context _context;

        public CustomerRepository()
        {
            _context = new Prn212VaccinesTracking2Context();
        }

        public List<Customer> GetAll()
        {
            return _context.Customers.Include(x => x.Children).ToList();
        }

        public Customer GetById(int id)
        {
            return _context.Customers.FirstOrDefault(x => x.Id == id && x.Status == 1);
        }

        public Customer GetByIdForAdmin(int id)
        {
            return _context.Customers.FirstOrDefault(x => x.Id == id);
        }

        public List<Customer> SearchCustomer(string search)
        {
            return _context.Customers.Where(x => x.Username.Contains(search) || x.Email.Contains(search)).ToList();
        }

        public Customer Login(string email, string password)
        {
            return _context.Customers.FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public bool HasChildTracking(int id)
        {
            // Lấy danh sách tất cả con của Customer
            var allChildren = _context.Children
                                      .Where(c => c.ParentId == id)
                                      .Select(c => c.Id)
                                      .ToList();

            // Nếu không có con nào, trả về false
            if (!allChildren.Any()) return false;

            // Lấy danh sách con có trong tracking
            var childrenInTracking = _context.VaccinesTrackings
                                             .Where(vt => vt.ParentId == id && vt.Status == 0)
                                             .Select(vt => vt.ChildId)
                                             .Distinct()
                                             .ToList();

            // Kiểm tra xem tất cả con của Customer đều có trong danh sách tracking hay không
            return allChildren.Any(childId => childrenInTracking.Contains(childId));
        }
    }
}
