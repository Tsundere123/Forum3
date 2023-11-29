using Forum3.DTOs.Lookup;

namespace Forum3.DTOs;

public class SearchDto
{
    public List<LookupThreadDto> Threads { get; set; } = default!;
    public List<LookupPostDto> Posts { get; set; } = default!;
    public List<LookupUserDto> Members { get; set; } = default!;
}