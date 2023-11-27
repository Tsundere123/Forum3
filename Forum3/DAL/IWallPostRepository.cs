using Forum3.Models;

namespace Forum3.DAL;

public interface IWallPostRepository
{
    Task<IEnumerable<WallPost>?> GetAllByProfile(string id);
    Task<WallPost?> GetById(int id);
    
    Task<bool> Create(WallPost wallPost);
    Task<bool> Delete(int wallPostId);
    Task<IEnumerable<WallPost>?> GetAllByCreator(string wallPostCreatorId);
}