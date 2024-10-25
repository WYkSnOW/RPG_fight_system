using UnityEngine;
using ZZZ;

public class CharacterHeath : CharacterHealthBase
{
    /// <summary>
    /// 处理角色受击时的逻辑
    /// </summary>
    /// <param name="damage">收到的伤害值</param>
    /// <param name="hitName">受击时的动画名称</param>
    /// <param name="parryName">格挡时的动画名称</param>
    protected override void CharacterHitAction(float damage, string hitName, string parryName)
    {
        base.CharacterHitAction(damage, hitName, parryName);

        if (healthInfo.hasStrength.Value) // 如果角色有力量值（可以格挡）
        {
            healthInfo.TakeStrength(damage); // 扣除力量值
            animator.CrossFadeInFixedTime(parryName, 0.1f, 0); // 播放格挡动画
            // 播放格挡音效
            // SFX_PoolManager.MainInstance.TryGetSoundPool("PARRY", transform.position, Quaternion.identity);
        }
        else // 如果不能格挡，直接扣除生命值
        {
            healthInfo.TakeDamage(damage); // 扣除生命值
            animator.CrossFadeInFixedTime(hitName, 0.1f, 0); // 播放受击动画
            // 播放受击音效
            // SFX_PoolManager.MainInstance.TryGetSoundPool("HIT", transform.position, Quaternion.identity);
        }

        // 无论是否格挡，都会减少防御值
        healthInfo.TakeDefenseValue(damage);
    }

    /// <summary>
    /// 更新角色的防御值
    /// </summary>
    /// <param name="value">新的防御值</param>
    protected override void OnUpdateDefenseValue(float value)
    {
        base.OnUpdateDefenseValue(value);

        if (currentEnemy == null) { return; }

        if (value <= 0) // 如果防御值为 0，触发QTE（快速反应事件）
        {
            GameEventsManager.MainInstance.CallEvent("达到QTE条件", currentEnemy);
            healthInfo.ReDefenseValue(); // 恢复防御值
            return;
        }
    }

    /// <summary>
    /// 播放受击音效
    /// </summary>
    /// <param name="characterNameList">角色名称列表，用于区分音效</param>
    protected override void SetHitSFX(CharacterNameList characterNameList)
    {
        // 播放受击音效
        SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.HIT, characterNameList.ToString(), transform.position);
    }
}
