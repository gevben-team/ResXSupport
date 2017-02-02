using System.Reflection;

namespace GevbenTeam.ResxSupport.Core
{
	public interface IResourceNameResolver
	{
		string Resolve(MemberInfo info);
	}
}