using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.DTOs;
using Models.Enum;
using Repositories;

namespace Services
{
    public class VaccinesTrackingServices
    {
        VaccinesTrackingRepository vaccinesTrackingRepository;
        public VaccinesTrackingServices()
        {
            vaccinesTrackingRepository = new();
        }
        public List<VaccinesTrackingResponse> GetAll()
        {
            return ConvertHelpers.convertToVaccinesTrackingResponse( vaccinesTrackingRepository.GetAll() );
        }

        public List<VaccinesTrackingResponse> GetByChildId( int childId )
        {
            return ConvertHelpers.convertToVaccinesTrackingResponse( vaccinesTrackingRepository.GetByChildId(childId) );
        }

        public List<VaccinesTrackingResponse> GetByUserId( int userId )
        {
            return ConvertHelpers.convertToVaccinesTrackingResponse( vaccinesTrackingRepository.GetByUserId( userId ) );
        }

        public bool MarkAsDone( int vaccinesTrackingId )
        {
            VaccinesTracking vaccinesTracking = vaccinesTrackingRepository.GetById( vaccinesTrackingId );
            if( vaccinesTracking == null )
            {
                return false;
            }
            vaccinesTracking.Status =(int) VaccinesTrackingStatus.Done;
            vaccinesTrackingRepository.UpdateVaccineTracking( vaccinesTracking );
            
            VaccinesTracking next = vaccinesTrackingRepository.GetNextVaccineTracking(vaccinesTrackingId);

            if( next != null )
            {
                next.Status = ( int ) VaccinesTrackingStatus.Scheduled;
                vaccinesTrackingRepository.UpdateVaccineTracking( next );
            }

            return true;
        }

        public bool Cancel( int vaccinesTrackingId )
        {
            VaccinesTracking vaccinesTracking = vaccinesTrackingRepository.GetById( vaccinesTrackingId );
            if( vaccinesTracking == null )
            {
                return false;
            }
            vaccinesTracking.Status = ( int ) VaccinesTrackingStatus.Canceled;
            vaccinesTrackingRepository.UpdateVaccineTracking( vaccinesTracking );
            
            while( true )
            {
                VaccinesTracking next = vaccinesTrackingRepository.GetNextVaccineTracking( vaccinesTrackingId );
                if( next == null )
                {
                    break;
                }
                next.Status = ( int ) VaccinesTrackingStatus.Canceled;
                vaccinesTrackingRepository.UpdateVaccineTracking( next );
                vaccinesTrackingId = next.Id;
            }

            return true;
        }
    }
}
