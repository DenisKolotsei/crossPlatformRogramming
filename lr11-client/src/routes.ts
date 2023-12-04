import { FC } from "react";
import Home from "./pages/Home";
import Register from "./pages/Register";
import Profile from "./pages/Profile";
import Lab3 from "./pages/Lab3";
import Lab2 from "./pages/Lab2";
import Lab1 from "./pages/Lab1";
import CodeExchange from "./pages/CodeExchange";

interface Page{
    path: string;
    component: FC;
}

export const privateRoutes : Page[] = [
   {
    path: "/lab1",
    component: Lab1
   },
   {
    path: "/lab2",
    component: Lab2
   },
   {
    path: "/lab3",
    component: Lab3
   },
   {
    path: "/profile",
    component: Profile
   }
];

export const publicRoutes : Page[] = [
    {
        path: "/",
        component: Home
    },
    {
        path: "/registration",
        component: Register
    },
    {
        path: "/code",
        component: CodeExchange
    },
 ];