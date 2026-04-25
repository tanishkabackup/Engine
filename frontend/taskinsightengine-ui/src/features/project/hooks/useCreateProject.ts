import { useMutation } from "@tanstack/react-query";
import { createProject } from "../services/projectservice";
import { CreateProjectRequest } from "../../../types/request";

export const useCreateProject = () => {
  return useMutation({
    mutationFn: (data: CreateProjectRequest) => createProject(data)
  });
};