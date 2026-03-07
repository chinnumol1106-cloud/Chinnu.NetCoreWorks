using Domain.Data;
using Domain.Interfaces.Pay;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Pay
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }

        public async Task<decimal> GetTotalPaymentByRequestAsync(Guid requestId)
        {
            return await _context.Payments
                .Where(p => p.CollectionRequestId == requestId)
                .SumAsync(p => (decimal?)p.Amount) ?? 0;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
