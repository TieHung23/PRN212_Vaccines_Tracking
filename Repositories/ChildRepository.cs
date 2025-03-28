using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class ChildRepository
    {
        private readonly Prn212VaccinesTracking2Context _context;
        public ChildRepository()
        {
            _context = new Prn212VaccinesTracking2Context();
        }
        public List<Child> GetAll()
        {
            return _context.Children.Include(x => x.Parent).Where( x => x.Status == 1).ToList();
        }

        public List<Child> GetAllForAdmin()
        {
            return _context.Children.Include(x => x.Parent).ToList();
        }

        public List<Child> GetByCustomerId(int id)
        {
            return _context.Children.Include(x => x.Parent).Where(x => x.ParentId == id && x.Status == 1).ToList();
        }

        public List<Child> GetByCustomerIdForAdmin(int id)
        {
            return _context.Children.Include(x => x.Parent).Where(x => x.ParentId == id).ToList();
        }

        public List<Child> GetChildByName( string name )
        {
            return _context.Children.Where( x => x.Name.Contains( name ) && x.Status == 1).ToList();
        }

        public List<Child> GetChildByNameForAdmin(string name)
        {
            return _context.Children.Where(x => x.Name.Contains(name)).ToList();
        }

        public Child GetChildById( int id )
        {
            return _context.Children.Include( x => x.Parent).Where( x => x.Status == 1).FirstOrDefault( x => x.Id == id )!;
        }

        public Child GetChildByIdForAdmin(int id)
        {
            return _context.Children.Include(x => x.Parent).FirstOrDefault(x => x.Id == id)!;
        }

        public void AddChild(Child child)
        {
            _context.Children.Add(child);
            _context.SaveChanges();
        }

        public void UpdateChild(Child child)
        {
            _context.Children.Update(child);
            _context.SaveChanges();
        }

        public void DeleteChild(Child child)
        {
            _context.Children.Remove(child);
            _context.SaveChanges();
        }

        public bool HasTracking(int id)
        {
            return _context.VaccinesTrackings.Any(x => x.ChildId == id && x.Status == 0);
        }
    }
}
