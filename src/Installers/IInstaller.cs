﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Installers;

public interface IInstaller
{
    void Install(IServiceCollection services, IConfiguration configuration);
}