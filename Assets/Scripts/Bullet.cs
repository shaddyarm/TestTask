// Класс, отвечающий за управление пулями
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SphereCollider _sphereCollider; // Ссылка на компонент SphereCollider
    private Transform _spherePosition; // Ссылка на компонент Transform
    [SerializeField] float _bulletSpeed; // Скорость пули

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>(); // Получаем компонент SphereCollider
        _spherePosition = GetComponent<Transform>(); // Получаем компонент Transform
    }

    private void FixedUpdate()
    {
        _spherePosition.Translate(new Vector3(0, 0, _bulletSpeed)); // Перемещаем пулю вперед
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obsticle") // Если пуля столкнулась с препятствием
            Destroy(this.gameObject); // Уничтожаем пулю
        else if (other.gameObject.tag == "Enemy") // Если пуля столкнулась с врагом
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("GetHit"); // Воспроизводим анимацию получения урона у врага
            Destroy(this.gameObject); // Уничтожаем пулю
            other.gameObject.GetComponent<Enemy>().GetHit(); // Вызываем метод получения урона у врага
        }
    }
}