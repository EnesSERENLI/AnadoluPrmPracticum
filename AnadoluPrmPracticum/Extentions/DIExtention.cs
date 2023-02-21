using AnadoluPrmPracticum.Data.Model;
using AnadoluPrmPracticum.Data.Repository.Abstract;
using AnadoluPrmPracticum.Data.UnitOfWork.Abstract;
using AnadoluPrmPracticum.Service.AutoMapper;
using AnadoluPrmPracticum.Service.Repository.Concrete;
using AnadoluPrmPracticum.Service.UnitOfWork.Concrete;
using AnadoluPrmPracticum.Utils.Abstract;
using AnadoluPrmPracticum.Utils.Concrete;
using AutoMapper;
using System.Reflection;

namespace AnadoluPrmPracticum.Extentions
{
    public static class DIExtention
    {
        public static void AddServicesDI(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGenericRepository<User>,GenericRepository<User>>();
            services.AddScoped<IGenericRepository<Game>,GenericRepository<Game>>();

            //Logger
            services.AddSingleton<ILoggerService, FileLogger>();

            //AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Mapperconfig
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Mapping());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}
