﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos
{
    public class GetPatientForPatientV
    {
        public string? Name { get; set; }
        public List<GetPatientVisitsByPhoneDTO>? PatientVisits { get; set; } = new();
    }
    public class GetPatientVisitsByPhoneDTO
    {
       
        
        public int Id { get; set; }
        public DateTime DateOfVisit { get; set; }
        public string? Comments { get; set; }
        public string? Symptoms { get; set; }
        public String? VisitStatus { get; set; }
        public DateTime ArrivalTime { get; set; }
        //waiting time = time of arrival - visit start time
        public DateTime VisitStartTime { get; set; }
        public DateTime VisitEndTime { get; set; }
        //In Progress Time = vist start time - visit end time
        public string? Prescription { get; set; }
    }
}