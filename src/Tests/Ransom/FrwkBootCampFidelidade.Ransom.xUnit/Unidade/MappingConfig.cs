using AutoMapper;
using FrwkBootCampFidelidade.Infraestrutura.IOC.IOC;

namespace FrwkBootCampFidelidade.RansomxUnit.Unidade
{
    public class MappingConfig
    {
        public IMapper MockerMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            var mapper = mockMapper.CreateMapper();

            return mapper;
        }
    }
}
