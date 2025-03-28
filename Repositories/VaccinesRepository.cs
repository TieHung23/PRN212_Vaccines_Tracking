using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<Vaccine> GetAll()
        {
            return _context.Vaccines.ToList();
        }

        public List<Vaccine> GetVaccinesByName( string name )
        {
            return _context.Vaccines.Where( x => x.Name.Contains( name ) ).ToList();
        }
    }
}
