using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterHealthInfo", menuName = "Create/Asset/CharacterHealthInfo")]
public class CharacterHealthInfo : ScriptableObject
{
    // �������ݵ����ã������������ֵ���������ֵ����Ϣ
    [SerializeField] private CharacterHealthData healthData;

    // �����ԣ��洢��ǰ��ɫ������ֵ
    public BindableProperty<float> currentHP = new BindableProperty<float>();

    // �����ԣ��洢��ǰ��ɫ������ֵ
    public BindableProperty<float> currentStrength = new BindableProperty<float>();

    // �����ԣ��洢��ǰ��ɫ�ķ���ֵ
    public BindableProperty<float> currentDefenseValue = new BindableProperty<float>();

    // �����ԣ���־��ɫ�Ƿ�����
    public BindableProperty<bool> onDead = new BindableProperty<bool>();

    // �����ԣ���־��ɫ�Ƿ��������ֵ���Ƿ���Ը񵲣�
    public BindableProperty<bool> hasStrength = new BindableProperty<bool>();

    // �������ֵ
    public float maxHP;

    // ��ʼ����ɫ�Ľ�������
    public void InitHealthData()
    {
        // �� healthData ��ȡ�������ֵ������ֵ�ͷ���ֵ������ʼ����ǰֵ
        maxHP = healthData.healthData.maxHP;
        currentHP.Value = healthData.healthData.maxHP;
        currentStrength.Value = healthData.healthData.maxStrength;
        currentDefenseValue.Value = healthData.healthData.maxDefenseValue;
        Debug.Log("���˳�ʼ����Ѫ��Ϊ" + currentHP.Value);
    }

    // ���ٽ�ɫ������ֵ
    public void TakeDamage(float Damage)
    {
        if (!hasStrength.Value)
        {
            currentHP.Value = TakeHealthValue(currentHP.Value, Damage, healthData.healthData.maxHP, false);
        }
    }

    // ���ٽ�ɫ������ֵ�����ڸ񵲣�
    public void TakeStrength(float Damage)
    {
        if (hasStrength.Value)
        {
            currentStrength.Value = TakeHealthValue(currentStrength.Value, Damage, healthData.healthData.maxStrength, false);
        }
    }

    // ���ٽ�ɫ�ķ���ֵ
    public void TakeDefenseValue(float Damage)
    {
        currentDefenseValue.Value = TakeHealthValue(currentDefenseValue.Value, Damage, healthData.healthData.maxDefenseValue, false);
    }

    // �ָ���ɫ������ֵ
    public void RePH(float Damage)
    {
        currentHP.Value = TakeHealthValue(currentHP.Value, Damage, healthData.healthData.maxHP, true);
    }

    // �ָ���ɫ������ֵ
    public void ReStrength(float Damage)
    {
        currentStrength.Value = TakeHealthValue(currentStrength.Value, Damage, healthData.healthData.maxStrength, true);
    }

    // ��������ֵ
    public void ReDefenseValue()
    {
        currentDefenseValue.Value = TakeHealthValue(currentDefenseValue.Value, healthData.healthData.maxDefenseValue, healthData.healthData.maxDefenseValue, true);
    }

    // ���������������������ı仯ֵ
    private float TakeHealthValue(float currentValue, float offsetValue, float maxValue, bool canAdd)
    {
        // ��� canAdd Ϊ true������ָ�ֵ������Ϊ����ֵ����������� 0 �����ֵ֮��
        return Mathf.Clamp(canAdd ? currentValue + offsetValue : currentValue - offsetValue, 0, maxValue);
    }
}
