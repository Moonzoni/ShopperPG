using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VsShopper_Infra;
using VsShopper_Infra.Interface;
using VsShopper_Infra.Repository;
using VsShopper_Infra.Validacoes;

namespace vsSHOPPER1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info()
                {
                    Description = "VsShopper1",
                    Title = "VsShopper",
                    Version = "1.0.1"
                });
            });

            services.AddDbContext<VsShopperContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("VsShopper"));
            });

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(ICategoriaRepository), typeof(CategoriaRepository));
            services.AddScoped(typeof(IPerfilRepository), typeof(PerfilRepository));
            services.AddScoped(typeof(IStatusRepository), typeof(StatusRepository));
            services.AddScoped(typeof(IUsuarioRepository), typeof(UsuarioRepository));
            services.AddScoped(typeof(IComprasRepository), typeof(ComprasRepository));
            services.AddScoped(typeof(IOrcamentoRepository), typeof(OrcamentoRepository));
            services.AddScoped(typeof(IBaseValida), typeof(BaseValidaRepository));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";

                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Minha API");
            });
        }
    }
}
