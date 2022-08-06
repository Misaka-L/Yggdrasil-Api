using System.Text.Json.Serialization;

namespace Yggdrasil.Models.YggdrasilApi {
    /// <summary>
    /// Yggdrasil 服务端元数据响应
    /// </summary>
    public class YggdrasilMetaDataResponse {
        /// <summary>
        /// 服务端元数据
        /// </summary>
        [JsonPropertyName("meta")]
        public YggdrasilMetaData Meta { get; set; }
        /// <summary>
        /// 材质域名白名单
        /// </summary>
        [JsonPropertyName("skinDomains")]
        public string[] SkinDomains { get; set; }
        /// <summary>
        /// 验证数字签名的公钥
        /// </summary>
        [JsonPropertyName("signaturePublickey")]
        public string SignaturePublickey { get; set; }
    }

    /// <summary>
    /// Yggdrasil 服务端链接
    /// </summary>
    public class YggdrasilLink {
        /// <summary>
        /// 验证服务器首页地址
        /// </summary>
        [JsonPropertyName("homepage")]
        public string Homepage { get; set; }
        /// <summary>
        /// 验证服务器注册地址
        /// </summary>
        [JsonPropertyName("register")]
        public string Register { get; set; }
    }

    /// <summary>
    /// Yggdrasil 服务端元数据
    /// </summary>
    public class YggdrasilMetaData {
        /// <summary>
        /// 服务器名称
        /// </summary>
        [JsonPropertyName("serverName")]
        public string ServerName { get; set; }
        /// <summary>
        /// 服务端实现的名称
        /// </summary>
        [JsonPropertyName("implementationName")]
        public string ImplementationName { get; set; }
        /// <summary>
        /// 服务端实现的版本
        /// </summary>
        [JsonPropertyName("implementationVersion")]
        public string ImplementationVersion { get; set; }

        /// <summary>
        /// 服务器网址
        /// </summary>
        [JsonPropertyName("links")]
        public YggdrasilLink Links { get; set; }

        /// <summary>
        /// 是否支持使用邮箱之外的凭证登录（如角色名登录）
        /// </summary>
        [JsonPropertyName("feature.non_email_login")]
        public bool IsSupportNoEmailLogin { get; set; } = false;
        /// <summary>
        /// 验证服务器是否支持旧式皮肤 API
        /// </summary>
        /// <remarks>即 GET <c>/skins/MinecraftSkins/{username}.png</c></remarks>
        [JsonPropertyName("feature.legacy_skin_api")]
        public bool IsSupportLegacySkinApi { get; set; } = false;
        /// <summary>
        /// 是否禁用 authlib-injector 的 Mojang 命名空间 (@mojang 后缀)
        /// </summary>
        [JsonPropertyName("feature.no_mojang_namespace")]
        public bool IsMojangNamespaceDisabled { get; set; } = false;
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
        [JsonPropertyName("feature.enable_mojang_anti_features")]
        public bool IsMojangAntiEnabled { get; set; } = false;
        /// <summary>
        /// 验证服务器是否支持 Minecraft 的消息签名密钥对功能 (由于该特性的重大漏洞，该 Yggdrasil Api 实现不会支持这个特性)
        /// </summary>
        [JsonPropertyName("feature.enable_profile_key")]
        public bool IsSupportProfileKey { get; set; } = false;
        /// <summary>
        /// 指示 authlib-injector 是否启用用户名验证功能
        /// </summary>
        /// <remarks>
        /// 是否启用玩家用户名检查, 若禁用, 则 authlib-injector 将关闭 Minecraft、BungeeCord 和 Paper 的用户名检查功能.
        /// 注意, 开启此功能将导致用户名包含非英文字符的玩家无法进入服务器.
        /// </remarks>
        [JsonPropertyName("feature.username_check")]
        public bool IsUsernameCheckEnabled { get; set; } = false;

    }
}
