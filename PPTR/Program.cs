using PPTR;
using PPTR.Domain;
using PPTR.Services;
using PPTR.Services.Abstractions;
using PPTR.Services.Utility;
using Serilog;
using Serilog.Events;
using Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<HostedReportService>();

        services.AddOptions();

        var reportOptions = new ReportOptions();
        hostContext.Configuration.GetSection("ReportOptions").Bind(reportOptions);
        services.Configure<ReportOptions>(hostContext.Configuration.GetSection(nameof(ReportOptions)));

        services.AddSingleton<IIntraDayReportGenerator, IntraDayReportGenerator>();
        services.AddSingleton<IIntraDayCsvReportWriter, IntraDayCsvReportWriter>();
        services.AddTransient<IPowerService, PowerService>();
        services.AddSingleton<IPositionAggrigator, PositionAggrigator>();
        services.AddSingleton<IReportPathProvider, ReportPathProvider>();
        services.AddSingleton<ITimePeriodConverter, TimePeriodConverter>();
        services.AddSingleton<IDateTimeOffsetProvider, DateTimeOffsetProvider>();
        services.AddSingleton<IPolicyProvider, DefaultPolicyProvider>();


        Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
              .Enrich.FromLogContext()
              .WriteTo.Console()
              .WriteTo.File(reportOptions.LogFilePath, rollingInterval: RollingInterval.Day)
              .CreateLogger();
    })
    .UseSerilog()
    .Build();


await host.RunAsync();
