using Autofac;
using EmailSender.Application.CQRS.Email;
using MediatR;

namespace EmailSender.IoC
{
    public class AutofacModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();

                return t => c.Resolve(t);
            });

            builder.RegisterType<SendEmailCommandHandler>().AsImplementedInterfaces().InstancePerDependency();
        }
    }
}
