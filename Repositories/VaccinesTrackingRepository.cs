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
        private readonly Prn212VaccinesTracking2Context context;

        public VaccinesTrackingRepository()
        {
            context = new();
        }

        public void AddVaccineTracking( VaccinesTracking vaccineTracking )
        {
            context.VaccinesTrackings.Add( vaccineTracking );
            context.SaveChanges();
        }

        public VaccinesTracking getLastestVaccineTracking()
        {
            return context.VaccinesTrackings.OrderByDescending( x => x.Id ).FirstOrDefault();
        }

        public void GenToVaccinesTracking( Booking booking, DateTime vaccinationDate )
        {
            VaccinesTracking flag = null;
            foreach( var children in booking.Children )
            {
                foreach( var vaccine in booking.Vaccines )
                {
                    for( int doses = 1; doses <= vaccine.DosesTimes; doses++ )
                    {
                        context.Customers.Attach( booking.Parent );
                        context.Children.Attach( children );
                        context.Vaccines.Attach( vaccine );

                        VaccinesTracking vaccinesTracking = new VaccinesTracking
                        {
                            Parent = booking.Parent,
                            Booking = booking,
                            Child = children,
                            Vaccine = vaccine,
                            DateVaccination = vaccinationDate,
                            Status = 1,
                            PreviousId = flag == null ? null : flag.Id,
                        };
                        AddVaccineTracking( vaccinesTracking );
                        flag = getLastestVaccineTracking();
                    }
                }
            }
        }
    }
}
