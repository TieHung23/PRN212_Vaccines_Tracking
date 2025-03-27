using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class BookingServices
    {
        BookingRepository bookingRepository;
        VaccinesTrackingServices vaccinesTrackingServices;
        ChildrenServices childrenServices;
        public void AddBooking( Booking booking )
        {
            bookingRepository = new BookingRepository();
            bookingRepository.AddBooking( booking );
        }
    }
}
