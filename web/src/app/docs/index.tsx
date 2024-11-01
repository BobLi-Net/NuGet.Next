import Menu from "@/components/Menu";
import { Markdown } from "@lobehub/ui";
import { memo, useEffect, useState } from "react";

import { Flexbox } from 'react-layout-kit';


const DocsPage = memo(() => {
    const [menu, setMenu] = useState('quick-start');
    const [content, setContent] = useState('');
    useEffect(() => {
        fetch(`/docs/${menu}.md`).then(v => v.text()).then(v => {
            setContent(v);
        });
    }, [menu]);

    return <>
        <Flexbox horizontal>
            <div style={{
                width: 300,
                minWidth: 300,
                maxWidth: 300,
                height: '100vh',
                backgroundColor: '#f0f0f0',
                borderRight: '1px solid #e0e0e0',
            }}>
                <Menu
                    key={menu}
                    selectedKeys={[menu]}
                    onClick={(v) => {
                        setMenu(v.key);
                    }}
                    items={[
                        {
                            label: "快速开始",
                            key: "quick-start"
                        },
                        {
                            label: "关于我们",
                            key: "about"
                        },
                        {
                            label: "隐私政策",
                            key: "privacy"
                        }
                    ]}
                />
            </div>
            <div style={{
                padding: 20,
                overflow: 'auto',
                height: 'calc(100vh - 64px)',
            }}>
                <Markdown
                    allowHtml={true}
                    lineHeight={1.8}
                >
                    {content}
                </Markdown>
            </div>
        </Flexbox>
    </>
});

export default DocsPage;
