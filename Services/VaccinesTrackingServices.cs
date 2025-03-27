using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class VaccinesTrackingServices
    {
        VaccinesTrackingRepository vaccinesTrackingRepository;
        public void AddVaccinesTracking( Booking booking, List<Vaccine> vaccines, DateTime vaccination )
        {
            vaccinesTrackingRepository = new VaccinesTrackingRepository();
            VaccinesTracking flag = null;
            foreach ( var children in booking.Children )
            {
                foreach( var vaccine in vaccines )
                {
                    for( int doses = 1; doses <= vaccine.DosesTimes; doses++ )
                    {
                        VaccinesTracking vaccinesTracking = new VaccinesTracking
                        {
                            Parent = booking.Parent,
                            Booking = booking,
                            Child = children,
                            Vaccine = vaccine,
                            DateVaccination = vaccination,
                            Status = 1,
                            PreviousId = flag == null ? 0 : flag.Id,

                        };
                        vaccinesTrackingRepository.AddVaccineTracking( vaccinesTracking );
                        flag = vaccinesTracking;
                    }
                }
            }
        }
    }
}
