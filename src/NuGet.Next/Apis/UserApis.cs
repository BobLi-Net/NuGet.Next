using Gnarly.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Next.Core;
using NuGet.Next.Core.Exceptions;
using NuGet.Next.Core.Infrastructure;
using NuGet.Next.Protocol.Models;

namespace NuGet.Next.Service;

public class UserApis(IContext context, IUserContext userContext) : IScopeDependency
{
    public async Task<OkResponse> CreateAsync(UserInput input)
    {
        if (userContext.Role != RoleConstant.Admin)
        {
            throw new ForbiddenException("无权限");
        }

        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            FullName = input.FullName,
            Username = input.Username,
            Email = input.Email,
            Avatar = input.Avatar,
            Role = input.Role
        };
        user.SetPassword(input.Password);

        await context.Users.AddAsync(user);

        await context.SaveChangesAsync(new CancellationToken());

        return OkResponse.Ok("创建成功");
    }

    public async Task<PageResponse<User>> GetAsync(string? keyword, int page, int pageSize)
    {
        if (userContext.Role != RoleConstant.Admin)
        {
            throw new ForbiddenException("无权限");
        }

        page = Math.Max(page, 1);

        pageSize = Math.Min(pageSize, 1000);

        var query = context.Users
            .Where(x => string.IsNullOrEmpty(keyword) || x.Username.Contains(keyword) || x.FullName.Contains(keyword))
            .OrderBy(x => x.Username);

        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PageResponse<User>(total, items);
    }

    public async Task<OkResponse> DeleteAsync(string id)
    {
        if (userContext.Role != RoleConstant.Admin)
        {
            throw new ForbiddenException("无权限");
        }

        var result = await context.Users.Where(x => x.Id == id && x.Role == RoleConstant.User).ExecuteDeleteAsync();

        if (result == 0)
        {
            return new OkResponse(false, "删除失败");
        }

        return OkResponse.Ok("删除成功");
    }

    /// <summary>
    /// 编辑用户
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <exception cref="ForbiddenException"></exception>
    public async Task UpdateAsync(string id, UserInput input)
    {
        if (userContext.Role != RoleConstant.Admin)
        {
            throw new ForbiddenException("无权限");
        }

        await context.Users.Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x.SetProperty(i => i.FullName, input.FullName)
                .SetProperty(i => i.Username, input.Username)
                .SetProperty(i => i.Email, input.Email)
                .SetProperty(i => i.Avatar, input.Avatar)
                .SetProperty(i => i.Role, input.Role));
    }

    /// <summary>
    /// 更新密码
    /// </summary>
    public async Task<OkResponse> UpdatePasswordAsync(UpdatePasswordInput input)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userContext.UserId);

        if (user == null)
        {
            throw new NotFoundException("用户不存在");
        }

        if (!user.VerifyPassword(input.CurrentPassword))
        {
            throw new BadRequestException("旧密码错误");
        }

        user.SetPassword(input.NewPassword);

        context.Users.Update(user);

        await context.SaveChangesAsync(new CancellationToken());

        return OkResponse.Ok("修改成功");
    }
}