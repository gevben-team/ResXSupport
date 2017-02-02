using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using GevbenTeam.ResxSupport.Core;

namespace GevbenTeam.ResxSupport
{
	public class DefaultResourceValueResolver : IResourceValueResolver
	{
		private readonly IResourceNameResolver resourceNameResolver;
		protected readonly IDictionary<Type, ResourceManager> resourceManagers;

		public DefaultResourceValueResolver(IResourceNameResolver resourceNameResolver)
		{
			this.resourceNameResolver = resourceNameResolver;
			resourceManagers = new Dictionary<Type, ResourceManager>();
		}

		public void AttachResource(Type resourceType)
		{
			if (!resourceManagers.ContainsKey(resourceType))
			{
				resourceManagers.Add(resourceType, new ResourceManager(resourceType));
			}
		}

		public void DetachResource(Type resourceType)
		{
			if (resourceManagers.ContainsKey(resourceType))
			{
				resourceManagers.Remove(resourceType);
			}
		}

		public void ResolveValues(object obj)
		{
			var type = obj.GetType();

			var properties = type
				.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Where(p => p.PropertyType == typeof(string) && p.CanWrite && p.GetIndexParameters().Length == 0 && p.GetCustomAttributes(typeof(ResxAttribute), false).Any());

			WriteResx(properties, obj);
		}

		private void WriteResx(IEnumerable<PropertyInfo> properties, object instance)
		{
			foreach (var propertyInfo in properties)
			{
				var str = TryGetString(propertyInfo);
				if(str != null)
					propertyInfo.SetValue(instance, str, new object[] { });
			}
		}

		private object TryGetString(MemberInfo memberInfo)
		{
			return resourceManagers.Values.Select(resourceManager => resourceManager.GetString(resourceNameResolver.Resolve(memberInfo))).FirstOrDefault(str => str != null);
		}
	}
}