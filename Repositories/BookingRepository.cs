using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class BookingRepository
    {
        private readonly VaccineTrackingContext context;
        public BookingRepository(  )
        {
            context = new VaccineTrackingContext();
        }
        public void AddBooking( Booking booking )
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
        }

    }
}
