﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.BusinessLogic.DTO.Application
{
    public class CreateIngredientDto : BaseDto
    {
        public string Name { get; set; }
        public int? IngredientTypeId { get; set; }
    }
}
