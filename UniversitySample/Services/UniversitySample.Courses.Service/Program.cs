using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using GraphQL;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using UniversitySample.Courses.Domain.Dto;
using UniversitySample.Courses.Service.ApiServices;
using UniversitySample.Courses.Service.GraphQl;
using UniversitySample.Courses.Service.GrpcApi;
using UniversitySample.Courses.Service.Interfaces;
using UniversitySample.Courses.Service.InternalService;

namespace UniversitySample.Courses.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.ConfigureKestrel((context, options) =>
            {
                options.AllowSynchronousIO = true;
                options.Listen(IPAddress.Any, 5001, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                });
            });

            // Add services to the container.

            builder.Services.AddServiceModelServices().AddServiceModelMetadata();
            builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<CourseProvider>();
            builder.Services.AddTransient<CourseService>();
            builder.Services.AddTransient<CoursesGraphQlService>();

            builder.Services.AddGrpc(options =>
            {
            });

            builder.Services.AddGraphQL(b => b
                .AddAutoSchema<GraphQl.Schema.Query>()
                .AddSystemTextJson());

            var app = builder.Build();
            //******GraphQL
            app.UseWebSockets();
            app.UseGraphQL("/graphql");            // url to host GraphQL endpoint
            app.UseGraphQLPlayground(
                "/playground",                               // url to host Playground at
                new GraphQL.Server.Ui.Playground.PlaygroundOptions
                {
                    GraphQLEndPoint = "/graphql",         // url of GraphQL endpoint
                    SubscriptionsEndPoint = "/graphql",   // url of GraphQL endpoint
                });


            //******SOAP

            // Configure an explicit none credential type for WSHttpBinding as it defaults to Windows which requires extra configuration in ASP.NET
            var myWSHttpBinding = new WSHttpBinding(SecurityMode.Transport);
            myWSHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

            app.UseServiceModel(builder =>
            {
                builder.AddService<CourseService>((serviceOptions) => { })
                    // Add a BasicHttpBinding at a specific endpoint
                    .AddServiceEndpoint<CourseService,
                        ICourseService>(new BasicHttpBinding(), "/CourseService/basichttp");
                // Add a WSHttpBinding with Transport Security for TLS
                //.AddServiceEndpoint<SoapService, ICourseService>(myWSHttpBinding, "/CourseService/WSHttps");
            });

            var serviceMetadataBehavior = app.Services.GetRequiredService<CoreWCF.Description.ServiceMetadataBehavior>();
            serviceMetadataBehavior.HttpGetEnabled = true;

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();
            app.MapGrpcService<GrpcCourseService>();

            app.Run();
        }
    }
}