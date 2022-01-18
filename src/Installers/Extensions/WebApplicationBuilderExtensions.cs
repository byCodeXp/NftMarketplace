using System.Reflection;
using Installers.Attributes;
using Microsoft.AspNetCore.Builder;

namespace Installers.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void InstallServicesInAssembly(this WebApplicationBuilder builder, Type type)
    {
        var installers = type.Assembly.ExportedTypes
            .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .OrderBy(x => x.GetCustomAttribute<InstallerOrderAttribute>()?.Order ?? 0)
            .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
        
        installers.ForEach(installer => installer.Install(builder.Services, builder.Configuration));
    }
}