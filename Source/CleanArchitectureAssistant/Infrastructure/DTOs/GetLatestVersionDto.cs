namespace CleanArchitectureAssistant.Infrastructure.DTOs;

public class GetLatestVersionDto
{
    public ResultDto[] Results { get; set; }
}


public class ResultDto
{
    public ExtensionDto[] Extensions { get; set; }
}

public class ExtensionDto
{
    public VersionDto[] Versions { get; set; }
}

public class VersionDto
{
    public string Version { get; set; }
    public string LastUpdated { get; set; }
}
