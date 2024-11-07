@echo off
REM 设置控制台代码页为 UTF-8
chcp 65001 >nul

REM 检查是否以管理员身份运行
openfiles >nul 2>&1
if %errorlevel% neq 0 (
    echo 请以管理员身份运行此脚本。
    pause
    exit /b
)

REM 设置服务名称和显示名称
set SERVICE_NAME=NuGetNext
set DISPLAY_NAME=NuGetNext 服务, 用于 NuGet.Next 的后台服务

REM 设置服务的可执行文件路径
set SERVICE_EXE=%~dp0NuGet.Next.exe

REM 创建服务
sc create %SERVICE_NAME% binPath= "%SERVICE_EXE%" DisplayName= "%DISPLAY_NAME%"

REM 设置服务为自动启动
sc config %SERVICE_NAME% start= auto

REM 启动服务
sc start %SERVICE_NAME%

echo 服务 %DISPLAY_NAME% 已创建并设置为开机自启动。
pause