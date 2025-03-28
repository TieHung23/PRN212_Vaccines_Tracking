using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class BookingRepository
    {
        private readonly Prn212VaccinesTracking2Context context;
        private readonly VaccinesTrackingRepository vaccinesTrackingRepository;
        public BookingRepository(  )
        {
            context = new Prn212VaccinesTracking2Context();
            vaccinesTrackingRepository = new VaccinesTrackingRepository();
        }
        public void AddBooking( Booking booking , DateTime vaccinationDate)
        {
            List<Child> children = new List<Child>();
            List<Vaccine> vaccines = new List<Vaccine>();
            foreach( var child in booking.Children )
            {
                context.Children.Attach( child );
                children.Add( child );
            }

            foreach( var vaccine in booking.Vaccines )
            {
                context.Vaccines.Attach( vaccine );
                vaccines.Add( vaccine );
            }

            var parent = context.Customers.FirstOrDefault( x => x.Id == booking.ParentId );

            booking.Children = children;
            booking.ParentId = parent.Id;
            context.Bookings.Add( booking );

            context.SaveChanges();

            var result = context.Bookings.OrderByDescending( x => x.CreatedAt ).FirstOrDefault();

            vaccinesTrackingRepository.GenToVaccinesTracking( result, vaccinationDate );
        }

    }
}
