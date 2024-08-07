using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour
{
    public float maxHP = 100;
    public float currentHP;
    public Image Bar;
    private AudioSource deadAudio;
    private AudioSource shootAudio;
    private PlayerController playerController;
    private Transform playerTransform;
    private float speed = 2.5f; // Скорость движения
    bool IsAlive;
    private void Start()
    {
        IsAlive = true;
        playerController = FindAnyObjectByType<PlayerController>();
        if (playerController != null)
        {
            playerTransform = playerController.transform;
            // Начинаем корутину для движения к игроку
            StartCoroutine(MoveTowardsPlayer());
        }
        else
        {
            Debug.LogError("PlayerController not found!");
        }

        currentHP = maxHP;

        GameObject obj = GameObject.Find("Dead");
        deadAudio = obj.GetComponent<AudioSource>();
        if (deadAudio != null)
        {
        }

        GameObject obj1 = GameObject.Find("Shoot");
        shootAudio = obj1.GetComponent<AudioSource>();
        if (shootAudio != null)
        {
        }

    }

     public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            StartCoroutine(Dead());
            // Дополнительная логика при смерти врага
           

        }
        else
        {
            shootAudio.volume = 1;
            shootAudio.PlayOneShot(shootAudio.clip);
        }
    }
    public IEnumerator Dead()
    {
        IsAlive = false;
        deadAudio.volume = 1;
        deadAudio.Play();
        currentHP = 0;
        Bar.gameObject.SetActive(false);

        float elapsedTime = 0;
        float duration = 1.0f; // Длительность падения

        Vector3 initialRotation = transform.rotation.eulerAngles;
        Vector3 finalRotation = new Vector3(-90, initialRotation.y, initialRotation.z);

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Euler(Vector3.Lerp(initialRotation, finalRotation, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(finalRotation);
        yield return new WaitForSeconds(1.0f); // Ожидание перед удалением объекта

        Destroy(gameObject); // Удаление объекта
    }
    private void Update()
    {
        Bar.fillAmount = currentHP / 100;
    }
    private IEnumerator MoveTowardsPlayer()
    {
        while (IsAlive)
        {
            // Двигаем объект в сторону позиции игрока
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            transform.LookAt(playerTransform);

            yield return null;
        }
    }
}
