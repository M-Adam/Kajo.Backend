using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kajo.Backend.Common;
using Kajo.Backend.Common.Authorization;
using Kajo.Backend.Common.Models;
using Kajo.Backend.Common.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using static Kajo.Backend.Common.Repositories.RepositoryConst;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(CommonFunctionsStartup))]
namespace Kajo.Backend.Common
{
    public class CommonFunctionsStartup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddWebJobs(options => options.AllowPartialHostStartup = true)
                .AddRequestBodyBinding();

            builder.Services.AddSingleton(provider => new MongoClient(ConnectionString).GetDatabase(DatabaseName));
            
            builder.Services.AddScoped<IChecklistRepository>(provider => 
                new ChecklistsRepository(provider.GetService<IMongoDatabase>()));
            builder.Services.AddScoped<IUserRepository>(provider =>
                new UserRepository(provider.GetService<IMongoDatabase>()));
        }
    }
}
