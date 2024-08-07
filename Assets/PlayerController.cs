using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed = 720.0f; // Скорость поворота в градусах в секунду

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
        // Получение ввода по осям
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Перемещение персонажа
        Vector3 movement = new Vector3(0, 0, vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // Поворот персонажа
        if (horizontal != 0)
        {
            transform.Rotate(0, horizontal * turnSpeed * Time.deltaTime, 0);
        }

        // Проверка на отсутствие ввода для остановки движения
        if (horizontal == 0 && vertical == 0)
        {
            rb.velocity = Vector3.zero; // Остановка движения
            Animator.SetTrigger("Idle");
            Animator.ResetTrigger("RunForward");
            Debug.Log("на месте");
        }
        else if (vertical != 0)
        {
            Animator.ResetTrigger("Idle");
            Animator.SetTrigger("RunForward");
            Debug.Log("вперед");
        }

        // Выравнивание персонажа по вертикальной оси
        AlignCharacter();
    }

    void AlignCharacter()
    {
        // Задать выравнивание только по оси Y, чтобы персонаж стоял ровно
        Vector3 alignedEulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        transform.eulerAngles = alignedEulerAngles;
    }
}
