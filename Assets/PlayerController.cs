using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed = 720.0f; // �������� �������� � �������� � �������

    private Rigidbody rb;
    private bool isGrounded;
    public Animator Animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        // ��������� ����� �� ����
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ����������� ���������
        Vector3 movement = new Vector3(0, 0, vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // ������� ���������
        if (horizontal != 0)
        {
            transform.Rotate(0, horizontal * turnSpeed * Time.deltaTime, 0);
        }

        // �������� �� ���������� ����� ��� ��������� ��������
        if (horizontal == 0 && vertical == 0)
        {
            rb.velocity = Vector3.zero; // ��������� ��������
            Animator.SetTrigger("Idle");
            Animator.ResetTrigger("RunForward");
            Debug.Log("�� �����");
        }
        else if (vertical != 0)
        {
            Animator.ResetTrigger("Idle");
            Animator.SetTrigger("RunForward");
            Debug.Log("������");
        }

        // ������������ ��������� �� ������������ ���
        AlignCharacter();
    }

    void AlignCharacter()
    {
        // ������ ������������ ������ �� ��� Y, ����� �������� ����� �����
        Vector3 alignedEulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        transform.eulerAngles = alignedEulerAngles;
    }
}
