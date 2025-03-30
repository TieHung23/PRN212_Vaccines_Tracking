using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Enum;

namespace Models.DTOs
{
    public class VaccinesTrackingResponse
    {
        public int VaccinesTrackingId
        {
            get; set;
        }
        public string VaccineName { get; set; } = string.Empty;

        public string ParentName { get; set; } = string.Empty;

        public string ParentEmail { get; set; } = string.Empty;

        public string ChildName { get; set; } = string.Empty;

        public DateTime DateVaccination { get; set; } = DateTime.Now;

        public int DoesTime { get; set; } = 1;

        public string Status
        {
            get; set;
        } = string.Empty;
    }
}
