import { Layout } from "@lobehub/ui";
import { memo } from "react";
import { Outlet } from "react-router-dom";
import Top from "./Top";

const DesktopLayout = memo(() => {
    return (
        <Layout 
            header={<Top/>}
            >
            <Outlet/>
        </Layout>
    )
})

DesktopLayout.displayName = 'DesktopLayout'

export default DesktopLayout;