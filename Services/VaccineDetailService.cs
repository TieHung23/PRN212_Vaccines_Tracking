using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class VaccineDetailService
    {
        private readonly VaccineDetailRepo _vaccineDetailRepo;

        public VaccineDetailService()
        {
            _vaccineDetailRepo = new VaccineDetailRepo();
        }
        public List<VaccineDetail> GetAll()
        {
            return _vaccineDetailRepo.GetAll().Where(x => x.Status == 1).ToList();
        }

        public List<VaccineDetail> GetAllForAdmin()
        {
            return _vaccineDetailRepo.GetAll();
        }

        public VaccineDetail GetById(int id)
        {
            return _vaccineDetailRepo.GetById(id);
        }

        public VaccineDetail GetByIdForAdmin(int id)
        {
            return _vaccineDetailRepo.GetByIdForAdmin(id);
        }


        public List<VaccineDetail> SearchVaccineDetail(string search)
        {
            return _vaccineDetailRepo.SearchVaccineDetail(search);
        }

        public void AddCVaccineDetail(VaccineDetail vaccineDetail)
        {
            _vaccineDetailRepo.AddVaccineDetail(vaccineDetail);
        }

        public void UpdateVaccineDetail(VaccineDetail vaccineDetail)
        {
            _vaccineDetailRepo.UpdateVaccineDetail(vaccineDetail);
        }

        public void DeleteVaccineDetail(VaccineDetail vaccineDetail)
        {
            _vaccineDetailRepo.DeleteVaccineDetail(vaccineDetail);
        }
    }
}

