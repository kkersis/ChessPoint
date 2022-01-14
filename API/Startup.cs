using Core;
using Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.MappingProfiles;
using DataAccess.Repositories.ApiCallers;
using Domain.Parsers;

namespace API
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
            var apiSettings = new ApiSettings
            {
                ChesscomApi = Configuration.GetSection("ChessApis").GetSection("Chesscom").Value,
                LichessApi = Configuration.GetSection("ChessApis").GetSection("Lichess").Value,
                LichessSite = Configuration.GetSection("ServerUrls").GetSection("Lichess").Value
            };

            services.AddSingleton(apiSettings);
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ILichessParser, LichessParser>();
            services.AddScoped<IChesscomParser, ChesscomParser>();
            services.AddScoped<IChesscomRepository, ChesscomRepository>();
            services.AddScoped<ILichessRepository, LichessRepository>();
            services.AddCors();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GameProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
