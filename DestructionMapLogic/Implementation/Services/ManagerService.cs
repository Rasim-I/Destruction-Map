using DestructionMapDAL;
using DestructionMapDAL.Entities;
using DestructionMapModel.Abstraction.IMappers;
using DestructionMapModel.Abstraction.IServices;
using DestructionMapModel.Implementation.Mappers;
using DestructionMapModel.Models;
using Type = DestructionMapDAL.Entities.Enums.Type;

namespace DestructionMapModel.Implementation.Services;

public class ManagerService : IManagerService
{
    private IUnitOfWork _unitOfWork;
    private IMapper<ManagerEntity, Manager> _managerMapper;

    public ManagerService(IUnitOfWork unitOfWork, IMapper<ManagerEntity, Manager> managerMapper)
    {
        _unitOfWork = unitOfWork;
        _managerMapper = managerMapper;
    }

    public Manager GetManager(string id)
    {
        ManagerEntity manager = _unitOfWork.Managers.Get(id);

        if (manager != null)
            return _managerMapper.ToModel(manager);
        else
            return null;
    }

    public bool CreateManager(string userId)
    {
        _unitOfWork.Managers.Create(new ManagerEntity{Id = userId, User_Id = userId});
        _unitOfWork.Save();
        return true;
    }

    public bool VoteForEvent(string eventId, string managerId, bool isApproved)
    {
        /*
        EventEntity eventToVote = _unitOfWork.Events.Get(eventId);
        ManagerEntity manager = _unitOfWork.Managers.Get(managerId);
        bool isAlreadyVoted = IsAlreadyVoted(eventId, managerId);
        if (eventToVote != null && manager != null && !isAlreadyVoted)
        {
            Managers_Approvals managerVote = new Managers_Approvals()
                { Id = Guid.NewGuid().ToString(), Event_Id = eventId, Manager_Id = managerId, IsApproved = isApproved };
            _unitOfWork.Approvals.Create(managerVote);
            _unitOfWork.Save();
            ApproveOrRemove(eventId);
            return true;
        }
        else
            return false;
*/
        EventEntity eventToVote = _unitOfWork.Events.Get(eventId);
        ManagerEntity manager = _unitOfWork.Managers.Get(managerId);

        if (eventToVote != null && manager != null)
        {
            Managers_Approvals managerVote = new Managers_Approvals()
            { Id = Guid.NewGuid().ToString(), Event_Id = eventId, Manager_Id = managerId, IsApproved = isApproved };
            
            _unitOfWork.Approvals.Create(managerVote);
            _unitOfWork.Save();
            
            ApproveOrRemove(eventId);
        }

        return true;
    }

    public bool IsAlreadyVoted(string eventId, string managerId)
    {
        Managers_Approvals? existingVote = _unitOfWork.Approvals.Find(a => a.Manager_Id == managerId && a.Event_Id == eventId).FirstOrDefault();
        if (existingVote != null)
            return true;
        else
            return false;
    }

    public void ApproveOrRemove(string eventId)
    {
        int approvingThreshold = 2;
        int removingThreshold = -2;

        EventEntity eventToCheck = _unitOfWork.Events.Get(eventId);
        List<Managers_Approvals> eventApprovals = _unitOfWork.Approvals.GetByEventId(eventId).ToList();

        int votes = 0;

        //count all votes and subtract maybe?
        
        foreach (Managers_Approvals managerVote in eventApprovals)
        {
            if (managerVote.IsApproved)
                votes++;
            else
                votes--;  //was ++ not checked
            
        }
        
        if (votes >= approvingThreshold)
        {
            eventToCheck.Type = Type.Event;
            _unitOfWork.Events.Update(eventToCheck);
            _unitOfWork.Save();
        }
        else if (votes <= removingThreshold)
        {
            var sources = _unitOfWork.Sources.GetAll().Where(s => s.Event_Id == eventId).ToList() ;

            foreach (var source in sources)
            {
                _unitOfWork.Sources.Remove(source);
            }

            var approvals = _unitOfWork.Approvals.GetByEventId(eventId).ToList();

            foreach (var approval in approvals)
            {
                _unitOfWork.Approvals.Remove(approval);
            }

            //_unitOfWork.Save();
                
            _unitOfWork.Events.Remove(eventToCheck);
            _unitOfWork.Save();
        }
        //approveCount = declineCount = 0;
        
        
    }
    
    
}