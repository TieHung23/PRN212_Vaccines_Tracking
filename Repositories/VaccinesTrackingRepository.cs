using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            VaccinesTracking flag = null!;
            bool isFirst = true;
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
                            Status = isFirst ? 0 : 1,
                            PreviousId = flag == null ? null : flag.Id,
                        };
                        isFirst = false;
                        AddVaccineTracking( vaccinesTracking );
                        flag = getLastestVaccineTracking();
                    }
                    flag = null!;
                    isFirst = true;
                }
            }
        }

        public List<VaccinesTracking> GetAll()
        {
            return context.VaccinesTrackings
                .Include( x => x.Vaccine )
                .Include( x => x.Child )
                .Include( x => x.Parent )
                .ToList();
        }

        public List<VaccinesTracking> GetByChildId( int childId )
        {
            return context.VaccinesTrackings.Where( x => x.ChildId == childId )
                .Include( x => x.Vaccine )
                .Include( x => x.Child )
                .Include( x => x.Parent )
                .ToList();
        }

        public List<VaccinesTracking> GetByUserId( int userId )
        {
            return context.VaccinesTrackings.Where( x => x.ParentId == userId )
                .Include( x => x.Vaccine )
                .Include( x => x.Child )
                .Include( x => x.Parent )
                .ToList();
        }
    
        public VaccinesTracking GetById( int id )
        {
            return context.VaccinesTrackings
                .Include( x => x.Vaccine )
                .Include( x => x.Child )
                .Include( x => x.Parent )
                .FirstOrDefault( x => x.Id == id )!;
        }

        public void UpdateVaccineTracking( VaccinesTracking vaccineTracking )
        {
            context.VaccinesTrackings.Update( vaccineTracking );
            context.SaveChanges();
        }
    
        public VaccinesTracking GetNextVaccineTracking( int previoudId )
        {
            return  context.VaccinesTrackings
                .Include( x => x.Vaccine )
                .Include( x => x.Child )
                .Include( x => x.Parent )
                .FirstOrDefault( x => x.PreviousId == previoudId )!;
        }
    }
}
