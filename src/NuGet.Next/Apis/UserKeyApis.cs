using Gnarly.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Next.Core;
using NuGet.Next.Protocol.Models;

namespace NuGet.Next.Service;

public class UserKeyApis(IContext context, IUserContext userContext) : IScopeDependency
{
    /// <summary>
    /// 创建密钥
    /// </summary>
    /// <returns></returns>
    public async Task<OkResponse> CreateAsync()
    {
        if (!userContext.IsAuthenticated)
        {
            throw new UnauthorizedAccessException();
        }

        if (await context.UserKeys.CountAsync(x => x.UserId == userContext.UserId) >= 10)
        {
            return new OkResponse(false, "最多只能创建10个Key");
        }

        var key = new UserKey(userContext.UserId);

        await context.UserKeys.AddAsync(key);

        await context.SaveChangesAsync(new CancellationToken());

        return OkResponse.Ok("创建成功");
    }

    /// <summary>
    /// 获取Key列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<UserKey>> GetListAsync()
    {
        if (!userContext.IsAuthenticated)
        {
            throw new UnauthorizedAccessException();
        }

        return await context.UserKeys
            .Where(x => x.UserId == userContext.UserId)
            .ToListAsync();
    }

    /// <summary>
    /// 删除Key
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<OkResponse> DeleteAsync(string id)
    {
        if (!userContext.IsAuthenticated)
        {
            throw new UnauthorizedAccessException();
        }

        var key = await context.UserKeys.FirstOrDefaultAsync(x => x.Id == id);
        if (key == null)
        {
            return new OkResponse(false, "Key不存在");
        }

        if (key.UserId != userContext.UserId)
        {
            return new OkResponse(false, "无权删除");
        }

        context.UserKeys.Remove(key);

        await context.SaveChangesAsync(new CancellationToken());

        return OkResponse.Ok("删除成功");
    }

    /// <summary>
    /// 启用/禁用Key
    /// </summary>
    /// <returns></returns>
    public async Task<OkResponse> EnableAsync(string id)
    {
        if (!userContext.IsAuthenticated)
        {
            throw new UnauthorizedAccessException();
        }

        var key = await context.UserKeys.FirstOrDefaultAsync(x => x.Id == id);
        if (key == null)
        {
            return new OkResponse(false, "Key不存在");
        }

        if (key.UserId != userContext.UserId)
        {
            return new OkResponse(false, "无权操作");
        }

        await context.UserKeys.Where(x => x.UserId == userContext.UserId && x.Id == id)
            .ExecuteUpdateAsync(x => x.SetProperty(i => i.Enabled, a => !a.Enabled));

        return OkResponse.Ok("操作成功");
    }
}