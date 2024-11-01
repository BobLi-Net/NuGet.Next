# NuGet Next

NuGet 最新版开源私有化包管理，我们基于BaGet的基础之上增加了更多的功能，并且对中国市场做更多兼容，比如国产化支持。

## 功能介绍

- 支持用户管理
- 支持包管理溯源
- 支持包管理
- 用户支持自定义Key
- 支持SqlServer数据库
- 支持PostgreSql数据库
- 支持MySql数据库
- 支持DM（达梦）数据库


## 快速部署

使用docker compose快速部署

```yaml
version: '3.8'
services:
  nuget.next:
    image: registry.token-ai.cn/ai-dotnet/nuget-next
    build:
      context: .
      dockerfile: src/NuGet.Next/Dockerfile
    container_name: nuget-next
    ports:
      - "5000:8080"
    volumes:
      - ./nuget:/app/data # 请注意手动创建data目录，负责在Linux下可能出现权限问题导致无法写入
    environment:
      - Database:Type=SqLite
      - Database:ConnectionString=Data Source=/app/data/nuget.db
      - Mirror:Enabled=true
      - Mirror:PackageSource=https://api.nuget.org/v3/index.json
      - RunMigrationsAtStartup:true # 是否在启动时运行迁移，如果是第一次启动请设置为true

```

```shell
docker-compose up -d
```