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
    public class VaccineServices
    {
        VaccinesRepository vaccinesRepository = new();

        public List<Vaccine> GetAll()
        {
           
            return vaccinesRepository.GetVaccinesToUser();
        }

        public List<Vaccine> GetByName( string name )
        {
            
            return vaccinesRepository.GetVaccinesByName( name );
        }
        public List<Vaccine> SearchVaccine(string search)
        {
            return vaccinesRepository.SearchVaccine(search);
        }
        public void AddVaccine(Vaccine vaccine)
        {
             vaccinesRepository.AddVaccine( vaccine );
        }

        public void UpdateVaccine(Vaccine vaccine)
        {
             vaccinesRepository.UpdateVaccine(vaccine);
        }

        public void DeleteVaccine(Vaccine vaccine)
        {
            vaccinesRepository.DeleteVaccine(vaccine);
        }
    }
}
