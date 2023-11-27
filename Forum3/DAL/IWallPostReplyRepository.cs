using Forum3.Models;

namespace Forum3.DAL;

public interface IWallPostReplyRepository
{
    Task<WallPostReply?> GetById(int id);
    
    Task<bool> Create(WallPostReply wallPostReply);
    Task<bool> Delete(int wallPostReplyId);
    Task<IEnumerable<WallPostReply>?> GetAllByCreator(string wallPostCreatorId);
}