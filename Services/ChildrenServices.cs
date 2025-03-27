using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class ChildrenServices
    {
        ChildRepository childRepository;

        public List<Child> GetAll()
        {
            childRepository = new ChildRepository();
            return childRepository.GetAll();
        }

        public List<Child> GetByCustomerId( int id )
        {
            childRepository = new ChildRepository();
            return childRepository.GetByCustomerId( id );
        }

        public List<Child> GetByName( string name )
        {
            childRepository = new ChildRepository();
            return childRepository.GetChildByName( name );
        }

        public Child GetById( int id )
        {
            childRepository = new ChildRepository();
            return childRepository.GetChildById( id );
        }
    }
}
