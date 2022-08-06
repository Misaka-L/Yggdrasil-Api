namespace Yggdrasil.Models.Options {
    public class YggdrasilOption {
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName { get; set; } = "Yggdrasil Api Server";
        /// <summary>
        /// 验证服务器首页地址
        /// </summary>
        public string HomepageLink { get; set; }
        /// <summary>
        /// 验证服务器注册地址
        /// </summary>
        public string RegisterLink { get; set; }
        /// <summary>
        /// 材质域名白名单
        /// </summary>
        public string[] SkinDomains { get; set; } = new string[0];
        
        /// <summary>
        /// 是否支持使用邮箱之外的凭证登录（如角色名登录）
        /// </summary>
        public bool IsSupportNoEmailLogin { get; set; } = true;
        /// <summary>
        /// 验证服务器是否支持旧式皮肤 API
        /// </summary>
        /// <remarks>即 GET <c>/skins/MinecraftSkins/{username}.png</c></remarks>
        public bool IsSupportLegacySkinApi { get; set; } = false;
        /// <summary>
        /// 是否禁用 authlib-injector 的 Mojang 命名空间 (@mojang 后缀)
        /// </summary>
        public bool IsMojangNamespaceDisabled { get; set; } = false;
        /// <summary>
        /// 指示 authlib-injector 是否启用用户名验证功能
        /// </summary>
        /// <remarks>
        /// 是否启用玩家用户名检查, 若禁用, 则 authlib-injector 将关闭 Minecraft、BungeeCord 和 Paper 的用户名检查功能.
        /// 注意, 开启此功能将导致用户名包含非英文字符的玩家无法进入服务器.
        /// </remarks>
        public bool IsUsernameCheckEnabled { get; set; } = false;
        /// <summary>
        /// 是否开启 Minecraft 的 anti-features
        /// </summary>
        /// <remarks>
        /// Minecraft 的 anti-feature 包括:
        ///     - Minecraft 服务器屏蔽列表
        ///     - 查询用户权限的接口, 涵盖以下项目:
        ///         * 聊天权限 (禁用后默认允许)
        ///         * 多人游戏权限 (禁用后默认允许)
        ///         * 领域权限(禁用后默认允许)
        ///         * 遥测(禁用后默认关闭)
        ///         * 冒犯性内容过滤(禁用后默认关闭)
        /// </remarks>
        public bool IsMojangAntiEnabled { get; set; } = false;
    }
}
