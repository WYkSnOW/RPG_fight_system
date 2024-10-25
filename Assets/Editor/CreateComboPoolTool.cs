#if UNITY_EDITOR
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CreateComboPoolTool : MonoBehaviour
{
    [MenuItem("HuHuTools/Create ComboSoundPool")]
    public static void GeneratePrefabs()
    {
        //����Ϸ�����л�ȡPool������
        SFX_PoolManager fX_PoolManager = SFX_PoolManager.MainInstance;
        //����SerializedObject
        SerializedObject serializedSoundPool = new SerializedObject(fX_PoolManager);
        //��ȡ�ó��ӵ�SerializedProperty
        SerializedProperty serializedPoolProperty = serializedSoundPool.FindProperty("soundPools");


        //�õ����е�ComboData
        string[] guids = AssetDatabase.FindAssets("t: ComboData");
        foreach (string guid in guids)
        {
            //�õ�·��
            string path = AssetDatabase.GUIDToAssetPath(guid);
            //�õ�SO
            ComboData comboData  = AssetDatabase.LoadAssetAtPath<ComboData>(path);

            if (comboData.AppAudioPrefab)

            {   if (comboData.weaponSound != null)
                {
                    if (comboData.weaponSound.Length > 0)
                    {
                        CreatePrefabAndAddToPool(comboData, serializedSoundPool, serializedPoolProperty, SoundStyle.WeaponSound);

                    }
                }
                if (comboData.characterVoice != null)
                {
                    if (comboData.characterVoice.Length > 0)
                    {
                        CreatePrefabAndAddToPool(comboData, serializedSoundPool, serializedPoolProperty, SoundStyle.ComboVoice);

                    }
                }
               
                
            }
        }
    }
    private static void CreatePrefabAndAddToPool(ComboData comboData, SerializedObject serializedSoundPool, SerializedProperty serializedPoolProperty,SoundStyle soundStyle)
    {
        //����Ԥ����
        GameObject go = new GameObject(comboData.name);
        //����AudioSource
        AudioSource audioSource = go.AddComponent<AudioSource>();
        //����Item�ű�
        ComboSFXtem comboCharacterVoiceItem = go.AddComponent<ComboSFXtem>();
        comboCharacterVoiceItem.GetComboData(comboData);
        comboCharacterVoiceItem.SetSoundStyle(soundStyle);
        //��Ԥ���屣�浽��Ŀ��

        string parentPath = "Assets/Prefabs/Audio/ComboAudio";
        if (!System.IO.Directory.Exists(parentPath))
        { 
           System.IO.Directory.CreateDirectory(parentPath);
        }
        //�����ļ���
        string targetPath = parentPath+"/"+ comboData.characterName.ToString();
        if (!System.IO.Directory.Exists(targetPath))
        {
            System.IO.Directory.CreateDirectory(targetPath);
        }
        string prefabPath = targetPath+"/"+ go.name + soundStyle.ToString() + ".prefab";
       GameObject prefab= PrefabUtility.SaveAsPrefabAssetAndConnect(go, prefabPath, InteractionMode.UserAction);


        //ע���ͨ����Ч����ص��б��
        //��Pool���д����µ�SoundItem
        SFX_PoolManager.SoundItem soundItem = new SFX_PoolManager.SoundItem();
        soundItem.soundName = comboData.comboName;
        soundItem.soundStyle = soundStyle;
        soundItem.soundPrefab = prefab;
        soundItem.soundCount = 4;//��Ĭ��Ϊ4
        soundItem.ApplyBigCenter = true;
        bool found =false;
        List<int> duplicateIndices = new List<int>();
        //������ͬ��Ԫ��
        for (int i = 0; i < serializedPoolProperty.arraySize; i++)
        {
            SerializedProperty poolItem = serializedPoolProperty.GetArrayElementAtIndex(i);
            if (poolItem.FindPropertyRelative("soundName").stringValue == soundItem.soundName && poolItem.FindPropertyRelative("soundStyle").enumValueIndex == (int)soundItem.soundStyle)
            {
                if (!found)
                {
                    poolItem.FindPropertyRelative("soundPrefab").objectReferenceValue = soundItem.soundPrefab;
                    poolItem.FindPropertyRelative("soundCount").intValue = soundItem.soundCount;
                    poolItem.FindPropertyRelative("ApplyBigCenter").boolValue = soundItem.ApplyBigCenter;
                    found = true;
                }
                else
                {
                    duplicateIndices.Add(i);
                }
            }
            
        }
        if (!found)
        {
            //����֮ǰSerializedSoundPool��ֵ
            serializedSoundPool.Update();
            //����µ���Ŀ
            serializedPoolProperty.arraySize++;
            //��ȡSerializedPoolProperty�����һ��Ԫ��
            SerializedProperty newSerializedProperty = serializedPoolProperty.GetArrayElementAtIndex(serializedPoolProperty.arraySize - 1);
            //�����һ��Ԫ�ؽ����޸�
            newSerializedProperty.FindPropertyRelative("soundName").stringValue = soundItem.soundName;
            newSerializedProperty.FindPropertyRelative("soundStyle").enumValueIndex = (int)soundItem.soundStyle;
            newSerializedProperty.FindPropertyRelative("soundPrefab").objectReferenceValue = soundItem.soundPrefab;
            newSerializedProperty.FindPropertyRelative("soundCount").intValue = soundItem.soundCount;
            newSerializedProperty.FindPropertyRelative("ApplyBigCenter").boolValue = soundItem.ApplyBigCenter;
        }
        else
        {
            for (int i = duplicateIndices.Count - 1; i >= 0; i--)
            {
                serializedPoolProperty.DeleteArrayElementAtIndex(duplicateIndices[i]);
            }
        }
        //�����޸�
        serializedSoundPool.ApplyModifiedProperties();
        DestroyImmediate(go);
    }
}
#endif
