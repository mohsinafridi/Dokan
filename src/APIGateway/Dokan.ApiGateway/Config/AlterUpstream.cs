using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Dokan.ApiGateway.Config
{
    public class AlterUpstream
    {
        public static string AlterUpstreamSwaggerJson(HttpContext context, string swaggerJson)
        {
            var swagger = JObject.Parse(swaggerJson);
            // ... alter upstream json
            return swagger.ToString(Formatting.Indented);
        }
    }
}
