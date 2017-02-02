using System;

namespace GevbenTeam.ResxSupport.Core
{
	public interface IResourceValueResolver
	{
		void AttachResource(Type resourceType);
		void DetachResource(Type resourceType);
		void ResolveValues(object obj);
	}
}