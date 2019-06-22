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

[assembly: WebJobsStartup(typeof(CommonFunctionsStartup))]
namespace Kajo.Backend.Common
{
    public class CommonFunctionsStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddAccessTokenBinding();
            builder.AddRequestBodyBinding();

            builder.Services.AddSingleton<IMongoClient>(provider => new MongoClient(ConnectionString));
            builder.Services.AddSingleton(provider => provider.GetService<IMongoClient>().GetDatabase(DatabaseName));

            builder.Services.AddScoped<IChecklistRepository>(provider => new ChecklistsRepository(GetDatabase(provider)));

            //builder.Services.AddAuthenticationCore(options =>
            //{
            //    options.AddScheme("asdf", schemeBuilder => schemeBuilder.);
            //})

            //builder.Services.AddAuthorization(options =>
            //{
            //    options.DefaultPolicy = new AuthorizationPolicy(new List<IAuthorizationRequirement>()
            //    {
            //        new AssertionRequirement(context => )
            //    },);
            //});
            
        }

        private static IMongoDatabase GetDatabase(IServiceProvider serviceProvider) => serviceProvider.GetService<IMongoDatabase>();
    }

    //class s : IAuthenticationHandler
    //{
    //    public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
    //    {
            
    //    }

    //    public Task<AuthenticateResult> AuthenticateAsync()
    //    {
    //        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket()))
    //    }

    //    public Task ChallengeAsync(AuthenticationProperties properties)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task ForbidAsync(AuthenticationProperties properties)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
