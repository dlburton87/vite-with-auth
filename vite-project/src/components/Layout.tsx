import React from "react";
import NavBar from "./NavBar";
import { Outlet } from "react-router-dom";

export default function Layout() {
    return (
        <div>
            <NavBar />
            <div className="container mx-auto">
                <Outlet />
            </div>
        </div>
    );
}