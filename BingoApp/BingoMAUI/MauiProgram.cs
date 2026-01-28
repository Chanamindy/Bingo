using Microsoft.Extensions.Logging;

namespace BingoMAUI
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
                    fonts.AddFont("ANTQUAB.TTF", "AntiquaB");
                    fonts.AddFont("ANTQUABI.TTF", "AntiquaBI");
                    fonts.AddFont("ANTQUAI.TTF", "AntiquaI");
                    fonts.AddFont("BKANT.TTF", "BookAntiqua");
                    fonts.AddFont("ITCKRIST.TTF", "KristenITC");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
