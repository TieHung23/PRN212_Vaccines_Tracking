using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class VaccineDetailRepo
    {
        private readonly Prn212VaccinesTracking2Context _context;

        public VaccineDetailRepo()
        {
            _context = new Prn212VaccinesTracking2Context();
        }

        public List<VaccineDetail> GetAll()
        {
            return _context.VaccineDetails.Include(x => x.Vaccine).ToList();
        }

        public VaccineDetail? GetById(int id)
        {
            return _context.VaccineDetails.FirstOrDefault(x => x.VaccineDetailsId == id && x.Status == 1);
        }

        public VaccineDetail? GetByIdForAdmin(int id)
        {
            return _context.VaccineDetails.FirstOrDefault(x => x.VaccineDetailsId == id);
        }

        public List<VaccineDetail> SearchVaccineDetail(string search)
        {
            return _context.VaccineDetails.Where(x => x.Vaccine.Name.Contains(search)).ToList();
        }

        public void AddVaccineDetail(VaccineDetail vaccineDetail)
        {
            _context.VaccineDetails.Add(vaccineDetail);
            _context.SaveChanges();
        }

        public void UpdateVaccineDetail(VaccineDetail vaccineDetail)
        {
            _context.VaccineDetails.Update(vaccineDetail);
            _context.SaveChanges();
        }

        public void DeleteVaccineDetail(VaccineDetail vaccineDetail)
        {
            _context.VaccineDetails.Remove(vaccineDetail);
            _context.SaveChanges();
        }


    }
}
