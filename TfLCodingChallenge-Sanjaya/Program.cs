using BusinessLogicLayer;
using Ninject;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TfLCodingChallenge_Sanjaya
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var roadId = "";
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter Road Id as parameter.");

                roadId = Console.ReadLine();
            }
            else
            {
                roadId = args.First();
            }

            if (string.IsNullOrWhiteSpace(roadId))
            {
                Console.WriteLine("Road Id not supplied");
                Console.ReadLine();
            }
            else
            {
                await CallWebAPIAsync(roadId);
                Console.ReadLine();
            }
        }
        static async Task CallWebAPIAsync(string roadId)
        {
            try
            {
                StandardKernel _kernel = new StandardKernel();
                _kernel.Load(Assembly.GetExecutingAssembly());

                IRoadStatusDetails status = _kernel.Get<IRoadStatusDetails>();
                var result = await status.GetRoadsStatusList(roadId);

                if (result == null || result.Count < 1)
                {
                    Console.WriteLine($"{roadId} is not a valid road");
                    return;
                }

                foreach (var roadStatus in result)
                {
                    Console.WriteLine($"The status of the {roadStatus.displayName} is as follows");
                    Console.WriteLine($"Road status is {roadStatus.statusSeverity}");
                    Console.WriteLine($"Road Status Description is {roadStatus.statusSeverityDescription}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error 201: Error while retrieving road status");
            }
        }
    }
}
