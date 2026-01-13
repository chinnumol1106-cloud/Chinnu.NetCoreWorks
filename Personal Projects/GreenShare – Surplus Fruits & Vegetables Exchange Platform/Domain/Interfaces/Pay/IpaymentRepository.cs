using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Pay
{
    public interface IpaymentRepository
    {
        Task<Interest?> GetAcceptedInterestWithDetailsAsync(Guid interestId);
        Task AddPaymentAsync(Payment payment);
        Task SaveAsync();
    }
}
