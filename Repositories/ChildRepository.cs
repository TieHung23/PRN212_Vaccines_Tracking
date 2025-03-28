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
        private readonly VaccineTrackingContext context;
        public ChildRepository()
        {
            context = new VaccineTrackingContext();
        }
        public List<Child> GetAll()
        {
            return context.Children
                .Include(x => x.Parent)
                .Include(x => x.VaccinesTrackings)
                .ToList();
        }

        public List<Child> GetByCustomerId( int id )
        {
            return context.Children
                .Include(x => x.Parent)
                .Include(x => x.VaccinesTrackings)
                .Where( x => x.ParentId == id).ToList();
        }

        public List<Child> GetChildByName( string name )
        {
            return context.Children
                .Include(x => x.Parent)
                .Include(x => x.VaccinesTrackings)
                .Where(x => x.Name.Contains(name)).ToList();
        }

        public Child GetChildByIdForAdmin( int id )
        {
            return context.Children
                .Include(x => x.Parent)
                .Include(x => x.VaccinesTrackings)
                //.Where( x => x.Status == 1)
                .FirstOrDefault( x => x.Id == id )!;
        }

        public Child GetChildById(int id)
        {
            return context.Children
                .Include(x => x.Parent)
                .Include(x => x.VaccinesTrackings)
                .Where( x => x.Status == 1)
                .FirstOrDefault(x => x.Id == id)!;
        }

        public void AddChild(Child child)
        {
            context.Children.Add( child );
            context.SaveChanges();
        }

        public void UpdateChild(Child child)
        {
            context.Children.Update( child );
            context.SaveChanges();
        }

        public void DeleteChild(Child child)
        {
            context.Children.Remove(child);
            context.SaveChanges();
        }

        public bool HasTrackingChildren(int customerId)
        {
            return context.VaccinesTrackings
                .Any(vt => context.Children
                           .Where(c => c.ParentId == customerId)
                           .Any(c => c.Id == vt.ChildId));
        }
    }
}
