using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Enum
{
    public enum LogStatus
    {
        [DisplayAttribute(Name = "Add", Order = 0)]
        Add = 'A',

        [DisplayAttribute(Name = "Edit", Order = 1)]
        Edit = 'E',

        [DisplayAttribute(Name = "Delete", Order = 2)]
        Delete = 'D',
    }
}
