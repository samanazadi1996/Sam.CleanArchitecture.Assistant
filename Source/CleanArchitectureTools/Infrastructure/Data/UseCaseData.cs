using System.Collections.Generic;
using CleanArchitectureTools.Infrastructure.Enums;

namespace CleanArchitectureTools.Infrastructure.Data;

internal class UseCaseData
{
    internal static List<UseCaseClassDto> GetData(string solutionName,
        string featureName, string useCaseName, UseCaseType type, string returnType)
    {
        List<UseCaseClassDto> result = [];
        if (type == UseCaseType.Query)
        {
            result.Add(new UseCaseClassDto($"{useCaseName}Query.cs")
            {
                Content = @$"﻿using {solutionName}.Application.Wrappers;
using MediatR;

namespace {solutionName}.Application.Features.{featureName}.Queries.{useCaseName}
{{
    public class {useCaseName}Query : IRequest<BaseResult<{returnType}>>
    {{
        public {returnType} MyProperty {{ get; set; }}
    }}
}}",
            });

            result.Add(new UseCaseClassDto($"{useCaseName}QueryHandler.cs")
            {
                Content = $@"using {solutionName}.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace {solutionName}.Application.Features.{featureName}.Queries.{useCaseName}
{{
    public class {useCaseName}QueryHandler : IRequestHandler<{useCaseName}Query, BaseResult<{returnType}>>
    {{
        public async Task<BaseResult<{returnType}>> Handle({useCaseName}Query request, CancellationToken cancellationToken)
        {{
            // Handler

            return request.MyProperty;
        }}
    }}
}}",
            });
        }
        else if (type == UseCaseType.QueryPagedList)
        {
            result.Add(new UseCaseClassDto($"{useCaseName}Query.cs")
            {
                Content = @$"﻿using {solutionName}.Application.Wrappers;
using {solutionName}.Application.Parameters;
using MediatR;

namespace {solutionName}.Application.Features.{featureName}.Queries.{useCaseName}
{{
    public class {useCaseName}Query : PaginationRequestParameter, IRequest<PagedResponse<{returnType}>>
    {{
        public {returnType} MyProperty {{ get; set; }}
    }}
}}",
            });

            result.Add(new UseCaseClassDto($"{useCaseName}QueryHandler.cs")
            {
                Content = @$"﻿using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using {solutionName}.Application.DTOs;
using {solutionName}.Application.Wrappers;

namespace {solutionName}.Application.Features.{featureName}.Queries.{useCaseName}
{{
    public class {useCaseName}QueryHandler : IRequestHandler<{useCaseName}Query, PagedResponse<{returnType}>>
    {{
        public async Task<PagedResponse<{returnType}>> Handle({useCaseName}Query request, CancellationToken cancellationToken)
        {{
            // Handler

            List<{returnType}> data = [];
            int totalCount = 100;

            return new PaginationResponseDto<{returnType}>(data, totalCount, request.PageNumber, request.PageSize);
        }}
    }}
}}",
            });
        }
        else
        {
            result.Add(new UseCaseClassDto($"{useCaseName}Command.cs")
            {
                Content = @$"using {solutionName}.Application.Wrappers;
using MediatR;

namespace {solutionName}.Application.Features.{featureName}.Commands.{useCaseName}
{{
    public class {useCaseName}Command : IRequest<BaseResult<{returnType}>>
    {{
        public {returnType} MyProperty {{ get; set; }}
    }}
}}",
            });

            result.Add(new UseCaseClassDto($"{useCaseName}CommandHandler.cs")
            {
                Content = @$"using {solutionName}.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace {solutionName}.Application.Features.{featureName}.Commands.{useCaseName}
{{
    public class {useCaseName}CommandHandler : IRequestHandler<{useCaseName}Command, BaseResult<{returnType}>>
    {{
        public async Task<BaseResult<{returnType}>> Handle({useCaseName}Command request, CancellationToken cancellationToken)
        {{
            // Handler

            return request.MyProperty;
        }}
    }}
}}",
            });

            result.Add(new UseCaseClassDto($"{useCaseName}CommandValidator.cs")
            {
                Content = @$"using {solutionName}.Application.Interfaces;
using FluentValidation;

namespace {solutionName}.Application.Features.{featureName}.Commands.{useCaseName}
{{
    public class {useCaseName}CommandValidator : AbstractValidator<{useCaseName}Command>
    {{
        public {useCaseName}CommandValidator(ITranslator translator)
        {{
            RuleFor(p => p.MyProperty)
                .NotNull()
                .WithName(p => translator[nameof(p.MyProperty)]);
        }}
    }}
}}",
            });
        }

        if (returnType == "void")
        {
            foreach (var item in result)
            {
                item.Content = item.Content
                    .Replace("<void>", "")
                    .Replace("void MyProperty", "string MyProperty")
                    .Replace("return request.MyProperty;", "return BaseResult.Ok();");

            }
        }
        return result;
    }

    internal class UseCaseClassDto(string name)
    {
        public string Name { get; } = name;
        public string Content { get; set; }

    }
}
