import { useQuery } from "@tanstack/react-query";
import { getProjectMembers } from "../services/taskservice";
import { GetProjectMembersRequest } from "../../../types/request";

export const useGetProjectMembers = (
request: GetProjectMembersRequest) => {
  return useQuery({
    queryKey: ["members", request.email],
    queryFn: () => getProjectMembers(request),
    enabled: !!request.email,
    select: (data) => data.members,
  });
};