using FrwkBootCampFidelidade.DTO.ProductContext;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> GetById(long productId);
    }
}
