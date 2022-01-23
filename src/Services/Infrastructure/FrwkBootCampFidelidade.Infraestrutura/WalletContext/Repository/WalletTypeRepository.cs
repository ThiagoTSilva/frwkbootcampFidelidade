using FrwkBootCampFidelidade.Dominio.WalletContext.Entities;
using FrwkBootCampFidelidade.Dominio.WalletContext.Interfaces;
using FrwkBootCampFidelidade.Infraestrutura.Base.Repository;
using FrwkBootCampFidelidade.Infraestrutura.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Infraestrutura.Data.WalletContext.Repository
{
    public class WalletTypeRepository : BaseRepository<WalletType>, IWalletTypeRepository
    {
        private readonly DBContext _context;

        public WalletTypeRepository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}
