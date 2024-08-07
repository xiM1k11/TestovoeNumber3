using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;        // ������ �� ��������� ���������
    public float distance = 5.0f;   // ���������� ������ �� ���������
    public float height = 2.0f;     // ������ ������ ������������ ���������
    public float smoothSpeed = 0.125f; // �������� ����������� ����������� ������
    private Vector3 offset;          // �������� ������

    void Start()
    {
        // ������������� �������� ������
        offset = new Vector3(0, height, -distance);
    }

    void LateUpdate()
    {
        // ������� ������� ������ � ������ �������� ���������
        Vector3 desiredPosition = player.position + player.rotation * offset;

        // ����������� ����������� ������
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // ������� ������ � ������� ���������
        transform.LookAt(player.position + Vector3.up * height);
    }
}
