﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraduationProject.BL.Dtos.Doctor;

namespace GraduationProject.BL
{
    public interface IPatientManager
    {
        public List<GetAllDoctorsDto> GetAllDoctors();

    }

}