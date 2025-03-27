using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repositories
{
    public class VaccinesTrackingRepository
    {
        private readonly VaccineTrackingContext context;
        public void AddVaccineTracking( VaccinesTracking vaccineTracking )
        {
            context.VaccinesTrackings.Add( vaccineTracking );
            context.SaveChanges();
        }
    }
}
