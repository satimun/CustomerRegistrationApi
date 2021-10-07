using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerRegistrationModel.Enum
{
    public enum Lang
    {
        [DisplayAttribute(Description = "Thai")]
        th,

        [DisplayAttribute(Description = "English")]
        en,
    }
}
