using NUnit.Framework;

using static CheckflixApp.Application.IntegrationTests.Testing;

namespace CheckflixApp.Application.IntegrationTests;
[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }
}
