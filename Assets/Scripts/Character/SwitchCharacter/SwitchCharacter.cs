using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using HuHu;

namespace ZZZ
{
    public class SwitchCharacter : Singleton<SwitchCharacter>
    {
        /// <summary>
        /// SwitchCharacterInfo 类用于存储每个角色的信息，包括角色名称、相机目标位置、跟随位置等。
        /// </summary>
        [System.Serializable]
        public class SwitchCharacterInfo
        {
            public CharacterNameList characterName; // 角色名称
            public Transform aimAtPos; // 角色的相机瞄准位置
            public Transform followAtPos; // 角色的相机跟随位置
            public GameObject character; // 角色对象
            public float spawnDistance; // 角色出生时距离上一角色的距离
            [HideInInspector]
            public Animator animator; // 角色的动画控制器
        }

        // 角色信息列表
        [SerializeField] private List<SwitchCharacterInfo> switchCharacterInfos = new List<SwitchCharacterInfo>();

        // 等待切换的角色列表
        [SerializeField] public List<CharacterNameList> waitingCharacterList = new List<CharacterNameList>();

        // 是否允许切换角色的输入
        private bool canSwitchInput;

        // 切换角色的缓冲时间
        [SerializeField, Header("切换角色的缓冲时间")] private float applyNextSwitchTime;

        // 退场角色的存留时间
        [SerializeField, Header("退场角色的存留时间")] private float switchOutCharacterTime;

        // 当前角色的名称
        [SerializeField] public CharacterNameList currentCharacterName;

        // 新角色的名称（通过绑定属性监控变化）
        [SerializeField] public BindableProperty<CharacterNameList> newCharacterName = new BindableProperty<CharacterNameList>();

        // 当前角色的游戏对象
        [SerializeField] private GameObject currentCharacter;

        // 新角色的游戏对象
        [SerializeField] public GameObject newCharacter;

        // 相机数组，包含多个虚拟相机
        [SerializeField] private CinemachineVirtualCamera[] virtualCameras;

        // 当前角色在列表中的索引
        private int characterIndex = 0;

        /// <summary>
        /// 初始化角色和相机
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            InitCharacter(); // 初始化角色
            InitCamera(); // 初始化相机
        }

        /// <summary>
        /// 订阅 newCharacterName 的值改变事件
        /// </summary>
        private void OnEnable()
        {
            newCharacterName.OnValueChanged += OnSetWaitingCharacterList;
        }

        /// <summary>
        /// 取消订阅 newCharacterName 的值改变事件
        /// </summary>
        private void OnDisable()
        {
            newCharacterName.OnValueChanged -= OnSetWaitingCharacterList;
        }

        /// <summary>
        /// 更新等待切换的角色列表
        /// </summary>
        private void OnSetWaitingCharacterList(CharacterNameList list)
        {
            // 如果新角色在等待列表中，移除该角色
            if (waitingCharacterList.Contains(list))
            {
                waitingCharacterList.Remove(list);
            }
            else
            {
                Debug.LogWarning(list + " 这个人物不在等候列表无法移除");
            }

            // 避免当前角色和新角色相同，确保旧角色加入等待列表
            if (currentCharacterName != list)
            {
                if (!waitingCharacterList.Contains(currentCharacterName))
                {
                    waitingCharacterList.Add(currentCharacterName);
                }
            }
        }

        /// <summary>
        /// 初始化相机数组
        /// </summary>
        private void InitCamera()
        {
            virtualCameras = FindObjectsOfType<CinemachineVirtualCamera>(); // 查找场景中的虚拟相机
        }

        /// <summary>
        /// 初始化角色的动画控制器和等待列表
        /// </summary>
        private void InitCharacter()
        {
            for (int i = 0; i < switchCharacterInfos.Count; i++)
            {
                switchCharacterInfos[i].animator = switchCharacterInfos[i].character.transform.GetComponent<Animator>(); // 获取角色的动画控制器
                waitingCharacterList.Add(switchCharacterInfos[i].characterName); // 将所有角色加入等待列表
            }
            newCharacterName.Value = CharacterNameList.AnBi; // 设置默认角色
        }

        /// <summary>
        /// 开始时允许切换输入并初始化相机的目标
        /// </summary>
        protected void Start()
        {
            canSwitchInput = true; // 允许切换输入
            SwitchCharacterInfo initCharacterInfo = switchCharacterInfos.Find(i => i.characterName == newCharacterName.Value);
            SwitchCamerasTarget(initCharacterInfo.aimAtPos, initCharacterInfo.followAtPos); // 设置相机的目标位置
        }

        /// <summary>
        /// 切换角色输入的外部接口
        /// </summary>
        public void SwitchInput()
        {
            if (!canSwitchInput) return; // 如果不允许切换，直接返回
            canSwitchInput = false; // 暂时禁止再次切换
            currentCharacterName = newCharacterName.Value; // 记录当前角色的名称
            newCharacterName.Value = UpdateCharacter(); // 更新新角色的名称
            ExecuteSwitchCharacter(newCharacterName.Value, false); // 执行角色切换
            TimerManager.MainInstance.GetOneTimer(applyNextSwitchTime, ApplyNextSwitch); // 启动切换缓冲计时器
        }

        /// <summary>
        /// 使用技能时的角色切换接口
        /// </summary>
        public void SwitchSkillInput(CharacterNameList SwitchInCharacter, string SwitchInSkillName)
        {
            currentCharacterName = newCharacterName.Value;
            newCharacterName.Value = SwitchInCharacter; // 切换至新角色
            ExecuteSwitchCharacter(newCharacterName.Value, true, SwitchInSkillName); // 执行角色切换并触发技能
            UpdateNewCharacterIndex(SwitchInCharacter); // 更新角色索引
        }

        /// <summary>
        /// 更新新角色的索引
        /// </summary>
        private void UpdateNewCharacterIndex(CharacterNameList characterName)
        {
            for (int i = 0; i < switchCharacterInfos.Count; i++)
            {
                if (switchCharacterInfos[i].characterName == characterName)
                {
                    characterIndex = i; return;
                }
            }
        }

        /// <summary>
        /// 更新下一个角色的名称
        /// </summary>
        private CharacterNameList UpdateCharacter()
        {
            characterIndex++; // 更新索引
            characterIndex %= switchCharacterInfos.Count; // 防止索引超出范围
            return switchCharacterInfos[characterIndex].characterName; // 返回新角色的名称
        }

        Coroutine switchOutCharacterTimeCoroutine; // 协程变量

        /// <summary>
        /// 执行角色切换
        /// </summary>
        public void ExecuteSwitchCharacter(CharacterNameList newCharacterName, bool isSwitchATK, string SwitchInAnimation = "SwitchIn", string SwitchOutAnimation = "SwitchOut")
        {
            SwitchCharacterInfo currentCharacterInfo = switchCharacterInfos.Find(i => i.characterName == currentCharacterName);
            if (currentCharacterInfo != null)
            {
                currentCharacter = currentCharacterInfo.character; // 设置当前角色
                currentCharacterInfo.animator.CrossFadeInFixedTime(SwitchOutAnimation, 0.1f); // 播放退场动画
            }

            SwitchCharacterInfo newCharacterInfo = switchCharacterInfos.Find(i => i.characterName == newCharacterName);
            if (newCharacterInfo != null)
            {
                newCharacter = newCharacterInfo.character;
                newCharacter.SetActive(false); // 先禁用新角色
                if (!isSwitchATK) // 如果不是技能切换，设置出生位置
                {
                    newCharacter.transform.position = currentCharacter.transform.position - currentCharacter.transform.forward * newCharacterInfo.spawnDistance - currentCharacter.transform.right * 0.6f;
                }
                else // 如果是连携技能切换，设置为敌人位置
                {
                    newCharacter.transform.position = GameBlackboard.MainInstance.GetEnemy().position - currentCharacter.transform.forward * 3;
                }

                newCharacter.transform.localRotation = currentCharacter.transform.localRotation; // 复制当前角色的旋转
                newCharacter.SetActive(true); // 启用新角色
                newCharacterInfo.animator.Play(SwitchInAnimation); // 播放进场动画
                SwitchCamerasTarget(newCharacterInfo.aimAtPos, newCharacterInfo.followAtPos); // 更新相机目标
            }

            if (switchOutCharacterTimeCoroutine != null)
            {
                StopCoroutine(switchOutCharacterTimeCoroutine); // 停止协程
            }
            switchOutCharacterTimeCoroutine = StartCoroutine(CharacterActiveTimerCoroutine(switchOutCharacterTime)); // 开始新的协程
        }

        /// <summary>
        /// 协程，用于设置角色存留时间
        /// </summary>
        IEnumerator CharacterActiveTimerCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            SetCharacterActive(); // 设置角色激活状态
        }

        /// <summary>
        /// 设置只有新角色保持激活，其他角色禁用
        /// </summary>
        private void SetCharacterActive()
        {
            for (int i = 0; i < switchCharacterInfos.Count; i++)
            {
                if (switchCharacterInfos[i].characterName != newCharacterName.Value)
                {
                    switchCharacterInfos[i].character.SetActive(false);
                }
            }
        }

        /// <summary>
        /// 允许下一次角色切换
        /// </summary>
        private void ApplyNextSwitch()
        {
            canSwitchInput = true; // 允许再次切换
        }

        /// <summary>
        /// 更新相机目标位置
        /// </summary>
        private void SwitchCamerasTarget(Transform aimPos, Transform followPos)
        {
            for (int i = 0; i < virtualCameras.Length; i++)
            {
                if (virtualCameras[i].gameObject.tag != "CloseShot")
                {
                    virtualCameras[i].LookAt = aimPos; // 更新相机的瞄准目标
                    virtualCameras[i].Follow = followPos; // 更新相机的跟随目标
                }
            }
        }
    }
}
