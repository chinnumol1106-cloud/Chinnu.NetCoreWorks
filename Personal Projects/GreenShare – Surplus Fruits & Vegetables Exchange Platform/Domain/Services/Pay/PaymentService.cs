using Domain.Enum;
using Domain.Exceptions;
using Domain.Helper;
using Domain.Interfaces.Buyer;
using Domain.Interfaces.Email;
using Domain.Interfaces.Pay;
using Domain.Models;
using Domain.Repositories.Pay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Pay
{
    public class PaymentService:IpaymentService
    {
        private readonly IpaymentRepository _repo;
       
        private readonly IEmailService _email;

        public PaymentService(
            IpaymentRepository repo,
           
            IEmailService email)
        {
            _repo = repo;
           
            _email = email;
        }


        public async Task<Payment> MakePaymentAsync(Guid buyerId, Guid interestId)
        {
            // 1️⃣ Load interest WITH Buyer, Item, Seller
            var interest = await _repo.GetAcceptedInterestWithDetailsAsync(interestId)
            ?? throw new BusinessException("Interest not found");

        if (interest.BuyerId != buyerId)
            throw new BusinessException("Unauthorized payment");

        if (interest.Status != InterestStatus.Accepted)
            throw new BusinessException("Payment allowed only for accepted interest");

        var item = interest.Item;

        // 2️⃣ Correct amount calculation
        decimal amount = item.Quantity * item.PriceUnit;

        // 3️⃣ Simulate bank processing
        await Task.Delay(1000);

        // 4️⃣ Save payment
        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            BuyerId = buyerId,
            SellerId = item.SellerId,
            ItemId = item.Id,
            Amount = amount,
            Status = PaymentStatus.Success,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddPaymentAsync(payment);
        await _repo.SaveAsync();

        // 5️⃣ EMAILS

        // Buyer – bank debit
        await _email.SendEmailAsync(new MailRequest
        {
            ToEmail = interest.Buyer.Email,
            Subject = "Bank Alert: Debit Successful",
            Body = $"₹{amount} debited from your account.",
            FromName="SwedBank"
           
        });

        // Buyer – GreenShare confirmation
        await _email.SendEmailAsync(new MailRequest
        {
            ToEmail = interest.Buyer.Email,
            Subject = "GreenShare Payment Successful",
            Body = "Payment successful. You can collect your item.",
            FromName="GreenShare"
        });

// Seller – bank credit
await _email.SendEmailAsync(new MailRequest
{
    ToEmail = item.Seller.Email,
    Subject = "Bank Alert: Credit Successful",
    Body = $"₹{amount} credited to your account.",
    FromName = "SwedBank"
});

return payment;
    }
    }
}
