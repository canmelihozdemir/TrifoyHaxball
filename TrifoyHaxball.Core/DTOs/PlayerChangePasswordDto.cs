﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrifoyHaxball.Core.DTOs
{
    public class PlayerChangePasswordDto:PlayerSaveDto
    {
        public string NewPassword { get; set; }
    }
}
