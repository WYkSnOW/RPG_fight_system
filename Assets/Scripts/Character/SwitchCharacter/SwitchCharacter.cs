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
        /// SwitchCharacterInfo �����ڴ洢ÿ����ɫ����Ϣ��������ɫ���ơ����Ŀ��λ�á�����λ�õȡ�
        /// </summary>
        [System.Serializable]
        public class SwitchCharacterInfo
        {
            public CharacterNameList characterName; // ��ɫ����
            public Transform aimAtPos; // ��ɫ�������׼λ��
            public Transform followAtPos; // ��ɫ���������λ��
            public GameObject character; // ��ɫ����
            public float spawnDistance; // ��ɫ����ʱ������һ��ɫ�ľ���
            [HideInInspector]
            public Animator animator; // ��ɫ�Ķ���������
        }

        // ��ɫ��Ϣ�б�
        [SerializeField] private List<SwitchCharacterInfo> switchCharacterInfos = new List<SwitchCharacterInfo>();

        // �ȴ��л��Ľ�ɫ�б�
        [SerializeField] public List<CharacterNameList> waitingCharacterList = new List<CharacterNameList>();

        // �Ƿ������л���ɫ������
        private bool canSwitchInput;

        // �л���ɫ�Ļ���ʱ��
        [SerializeField, Header("�л���ɫ�Ļ���ʱ��")] private float applyNextSwitchTime;

        // �˳���ɫ�Ĵ���ʱ��
        [SerializeField, Header("�˳���ɫ�Ĵ���ʱ��")] private float switchOutCharacterTime;

        // ��ǰ��ɫ������
        [SerializeField] public CharacterNameList currentCharacterName;

        // �½�ɫ�����ƣ�ͨ�������Լ�ر仯��
        [SerializeField] public BindableProperty<CharacterNameList> newCharacterName = new BindableProperty<CharacterNameList>();

        // ��ǰ��ɫ����Ϸ����
        [SerializeField] private GameObject currentCharacter;

        // �½�ɫ����Ϸ����
        [SerializeField] public GameObject newCharacter;

        // ������飬��������������
        [SerializeField] private CinemachineVirtualCamera[] virtualCameras;

        // ��ǰ��ɫ���б��е�����
        private int characterIndex = 0;

        /// <summary>
        /// ��ʼ����ɫ�����
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            InitCharacter(); // ��ʼ����ɫ
            InitCamera(); // ��ʼ�����
        }

        /// <summary>
        /// ���� newCharacterName ��ֵ�ı��¼�
        /// </summary>
        private void OnEnable()
        {
            newCharacterName.OnValueChanged += OnSetWaitingCharacterList;
        }

        /// <summary>
        /// ȡ������ newCharacterName ��ֵ�ı��¼�
        /// </summary>
        private void OnDisable()
        {
            newCharacterName.OnValueChanged -= OnSetWaitingCharacterList;
        }

        /// <summary>
        /// ���µȴ��л��Ľ�ɫ�б�
        /// </summary>
        private void OnSetWaitingCharacterList(CharacterNameList list)
        {
            // ����½�ɫ�ڵȴ��б��У��Ƴ��ý�ɫ
            if (waitingCharacterList.Contains(list))
            {
                waitingCharacterList.Remove(list);
            }
            else
            {
                Debug.LogWarning(list + " ������ﲻ�ڵȺ��б��޷��Ƴ�");
            }

            // ���⵱ǰ��ɫ���½�ɫ��ͬ��ȷ���ɽ�ɫ����ȴ��б�
            if (currentCharacterName != list)
            {
                if (!waitingCharacterList.Contains(currentCharacterName))
                {
                    waitingCharacterList.Add(currentCharacterName);
                }
            }
        }

        /// <summary>
        /// ��ʼ���������
        /// </summary>
        private void InitCamera()
        {
            virtualCameras = FindObjectsOfType<CinemachineVirtualCamera>(); // ���ҳ����е��������
        }

        /// <summary>
        /// ��ʼ����ɫ�Ķ����������͵ȴ��б�
        /// </summary>
        private void InitCharacter()
        {
            for (int i = 0; i < switchCharacterInfos.Count; i++)
            {
                switchCharacterInfos[i].animator = switchCharacterInfos[i].character.transform.GetComponent<Animator>(); // ��ȡ��ɫ�Ķ���������
                waitingCharacterList.Add(switchCharacterInfos[i].characterName); // �����н�ɫ����ȴ��б�
            }
            newCharacterName.Value = CharacterNameList.AnBi; // ����Ĭ�Ͻ�ɫ
        }

        /// <summary>
        /// ��ʼʱ�����л����벢��ʼ�������Ŀ��
        /// </summary>
        protected void Start()
        {
            canSwitchInput = true; // �����л�����
            SwitchCharacterInfo initCharacterInfo = switchCharacterInfos.Find(i => i.characterName == newCharacterName.Value);
            SwitchCamerasTarget(initCharacterInfo.aimAtPos, initCharacterInfo.followAtPos); // ���������Ŀ��λ��
        }

        /// <summary>
        /// �л���ɫ������ⲿ�ӿ�
        /// </summary>
        public void SwitchInput()
        {
            if (!canSwitchInput) return; // ����������л���ֱ�ӷ���
            canSwitchInput = false; // ��ʱ��ֹ�ٴ��л�
            currentCharacterName = newCharacterName.Value; // ��¼��ǰ��ɫ������
            newCharacterName.Value = UpdateCharacter(); // �����½�ɫ������
            ExecuteSwitchCharacter(newCharacterName.Value, false); // ִ�н�ɫ�л�
            TimerManager.MainInstance.GetOneTimer(applyNextSwitchTime, ApplyNextSwitch); // �����л������ʱ��
        }

        /// <summary>
        /// ʹ�ü���ʱ�Ľ�ɫ�л��ӿ�
        /// </summary>
        public void SwitchSkillInput(CharacterNameList SwitchInCharacter, string SwitchInSkillName)
        {
            currentCharacterName = newCharacterName.Value;
            newCharacterName.Value = SwitchInCharacter; // �л����½�ɫ
            ExecuteSwitchCharacter(newCharacterName.Value, true, SwitchInSkillName); // ִ�н�ɫ�л�����������
            UpdateNewCharacterIndex(SwitchInCharacter); // ���½�ɫ����
        }

        /// <summary>
        /// �����½�ɫ������
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
        /// ������һ����ɫ������
        /// </summary>
        private CharacterNameList UpdateCharacter()
        {
            characterIndex++; // ��������
            characterIndex %= switchCharacterInfos.Count; // ��ֹ����������Χ
            return switchCharacterInfos[characterIndex].characterName; // �����½�ɫ������
        }

        Coroutine switchOutCharacterTimeCoroutine; // Э�̱���

        /// <summary>
        /// ִ�н�ɫ�л�
        /// </summary>
        public void ExecuteSwitchCharacter(CharacterNameList newCharacterName, bool isSwitchATK, string SwitchInAnimation = "SwitchIn", string SwitchOutAnimation = "SwitchOut")
        {
            SwitchCharacterInfo currentCharacterInfo = switchCharacterInfos.Find(i => i.characterName == currentCharacterName);
            if (currentCharacterInfo != null)
            {
                currentCharacter = currentCharacterInfo.character; // ���õ�ǰ��ɫ
                currentCharacterInfo.animator.CrossFadeInFixedTime(SwitchOutAnimation, 0.1f); // �����˳�����
            }

            SwitchCharacterInfo newCharacterInfo = switchCharacterInfos.Find(i => i.characterName == newCharacterName);
            if (newCharacterInfo != null)
            {
                newCharacter = newCharacterInfo.character;
                newCharacter.SetActive(false); // �Ƚ����½�ɫ
                if (!isSwitchATK) // ������Ǽ����л������ó���λ��
                {
                    newCharacter.transform.position = currentCharacter.transform.position - currentCharacter.transform.forward * newCharacterInfo.spawnDistance - currentCharacter.transform.right * 0.6f;
                }
                else // �������Я�����л�������Ϊ����λ��
                {
                    newCharacter.transform.position = GameBlackboard.MainInstance.GetEnemy().position - currentCharacter.transform.forward * 3;
                }

                newCharacter.transform.localRotation = currentCharacter.transform.localRotation; // ���Ƶ�ǰ��ɫ����ת
                newCharacter.SetActive(true); // �����½�ɫ
                newCharacterInfo.animator.Play(SwitchInAnimation); // ���Ž�������
                SwitchCamerasTarget(newCharacterInfo.aimAtPos, newCharacterInfo.followAtPos); // �������Ŀ��
            }

            if (switchOutCharacterTimeCoroutine != null)
            {
                StopCoroutine(switchOutCharacterTimeCoroutine); // ֹͣЭ��
            }
            switchOutCharacterTimeCoroutine = StartCoroutine(CharacterActiveTimerCoroutine(switchOutCharacterTime)); // ��ʼ�µ�Э��
        }

        /// <summary>
        /// Э�̣��������ý�ɫ����ʱ��
        /// </summary>
        IEnumerator CharacterActiveTimerCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            SetCharacterActive(); // ���ý�ɫ����״̬
        }

        /// <summary>
        /// ����ֻ���½�ɫ���ּ��������ɫ����
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
        /// ������һ�ν�ɫ�л�
        /// </summary>
        private void ApplyNextSwitch()
        {
            canSwitchInput = true; // �����ٴ��л�
        }

        /// <summary>
        /// �������Ŀ��λ��
        /// </summary>
        private void SwitchCamerasTarget(Transform aimPos, Transform followPos)
        {
            for (int i = 0; i < virtualCameras.Length; i++)
            {
                if (virtualCameras[i].gameObject.tag != "CloseShot")
                {
                    virtualCameras[i].LookAt = aimPos; // �����������׼Ŀ��
                    virtualCameras[i].Follow = followPos; // ��������ĸ���Ŀ��
                }
            }
        }
    }
}
