using UnityEngine;
using static OnAnimationTranslation;

namespace ZZZ
{
    // Player��̳��� CharacterMoveControllerBase�����ڹ�����ҽ�ɫ��״̬�����С���Ч���߼�
    [RequireComponent(typeof(Animator), typeof(CharacterController))]
    public class Player : CharacterMoveControllerBase
    {
        [SerializeField] public CharacterNameList characterName; // ��ҽ�ɫ������
        [SerializeField] public string currentMovementState; // ��ǰ���ƶ�״̬
        [SerializeField] public string currentComboState; // ��ǰ������״̬
        [SerializeField] public Transform enemy; // ��ǰ�����ĵ���
        [SerializeField] public PlayerSO playerSO; // �������
        [SerializeField] public PlayerCameraUtility playerCameraUtility; // ������������

        public PlayerMovementStateMachine movementStateMachine { get; private set; } // ����ƶ�״̬��
        public PlayerComboStateMachine comboStateMachine { get; private set; } // �������״̬��
        public new Transform camera { get; private set; } // �������Transform����

        // �Ƿ��������л���ɫʱ���г��
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

        // ��ʼ�����
        protected override void Awake()
        {
            base.Awake();

            camera = Camera.main.transform; // ��ȡ������� Transform
            movementStateMachine = new PlayerMovementStateMachine(this); // ��ʼ���ƶ�״̬��
            comboStateMachine = new PlayerComboStateMachine(this); // ��ʼ������״̬��
            playerCameraUtility.Init(); // ��ʼ���������
        }

        // ��ʼ��״̬
        protected override void Start()
        {
            base.Start();

            // �����ɫ�����ǵ�ǰ�л���ɫ������ȴ�״̬����������л��˳�״̬
            if (characterName == SwitchCharacter.MainInstance.newCharacterName.Value)
            {
                movementStateMachine.ChangeState(movementStateMachine.idlingState);
            }
            else
            {
                movementStateMachine.ChangeState(movementStateMachine.onSwitchOutState);
            }

            comboStateMachine.ChangeState(comboStateMachine.NullState); // ��������״̬ΪNull

            // ע��ڰ���Ϣ
            Player player = GetComponent<Player>();
            GameBlackboard.MainInstance.SetGameData<Player>(characterName.ToString(), player);
        }

        // �������״̬
        protected override void Update()
        {
            base.Update();

            // �����ǰ��ɫ���л���Ŀ�꣬�������벢����״̬��
            if (characterName == SwitchCharacter.MainInstance.newCharacterName.Value)
            {
                movementStateMachine.HandInput(); // ��������
                movementStateMachine.Update(); // �����ƶ�״̬��
                comboStateMachine.Update(); // ��������״̬��
            }
        }

        #region ���������¼�
        /// <summary>
        /// ���ݶ���״̬����״̬���е���Ӧ����
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
        /// �����˳�ʱ���õķ���
        /// </summary>
        public void OnAnimationExitEvent()
        {
            movementStateMachine.OnAnimationExitEvent();
            comboStateMachine.OnAnimationExitEvent();
        }
        #endregion

        #region ״̬����¼�
        // ע���¼�
        public void OnEnable()
        {
            movementStateMachine.currentState.OnValueChanged += MovementStateChanged;
            comboStateMachine.currentState.OnValueChanged += ComboStateChanged;
            GameBlackboard.MainInstance.enemy.OnValueChanged += EnemyChanged;
        }

        // ȡ���¼�ע��
        public void OnDisable()
        {
            movementStateMachine.currentState.OnValueChanged -= MovementStateChanged;
            comboStateMachine.currentState.OnValueChanged -= ComboStateChanged;
            GameBlackboard.MainInstance.enemy.OnValueChanged -= EnemyChanged;
        }

        /// <summary>
        /// �ƶ�״̬���
        /// </summary>
        public void MovementStateChanged(IState currentState)
        {
            currentMovementState = currentState.GetType().Name;
        }

        /// <summary>
        /// ����״̬���
        /// </summary>
        private void ComboStateChanged(IState state)
        {
            currentComboState = state.GetType().Name;
        }

        /// <summary>
        /// ���˱��
        /// </summary>
        private void EnemyChanged(Transform transform)
        {
            enemy = transform;
        }
        #endregion

        #region ���ж����¼�
        // ����Ԥ����
        public void EnablePreInput()
        {
            comboStateMachine.ATKIngState.EnablePreInput();
        }

        // ȡ��������ȴʱ��
        public void CancelAttackColdTime()
        {
            comboStateMachine.ATKIngState.CancelAttackColdTime();
        }

        // ��������
        public void DisableLinkCombo()
        {
            comboStateMachine.ATKIngState.DisableLinkCombo();
        }

        // �����ƶ��ж�
        public void EnableMoveInterrupt()
        {
            comboStateMachine.ATKIngState.EnableMoveInterrupt();
        }

        // ִ�й���
        public void ATK()
        {
            comboStateMachine.ATKIngState.ATK();
        }
        #endregion

        #region �����¼���Ч
        /// <summary>
        /// �����Ӿ���Ч
        /// </summary>
        public void PlayVFX(string name)
        {
            VFX_PoolManager.MainInstance.TryGetVFX(characterName, name);
        }

        // ���ŽŲ���
        public void PlayFootSound()
        {
            SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.FOOT, transform.position, Quaternion.identity);
        }

        // ���ź��˽Ų���
        public void PlayFootBackSound()
        {
            SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.FOOTBACK, transform.position, Quaternion.identity);
        }

        // ���������ջ���Ч
        public void PlayWeaponBackSound()
        {
            SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.WeaponBack, characterName.ToString(), transform.position);
        }

        // ��������������Ч
        public void PlayWeaponEndSound()
        {
            SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.WeaponEnd, characterName.ToString(), transform.position);
        }
        #endregion

        // ����������Ч
        public void PlayDodgeSound()
        {
            SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.DodgeSound, transform.position, Quaternion.identity);
        }

        // ���Ž�ɫ�л�����
        public void PlaySwitchWindSound()
        {
            SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.SwitchInWindSound, transform.position, Quaternion.identity);
        }

        // ���Ž�ɫ�л�����
        public void PlaySwitchInVoice()
        {
            SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.SwitchInVoice, characterName.ToString(), transform.position);
        }
    }
}
