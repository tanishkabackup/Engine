import { NextResponse } from "next/server";
import type { NextRequest } from "next/server";

export function  proxy(request: NextRequest)
{
    const {pathname} = request.nextUrl;
    
    const accessToken = request.cookies.get("accessToken")?.value;
    const refreshToken = request.cookies.get("refreshToken")?.value;

    const isLoggedIn = accessToken || refreshToken

    const isAuthPage= pathname==="/account/login" || pathname ==="/account/register";

    if(!isAuthPage && !isLoggedIn)
    {
        const loginUrl = new URL("/account/login", request.url);
    }

    if(isAuthPage && isLoggedIn)
    {
        return NextResponse.redirect(new URL('/dashboard/createproject',request.url));
    }

    return NextResponse.next();
}
    export const config = {
          matcher: ["/account/login","/account/register","/dashboard/:path*","/"],
    };

