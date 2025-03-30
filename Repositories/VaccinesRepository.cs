using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class VaccinesRepository
    {
        private readonly Prn212VaccinesTracking2Context _context;

        public VaccinesRepository()
        {
            _context = new Prn212VaccinesTracking2Context();
        }

        public List<Vaccine> GetVaccinesToUser()
        {
            return _context.Vaccines.ToList();
        }


        public List<Vaccine> GetVaccinesByName( string name )
        {
            return _context.Vaccines.Where( x => x.Name.Contains( name ) ).ToList();
        }

        public List<Vaccine> SearchVaccine(string search)
        {
            return _context.Vaccines.Where(x => x.Name.Contains(search)).ToList();
        }

        public void AddVaccine(Vaccine vaccine)
        {
            _context.Vaccines.Add(vaccine);
            _context.SaveChanges();
        }

        public void UpdateVaccine(Vaccine vaccine)
        {
            _context.Vaccines.Update(vaccine);
            _context.SaveChanges();
        }

        public void DeleteVaccine(Vaccine vaccine)
        {
            _context.Vaccines.Remove(vaccine);
            _context.SaveChanges();
        }

    }
}
