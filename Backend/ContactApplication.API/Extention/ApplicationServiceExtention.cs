using ContactApplication.API.Helper;
using ContactApplication.Application.Interfaces.Repository;
using ContactApplication.Application.Interfaces.Services;
using ContactApplication.Application.Services;
using ContactApplication.Infrastructure.Repository;

namespace ContactApplication.API.Extention
{
    public static class ApplicationServiceExtention
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactService>();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            return services;
        }
    }
}
