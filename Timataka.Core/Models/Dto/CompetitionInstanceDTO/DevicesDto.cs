﻿using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.ViewModels.DeviceViewModels;

namespace Timataka.Core.Models.Dto.CompetitionInstanceDTO
{
    public class DevicesDto
    {
        public IEnumerable<DeviceInEventViewModel> DeviceInEventViewModels { get; set; }
        public string CompetitionName { get; set; }
        public string CompetitionInstanceName { get; set; }
    }
}
