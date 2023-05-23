using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

namespace OutSystems.Lambda.HtmlToPdfGenerator.Tests;

public class FunctionTest
{
    [Fact]
    public async Task TestToUpperFunctionAsync()
    {
        // Invoke the lambda function and confirm the string was upper cased.
        var context = new TestLambdaContext();
        var upperCase = await Function.FunctionHandler("hello world", context);

        Assert.Equal(typeof(Task<byte[]>), upperCase.GetType());
    }
}