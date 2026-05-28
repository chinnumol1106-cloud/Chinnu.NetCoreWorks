using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public enum CompanyRequestStatus
    {
        Pending = 1,
        Approved = 2,
        Paid = 3,
        PaymentConfirmed = 4,
        Completed = 5,
        Rejected = 6
    }
}
