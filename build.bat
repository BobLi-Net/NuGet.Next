@REM 构建 NuGet.Next 的平台部署

dotnet publish --project src\NuGet.Next\NuGet.Next.csproj -c Release -o .\artifacts\NuGet.Next\win-x64 --self-contained --runtime win-x64 