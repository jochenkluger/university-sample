using GraphQL.Types;

namespace UniversitySample.Courses.Service.GraphQl.Schema
{
    public class CoursesSchema: GraphQL.Types.Schema
    {
        public CoursesSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = new AutoRegisteringObjectGraphType<Query>();
        }
    }
}
