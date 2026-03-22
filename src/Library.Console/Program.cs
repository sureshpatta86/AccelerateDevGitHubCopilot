--- a/AccelerateDevGitHubCopilot/src/Library.Console/Program.cs
+++ b/AccelerateDevGitHubCopilot/src/Library.Console/Program.cs
@@ -1,6 +1,7 @@
 using Microsoft.Extensions.DependencyInjection;
 using Library.Infrastructure.Data;
 using Library.ApplicationCore;
+using Microsoft.AspNetCore.Hosting;
 using Microsoft.Extensions.Configuration;
 
 var services = new ServiceCollection();
@@ -17,6 +18,20 @@
 services.AddSingleton<ConsoleApp>();
 
 var servicesProvider = services.BuildServiceProvider();
+
+var host = new WebHostBuilder()
+    .UseKestrel()
+    .UseContentRoot(Directory.GetCurrentDirectory())
+    .ConfigureServices(services => services.AddSingleton<IConfiguration>(configuration))
+    .Configure(app =>
+    {
+        app.UseHttpsRedirection();
+        // Additional middleware configuration can go here
+    })
+    .Build();
+
+host.RunAsync();
+
 var consoleApp = servicesProvider.GetRequiredService<ConsoleApp>();
 consoleApp.Run().Wait();