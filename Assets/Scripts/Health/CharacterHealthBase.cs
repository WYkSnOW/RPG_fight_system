using GGG.Tool;
using UnityEngine;
using ZZZ;
using static UnityEngine.Rendering.DebugUI;

public class CharacterHealthBase : MonoBehaviour
{
    // ��ǰ��ɫ������ֵ
    [SerializeField] private float currentHP;

    // ��ǰ��ɫ������ֵ
    [SerializeField] private float currentStrength;

    // ��ǰ��ɫ�ķ���ֵ
    [SerializeField] private float currentDefenseValue;

    // ��ǰ�����ĵ���
    protected Transform currentEnemy;

    // ��ɫ�Ľ�����Ϣ����������ֵ������ֵ��
    [SerializeField] protected CharacterHealthInfo characterHealthInfo;
    protected CharacterHealthInfo healthInfo;

    // ���������������ڿ��ƽ�ɫ�Ķ���״̬
    protected Animator animator;

    // ��ʼ�����������ڻ�ȡ������������ʵ����������Ϣ
    protected virtual void Awake()
    {
        // ��ȡ����������
        animator = GetComponent<Animator>();

        // ʵ����������Ϣ����
        healthInfo = Instantiate(characterHealthInfo);

        // �󶨽�����Ϣ�ı仯�¼�����������ֵ������ֵ������ֵ
        healthInfo.currentHP.OnValueChanged += OnUpdatePH;
        healthInfo.currentStrength.OnValueChanged += OnUpdateStrength;
        healthInfo.currentDefenseValue.OnValueChanged += OnUpdateDefenseValue;
    }

    // ��Start�����г�ʼ����������
    private void Start()
    {
        healthInfo.InitHealthData();
    }

    // ÿ֡���ã������ý�ɫ��Ե�ǰ������
    protected virtual void Update()
    {
        LookAtAttacker();
    }

    // �ڶ�������ʱע���¼����������ڴ����ɫ������ʱ���߼�
    protected virtual void OnEnable()
    {
        GameEventsManager.MainInstance.AddEventListening<float, string, string, Transform, Transform, ZZZ.CharacterComboBase>("�����˺�", OnCharacterHitEventHandler);
        GameEventsManager.MainInstance.AddEventListening<float>("�����˺�", OnCharacterDamageAction);
    }

    // �ڶ������ʱ�Ƴ��¼������������¼�й¶
    protected virtual void OnDisable()
    {
        GameEventsManager.MainInstance.ReMoveEvent<float, string, string, Transform, Transform, ZZZ.CharacterComboBase>("�����˺�", OnCharacterHitEventHandler);
        GameEventsManager.MainInstance.ReMoveEvent<float>("�����˺�", OnCharacterDamageAction);

        // �Ƴ��󶨵��¼�������
        healthInfo.currentHP.OnValueChanged -= OnUpdatePH;
        healthInfo.currentStrength.OnValueChanged -= OnUpdateStrength;
        healthInfo.currentDefenseValue.OnValueChanged -= OnUpdateDefenseValue;
    }

    // ��ɫ������ʱ���¼�������
    private void OnCharacterHitEventHandler(float Damage, string HitName, string ParryName, Transform Attacker, Transform Bearer, ZZZ.CharacterComboBase characterCombo)
    {
        // ������ǵ�ǰ�����ܻ���ֱ�ӷ���
        if (Bearer != this.transform) { return; }

        // ���ù�����Ϊ��ǰ����
        SetEnemy(Attacker);

        // ִ�л��������˺�����
        CharacterHitAction(Damage, HitName, ParryName);
        OnCharacterDamageAction(Damage);

        // ���Ż�����Ч����Ч
        SetHitFVX(Attacker, Bearer);
        SetHitSFX(characterCombo.player.characterName);
    }

    // �����ɫ���˺�
    protected void OnCharacterDamageAction(float damage)
    {
        healthInfo.TakeDamage(damage);
    }

    // �����ɫ������ֵ����
    protected void CharacterStrengthAction(float damage)
    {
        healthInfo.TakeStrength(damage);
    }

    // ��ɫ����ʱ�Ķ���������
    protected virtual void CharacterHitAction(float damage, string hitName, string parryName)
    {
        // ��������������д�˷���ʵ�ֲ�ͬ�Ļ����߼�
    }

    // ���õ�ǰ�Ĺ�����
    protected virtual void SetEnemy(Transform attacker)
    {
        if (currentEnemy != attacker || currentEnemy == null)
        {
            currentEnemy = attacker;
        }
    }

    // �ý�ɫ��Թ�����
    private void LookAtAttacker()
    {
        if (currentEnemy == null) { return; }
        if (animator.AnimationAtTag("Hit") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.3f)
        {
            transform.Look(currentEnemy.position, 50);
        }
    }

    // ���Ż�����Ч
    protected void SetHitFVX(Transform attacker, Transform hitter)
    {
        Vector3 hitDir = (attacker.position - hitter.position).normalized;
        Vector3 targetPos = hitter.position + hitDir * 0.8f + Vector3.up * 1f;
        VFX_PoolManager.MainInstance.GetVFX(ZZZ.CharacterNameList.Enemy, "Hit", targetPos);
    }

    // ���Ż�����Ч
    protected virtual void SetHitSFX(CharacterNameList characterNameList)
    {
        // ��������������д��ʵ�ֲ�ͬ����Ч����
    }

    // ���½�ɫ������ֵ
    private void OnUpdatePH(float value)
    {
        Debug.Log("���˵�Ѫ��Ϊ" + value);
        currentHP = value;
        if (value > 0)
        {
            healthInfo.onDead.Value = false;
            UIManager.MainInstance.stateBarUI.UpdateBlood(currentHP / healthInfo.maxHP);
        }
        else
        {
            healthInfo.onDead.Value = true;
        }
    }

    // ���½�ɫ������ֵ
    private void OnUpdateStrength(float value)
    {
        currentStrength = value;
        healthInfo.hasStrength.Value = value > 0;
    }

    // ���½�ɫ�ķ���ֵ
    protected virtual void OnUpdateDefenseValue(float value)
    {
        currentDefenseValue = value;
    }
}
