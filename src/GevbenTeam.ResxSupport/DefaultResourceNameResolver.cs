using System.Reflection;
using GevbenTeam.ResxSupport.Core;

namespace GevbenTeam.ResxSupport
{
	public class DefaultResourceNameResolver : IResourceNameResolver
	{
		public string Resolve(MemberInfo info)
		{
			return $"{info.DeclaringType.FullName.Replace('.', '_')}_{info.Name}";
		}
	}
}