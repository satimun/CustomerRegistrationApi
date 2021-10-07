using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerRegistrationModel.Enum
{
    public enum Status
    {
        [DisplayAttribute(Name = "Active", Order = 0)]
        Active = 'A',

        [DisplayAttribute(Name = "Inactive", Order = 1)]
        Inactive = 'I',

        [DisplayAttribute(Name = "Cancel", Order = 2)]
        Cancel = 'C',
    }
}
