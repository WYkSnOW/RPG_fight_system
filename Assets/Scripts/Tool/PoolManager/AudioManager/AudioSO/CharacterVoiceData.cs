using System;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStyle
{
    Null,

}
public enum CharacterSoundStyle
{
    Null,
    //��������
    DodgeF,DodgeB,Jump
}


[CreateAssetMenu(menuName = "Asset/Audio/CharacterVoiceData")]
public class CharacterVoiceData : ScriptableObject
{ 

    [System.Serializable]
    public class CharacterVoiceInfo
    {
        public CharacterStyle characterStyle;
        public AudioClip[] audioClips;
        public float audioVolume;//0Ĭ��ΪĬ������
        public float lifeTime;//0Ĭ�ϲ����Զ��ر�
    }
    public List<CharacterVoiceInfo> promptToneInfoList = new List<CharacterVoiceInfo>();
   
}
