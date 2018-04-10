﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Util.Applications;
using Util.Applications.Dtos;
using Util.Datas.Queries;
using Util.Exceptions;

namespace Util.Webs.Controllers {
    /// <summary>
    /// Crud控制器
    /// </summary>
    /// <typeparam name="TDto">数据传输对象类型</typeparam>
    /// <typeparam name="TQuery">查询参数类型</typeparam>
    public abstract class CrudControllerBase<TDto, TQuery> : QueryControllerBase<TDto, TQuery>
        where TQuery : IQueryParameter
        where TDto : DtoBase, new() {
        /// <summary>
        /// Crud服务
        /// </summary>
        private readonly ICrudService<TDto, TQuery> _service;

        /// <summary>
        /// 初始化Crud控制器
        /// </summary>
        /// <param name="service">Crud服务</param>
        protected CrudControllerBase( ICrudService<TDto, TQuery> service )
            : base( service ) {
            _service = service;
        }

        /// <summary>
        /// 创建，调用范例：POST URL(/api/customers) BODY({name:'a',age:2})
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync( [FromBody] TDto dto ) {
            if ( dto == null )
                return Fail( "请求参数不能为空" );
            CreateBefore( dto );
            await _service.CreateAsync( dto );
            var result = await _service.GetByIdAsync( dto.Id );
            return Success( result );
        }

        /// <summary>
        /// 创建前操作
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        protected virtual void CreateBefore( TDto dto ) {
        }

        /// <summary>
        /// 修改，调用范例：PUT URL(/api/customers/1 或 /api/customers) BODY({id:1,name:'a'})
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="dto">数据传输对象</param>
        [HttpPut("{id?}")]
        public virtual async Task<IActionResult> UpdateAsync( string id, [FromBody] TDto dto ) {
            if( dto == null )
                return Fail( "请求参数不能为空" );
            if( id.IsEmpty() && dto.Id.IsEmpty() )
                throw new Warning( "Id不能为空" );
            if ( dto.Id.IsEmpty() )
                dto.Id = id;
            UpdateBefore( dto );
            await _service.UpdateAsync( dto );
            var result = await _service.GetByIdAsync( dto.Id );
            return Success( result );
        }

        /// <summary>
        /// 修改前操作
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        protected virtual void UpdateBefore( TDto dto ) {
        }

        /// <summary>
        /// 保存，调用范例：POST URL(/api/customers/save) BODY({name:'a',age:2})
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        [HttpPost("save")]
        public virtual async Task<IActionResult> SaveAsync( [FromBody] TDto dto ) {
            if( dto == null )
                return Fail( "请求参数不能为空" );
            SaveBefore( dto );
            await _service.SaveAsync( dto );
            var result = await _service.GetByIdAsync( dto.Id );
            return Success( result );
        }

        /// <summary>
        /// 保存前操作
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        protected virtual void SaveBefore( TDto dto ) {
        }

        /// <summary>
        /// 删除，注意：该方法用于删除单个实体，批量删除请使用POST提交，否则可能失败
        /// </summary>
        /// <remarks>
        /// 调用范例:
        /// DELETE
        /// /api/customer/1
        /// </remarks>
        /// <param name="id">标识</param>
        [HttpDelete( "{id}" )]
        public virtual async Task<IActionResult> DeleteAsync( string id ) {
            await _service.DeleteAsync( id );
            return Success();
        }

        /// <summary>
        /// 批量删除，注意：body参数需要添加引号，"'1,2,3'"而不是"1,2,3"
        /// </summary>
        /// <remarks>
        /// 调用范例:
        /// POST   
        /// /api/customer/delete
        /// body: "'1,2,3'"
        /// </remarks>
        /// <param name="ids">标识列表，多个Id用逗号分隔，范例：1,2,3</param>
        [HttpPost( "delete" )]
        public virtual async Task<IActionResult> BatchDeleteAsync( [FromBody] string ids ) {
            await _service.DeleteAsync( ids );
            return Success();
        }
    }
}
