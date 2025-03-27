using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class VaccineServices
    {
        VaccinesRepository vaccinesRepository;

        public List<Vaccine> GetAll()
        {
            vaccinesRepository = new VaccinesRepository();
            return vaccinesRepository.GetAll();
        }

        public List<Vaccine> GetByName( string name )
        {
            vaccinesRepository = new VaccinesRepository();
            return vaccinesRepository.GetVaccinesByName( name );
        }
    }
}
