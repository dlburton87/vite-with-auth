import { useEffect, useState } from "react";
import { axiosPrivate } from "../common/axiosPrivate";
import { useParams } from "react-router-dom";

interface Response { id: number, resourceName:string}

export default function Resource() {
    const [resourceName, setResourceName] = useState("");
    const { id } = useParams();

    useEffect(() => {
        async function fetchData() {
            const result = await axiosPrivate.get<Response>(`api/resource/${id}`);
            setResourceName(result.data.resourceName);
        }

        fetchData();
    }, []);

    return (
        <div className="flex flex-col">
            <div>Id: {id}</div>
            <div>Resource Name: {resourceName}</div>
        </div>
    )

}