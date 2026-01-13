using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Pay
{
    public interface IpaymentService
    {
        Task<Payment> MakePaymentAsync(Guid buyerId, Guid interestId);
    }
}
