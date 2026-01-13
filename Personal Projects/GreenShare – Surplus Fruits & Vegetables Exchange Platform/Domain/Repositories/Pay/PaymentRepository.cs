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
    public class PaymentRepository:IpaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }




        public async Task<Interest?> GetAcceptedInterestWithDetailsAsync(Guid interestId)
        {
            return await _context.Interests
                .Include(i => i.Buyer)
                .Include(i => i.Item)
                    .ThenInclude(it => it.Seller)
                .FirstOrDefaultAsync(i => i.Id == interestId);
        }
        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
