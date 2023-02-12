using AnadoluPrmPracticum.Data.UnitOfWork.Abstract;
using AnadoluPrmPracticum.Service.UnitOfWork.Concrete;

namespace AnadoluPrmPracticum.Extentions
{
    public static class DIExtention
    {
        public static void AddServicesDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
