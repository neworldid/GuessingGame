using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace GuessingGame.API;

public class GuessingGameModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterAssemblyTypes(
				Assembly.Load("GuessingGame.Domain"),
				Assembly.Load("GuessingGame.Infrastructure"))
			.AsImplementedInterfaces()
			.InstancePerLifetimeScope();
		
		builder.RegisterAssemblyTypes(
				Assembly.Load("GuessingGame.Domain"),
				Assembly.Load("GuessingGame.Application"))
			.AsImplementedInterfaces()
			.InstancePerLifetimeScope();
	}
}