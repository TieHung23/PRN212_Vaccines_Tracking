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
            bookingRepository = new BookingRepository() ?? throw new ArgumentNullException(nameof(bookingRepository));
            vaccinesTrackingServices = new VaccinesTrackingServices() ?? throw new ArgumentNullException(nameof(vaccinesTrackingServices));


            bookingRepository.AddBooking( booking, vaccinationDate );
        }

        public List<Booking> GetBooking()
        {
            return bookingRepository.GetBookings().Where(x => x.Status == 1).ToList();
        }

        public List<Booking> GetBookingForAdmin()
        {
            bookingRepository = new BookingRepository();
            return bookingRepository.GetBookings();
        }

        public List<Booking> GetBookingByParent(int parentId)
        {
            bookingRepository = new BookingRepository();
            return bookingRepository.GetBookingsByParent(parentId).Where(x => x.Status == 1).ToList() ;
        }

        public List<Booking> GetBookingByParentForAdmin(int parentId)
        {
            return bookingRepository.GetBookingsByParent(parentId);
        }
    }
}
