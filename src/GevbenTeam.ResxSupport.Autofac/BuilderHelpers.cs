using System;
using Autofac;

namespace GevbenTeam.ResxSupport.Autofac
{
	public static class BuilderHelpers
	{
		private static readonly ResxSupportModule resxSupportModule = new ResxSupportModule();

		public static void UseResx(this ContainerBuilder builder, Type resourceType)
		{
			if (!resxSupportModule.IsRegistered)
				builder.RegisterModule(resxSupportModule);
			resxSupportModule.ResourcesTypes.Add(resourceType);
		}

		public static void UseResx<TResx>(this ContainerBuilder builder)
		{
			builder.UseResx(typeof(TResx));
		}
	}
}