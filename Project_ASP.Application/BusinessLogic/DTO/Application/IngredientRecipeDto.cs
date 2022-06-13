using Project_ASP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.BusinessLogic.DTO.Application
{
    public class IngredientRecipeDto
    {
        public int IngredientId { get; set; }
        public eMeasure? MeasureId { get; set; }
        public string Quantity { get; set; }
        public string IngredientName { get; set; }
        public string MeasureName{ get; set; }
    }
}
