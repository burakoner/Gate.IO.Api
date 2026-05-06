using System.Reflection;

namespace Gate.IO.Api.Tests.Infrastructure;

[Trait("Category", TestCategories.Unit)]
public class TestCategoryCoverageTests
{
    [Fact]
    public void All_test_classes_have_supported_category_traits()
    {
        var testClasses = typeof(TestCategoryCoverageTests).Assembly
            .GetTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false })
            .Where(type => type.GetMethods().Any(IsTestMethod))
            .OrderBy(type => type.FullName, StringComparer.Ordinal)
            .ToArray();

        Assert.NotEmpty(testClasses);

        foreach (var testClass in testClasses)
        {
            var categories = GetCategoryTraits(testClass).ToArray();

            Assert.True(categories.Length > 0, $"{testClass.FullName} is missing a Category trait.");
            Assert.Equal(categories.Length, categories.Distinct(StringComparer.Ordinal).Count());

            foreach (var category in categories)
                Assert.Contains(category, TestCategories.All);
        }
    }

    [Fact]
    public void Standard_test_categories_are_defined_for_filtering()
    {
        var expected = new[]
        {
            TestCategories.Contract,
            TestCategories.LiveCapture,
            TestCategories.LiveWebSocket,
            TestCategories.PublicIntegration,
            TestCategories.RequiresCredentials,
            TestCategories.Unit,
        };

        Assert.Equal(expected, TestCategories.All.OrderBy(x => x, StringComparer.Ordinal));
    }

    private static bool IsTestMethod(MethodInfo method)
        => method.GetCustomAttributesData().Any(attribute =>
            typeof(FactAttribute).IsAssignableFrom(attribute.AttributeType)
            || typeof(TheoryAttribute).IsAssignableFrom(attribute.AttributeType));

    private static IEnumerable<string> GetCategoryTraits(MemberInfo member)
        => member.GetCustomAttributesData()
            .Where(attribute => attribute.AttributeType == typeof(TraitAttribute))
            .Where(attribute => attribute.ConstructorArguments.Count == 2)
            .Where(attribute => string.Equals(attribute.ConstructorArguments[0].Value as string, "Category", StringComparison.Ordinal))
            .Select(attribute => (string)attribute.ConstructorArguments[1].Value!);
}
