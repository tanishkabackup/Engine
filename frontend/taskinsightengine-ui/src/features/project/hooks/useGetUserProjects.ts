import { useMemo } from "react";
import { useGetProjectMembers } from "@/features/project/hooks/useProjects";
import { useGetAllProjects } from "@/features/project/hooks/useProjects";
import { MemberDetail, ProjectDetail } from "@/types/response";


interface UserProjectsResult {
  projects: ProjectDetail[]
  isLoading: boolean;
  memberRecords: MemberDetail[];
}

export const useGetUserProjects =(email:string): UserProjectsResult =>{
    const {data: memberRecords=[] as MemberDetail[], isLoading: loadingMembers} = useGetProjectMembers({email});

      const projectIds = useMemo(
      () => [...new Set(memberRecords.map((m: MemberDetail) => Number(m.projectId)))],
      [memberRecords]
      );

      const {data: projects=[] as ProjectDetail[], isLoading: loadingProjects} = useGetAllProjects({projectIds},{enabled:projectIds.length>0});

      const sortedProjects = useMemo( ()=>[...projects].sort((a,b)=> new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()), [projects]);

      return{
          projects: sortedProjects,
          isLoading: loadingMembers || loadingProjects,
          memberRecords : memberRecords
      };
}
