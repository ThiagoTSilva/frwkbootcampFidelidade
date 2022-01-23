using FrwkBootCampFidelidade.Dominio.RansomContext.Entities;
using FrwkBootCampFidelidade.Dominio.RansomContext.Interfaces;
using FrwkBootCampFidelidade.Infraestrutura.Base.Repository;
using FrwkBootCampFidelidade.Infraestrutura.Context;

namespace FrwkBootCampFidelidade.Infraestrutura.RansomContext.Repository
{
    public class RansomStatusRepository : BaseRepository<RansomStatus>, IRansomStatusRepository
    {
        private readonly DBContext _context;

        public RansomStatusRepository(DBContext context): base(context)
        {
            _context = context;
        }
    }
}
