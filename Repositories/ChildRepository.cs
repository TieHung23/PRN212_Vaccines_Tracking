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
            return context.Children.Where( x => x.Status == 1).ToList();
        }

        public List<Child> GetByCustomerId( int id )
        {
            return context.Children.Include( x => x.Parent ).Where( x => x.ParentId == id && x.Status == 1).ToList();
        }

        public List<Child> GetChildByName( string name )
        {
            return context.Children.Where( x => x.Name.Contains( name ) && x.Status == 1).ToList();
        }

        public Child GetChildById( int id )
        {
            return context.Children.Include( x => x.Parent).Where( x => x.Status == 1).FirstOrDefault( x => x.Id == id )!;
        }
    }
}
