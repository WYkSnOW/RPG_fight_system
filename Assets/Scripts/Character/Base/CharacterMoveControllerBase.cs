using GGG.Tool;
using UnityEngine;

public class CharacterMoveControllerBase : MonoBehaviour
{
    // 动画控制器，用于控制角色动画状态的切换
    public Animator characterAnimator { get; private set; }

    // 角色控制器组件，用于控制角色的移动
    protected CharacterController characterController;

    // 重力相关参数
    [SerializeField, Header("重力")] private float characterGravity = -9; // 角色的重力加速度
    protected float fallOutdeltaTimer; // 角色在空中滞留的时间计时器
    protected float fallOutTimer = 0.2f; // 角色从空中落地的时间间隔
    [SerializeField] protected float maxVerticalSpeed = 20; // 最大垂直速度（自由下落时的最大速度）
    [SerializeField] protected float minVerticalSpeed = -3; // 最小垂直速度
    [SerializeField] protected float verticalSpeed; // 当前垂直速度
    protected Vector3 verticalVelocity; // 垂直方向上的速度向量

    // 地面检测相关参数
    [SerializeField, Header("地面检测")] private float GroundDetectionRadius; // 地面检测的半径（用于检测角色是否在地面上）
    [SerializeField] private float GroundDetectionOffset; // 地面检测的偏移量（用于调整检测起点的高度）
    [SerializeField] private LayerMask whatIsGround; // 用于识别地面的图层掩码
    [SerializeField] protected bool isOnGround; // 是否在地面上
    private Vector3 groundDetectionOrigin; // 地面检测的原点位置

    // 坡面检测相关参数
    [SerializeField, Header("坡面检测")] private float SlopDetectionLenth = 1; // 坡面检测的射线长度
    private ColliderHit groundHit; // 碰撞检测结果

    // 移动和闪避相关参数
    [Range(0.2f, 100), SerializeField, Header("移动位移倍率")] private float moveMult; // 移动时的位移倍率
    [Range(0.2f, 60), SerializeField, Header("闪避位移倍率")] private float dodgeMult; // 闪避时的位移倍率

    // 初始化组件
    protected virtual void Awake()
    {
        // 获取角色的动画控制器组件
        characterAnimator = GetComponent<Animator>();
        // 获取角色的控制器组件
        characterController = GetComponent<CharacterController>();
    }

    // 初始化参数
    protected virtual void Start()
    {
        // 初始化落地计时器
        fallOutdeltaTimer = fallOutTimer;
    }

    // 每帧更新角色状态
    protected virtual void Update()
    {
        GroundDetecion(); // 检测是否在地面上
        UpdateChracterGravity(); // 更新角色的重力状态
        UpDateVerticalVelocity(); // 更新垂直方向的速度
        UpDateVerticalVelocity(); // 再次更新垂直速度，可能有冗余
    }

    /// <summary>
    /// 在动画移动时更新角色的位移
    /// </summary>
    protected virtual void OnAnimatorMove()
    {
        // 开启RootMotion，让角色根据动画的位移移动
        characterAnimator.ApplyBuiltinRootMotion();
        // 更新角色的水平速度（即动画中的位移）
        UpdateCharacterVelocity(characterAnimator.deltaPosition);
    }

    /// <summary>
    /// 地面检测，判断角色是否站在地面上
    /// </summary>
    protected void GroundDetecion()
    {
        // 通过一个球体检测角色脚下是否接触地面
        groundDetectionOrigin = new Vector3(transform.position.x, transform.position.y - GroundDetectionOffset, transform.position.z);
        isOnGround = Physics.CheckSphere(groundDetectionOrigin, GroundDetectionRadius, whatIsGround, QueryTriggerInteraction.Ignore);
    }

    /// <summary>
    /// 更新角色的重力效果
    /// </summary>
    protected void UpdateChracterGravity()
    {
        if (isOnGround)
        {
            // 如果在地面上，重置落地计时器
            fallOutdeltaTimer = fallOutTimer;

            // 如果垂直速度小于0（即角色在下降），将其重置为-2（接触地面的微小速度）
            if (verticalSpeed < 0)
            {
                verticalSpeed = -2;
            }
        }
        else
        {
            // 如果不在地面上，开始减少落地计时器
            if (fallOutdeltaTimer >= 0)
            {
                fallOutdeltaTimer -= Time.deltaTime;
            }
            else
            {
                // 角色在空中，加速自由下落
                if (verticalSpeed < maxVerticalSpeed && verticalSpeed > minVerticalSpeed)
                {
                    verticalSpeed += characterGravity * Time.deltaTime;
                }
            }
        }
    }

    /// <summary>
    /// 更新垂直方向上的速度
    /// </summary>
    protected void UpDateVerticalVelocity()
    {
        // 设置垂直方向的速度
        verticalVelocity.Set(0, verticalSpeed, 0);
        // 根据当前的垂直速度移动角色
        characterController.Move(verticalVelocity * Time.deltaTime);
    }

    /// <summary>
    /// 坡面检测，用于调整角色在坡面上的移动速度
    /// </summary>
    /// <param name="characterVelosity">角色的当前速度</param>
    /// <returns>调整后的速度</returns>
    protected Vector3 ResetVelocityOnSlop(Vector3 characterVelosity)
    {
        // 通过射线检测角色脚下是否接触到坡面
        if (Physics.Raycast(transform.position, Vector3.down, out var groundHit, SlopDetectionLenth, whatIsGround))
        {
            float newAngle = Vector3.Dot(Vector3.up, groundHit.normal);
            // 如果不在天花板或角色正在下坡，将角色的速度投影到坡面上
            if (newAngle != -1 && verticalSpeed <= 0)
            {
                return Vector3.ProjectOnPlane(characterVelosity, groundHit.normal);
            }
        }
        return characterVelosity;
    }

    /// <summary>
    /// 更新角色的水平速度
    /// </summary>
    /// <param name="movement">角色的移动方向</param>
    protected virtual void UpdateCharacterVelocity(Vector3 movement)
    {
        // 根据坡面的角度调整角色的速度
        Vector3 dir = ResetVelocityOnSlop(movement);

        // 如果动画状态是“移动”，则根据移动倍率移动角色
        if (characterAnimator.AnimationAtTag("Movement"))
        {
            characterController.Move(dir * Time.deltaTime * moveMult);
        }
        // 如果动画状态是“闪避”，则根据闪避倍率移动角色
        else if (characterAnimator.AnimationAtTag("Dodge"))
        {
            characterController.Move(dir * Time.deltaTime * dodgeMult);
        }
    }

    /// <summary>
    /// 绘制地面检测的Gizmos，用于在编辑器中可视化地面检测区域
    /// </summary>
    private void OnDrawGizmos()
    {
        // 如果角色在地面上，显示绿色的检测区域
        if (isOnGround)
        {
            Gizmos.color = Color.green;
        }
        // 如果角色不在地面上，显示红色的检测区域
        else
        {
            Gizmos.color = Color.red;
        }

        // 在角色的地面检测位置绘制一个球体
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - GroundDetectionOffset, transform.position.z), GroundDetectionRadius);
    }
}
