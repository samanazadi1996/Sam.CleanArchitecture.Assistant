using System.Collections.Generic;
using CleanArchitectureTools.Infrastructure.Enums;

namespace CleanArchitectureTools.Infrastructure.Data
{
    internal class UseCaseData
    {
        internal static IEnumerable<UseCaseClassDto> GetData(string featureName, string useCaseName, UseCaseType type)
        {
            yield return new UseCaseClassDto()
            {
                Content = "aaaa.cs",
                Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };

            yield return new UseCaseClassDto()
            {
                Content = "bbbbb.cs",
                Name = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb"
            };
        }

        internal class UseCaseClassDto
        {
            public string Name { get; set; }
            public string Content { get; set; }

        }
    }
}
