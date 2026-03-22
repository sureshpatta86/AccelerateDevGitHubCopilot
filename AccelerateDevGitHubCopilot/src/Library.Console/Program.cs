--- a/AccelerateDevGitHubCopilot/src/Library.Console/Program.cs
+++ b/AccelerateDevGitHubCopilot/src/Library.Console/Program.cs
@@ -1,6 +1,10 @@
 using Microsoft.Extensions.DependencyInjection;
 using Library.Infrastructure.Data;
 using Library.ApplicationCore;
+using Microsoft.AspNetCore.Builder;
 using Microsoft.Extensions.Configuration;
 
+// Ensure HTTPS redirection is configured in the web application startup
+// Example for ASP.NET Core application:
+// var app = builder.Build();
+// app.UseHttpsRedirection();
 
 var services = new ServiceCollection();