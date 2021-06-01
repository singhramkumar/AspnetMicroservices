using AutoMapper;
using Discount.Grpc.Entites;
using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Mapper
{
    public class DisoucntProfile : Profile
    {
        public DisoucntProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
