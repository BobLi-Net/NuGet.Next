import { Header, Logo, TabsNav } from "@lobehub/ui";
import { memo } from "react";
import Avatar from "./Avatar";
import { useNavigate } from "react-router-dom";
import { useActiveTabKey } from "@/hooks/useActiveTabKey";

const Top = memo(() => {
    const navigate = useNavigate();
    const activeTabKey = useActiveTabKey();

    return (
        <Header
            logo={<Logo extra={'NuGet Next'} onClick={() => {
                navigate('/');
            }} />}
            title="NuGet Next"
            nav={<>
                <TabsNav
                    activeKey={activeTabKey}
                    onChange={(key) => {
                        navigate("/" + key);
                    }}
                    items={[
                        {
                            key: 'packages',
                            label: '包',
                        },
                        {
                            key: 'upload',
                            label: '上传',
                        },
                        {
                            key: 'key-manager',
                            label: '密钥管理',
                        },
                        {
                            key: 'docs',
                            label: '文档',
                        }
                    ]} />
            </>}
            actions={<>
                <Avatar />
            </>}
        >

        </Header>
    )
});

Top.displayName = 'Header'

export default Top;