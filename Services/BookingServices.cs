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
        public void AddBooking( Booking booking, DateTime vaccinationDate )
        {
            bookingRepository = new BookingRepository();
            vaccinesTrackingServices = new VaccinesTrackingServices();


            bookingRepository.AddBooking( booking, vaccinationDate );
        }
    }
}
