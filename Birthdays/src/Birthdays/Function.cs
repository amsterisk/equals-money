using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Birthdays;

public class Function
{
    public string FunctionHandler(string input, ILambdaContext context)
    {
        return input;
    }
}
