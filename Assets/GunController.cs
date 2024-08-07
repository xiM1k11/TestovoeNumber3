using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Camera playerCamera;
    public RectTransform crosshair;
    public float damage = 10f;
    public float range = 100f;
    public LayerMask enemyLayer;
    public GameObject hitEffectPrefab;

    private void Start()
    {
    }
    private void Update()
    {
        Aim();
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Aim()
    {
        
        // Перемещаем прицел в позицию курсора мыши
        Vector3 mousePosition = Input.mousePosition;
        crosshair.position = mousePosition;

        // Создаем луч из камеры в позицию прицела
        Ray ray = playerCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, enemyLayer))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
    }

    private void Shoot()
    {
        Ray ray = playerCamera.ScreenPointToRay(crosshair.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, enemyLayer))
        {

            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage((int)damage);
                GameObject hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));

                // Делаем партикл дочерним объектом врага
                hitEffect.transform.parent = hit.transform;

                // Уничтожаем партикл через 0.5 секунд
                Destroy(hitEffect, 0.5f);
            }
        }
    }
}
