using AutoMapper;
using FrwkBootCampFidelidade.Aplicacao.Constants;
using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.Aplicacao.Interfaces.RpcService;
using FrwkBootCampFidelidade.Dominio.Base;
using FrwkBootCampFidelidade.Dominio.ProductContext.Entities;
using FrwkBootCampFidelidade.DTO.ProductContext;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Services
{
    public class ProductService : IProductService
    {
        //private readonly IRpcClientService _rpcClientService;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper/*, IRpcClientService rpcClientService*/)
        {
            //_rpcClientService = rpcClientService;
            _mapper = mapper;
        }

        public async Task<ProductDTO> GetById(long productId)
        {
            //var message = new MessageInputModel()
            //{
            //    Queue = DomainConstant.PRODUCT,
            //    Method = MethodConstant.GETBYID,
            //    Content = productId.ToString(),
            //};

            //var response = _rpcClientService.Call(message);
            //_rpcClientService.Close();

            var products = new Product
            {
                Id = 1,
                Name = "Nome",
                Description = "Descrição",
                Price = 10
            };

            var productDTO = _mapper.Map<ProductDTO>(products);
            return productDTO;
        }
    }
}
