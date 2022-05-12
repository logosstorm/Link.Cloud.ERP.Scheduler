using Quartz;

namespace Scheduler.API.Jobs.HelloJob;

public static class HelloWorldJobExtension
{
    public static async void AddHelloWordJob(this IServiceCollectionQuartzConfigurator quartzConfigurator)
    {
        IJobDetail job = JobBuilder.Create<HelloWorldJob>()
           .WithIdentity("jobHelloWorld", "ExampleGroup")
           .Build();

        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("triggerHelloWorld", "ExampleGroup")
            .WithCronSchedule("0/5 * * * * ?")
            .ForJob("jobHelloWorld", "ExampleGroup")
            .Build();

        quartzConfigurator.AddJob<HelloWorldJob>(c =>
        {
            c.WithIdentity("jobHelloWorld", "ExampleGroup");
        });
        quartzConfigurator.AddTrigger(c =>
        {
            c.WithIdentity("triggerHelloWorld", "ExampleGroup");
            c.WithCronSchedule("0/5 * * * * ?");
            c.ForJob("jobHelloWorld", "ExampleGroup");
        });
    }
}