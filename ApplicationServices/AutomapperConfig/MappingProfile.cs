using ApplicationServices.Wallet.Commands;
using ApplicationServices.Wallet.Queries;
using AutoMapper;
using Domain;
using System;
using ViewModel;

namespace ApplicationServices.AutomapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Wallet, CreateWalletResponse>();
            CreateMap<CreateWalletRequest, Domain.Wallet>();
            CreateMap<WithdrawRequest, Domain.Transaction>();

            CreateMap<DepositRequest, Domain.Transaction>();
            CreateMap<Domain.Wallet, DepositResponse>();
            CreateMap<Domain.Wallet, WithdrawResponse>();
            CreateMap<Transaction, TransactionResponse>()
                  .ForMember(destination => destination.TransactionType,
                    opt => opt.MapFrom(source => Enum.GetName(typeof(TransactionType), source.TransactionType)));
        }
    }
}