﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraduationProject.BL.Dtos;


namespace GraduationProject.BL
{
    public interface IPatientManager
    {
        // public List<GetAllDoctorsDto> GetAllDoctors();
        public GetPatientByPhoneDTO getPatientByPhoneDTO(string phoneNumber);

        public GetMedicalHistoryByPhoneDto? GetMedicalHistoryByPhoneNumber(string phoneNumber);
        public GetPatientVisitDto? GetPatientVisitsByPhoneNumber(string phoneNumber);
    }

}
