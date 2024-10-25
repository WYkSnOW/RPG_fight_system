using UnityEngine;
using static OnAnimationTranslation;

namespace ZZZ
{
    // Player类继承自 CharacterMoveControllerBase，用于管理玩家角色的状态、连招、音效等逻辑
    [RequireComponent(typeof(Animator), typeof(CharacterController))]
    public class Player : CharacterMoveControllerBase
    {
        [SerializeField] public CharacterNameList characterName; // 玩家角色的名称
        [SerializeField] public string currentMovementState; // 当前的移动状态
        [SerializeField] public string currentComboState; // 当前的连招状态
        [SerializeField] public Transform enemy; // 当前锁定的敌人
        [SerializeField] public PlayerSO playerSO; // 玩家数据
        [SerializeField] public PlayerCameraUtility playerCameraUtility; // 玩家相机工具类

        public PlayerMovementStateMachine movementStateMachine { get; private set; } // 玩家移动状态机
        public PlayerComboStateMachine comboStateMachine { get; private set; } // 玩家连招状态机
        public new Transform camera { get; private set; } // 主相机的Transform引用

        // 是否允许在切换角色时进行冲刺
        private bool canSprintOnSwitch;
        public bool CanSprintOnSwitch
        {
            get { return canSprintOnSwitch; }
            set
            {
                if (value != canSprintOnSwitch)
                {
                    canSprintOnSwitch = value;
                }
            }
        }

        // 初始化组件
        protected override void Awake()
        {
            base.Awake();

            camera = Camera.main.transform; // 获取主相机的 Transform
            movementStateMachine = new PlayerMovementStateMachine(this); // 初始化移动状态机
            comboStateMachine = new PlayerComboStateMachine(this); // 初始化连招状态机
            playerCameraUtility.Init(); // 初始化相机工具
        }

        // 初始化状态
        protected override void Start()
        {
            base.Start();

            // 如果角色名称是当前切换角色，进入等待状态；否则进入切换退出状态
            if (characterName == SwitchCharacter.MainInstance.newCharacterName.Value)
            {
                movementStateMachine.ChangeState(movementStateMachine.idlingState);
            }
            else
            {
                movementStateMachine.ChangeState(movementStateMachine.onSwitchOutState);
            }

            comboStateMachine.ChangeState(comboStateMachine.NullState); // 设置连招状态为Null

            // 注册黑板信息
            Player player = GetComponent<Player>();
            GameBlackboard.MainInstance.SetGameData<Player>(characterName.ToString(), player);
        }

        // 更新玩家状态
        protected override void Update()
        {
            base.Update();

            // 如果当前角色是切换的目标，处理输入并更新状态机
            if (characterName == SwitchCharacter.MainInstance.newCharacterName.Value)
            {
                movementStateMachine.HandInput(); // 处理输入
                movementStateMachine.Update(); // 更新移动状态机
                comboStateMachine.Update(); // 更新连招状态机
            }
        }

        #region 动画触发事件
        /// <summary>
        /// 根据动画状态调用状态机中的相应方法
        /// </summary>
        public void OnAnimationTranslateEvent(OnEnterAnimationPlayerState playerState)
        {
            switch (playerState)
            {
                case OnEnterAnimationPlayerState.TurnBack:
                    movementStateMachine.OnAnimationTranslateEvent(movementStateMachine.returnRunState);
                    break;
                case OnEnterAnimationPlayerState.Dash:
                    movementStateMachine.OnAnimationTranslateEvent(movementStateMachine.dashingState);
                    comboStateMachine.OnAnimationTranslateEvent(comboStateMachine.NullState);
                    break;
                case OnEnterAnimationPlayerState.Switch:
                    movementStateMachine.OnAnimationTranslateEvent(movementStateMachine.onSwitchState);
                    break;
                case OnEnterAnimationPlayerState.SwitchOut:
                    movementStateMachine.OnAnimationTranslateEvent(movementStateMachine.onSwitchOutState);
                    comboStateMachine.OnAnimationTranslateEvent(comboStateMachine.NullState);
                    break;
                case OnEnterAnimationPlayerState.ATK:
                    comboStateMachine.OnAnimationTranslateEvent(comboStateMachine.ATKIngState);
                    movementStateMachine.OnAnimationTranslateEvent(movementStateMachine.playerMovementNullState);
                    break;
                case OnEnterAnimationPlayerState.DashBack:
                    movementStateMachine.OnAnimationTranslateEvent(movementStateMachine.dashBackingState);
                    comboStateMachine.OnAnimationTranslateEvent(comboStateMachine.NullState);
                    break;
            }
        }

        /// <summary>
        /// 动画退出时调用的方法
        /// </summary>
        public void OnAnimationExitEvent()
        {
            movementStateMachine.OnAnimationExitEvent();
            comboStateMachine.OnAnimationExitEvent();
        }
        #endregion

        #region 状态变更事件
        // 注册事件
        public void OnEnable()
        {
            movementStateMachine.currentState.OnValueChanged += MovementStateChanged;
            comboStateMachine.currentState.OnValueChanged += ComboStateChanged;
            GameBlackboard.MainInstance.enemy.OnValueChanged += EnemyChanged;
        }

        // 取消事件注册
        public void OnDisable()
        {
            movementStateMachine.currentState.OnValueChanged -= MovementStateChanged;
            comboStateMachine.currentState.OnValueChanged -= ComboStateChanged;
            GameBlackboard.MainInstance.enemy.OnValueChanged -= EnemyChanged;
        }

        /// <summary>
        /// 移动状态变更
        /// </summary>
        public void MovementStateChanged(IState currentState)
        {
            currentMovementState = currentState.GetType().Name;
        }

        /// <summary>
        /// 连招状态变更
        /// </summary>
        private void ComboStateChanged(IState state)
        {
            currentComboState = state.GetType().Name;
        }

        /// <summary>
        /// 敌人变更
        /// </summary>
        private void EnemyChanged(Transform transform)
        {
            enemy = transform;
        }
        #endregion

        #region 连招动画事件
        // 启用预输入
        public void EnablePreInput()
        {
            comboStateMachine.ATKIngState.EnablePreInput();
        }

        // 取消攻击冷却时间
        public void CancelAttackColdTime()
        {
            comboStateMachine.ATKIngState.CancelAttackColdTime();
        }

        // 禁用连击
        public void DisableLinkCombo()
        {
            comboStateMachine.ATKIngState.DisableLinkCombo();
        }

        // 启用移动中断
        public void EnableMoveInterrupt()
        {
            comboStateMachine.ATKIngState.EnableMoveInterrupt();
        }

        // 执行攻击
        public void ATK()
        {
            comboStateMachine.ATKIngState.ATK();
        }
        #endregion

        #region 动画事件音效
        /// <summary>
        /// 播放视觉特效
        /// </summary>
        public void PlayVFX(string name)
        {
            VFX_PoolManager.MainInstance.TryGetVFX(characterName, name);
        }

        // 播放脚步声
        public void PlayFootSound()
        {
            SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.FOOT, transform.position, Quaternion.identity);
        }

        // 播放后退脚步声
        public void PlayFootBackSound()
        {
            SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.FOOTBACK, transform.position, Quaternion.identity);
        }

        // 播放武器收回音效
        public void PlayWeaponBackSound()
        {
            SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.WeaponBack, characterName.ToString(), transform.position);
        }

        // 播放武器结束音效
        public void PlayWeaponEndSound()
        {
            SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.WeaponEnd, characterName.ToString(), transform.position);
        }
        #endregion

        // 播放闪避音效
        public void PlayDodgeSound()
        {
            SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.DodgeSound, transform.position, Quaternion.identity);
        }

        // 播放角色切换风声
        public void PlaySwitchWindSound()
        {
            SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.SwitchInWindSound, transform.position, Quaternion.identity);
        }

        // 播放角色切换语音
        public void PlaySwitchInVoice()
        {
            SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.SwitchInVoice, characterName.ToString(), transform.position);
        }
    }
}
