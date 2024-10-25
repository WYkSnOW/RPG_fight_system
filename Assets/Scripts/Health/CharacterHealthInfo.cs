using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterHealthInfo", menuName = "Create/Asset/CharacterHealthInfo")]
public class CharacterHealthInfo : ScriptableObject
{
    // 健康数据的引用，包含最大生命值、最大力量值等信息
    [SerializeField] private CharacterHealthData healthData;

    // 绑定属性，存储当前角色的生命值
    public BindableProperty<float> currentHP = new BindableProperty<float>();

    // 绑定属性，存储当前角色的力量值
    public BindableProperty<float> currentStrength = new BindableProperty<float>();

    // 绑定属性，存储当前角色的防御值
    public BindableProperty<float> currentDefenseValue = new BindableProperty<float>();

    // 绑定属性，标志角色是否死亡
    public BindableProperty<bool> onDead = new BindableProperty<bool>();

    // 绑定属性，标志角色是否具有力量值（是否可以格挡）
    public BindableProperty<bool> hasStrength = new BindableProperty<bool>();

    // 最大生命值
    public float maxHP;

    // 初始化角色的健康数据
    public void InitHealthData()
    {
        // 从 healthData 获取最大生命值、力量值和防御值，并初始化当前值
        maxHP = healthData.healthData.maxHP;
        currentHP.Value = healthData.healthData.maxHP;
        currentStrength.Value = healthData.healthData.maxStrength;
        currentDefenseValue.Value = healthData.healthData.maxDefenseValue;
        Debug.Log("敌人初始化的血量为" + currentHP.Value);
    }

    // 减少角色的生命值
    public void TakeDamage(float Damage)
    {
        if (!hasStrength.Value)
        {
            currentHP.Value = TakeHealthValue(currentHP.Value, Damage, healthData.healthData.maxHP, false);
        }
    }

    // 减少角色的力量值（用于格挡）
    public void TakeStrength(float Damage)
    {
        if (hasStrength.Value)
        {
            currentStrength.Value = TakeHealthValue(currentStrength.Value, Damage, healthData.healthData.maxStrength, false);
        }
    }

    // 减少角色的防御值
    public void TakeDefenseValue(float Damage)
    {
        currentDefenseValue.Value = TakeHealthValue(currentDefenseValue.Value, Damage, healthData.healthData.maxDefenseValue, false);
    }

    // 恢复角色的生命值
    public void RePH(float Damage)
    {
        currentHP.Value = TakeHealthValue(currentHP.Value, Damage, healthData.healthData.maxHP, true);
    }

    // 恢复角色的力量值
    public void ReStrength(float Damage)
    {
        currentStrength.Value = TakeHealthValue(currentStrength.Value, Damage, healthData.healthData.maxStrength, true);
    }

    // 回满防御值
    public void ReDefenseValue()
    {
        currentDefenseValue.Value = TakeHealthValue(currentDefenseValue.Value, healthData.healthData.maxDefenseValue, healthData.healthData.maxDefenseValue, true);
    }

    // 计算生命、力量、防御的变化值
    private float TakeHealthValue(float currentValue, float offsetValue, float maxValue, bool canAdd)
    {
        // 如果 canAdd 为 true，代表恢复值；否则为减少值。结果限制在 0 到最大值之间
        return Mathf.Clamp(canAdd ? currentValue + offsetValue : currentValue - offsetValue, 0, maxValue);
    }
}
