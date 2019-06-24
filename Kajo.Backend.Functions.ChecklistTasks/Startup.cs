using Kajo.Backend.Common;
using Kajo.Backend.Functions.ChecklistTasks;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Kajo.Backend.Functions.ChecklistTasks
{
    public class Startup : CommonFunctionsStartup
    {

    }
}