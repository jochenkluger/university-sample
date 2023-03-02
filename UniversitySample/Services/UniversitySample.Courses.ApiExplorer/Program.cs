using Grpc.Net.Client;
using UniversitySample.Courses.Domain.GrpcApi;

namespace UniversitySample.Courses.ApiExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5001");
            var client = new GrpcCourses.GrpcCoursesClient(channel);

            var response = client.Get(new GetCoursesRequest() {Name = "Test1"});

            Console.WriteLine($"Die Anfrage gab {response.Courses.Count} Kurse zurück");
        }
    }
}