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
        private readonly ChildRepository childRepository;

        public ChildrenServices()
        {
            childRepository = new ChildRepository();
        }

        public List<Child> GetAll()
        {
            return childRepository.GetAll().Where(c => c.Status == 1).ToList();
        }

        public List<Child> GetAllForAdmin()
        {
            return childRepository.GetAll();
        }

        public List<Child> GetByCustomerId( int id )
        {
            return childRepository.GetByCustomerId( id ).Where(x => x.Status == 1).ToList();
        }

        public List<Child> GetByCustomerIdForAdmin(int id)
        {
            return childRepository.GetByCustomerId(id);
        }

        public List<Child> GetByName( string name )
        {
            return childRepository.GetChildByName( name ).Where(x => x.Status == 1).ToList();
        }

        public List<Child> GetByNameForAdmin(string name)
        {
            return childRepository.GetChildByName(name);
        }

        public Child GetById( int id )
        {
            return childRepository.GetChildById( id );
        }

        public Child GetByIdForAdmin(int id)
        {
            return childRepository.GetChildByIdForAdmin(id);
        }

        public void AddChild( Child child )
        {
            childRepository.AddChild( child );
        }

        public void UpdateChild(Child child)
        {
            childRepository.UpdateChild( child );
        }

        public void DeleteChild( Child child )
        {
            childRepository.DeleteChild( child );
        }
    }
}
