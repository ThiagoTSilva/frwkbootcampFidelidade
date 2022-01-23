using AutoMapper;
using FrwkBootCampFidelidade.Dominio.BonificationContext.Entities;
using FrwkBootCampFidelidade.Dominio.ExtractContext.Entities;
using FrwkBootCampFidelidade.Dominio.OrderContext.Entities;
using FrwkBootCampFidelidade.Dominio.ProductContext.Entities;
using FrwkBootCampFidelidade.Dominio.PromotionContext.Entities;
using FrwkBootCampFidelidade.Dominio.RansomContext.Entities;
using FrwkBootCampFidelidade.Dominio.WalletContext.Entities;
using FrwkBootCampFidelidade.DTO.BonificationContext;
using FrwkBootCampFidelidade.DTO.ExtractContext;
using FrwkBootCampFidelidade.DTO.OrderContext;
using FrwkBootCampFidelidade.DTO.ProductContext;
using FrwkBootCampFidelidade.DTO.PromotionContext;
using FrwkBootCampFidelidade.DTO.RansomContext;
using FrwkBootCampFidelidade.DTO.WalletContext;
using System.Collections.Generic;
using System.Linq;

namespace FrwkBootCampFidelidade.Infraestrutura.IOC.IOC
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Bonification, BonificationDTO>();
            CreateMap<BonificationDTO, Bonification>();
            CreateMap<List<BonificationDTO>, List<Bonification>>();
            CreateMap<List<Bonification>, List<BonificationDTO>>();

            CreateMap<Wallet, WalletDTO>();
            CreateMap<WalletDTO, Wallet>();
            CreateMap<List<WalletDTO>, List<Wallet>>();
            CreateMap<List<Wallet>, List<WalletDTO>>();

            CreateMap<WalletHistoryTransfer, WalletTransferDTO>();
            CreateMap<WalletTransferDTO, WalletHistoryTransfer>();
            CreateMap<List<WalletTransferDTO>, List<WalletHistoryTransfer>>();
            CreateMap<List<WalletHistoryTransfer>, List<WalletTransferDTO>>();

            #region RANSOM
            CreateMap<Ransom, RansomDTO>();
            CreateMap<RansomDTO, Ransom>();
            CreateMap<List<RansomDTO>, List<Ransom>>();
            CreateMap<List<Ransom>, List<RansomDTO>>();
            CreateMap<IEnumerable<RansomDTO>, IQueryable<RansomDTO>>();

            CreateMap<RansomHistoryStatus, RansomHistoryStatusDTO>();
            CreateMap<RansomHistoryStatusDTO, RansomHistoryStatus>();
            CreateMap<List<RansomHistoryStatusDTO>, List<RansomHistoryStatus>>();
            CreateMap<List<RansomHistoryStatus>, List<RansomHistoryStatusDTO>>();
            #endregion

            CreateMap<Promotion, PromotionDTO>().ReverseMap();
            CreateMap<List<PromotionDTO>, List<Promotion>>().ReverseMap();

            CreateMap<PromotionItem, PromotionItemDTO>().ReverseMap();
            CreateMap<List<PromotionItemDTO>, List<PromotionItem>>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<List<ProductDTO>, List<Product>>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<List<OrderItem>, OrderItemDTO>().ReverseMap();

        }
    }
}
