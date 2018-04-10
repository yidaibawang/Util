﻿using Util.Domains.Services;
using Util.Security.Identity.Models;

namespace Util.Security.Identity.Services.Abstractions {
    /// <summary>
    /// 角色服务
    /// </summary>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TKey">角色标识类型</typeparam>
    /// <typeparam name="TParentId">角色父标识类型</typeparam>
    public interface IRoleManager<TRole, in TKey, TParentId> : IDomainService where TRole : Role<TRole, TKey, TParentId> {
    }
}
