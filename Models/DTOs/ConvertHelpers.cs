using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Enum;

namespace Models.DTOs
{
    public class ConvertHelpers
    {
        public static List<VaccinesTrackingResponse> convertToVaccinesTrackingResponse( List<VaccinesTracking> vaccinesTracking )
        {
            List<VaccinesTrackingResponse> vaccinesTrackingResponse = new List<VaccinesTrackingResponse>();
            int flag = 0;
            int doesTime = 1;
            foreach( VaccinesTracking vaccineTracking in vaccinesTracking )
            {
                if( vaccineTracking.PreviousId == flag )
                {
                    doesTime++;
                }
                else
                {
                    doesTime = 1;
                }

                VaccinesTrackingResponse response = new VaccinesTrackingResponse();
                response.VaccinesTrackingId = vaccineTracking.Id;
                response.VaccineName = vaccineTracking.Vaccine.Name;
                response.ParentName = vaccineTracking.Parent.Username;
                response.ParentEmail = vaccineTracking.Parent.Email;
                response.ChildName = vaccineTracking.Child.Name;
                response.DateVaccination = vaccineTracking.DateVaccination;
                response.DoesTime = doesTime;
                response.Status = (( VaccinesTrackingStatus ) vaccineTracking.Status).ToString() ;
                vaccinesTrackingResponse.Add( response );

                flag = vaccineTracking.Id;
            }

            return vaccinesTrackingResponse;
        }
    }
}
