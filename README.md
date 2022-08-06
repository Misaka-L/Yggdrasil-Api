# Yggdrasil-Api
使用 ASP.NET 实现的 Yggdrasil Api 层，基于 [Yggdrasil 服务端技术规范](https://github.com/yushijinhun/authlib-injector/wiki/Yggdrasil-%E6%9C%8D%E5%8A%A1%E7%AB%AF%E6%8A%80%E6%9C%AF%E8%A7%84%E8%8C%83) 实现。
## 如何使用
1. clone 或者 fork 项目
2. 实现 `IYggdrasilConnecterService` 接口对接现有的后端
3. 修改 `appsettings.json` 的 `Yggdrasil` 字段配置 Yggdrasil Api 元数据和 `Jwt` 字段配置 Jwt
