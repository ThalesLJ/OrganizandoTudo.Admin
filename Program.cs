using Microsoft.AspNetCore.Hosting;

namespace OrganizandoTudo.Admin
{
    public class Program
    {
        public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseUrls("http://*:5000");  // Configura para escutar em todas as interfaces na porta 5000
            });
    }
}
