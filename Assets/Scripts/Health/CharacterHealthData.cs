using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterHealthData", menuName = "Create/Asset/CharacterHealthData")]
public class CharacterHealthData : ScriptableObject
{
    // 用于存储角色的健康数据，包含生命值、力量值、防御值等信息
    [field: SerializeField] public HealthData healthData { get; private set; }
    // 使用 field 关键字使得属性可以被序列化并且仍然保持私有访问权限
}
