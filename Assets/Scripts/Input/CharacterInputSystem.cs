using UnityEngine;
using HuHu;
using UnityEngine.InputSystem;

// CharacterInputSystem 类继承自 Singleton，确保在整个游戏中只有一个实例。
// 该类用于管理并封装玩家的输入，通过 Unity 的新输入系统来处理各种玩家操作。
public class CharacterInputSystem : Singleton<CharacterInputSystem>
{
    // CharacterInput 是通过 Input System 生成的输入动作类，包含了所有定义好的输入动作。
    public CharacterInput inputActions;

    // 重写 Awake 方法，确保 inputActions 已被实例化。
    // Singleton 的 Awake 会确保该类的唯一实例。
    protected override void Awake()
    {
        base.Awake();

        // 如果 inputActions 为空，创建新的 CharacterInput 实例（该实例包含输入配置）。
        if (inputActions == null)
            inputActions = new CharacterInput();
    }

    // 当该组件启用时，激活输入系统。
    private void OnEnable()
    {
        inputActions?.Enable(); // 启用所有定义的输入动作
    }

    // 当该组件禁用时，禁用输入系统。
    private void OnDisable()
    {
        inputActions?.Disable(); // 禁用所有输入动作，避免不必要的输入处理
    }

    // 属性封装：以下部分封装了具体的输入动作，提供给外部类调用。

    // 读取玩家的移动输入，返回一个 Vector2 类型（通常为 WASD 或手柄左摇杆）。
    public Vector2 PlayerMove
    {
        get => inputActions.Player.Movement.ReadValue<Vector2>(); // 获取玩家的移动向量
    }

    // 读取玩家的相机控制输入，返回一个 Vector2 类型（通常为鼠标移动或手柄右摇杆）。
    public Vector2 CameraLook
    {
        get => inputActions.Player.CameraLook.ReadValue<Vector2>(); // 获取玩家的相机视角输入
    }

    // 检测玩家是否按下了跑步键（通常为 Shift 键或手柄某个按钮）。
    public bool Run
    {
        get => inputActions.Player.Run.triggered; // `triggered` 表示该按键是否刚刚被按下
    }

    // 检测玩家是否正在持续跑步（按住不松开时，`phase` 会保持在 Performed 状态）。
    public bool Run_Continue
    {
        get => inputActions.Player.Run.phase == InputActionPhase.Performed; // 检测按键是否处于执行状态
    }

    // 检测玩家是否触发了跳跃（通常为空格键或手柄上的跳跃按钮）。
    public bool Jump
    {
        get => inputActions.Player.Jump.triggered; // 跳跃按键是否被按下
    }

    // 检测玩家是否在蹲伏（按下蹲伏键时，`phase` 进入 Performed 状态）。
    public bool Crouch
    {
        get => inputActions.Player.Crouch.phase == InputActionPhase.Performed; // 是否处于蹲伏状态
    }

    // 检测玩家是否进行了左侧攻击（通常为鼠标左键或手柄某个攻击按钮）。
    public bool L_Atk
    {
        get => inputActions.Player.L_AtK.triggered; // 左攻击按键是否触发
    }

    // 检测玩家是否进行了右侧攻击（通常为鼠标右键或手柄另一个攻击按钮）。
    public bool R_Atk
    {
        get => inputActions.Player.R_Atk.triggered; // 右攻击按键是否触发
    }

    // 检测玩家是否在进行瞄准操作（右攻击按钮是否处于执行状态）。
    public bool Aim
    {
        get => inputActions.Player.R_Atk.phase == InputActionPhase.Performed; // 瞄准状态（右攻击按住不放）
    }

    // 检测玩家是否在持续进行左侧攻击连击（通常为连击按键）。
    public bool L_Atk_Continue
    {
        get => inputActions.Player.Continue_Atk.phase == InputActionPhase.Performed; // 连击按键是否处于执行状态
    }

    // 检测玩家是否执行某个特殊动作（通常为执行按键，如 F 键）。
    public bool Execute
    {
        get => inputActions.Player.Execute.triggered; // 特殊执行动作是否被触发
    }

    // 检测玩家是否进行了角色切换操作（通常为切换角色的按键）。
    public bool SwitchCharacter
    {
        get => inputActions.Player.SwitchCharacter.triggered; // 切换角色按键是否被触发
    }

    // 检测玩家是否使用了技能（通常为技能按键，如 Q 或 E 键）。
    public bool Skill
    {
        get => inputActions.Player.Skill.triggered; // 技能按键是否被触发
    }

    // 检测技能是否完成或结束（技能结束的按键）。
    public bool FinishSkill
    {
        get => inputActions.Player.FinishSkill.triggered; // 技能结束按键是否被触发
    }

    // 检测玩家是否切换为走路模式（通常为走路/跑步切换按键）。
    public bool Walk
    {
        get => inputActions.Player.Walk.triggered; // 切换走路模式的按键是否被触发
    }
}
