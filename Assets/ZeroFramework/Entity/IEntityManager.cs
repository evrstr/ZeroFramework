using System;
using System.Collections.Generic;

namespace ZeroFramework.Entity
{
    /// <summary>
    /// 实体管理器接口。
    /// </summary>
    public interface IEntityManager
    {
        /// <summary>
        /// 获取实体数量。
        /// </summary>
        int EntityCount
        {
            get;
        }

        /// <summary>
        /// 获取实体组数量。
        /// </summary>
        int EntityGroupCount
        {
            get;
        }

    }
}
