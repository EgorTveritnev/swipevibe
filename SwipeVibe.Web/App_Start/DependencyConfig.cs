using System.Web.Mvc;
using AutoMapper;
using SwipeVibe.BusinessLogic.BL;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.Web
{
    public static class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            // Настройка AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SwipeVibe.Domain.Entities.User.User, UserReturn>()
                   .ForMember(d => d.Role, o => o.MapFrom(s => s.Role.ToString()));
                cfg.CreateMap<UserRegister, SwipeVibe.Domain.Entities.User.User>();
            });
            var mapper = mapperConfig.CreateMapper();

            // Создание фабрики для зависимостей
            DependencyResolver.SetResolver(new SimpleDependencyResolver(mapper));
        }
    }

    public class SimpleDependencyResolver : IDependencyResolver
    {
        private readonly IMapper _mapper;

        public SimpleDependencyResolver(IMapper mapper)        {
            _mapper = mapper;
        }

        public object GetService(System.Type serviceType)
        {
            if (serviceType == typeof(Controllers.AccountController))
            {
                // Теперь AccountController создаёт зависимости сам через фабричный метод
                return new Controllers.AccountController();
            }

            return null;
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(System.Type serviceType)
        {
            return new object[0];
        }
    }
}
