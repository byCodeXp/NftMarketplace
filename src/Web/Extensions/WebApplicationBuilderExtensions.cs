﻿using Web.Installers.Base;

namespace Web.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void InstallServicesInAssembly(this WebApplicationBuilder builder)
    {
        var installers = typeof(Program).Assembly.ExportedTypes
            .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
        
        installers.ForEach(installer => installer.Install(builder.Services, builder.Configuration));
    }
}