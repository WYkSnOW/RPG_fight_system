using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterHealthData", menuName = "Create/Asset/CharacterHealthData")]
public class CharacterHealthData : ScriptableObject
{
    // ���ڴ洢��ɫ�Ľ������ݣ���������ֵ������ֵ������ֵ����Ϣ
    [field: SerializeField] public HealthData healthData { get; private set; }
    // ʹ�� field �ؼ���ʹ�����Կ��Ա����л�������Ȼ����˽�з���Ȩ��
}
