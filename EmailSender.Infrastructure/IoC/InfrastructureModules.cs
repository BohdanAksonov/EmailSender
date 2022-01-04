using Autofac;
using EmailSender.Infrastructure.Services;

namespace EmailSender.Infrastructure.IoC
{
    public class InfrastructureModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailService>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<MimeMessageService>().AsImplementedInterfaces().InstancePerDependency();
        }
    }
}
