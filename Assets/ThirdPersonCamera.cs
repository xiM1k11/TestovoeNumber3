using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;        // Ссылка на трансформ персонажа
    public float distance = 5.0f;   // Расстояние камеры от персонажа
    public float height = 2.0f;     // Высота камеры относительно персонажа
    public float smoothSpeed = 0.125f; // Скорость сглаживания перемещения камеры
    private Vector3 offset;          // Смещение камеры

    void Start()
    {
        // Инициализация смещения камеры
        offset = new Vector3(0, height, -distance);
    }

    void LateUpdate()
    {
        // Целевая позиция камеры с учетом вращения персонажа
        Vector3 desiredPosition = player.position + player.rotation * offset;

        // Сглаживание перемещения камеры
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Поворот камеры в сторону персонажа
        transform.LookAt(player.position + Vector3.up * height);
    }
}
