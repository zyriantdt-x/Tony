using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Util;
internal static class ApplicationServices {
    public static void AddApplicationServices( this IServiceCollection services ) {
        services.AddSingleton<ITonyEnvironment, TonyEnvironment>();
    }
}
