import { GridShowcase, Input } from "@lobehub/ui";
import { Button } from 'antd';
import { useState } from "react";
import { useNavigate } from "react-router-dom";
const Welcome = () => {
    const [search, setSearch] = useState("");
    const navigate = useNavigate();

    return <>
        <GridShowcase style={{ width: '100%' }}>
            <div style={{ fontSize: 48, fontWeight: 600, marginTop: -16 }}>
                使用NuGet Next 更快创建 .NET 程序。</div>
            <div style={{
                marginLeft: 'auto',
                marginRight: 'auto',
                width: 500,
            }}>
                <Input
                    value={search}
                    onChange={(e) => setSearch(e.target.value)}
                    suffix={<Button onClick={() => {
                        var query = new URLSearchParams();
                        query.set('q', encodeURIComponent(search));
                        navigate(`/packages?${query}`);
                    }} type="text">🔍</Button>}
                    placeholder="搜索包..." />
            </div>
        </GridShowcase>
    </>;
}

export default Welcome;