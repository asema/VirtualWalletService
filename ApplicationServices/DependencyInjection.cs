using ApplicationServices.AutomapperConfig;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationServices
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddMediatR(typeof(DependencyInjection).Assembly);
            var config = new MapperConfiguration(configure => { configure.AddProfile(new MappingProfile()); });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
