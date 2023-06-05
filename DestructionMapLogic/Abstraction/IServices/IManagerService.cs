using DestructionMapModel.Models;

namespace DestructionMapModel.Abstraction.IServices;

public interface IManagerService
{

    public Manager GetManager(string id);

    public bool VoteForEvent(string eventId, string managerId, bool isApproved);

    public bool IsAlreadyVoted(string eventId, string managerId);

    public void ApproveOrRemove(string eventId);

    public bool CreateManager(string userId);


}