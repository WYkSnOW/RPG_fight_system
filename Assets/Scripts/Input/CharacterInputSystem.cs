using UnityEngine;
using HuHu;
using UnityEngine.InputSystem;

// CharacterInputSystem ��̳��� Singleton��ȷ����������Ϸ��ֻ��һ��ʵ����
// �������ڹ�����װ��ҵ����룬ͨ�� Unity ��������ϵͳ�����������Ҳ�����
public class CharacterInputSystem : Singleton<CharacterInputSystem>
{
    // CharacterInput ��ͨ�� Input System ���ɵ����붯���࣬���������ж���õ����붯����
    public CharacterInput inputActions;

    // ��д Awake ������ȷ�� inputActions �ѱ�ʵ������
    // Singleton �� Awake ��ȷ�������Ψһʵ����
    protected override void Awake()
    {
        base.Awake();

        // ��� inputActions Ϊ�գ������µ� CharacterInput ʵ������ʵ�������������ã���
        if (inputActions == null)
            inputActions = new CharacterInput();
    }

    // �����������ʱ����������ϵͳ��
    private void OnEnable()
    {
        inputActions?.Enable(); // �������ж�������붯��
    }

    // �����������ʱ����������ϵͳ��
    private void OnDisable()
    {
        inputActions?.Disable(); // �����������붯�������ⲻ��Ҫ�����봦��
    }

    // ���Է�װ�����²��ַ�װ�˾�������붯�����ṩ���ⲿ����á�

    // ��ȡ��ҵ��ƶ����룬����һ�� Vector2 ���ͣ�ͨ��Ϊ WASD ���ֱ���ҡ�ˣ���
    public Vector2 PlayerMove
    {
        get => inputActions.Player.Movement.ReadValue<Vector2>(); // ��ȡ��ҵ��ƶ�����
    }

    // ��ȡ��ҵ�����������룬����һ�� Vector2 ���ͣ�ͨ��Ϊ����ƶ����ֱ���ҡ�ˣ���
    public Vector2 CameraLook
    {
        get => inputActions.Player.CameraLook.ReadValue<Vector2>(); // ��ȡ��ҵ�����ӽ�����
    }

    // �������Ƿ������ܲ�����ͨ��Ϊ Shift �����ֱ�ĳ����ť����
    public bool Run
    {
        get => inputActions.Player.Run.triggered; // `triggered` ��ʾ�ð����Ƿ�ոձ�����
    }

    // �������Ƿ����ڳ����ܲ�����ס���ɿ�ʱ��`phase` �ᱣ���� Performed ״̬����
    public bool Run_Continue
    {
        get => inputActions.Player.Run.phase == InputActionPhase.Performed; // ��ⰴ���Ƿ���ִ��״̬
    }

    // �������Ƿ񴥷�����Ծ��ͨ��Ϊ�ո�����ֱ��ϵ���Ծ��ť����
    public bool Jump
    {
        get => inputActions.Player.Jump.triggered; // ��Ծ�����Ƿ񱻰���
    }

    // �������Ƿ��ڶ׷������¶׷���ʱ��`phase` ���� Performed ״̬����
    public bool Crouch
    {
        get => inputActions.Player.Crouch.phase == InputActionPhase.Performed; // �Ƿ��ڶ׷�״̬
    }

    // �������Ƿ��������๥����ͨ��Ϊ���������ֱ�ĳ��������ť����
    public bool L_Atk
    {
        get => inputActions.Player.L_AtK.triggered; // �󹥻������Ƿ񴥷�
    }

    // �������Ƿ�������Ҳ๥����ͨ��Ϊ����Ҽ����ֱ���һ��������ť����
    public bool R_Atk
    {
        get => inputActions.Player.R_Atk.triggered; // �ҹ��������Ƿ񴥷�
    }

    // �������Ƿ��ڽ�����׼�������ҹ�����ť�Ƿ���ִ��״̬����
    public bool Aim
    {
        get => inputActions.Player.R_Atk.phase == InputActionPhase.Performed; // ��׼״̬���ҹ�����ס���ţ�
    }

    // �������Ƿ��ڳ���������๥��������ͨ��Ϊ������������
    public bool L_Atk_Continue
    {
        get => inputActions.Player.Continue_Atk.phase == InputActionPhase.Performed; // ���������Ƿ���ִ��״̬
    }

    // �������Ƿ�ִ��ĳ�����⶯����ͨ��Ϊִ�а������� F ������
    public bool Execute
    {
        get => inputActions.Player.Execute.triggered; // ����ִ�ж����Ƿ񱻴���
    }

    // �������Ƿ�����˽�ɫ�л�������ͨ��Ϊ�л���ɫ�İ�������
    public bool SwitchCharacter
    {
        get => inputActions.Player.SwitchCharacter.triggered; // �л���ɫ�����Ƿ񱻴���
    }

    // �������Ƿ�ʹ���˼��ܣ�ͨ��Ϊ���ܰ������� Q �� E ������
    public bool Skill
    {
        get => inputActions.Player.Skill.triggered; // ���ܰ����Ƿ񱻴���
    }

    // ��⼼���Ƿ���ɻ���������ܽ����İ�������
    public bool FinishSkill
    {
        get => inputActions.Player.FinishSkill.triggered; // ���ܽ��������Ƿ񱻴���
    }

    // �������Ƿ��л�Ϊ��·ģʽ��ͨ��Ϊ��·/�ܲ��л���������
    public bool Walk
    {
        get => inputActions.Player.Walk.triggered; // �л���·ģʽ�İ����Ƿ񱻴���
    }
}
