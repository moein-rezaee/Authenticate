﻿using System.Reflection;
using Mapster;
using Authenticate.DTOs;
namespace Authenticate.Mappings;

public static class MapsterConfig
{
	public static void RegisterMapsterConfiguration(this IServiceCollection services)
	{
		TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
	}
}
