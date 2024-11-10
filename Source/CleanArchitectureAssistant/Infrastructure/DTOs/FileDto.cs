namespace CleanArchitectureAssistant.Infrastructure.DTOs;

public class FileDto(string name)
{
    public string Name { get; } = name;
    public string Content { get; set; }

}
