using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.UserControls.Enums
{
    public enum SizeIcon
    {
        [Display(Name = "Extra large icons")]
        ExtraLarge,
        [Display(Name = "Large icons")]
        Large,
        [Display(Name = "Medium icons")]
        Medium
    }
}
