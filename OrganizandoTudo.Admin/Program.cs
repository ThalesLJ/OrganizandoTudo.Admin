namespace OrganizandoTudo.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurando o appsettings.json
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Adicionando serviços ao contêiner de injeção de dependência
            builder.Services.AddControllersWithViews(); // Exemplo: adicionando suporte a MVC

            var app = builder.Build();

            // Configurando a porta de escuta
            app.Urls.Add("http://0.0.0.0:5000");

            // Configurando o middleware do pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
