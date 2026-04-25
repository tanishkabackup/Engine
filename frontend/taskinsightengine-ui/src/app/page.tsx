import {redirect } from "next/navigation";

export default function MainPage(){
    redirect("/account/login");
}