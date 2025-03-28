using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;

namespace Services
{
    public class ChildrenServices
    {
        private readonly ChildRepository _childRepository;


        public ChildrenServices()
        {
            _childRepository = new ChildRepository();
        }

        public List<Child> GetAll()
        {
            return _childRepository.GetAll();
        }

        public List<Child> GetAllForAdmin()
        {
            return _childRepository.GetAllForAdmin();
        }

        public List<Child> GetByCustomerId(int id)
        {
            return _childRepository.GetByCustomerId(id);
        }

        public List<Child> GetByCustomerIdForAdmin(int id)
        {
            return _childRepository.GetByCustomerIdForAdmin(id);
        }

        public List<Child> GetChildByName(string name)
        {
            return _childRepository.GetChildByName(name);
        }

        public List<Child> GetChildByNameForAdmin(string name)
        {
            return _childRepository.GetChildByNameForAdmin(name);
        }

        public Child GetChildById(int id)
        {
            return _childRepository.GetChildById(id);
        }

        public Child GetChildByIdForAdmin(int id)
        {
            return _childRepository.GetChildByIdForAdmin(id);
        }

        public List<Child> SearchChild(string search)
        {
            return _childRepository.GetAll().Where(x => x.Name.Contains(search)).ToList();
        }

        public void AddChild(Child child)
        {
            _childRepository.AddChild(child);
        }

        public void UpdateChild(Child child)
        {
            _childRepository.UpdateChild(child);
        }

        //not done
        public void DeleteChild(int id)
        {
            var child = _childRepository.GetChildById(id);
            var check = _childRepository.HasTracking(id);

            if (!check)
            {
                _childRepository.DeleteChild(child);
            }
            else
            {
                throw new ArgumentException("Child is in vaccine tracking, can not delete");
            }
        }


    }
}
