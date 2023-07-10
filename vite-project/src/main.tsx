import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import ApiAuthorizationRoutes from './components/authorization/ApiAuthorizationRoutes'
import './index.css'
//import Login from './components/authorization/Login.tsx'
import Layout from './components/Layout.tsx'
import Resource from './components/Resource.tsx'

const router = createBrowserRouter([
    {
        element: <Layout />,
        children: [
            {
                path: "/",
                element: <div>Hello World!</div>
            },
            {
                path: "resource/:id",
                element: <Resource />
            },
            ...ApiAuthorizationRoutes
        ]
    }
]);

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>,
    )
