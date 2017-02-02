using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using GevbenTeam.ResxSupport.Core;
using Module = Autofac.Module;

namespace GevbenTeam.ResxSupport.Autofac
{
	public class ResxSupportModule : Module
	{
		public IList<Type> ResourcesTypes { get; }
		public bool IsRegistered { get; set; }

		public ResxSupportModule()
		{
			ResourcesTypes = new List<Type>();
		}

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<DefaultResourceNameResolver>().As<IResourceNameResolver>();
			builder.RegisterType<DefaultResourceValueResolver>().As<IResourceValueResolver>().SingleInstance();
		}

		protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
		{
			registration.Activated += AttachResources;
			registration.Activated += RegistrationOnActivated;
		}

		private void AttachResources(object sender, ActivatedEventArgs<object> activatedEventArgs)
		{
			if (!(activatedEventArgs.Instance is IResourceValueResolver)) return;
			var instance = (IResourceValueResolver) activatedEventArgs.Instance;
			foreach (var resourcesType in ResourcesTypes)
			{
				instance.AttachResource(resourcesType);
			}
		}

		private void RegistrationOnActivated(object sender, ActivatedEventArgs<object> activatedEventArgs)
		{
			var resolver = activatedEventArgs.Context.Resolve<IResourceValueResolver>();
			resolver.ResolveValues(activatedEventArgs.Instance);
		}

	}
}