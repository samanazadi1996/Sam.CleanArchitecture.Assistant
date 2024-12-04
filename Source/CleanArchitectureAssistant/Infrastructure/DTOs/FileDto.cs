namespace CleanArchitectureAssistant.Infrastructure.DTOs;

public class FileDto(string name)
{
    public string Name { get; } = name;
    public string Path{ get; set; }
    public string Content { get; set; }
}
public class EntityDto
{
    public string ClassName { get; set; }
    public string Namespace { get; set; }
}
