using Forum3.DTOs.Lookup;
using Forum3.Models;

namespace Forum3.Utilities;

public static class DtoUtilities
{
    public static LookupUserDto GetUserDto(ApplicationUser user)
    {
        return new LookupUserDto
        {
            UserName = user.UserName,
            Avatar = user.Avatar,
            CreatedAt = user.CreatedAt
        };
    }
}