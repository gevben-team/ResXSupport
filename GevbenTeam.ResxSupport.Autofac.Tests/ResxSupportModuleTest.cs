using Autofac;
using Xunit;

namespace GevbenTeam.ResxSupport.Autofac.Tests
{
	public class ResxSupportModuleTest
	{
		private readonly ILifetimeScope scope;

		public ResxSupportModuleTest()
		{
			var builder = new ContainerBuilder();
			builder.UseResx<TestResources>();
			builder.UseResx<TestResources2>();
			builder.RegisterType<TestClass>();
			scope = builder.Build().BeginLifetimeScope();
		}

		[Fact]
		public void Should_Inject_String_From_Resx()
		{
			var obj = scope.Resolve<TestClass>();

			Assert.Equal("TestValue", obj.TestProp);
			Assert.Equal("TestValue2", obj.TestProp2);
		}

		~ResxSupportModuleTest()
		{
			scope.Disposer.Dispose();
		}
	}
}