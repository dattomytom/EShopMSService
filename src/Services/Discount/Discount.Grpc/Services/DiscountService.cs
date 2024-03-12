using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountDbcontext discountDbcontext, ILogger<DiscountService> logger)
        :DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountDbcontext.Coupones.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if(coupon == null) 
            {
                coupon = new Coupon {ProductName = "No Product Name", Description = "No Product Name", Amount = 0 };
            }
            logger.LogInformation("Discount is retrieved for ProductName: {}", coupon.ProductName);
            var couponModal = coupon.Adapt<CouponModel>();
            return couponModal; 
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Argument"));
            }
            discountDbcontext.Coupones.Add(coupon);
            await discountDbcontext.SaveChangesAsync();
            logger.LogInformation("Discount is Successfully created. ProductName: {}", coupon.ProductName);
            var couponModal = coupon.Adapt<CouponModel>();
            return couponModal;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Argument"));
            }
            discountDbcontext.Coupones.Update(coupon);
            await discountDbcontext.SaveChangesAsync();
            logger.LogInformation("Discount is Successfully Update. ProductName: {}", coupon.ProductName);
            var couponModal = coupon.Adapt<CouponModel>();
            return couponModal;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = discountDbcontext.Coupones.FirstOrDefault(x => x.ProductName == request.ProductName);
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discont with ProductName :{request.ProductName} not found"));
            discountDbcontext.Coupones.Remove(coupon);
            await discountDbcontext.SaveChangesAsync();
            logger.LogInformation($"Discount is Successfully Deleted. ProductName: {coupon.ProductName}");
            return new DeleteDiscountResponse { Success = true };
        }
    }
}
