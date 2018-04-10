﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using Util.Ui.Material.Buttons.Configs;
using Util.Ui.Material.Buttons.Renders;
using Util.Ui.Material.Enums;
using Util.Ui.Renders;
using Util.Ui.TagHelpers;

namespace Util.Ui.Material.Buttons.TagHelpers {
    /// <summary>
    /// 按钮
    /// </summary>
    [HtmlTargetElement("util-button")]
    public class ButtonTagHelper : TagHelperBase {
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 文本属性绑定
        /// </summary>
        public string BindText { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public ButtonType Type { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public ButtonStyle Styles { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// 禁用
        /// </summary>
        public string Disabled { get; set; }
        /// <summary>
        /// 提示
        /// </summary>
        public string Tooltip { get; set; }
        /// <summary>
        /// 单击事件处理函数,范例：handle()
        /// </summary>
        public string OnClick { get; set; }
        /// <summary>
        /// 菜单标识
        /// </summary>
        public string MenuId { get; set; }
        /// <summary>
        /// 关闭弹出层，设置返回消息
        /// </summary>
        public string CloseDialog { get; set; }

        /// <summary>
        /// 获取渲染器
        /// </summary>
        /// <param name="context">上下文</param>
        protected override IRender GetRender( Context context ) {
            var config = new ButtonConfig( context );
            if( config.UseButtonRender() )
                return new ButtonRender( config );
            return new ButtonWrapperRender( config );
        }
    }
}
