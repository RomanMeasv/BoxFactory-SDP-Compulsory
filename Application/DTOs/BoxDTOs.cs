namespace Application.DTOs;

public class PostBoxDTO
{
    public int Width { get; set; }
    public int Height { get; set; }
}

public class PartialUpdateBoxDTO
{
    public int Id { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
}