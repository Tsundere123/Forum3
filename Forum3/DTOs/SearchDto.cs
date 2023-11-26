namespace Forum3.DTOs;

public class SearchDto
{
    public List<LookupThreadDto> threads { get; set; }
    public List<LookupPostDto> posts { get; set; }
    public List<LookupUserDto> members { get; set; }
}