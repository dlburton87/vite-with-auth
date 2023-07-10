import { NavLink } from "react-router-dom";
import { ApplicationPaths } from "./authorization/ApiAuthorizationConstants";
//import LoginMenu from "./authorization/LoginMenu";
import { LoginMenu } from "./authorization/LoginMenu"

export default function NavBar() {
    return (
        <div className="bg-slate-700 w-full sticky flex flex-row">
            <div className="p-5 text-xl text-white flex-grow">
                <NavLink to="/">Sample React App</NavLink>
            </div>
            <div className="flex justify-end flex-row gap-5 items-center me-5 text-white">
                <NavLink to="resource/1" className="text-white">Resource</NavLink>
                <LoginMenu />
            </div>
        </div>
    );
}