
import { memo } from "react";
import { Outlet } from "react-router-dom";
import { Layout, Menu, theme } from 'antd';
import './index.css'
import { Logo } from "@lobehub/ui";
import { Package, User, Gauge, ChartCandlestick, Settings } from 'lucide-react'
import { useNavigate, useLocation } from "react-router-dom";
import React from "react";
const { Header, Content, Footer, Sider } = Layout;

const DesktopLayout = memo(() => {
    const navigate = useNavigate();
    const location = useLocation();

    const [selectedKey, setSelectedKey] = React.useState(location.pathname);
    console.log(selectedKey);

    const {
        token: { colorBgContainer, borderRadiusLG },
    } = theme.useToken();


    return (<Layout className="admin-layout" style={{
        height: '100vh',
    }}>
        <Sider
            collapsedWidth="0"
            onBreakpoint={(broken) => {
                console.log(broken);
            }}
            onCollapse={(collapsed, type) => {
                console.log(collapsed, type);
            }}
        >
            <div>
                <div
                    className="logo"
                    style={{
                        height: '32px',
                        background: 'rgba(255, 255, 255, 0.2)',
                        margin: '16px',
                    }}
                >
                    <Logo size={35} extra="NuGet Next" />
                </div>
            </div>
            <Menu mode="inline"
                defaultSelectedKeys={[selectedKey]}
                selectedKeys={[selectedKey]}
                onSelect={({ key }) => {
                    if (key === "dashboard") {
                        setSelectedKey(key);
                        navigate("/admin");
                        return;
                    } else {
                        setSelectedKey(key);
                        navigate(`/admin/${key}`);
                    }
                }}
                style={{
                    background: colorBgContainer,
                    borderRadius: borderRadiusLG,
                    border: 'none',
                    height: '100%',
                }}
                items={[
                    {
                        key: 'dashboard',
                        icon: <Gauge />,
                        label: '控制面板',
                    },
                    {
                        key: 'user-management',
                        icon: <User />,
                        label: '用户管理',
                    },
                    {
                        key: 'package-management',
                        icon: <Package />,
                        label: '包管理',
                    },
                    {
                        key: 'common-history',
                        icon: <ChartCandlestick />,
                        label: '提交记录',
                    },
                    {
                        key: 'settings',
                        icon: <Settings />,
                        label: '系统设置',
                    }
                ]} />
        </Sider>
        <Layout>
            <Header style={{ padding: 0, background: colorBgContainer }} />
            <Content style={{ margin: '24px 16px 0' }}>
                <div
                    style={{
                        padding: 24,
                        minHeight: 360,
                        background: colorBgContainer,
                        borderRadius: borderRadiusLG,
                    }}
                >
                    <Outlet />
                </div>
            </Content>
            <Footer style={{ textAlign: 'center' }}>
                Ant Design ©{new Date().getFullYear()} Created by Ant UED
            </Footer>
        </Layout>
    </Layout>
    )
})

DesktopLayout.displayName = 'DesktopLayout'

export default DesktopLayout;