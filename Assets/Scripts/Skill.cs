using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private GameObject skillApplied; // Ссылка на объект, который будет применяться при активации скилла

    void Start()
    {
        _transform = GetComponent<Transform>(); // Получаем ссылку на компонент Transform текущего объекта
        StartCoroutine(ActivateSkill()); // Запускаем корутину активации скилла
    }

    IEnumerator ActivateSkill(float duration = 1f)
    {
        float time = 0; // Время, прошедшее с начала активации скилла
        while (time < duration) // Пока не истекла длительность активации скилла
        {
            time += Time.deltaTime; // Увеличиваем время на прошедшее с момента последнего кадра

            float t = time / duration; // Процент времени, прошедший от начала активации скилла
            float scale = Mathf.Lerp(0, 15, t); // Интерполируем между начальным и конечным значением масштаба скилла
            _transform.localScale = new Vector3(scale, scale, scale); // Применяем новый масштаб скилла

            yield return null; // Переходим на следующий кадр
        }

        time = 0; // Сбрасываем время активации
        while (time < duration) // Пока не истекла длительность активации скилла
        {
            time += Time.deltaTime; // Увеличиваем время на прошедшее с момента последнего кадра

            float t = time / duration; // Процент времени, прошедший от начала активации скилла
            float scale = Mathf.Lerp(15, 0, t); // Интерполируем между начальным и конечным значеним масштаба скилла
            _transform.localScale = new Vector3(scale, scale, scale); // Применяем новый масштаб скилла

            yield return null; // Переходим на следующий кадр
        }

        Destroy(this.gameObject); // Уничтожаем объект скилла
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") // Если столкнулись с врагом
        {
            other.GetComponent<CapsuleCollider>().radius *= 2f; // Увеличиваем радиус коллайдера врага
            GameObject skillIsApplied = Instantiate(skillApplied, other.gameObject.transform); // Создаем объект, который будет применен к врагу при активации
            other.gameObject.GetComponent<Enemy>().ApplySkill(other.gameObject, skillIsApplied); // Применяем скилл к врагу
        }
    }
}