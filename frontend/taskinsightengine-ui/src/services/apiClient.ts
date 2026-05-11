const BASE = process.env.NEXT_PUBLIC_API_URL

export async function apiClient<T>(endpoint: string, options: RequestInit = {}):Promise<T>
{
    const res = await fetch (`${BASE}${endpoint}`, {
        ...options,
        credentials: 'include',
    headers:{
        'Content-Type': 'application/json',
        ...options.headers,
    },
    });

    if(res.status === 401)
    {
        const refreshed = await fetch(`${BASE}/User/Refresh`,{
            method: 'POST',
            credentials: 'include',
        });

        
        if(!refreshed.ok)
            {
                sessionStorage.removeItem('user');
                window.location.href = '/account/login';
            }
         return apiClient(endpoint, options);
    }

    if(!res.ok)
    {
         const error = await res.json().catch(() => ({}));
         throw new Error(error.message || 'Something went wrong');
    }

    return res.json() as Promise<T>;
  }
  
    export const api ={
      post: <T>(endpoint: string, body: any, options?: RequestInit) => 
      apiClient<T>(endpoint, { 
      ...options, 
      method: 'POST', 
      body: JSON.stringify(body) 
    }),
    }

