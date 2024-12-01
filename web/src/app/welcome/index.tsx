import React from 'react';
import { Button, Typography, Card } from 'antd';
import { useNavigate } from 'react-router-dom';
import { Flexbox } from 'react-layout-kit';

const { Title, Paragraph } = Typography;

const WelcomePage = () => {
    const navigate = useNavigate();

    return (
        <Flexbox
            style={{
                height: '100vh',
                justifyContent: 'center',
                alignItems: 'center',
                backgroundColor: '#f0f2f5',
                padding: '20px',
            }}
        >
            <Card
                style={{
                    maxWidth: '600px',
                    textAlign: 'center',
                    boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)',
                }}
            >
                <Title level={2}>欢迎来到 NuGet Next</Title>
                <Paragraph>
                    这是一个现代化的NuGet包管理平台，提供快速、可靠的包上传和下载服务。
                </Paragraph>
                <Paragraph>
                    了解更多关于如何创建和管理NuGet包的信息，请访问我们的文档。
                </Paragraph>
                <Button
                    type="primary"
                    size="large"
                    onClick={() => navigate('/upload')}
                    style={{ marginTop: '20px' }}
                >
                    开始上传
                </Button>
            </Card>
        </Flexbox>
    );
};

export default WelcomePage;