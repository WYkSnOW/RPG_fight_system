using UnityEngine;
using ZZZ;

public class CharacterHeath : CharacterHealthBase
{
    /// <summary>
    /// �����ɫ�ܻ�ʱ���߼�
    /// </summary>
    /// <param name="damage">�յ����˺�ֵ</param>
    /// <param name="hitName">�ܻ�ʱ�Ķ�������</param>
    /// <param name="parryName">��ʱ�Ķ�������</param>
    protected override void CharacterHitAction(float damage, string hitName, string parryName)
    {
        base.CharacterHitAction(damage, hitName, parryName);

        if (healthInfo.hasStrength.Value) // �����ɫ������ֵ�����Ը񵲣�
        {
            healthInfo.TakeStrength(damage); // �۳�����ֵ
            animator.CrossFadeInFixedTime(parryName, 0.1f, 0); // ���Ÿ񵲶���
            // ���Ÿ���Ч
            // SFX_PoolManager.MainInstance.TryGetSoundPool("PARRY", transform.position, Quaternion.identity);
        }
        else // ������ܸ񵲣�ֱ�ӿ۳�����ֵ
        {
            healthInfo.TakeDamage(damage); // �۳�����ֵ
            animator.CrossFadeInFixedTime(hitName, 0.1f, 0); // �����ܻ�����
            // �����ܻ���Ч
            // SFX_PoolManager.MainInstance.TryGetSoundPool("HIT", transform.position, Quaternion.identity);
        }

        // �����Ƿ�񵲣�������ٷ���ֵ
        healthInfo.TakeDefenseValue(damage);
    }

    /// <summary>
    /// ���½�ɫ�ķ���ֵ
    /// </summary>
    /// <param name="value">�µķ���ֵ</param>
    protected override void OnUpdateDefenseValue(float value)
    {
        base.OnUpdateDefenseValue(value);

        if (currentEnemy == null) { return; }

        if (value <= 0) // �������ֵΪ 0������QTE�����ٷ�Ӧ�¼���
        {
            GameEventsManager.MainInstance.CallEvent("�ﵽQTE����", currentEnemy);
            healthInfo.ReDefenseValue(); // �ָ�����ֵ
            return;
        }
    }

    /// <summary>
    /// �����ܻ���Ч
    /// </summary>
    /// <param name="characterNameList">��ɫ�����б�����������Ч</param>
    protected override void SetHitSFX(CharacterNameList characterNameList)
    {
        // �����ܻ���Ч
        SFX_PoolManager.MainInstance.TryGetSoundPool(SoundStyle.HIT, characterNameList.ToString(), transform.position);
    }
}
