using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Enum
{
    public enum UseStatus
    {
        [DisplayAttribute(Name = "Active", Order = 0)]
        Active = 'Y',

        [DisplayAttribute(Name = "Inactive", Order = 1)]
        Inactive = 'N',
    }
}
