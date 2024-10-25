using GGG.Tool;
using UnityEngine;

public class CharacterMoveControllerBase : MonoBehaviour
{
    // ���������������ڿ��ƽ�ɫ����״̬���л�
    public Animator characterAnimator { get; private set; }

    // ��ɫ��������������ڿ��ƽ�ɫ���ƶ�
    protected CharacterController characterController;

    // ������ز���
    [SerializeField, Header("����")] private float characterGravity = -9; // ��ɫ���������ٶ�
    protected float fallOutdeltaTimer; // ��ɫ�ڿ���������ʱ���ʱ��
    protected float fallOutTimer = 0.2f; // ��ɫ�ӿ�����ص�ʱ����
    [SerializeField] protected float maxVerticalSpeed = 20; // ���ֱ�ٶȣ���������ʱ������ٶȣ�
    [SerializeField] protected float minVerticalSpeed = -3; // ��С��ֱ�ٶ�
    [SerializeField] protected float verticalSpeed; // ��ǰ��ֱ�ٶ�
    protected Vector3 verticalVelocity; // ��ֱ�����ϵ��ٶ�����

    // ��������ز���
    [SerializeField, Header("������")] private float GroundDetectionRadius; // ������İ뾶�����ڼ���ɫ�Ƿ��ڵ����ϣ�
    [SerializeField] private float GroundDetectionOffset; // �������ƫ���������ڵ���������ĸ߶ȣ�
    [SerializeField] private LayerMask whatIsGround; // ����ʶ������ͼ������
    [SerializeField] protected bool isOnGround; // �Ƿ��ڵ�����
    private Vector3 groundDetectionOrigin; // �������ԭ��λ��

    // ��������ز���
    [SerializeField, Header("������")] private float SlopDetectionLenth = 1; // ����������߳���
    private ColliderHit groundHit; // ��ײ�����

    // �ƶ���������ز���
    [Range(0.2f, 100), SerializeField, Header("�ƶ�λ�Ʊ���")] private float moveMult; // �ƶ�ʱ��λ�Ʊ���
    [Range(0.2f, 60), SerializeField, Header("����λ�Ʊ���")] private float dodgeMult; // ����ʱ��λ�Ʊ���

    // ��ʼ�����
    protected virtual void Awake()
    {
        // ��ȡ��ɫ�Ķ������������
        characterAnimator = GetComponent<Animator>();
        // ��ȡ��ɫ�Ŀ��������
        characterController = GetComponent<CharacterController>();
    }

    // ��ʼ������
    protected virtual void Start()
    {
        // ��ʼ����ؼ�ʱ��
        fallOutdeltaTimer = fallOutTimer;
    }

    // ÿ֡���½�ɫ״̬
    protected virtual void Update()
    {
        GroundDetecion(); // ����Ƿ��ڵ�����
        UpdateChracterGravity(); // ���½�ɫ������״̬
        UpDateVerticalVelocity(); // ���´�ֱ������ٶ�
        UpDateVerticalVelocity(); // �ٴθ��´�ֱ�ٶȣ�����������
    }

    /// <summary>
    /// �ڶ����ƶ�ʱ���½�ɫ��λ��
    /// </summary>
    protected virtual void OnAnimatorMove()
    {
        // ����RootMotion���ý�ɫ���ݶ�����λ���ƶ�
        characterAnimator.ApplyBuiltinRootMotion();
        // ���½�ɫ��ˮƽ�ٶȣ��������е�λ�ƣ�
        UpdateCharacterVelocity(characterAnimator.deltaPosition);
    }

    /// <summary>
    /// �����⣬�жϽ�ɫ�Ƿ�վ�ڵ�����
    /// </summary>
    protected void GroundDetecion()
    {
        // ͨ��һ���������ɫ�����Ƿ�Ӵ�����
        groundDetectionOrigin = new Vector3(transform.position.x, transform.position.y - GroundDetectionOffset, transform.position.z);
        isOnGround = Physics.CheckSphere(groundDetectionOrigin, GroundDetectionRadius, whatIsGround, QueryTriggerInteraction.Ignore);
    }

    /// <summary>
    /// ���½�ɫ������Ч��
    /// </summary>
    protected void UpdateChracterGravity()
    {
        if (isOnGround)
        {
            // ����ڵ����ϣ�������ؼ�ʱ��
            fallOutdeltaTimer = fallOutTimer;

            // �����ֱ�ٶ�С��0������ɫ���½�������������Ϊ-2���Ӵ������΢С�ٶȣ�
            if (verticalSpeed < 0)
            {
                verticalSpeed = -2;
            }
        }
        else
        {
            // ������ڵ����ϣ���ʼ������ؼ�ʱ��
            if (fallOutdeltaTimer >= 0)
            {
                fallOutdeltaTimer -= Time.deltaTime;
            }
            else
            {
                // ��ɫ�ڿ��У�������������
                if (verticalSpeed < maxVerticalSpeed && verticalSpeed > minVerticalSpeed)
                {
                    verticalSpeed += characterGravity * Time.deltaTime;
                }
            }
        }
    }

    /// <summary>
    /// ���´�ֱ�����ϵ��ٶ�
    /// </summary>
    protected void UpDateVerticalVelocity()
    {
        // ���ô�ֱ������ٶ�
        verticalVelocity.Set(0, verticalSpeed, 0);
        // ���ݵ�ǰ�Ĵ�ֱ�ٶ��ƶ���ɫ
        characterController.Move(verticalVelocity * Time.deltaTime);
    }

    /// <summary>
    /// �����⣬���ڵ�����ɫ�������ϵ��ƶ��ٶ�
    /// </summary>
    /// <param name="characterVelosity">��ɫ�ĵ�ǰ�ٶ�</param>
    /// <returns>��������ٶ�</returns>
    protected Vector3 ResetVelocityOnSlop(Vector3 characterVelosity)
    {
        // ͨ�����߼���ɫ�����Ƿ�Ӵ�������
        if (Physics.Raycast(transform.position, Vector3.down, out var groundHit, SlopDetectionLenth, whatIsGround))
        {
            float newAngle = Vector3.Dot(Vector3.up, groundHit.normal);
            // ��������컨����ɫ�������£�����ɫ���ٶ�ͶӰ��������
            if (newAngle != -1 && verticalSpeed <= 0)
            {
                return Vector3.ProjectOnPlane(characterVelosity, groundHit.normal);
            }
        }
        return characterVelosity;
    }

    /// <summary>
    /// ���½�ɫ��ˮƽ�ٶ�
    /// </summary>
    /// <param name="movement">��ɫ���ƶ�����</param>
    protected virtual void UpdateCharacterVelocity(Vector3 movement)
    {
        // ��������ĽǶȵ�����ɫ���ٶ�
        Vector3 dir = ResetVelocityOnSlop(movement);

        // �������״̬�ǡ��ƶ�����������ƶ������ƶ���ɫ
        if (characterAnimator.AnimationAtTag("Movement"))
        {
            characterController.Move(dir * Time.deltaTime * moveMult);
        }
        // �������״̬�ǡ����ܡ�����������ܱ����ƶ���ɫ
        else if (characterAnimator.AnimationAtTag("Dodge"))
        {
            characterController.Move(dir * Time.deltaTime * dodgeMult);
        }
    }

    /// <summary>
    /// ���Ƶ������Gizmos�������ڱ༭���п��ӻ�����������
    /// </summary>
    private void OnDrawGizmos()
    {
        // �����ɫ�ڵ����ϣ���ʾ��ɫ�ļ������
        if (isOnGround)
        {
            Gizmos.color = Color.green;
        }
        // �����ɫ���ڵ����ϣ���ʾ��ɫ�ļ������
        else
        {
            Gizmos.color = Color.red;
        }

        // �ڽ�ɫ�ĵ�����λ�û���һ������
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - GroundDetectionOffset, transform.position.z), GroundDetectionRadius);
    }
}
