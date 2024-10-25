using GGG.Tool;
using UnityEngine;
using ZZZ;
using static UnityEngine.Rendering.DebugUI;

public class CharacterHealthBase : MonoBehaviour
{
    // 当前角色的生命值
    [SerializeField] private float currentHP;

    // 当前角色的力量值
    [SerializeField] private float currentStrength;

    // 当前角色的防御值
    [SerializeField] private float currentDefenseValue;

    // 当前锁定的敌人
    protected Transform currentEnemy;

    // 角色的健康信息，包含生命值、防御值等
    [SerializeField] protected CharacterHealthInfo characterHealthInfo;
    protected CharacterHealthInfo healthInfo;

    // 动画控制器，用于控制角色的动画状态
    protected Animator animator;

    // 初始化函数，用于获取动画控制器和实例化健康信息
    protected virtual void Awake()
    {
        // 获取动画控制器
        animator = GetComponent<Animator>();

        // 实例化健康信息对象
        healthInfo = Instantiate(characterHealthInfo);

        // 绑定健康信息的变化事件，更新生命值、力量值、防御值
        healthInfo.currentHP.OnValueChanged += OnUpdatePH;
        healthInfo.currentStrength.OnValueChanged += OnUpdateStrength;
        healthInfo.currentDefenseValue.OnValueChanged += OnUpdateDefenseValue;
    }

    // 在Start方法中初始化健康数据
    private void Start()
    {
        healthInfo.InitHealthData();
    }

    // 每帧调用，用于让角色面对当前攻击者
    protected virtual void Update()
    {
        LookAtAttacker();
    }

    // 在对象启用时注册事件监听，用于处理角色被攻击时的逻辑
    protected virtual void OnEnable()
    {
        GameEventsManager.MainInstance.AddEventListening<float, string, string, Transform, Transform, ZZZ.CharacterComboBase>("触发伤害", OnCharacterHitEventHandler);
        GameEventsManager.MainInstance.AddEventListening<float>("生成伤害", OnCharacterDamageAction);
    }

    // 在对象禁用时移除事件监听，避免事件泄露
    protected virtual void OnDisable()
    {
        GameEventsManager.MainInstance.ReMoveEvent<float, string, string, Transform, Transform, ZZZ.CharacterComboBase>("触发伤害", OnCharacterHitEventHandler);
        GameEventsManager.MainInstance.ReMoveEvent<float>("生成伤害", OnCharacterDamageAction);

        // 移除绑定的事件处理方法
        healthInfo.currentHP.OnValueChanged -= OnUpdatePH;
        healthInfo.currentStrength.OnValueChanged -= OnUpdateStrength;
        healthInfo.currentDefenseValue.OnValueChanged -= OnUpdateDefenseValue;
    }

    // 角色被击中时的事件处理函数
    private void OnCharacterHitEventHandler(float Damage, string HitName, string ParryName, Transform Attacker, Transform Bearer, ZZZ.CharacterComboBase characterCombo)
    {
        // 如果不是当前对象受击，直接返回
        if (Bearer != this.transform) { return; }

        // 设置攻击者为当前敌人
        SetEnemy(Attacker);

        // 执行击打动作和伤害处理
        CharacterHitAction(Damage, HitName, ParryName);
        OnCharacterDamageAction(Damage);

        // 播放击打特效和音效
        SetHitFVX(Attacker, Bearer);
        SetHitSFX(characterCombo.player.characterName);
    }

    // 处理角色的伤害
    protected void OnCharacterDamageAction(float damage)
    {
        healthInfo.TakeDamage(damage);
    }

    // 处理角色的力量值减少
    protected void CharacterStrengthAction(float damage)
    {
        healthInfo.TakeStrength(damage);
    }

    // 角色击打时的动作处理函数
    protected virtual void CharacterHitAction(float damage, string hitName, string parryName)
    {
        // 可以在子类中重写此方法实现不同的击打逻辑
    }

    // 设置当前的攻击者
    protected virtual void SetEnemy(Transform attacker)
    {
        if (currentEnemy != attacker || currentEnemy == null)
        {
            currentEnemy = attacker;
        }
    }

    // 让角色面对攻击者
    private void LookAtAttacker()
    {
        if (currentEnemy == null) { return; }
        if (animator.AnimationAtTag("Hit") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.3f)
        {
            transform.Look(currentEnemy.position, 50);
        }
    }

    // 播放击打特效
    protected void SetHitFVX(Transform attacker, Transform hitter)
    {
        Vector3 hitDir = (attacker.position - hitter.position).normalized;
        Vector3 targetPos = hitter.position + hitDir * 0.8f + Vector3.up * 1f;
        VFX_PoolManager.MainInstance.GetVFX(ZZZ.CharacterNameList.Enemy, "Hit", targetPos);
    }

    // 播放击打音效
    protected virtual void SetHitSFX(CharacterNameList characterNameList)
    {
        // 可以在子类中重写以实现不同的音效播放
    }

    // 更新角色的生命值
    private void OnUpdatePH(float value)
    {
        Debug.Log("敌人的血量为" + value);
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

    // 更新角色的力量值
    private void OnUpdateStrength(float value)
    {
        currentStrength = value;
        healthInfo.hasStrength.Value = value > 0;
    }

    // 更新角色的防御值
    protected virtual void OnUpdateDefenseValue(float value)
    {
        currentDefenseValue = value;
    }
}
