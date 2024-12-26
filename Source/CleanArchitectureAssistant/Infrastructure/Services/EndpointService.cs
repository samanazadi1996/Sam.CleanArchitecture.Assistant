using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureAssistant.Infrastructure.Data;
using CleanArchitectureAssistant.Infrastructure.DTOs;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CleanArchitectureAssistant.Infrastructure.Services;

public class EndpointService
{
    public static async Task<List<string>> GetControllerVersions()
    {
        var domainPath = await CommonService.GetEndpointPath();
        if (string.IsNullOrEmpty(domainPath))
            return [];

        var dir = Path.Combine(domainPath, "Controllers");

        return Directory.GetDirectories(dir)
            .Select(Path.GetFileName)
            .ToList();
    }
}