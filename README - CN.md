
### CharacterController
- 使用 `CharacterController` 的 `Move` 方法实现角色的移动。
- 基于 `OnAnimationMove` 方法，在动画刷新频率的驱动下实现根运动移动。

### OnAnimationMove
- 通过 `OnAnimationMove` 提供的动画更新频率，利用根运动实现平滑的移动效果。

### Animator 和 状态机设计
- 动画过渡和播放逻辑基于 Unity 内置的 `Animator` 组件及有限状态机。
- 所有动画状态都继承 `IState` 接口，每个状态机仅允许刷新一种状态。
- 分为 `MovementStateMachine` 和 `ComboStateMachine`，并通过基类 `StateMachine` 实现状态切换。
- 子类实现各个状态的缓存与初始化，内部包含输入事件注册/注销、动画播放、数据初始化/变更、状态退出与过渡等逻辑。

### ComboState（连招系统）
- 控制攻击键的输入事件，管理预输入动画事件、必要时间动画事件、打断连招及衔接动画等。

### CharacterHealth
- 注册并处理伤害事件，包括伤害来源的记录、受伤动画、格挡动画、以及受击音效与特效的生成。

### 第三人称镜头和技能镜头控制
- 使用 Cinemachine 的 State-Driven Camera、Dolly 轨道相机和 Timeline 实现动态 Dolly 数据的更新。
- 通过 Cinemachine 实现移动状态的水平居中效果。

### ScriptableObject 和 ReusableData
- `ScriptableObject` 存储角色所有静态数据。
- `ReusableData` 存储可变数据，并通过 BindableProperty 实现变更事件绑定。

### TimerManager（计时器工具）
- 提供基于 `ScaleTime` 与 `UnScaleTime` 的倒计时触发函数。
- 提供销毁正在计时的委托的 API。

### GameEventManager（事件工具）
- 提供事件注册、触发、销毁 API，通过 `string` 类型映射委托事件。

### SFX_PoolManager（音效池管理）
- 提供基于 `SoundStyle` 枚举类型映射音效激活的 API。
- 使用对象池设计模式，自动化管理音效预制体的生成与配置。

### VFXManager（特效管理）
- 控制全场特效播放速度，并提供场景特效播放控制的 API。

### CameraHitFeel（镜头反馈）
- 控制角色慢动作、顿帧和震屏效果，提供改变 `ScaleTime`、动画播放速度、全场特效播放速度的 API。

### SwitchCharacter（角色切换）
- 实现角色切换、切换动画、相机目标点变更等逻辑。
- 提供角色切换与连携的 API。

### GameBlackboard（角色数据共享）
- 实现角色之间的数据共享与敌人的同步。
- 提供 `GetGameData(string)` 和 `GetEnemies()` API，用于获取角色与敌人相关数据。
