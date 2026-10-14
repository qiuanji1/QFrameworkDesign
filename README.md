# QFramework System Design

Unity C# 游戏业务框架，按数据、规则、交互和表现拆分代码。

## 分层

- `Architecture`：统一注册 Model、System、Utility 和事件系统。
- `Model`：共享业务数据与持久化入口。
- `System`：跨对象规则、状态协调和长流程逻辑。
- `Command`：一次会改变业务状态的动作。
- `Query`：只读查询和派生数据。
- `Event`：状态变化通知。
- `Controller`：Unity 输入、碰撞、动画和 UI 等表现层逻辑。
- `Utility`：存储、网络、SDK 等基础能力。
- `IOC`：接口注册与依赖获取。
- `Singleton`：基础单例实现。

## Unity 导入

将本仓库中的以下目录复制到 Unity 项目的 `Assets` 目录：

```text
Assets/FrameworkDesign/FrameWork
```

请同时保留 `.meta` 文件，避免 Unity 重新生成引用。

## 最小示例

```csharp
public class GameArchitecture : Architecture<GameArchitecture>
{
    protected override void Init()
    {
        RegisterModel<IPlayerModel>(new PlayerModel());
        RegisterSystem<IGameSystem>(new GameSystem());
    }
}
```

业务代码通过 Command、Query 和 Event 与 Model、System 交互，Controller 只处理 Unity 生命周期与表现。
