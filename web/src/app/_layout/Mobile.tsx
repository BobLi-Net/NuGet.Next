import { memo } from "react";

const MobileLayout = memo(() => {
    return (
        <div>
            <Outlet/>
        </div>
    )
})

MobileLayout.displayName = 'MobileLayout'

export default MobileLayout;