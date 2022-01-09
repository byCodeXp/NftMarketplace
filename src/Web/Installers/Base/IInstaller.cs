namespace Web.Installers.Base;

public interface IInstaller
{
    void Install(IServiceCollection services, IConfiguration configuration);
}