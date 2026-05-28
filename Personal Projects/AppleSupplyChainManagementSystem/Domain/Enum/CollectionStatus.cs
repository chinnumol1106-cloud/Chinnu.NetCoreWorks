using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public enum CollectionStatus
    {
        Requested = 1,
        Assigned = 2,
        Collected = 3,
        Completed=4,
        Cancelled=5
    }
}
