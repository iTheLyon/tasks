using Microsoft.Extensions.Logging;
using AppTareaFinal.DataAccess;
using AppTareaFinal.ViewsModels;
using AppTareaFinal.Views;

namespace AppTareaFinal
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var dbContext = new TareaDbContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();
            builder.Services.AddDbContext<TareaDbContext>();
            builder.Services.AddTransient<TareaPage>();
            builder.Services.AddTransient<TareaViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();
            Routing.RegisterRoute(nameof(TareaPage), typeof(TareaPage));
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
