﻿using DataComp.Training.IServices;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataComp.Training.Api.Requriments
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }

        public int MinimumAge { get; set; }
    }
}
