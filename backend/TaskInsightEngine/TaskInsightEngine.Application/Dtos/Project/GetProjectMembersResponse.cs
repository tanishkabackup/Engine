using TaskInsightEngine.Application.Dtos.Member;

namespace TaskInsightEngine.Application.Dtos.Project
{
    public class GetProjectMembersResponse
    {
       public List<MemberDetail> Members { get; set; }
    }
}
