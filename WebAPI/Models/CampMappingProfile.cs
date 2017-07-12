﻿using AutoMapper;
using MyCodeCamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class CampMappingProfile:Profile
    {
        public CampMappingProfile()
        {
            CreateMap<Camp, CampModel>();
        }
    }
}
