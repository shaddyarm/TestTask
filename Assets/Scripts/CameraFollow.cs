// Код отвечает за слежение камеры за заданным объектом
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] private float _angularSpeed = 1f;  // Скорость вращения камеры
    [SerializeField] private Transform _target;  // Объект, за которым будет следить камера
    private float _angleY;  // Угол поворота камеры по Y-оси

    void Start()
    {
        _angleY = transform.rotation.y;  // Запоминаем текущий угол поворота
    }

    void Update()
    {
        // Проверяем нажатия клавиш Z и X
        if (Input.GetKey(KeyCode.Z))
            _angleY -= _angularSpeed;  // Уменьшаем угол поворота по Y-оси
        if (Input.GetKey(KeyCode.X))
            _angleY += _angularSpeed;  // Увеличиваем угол поворота по Y-оси

        // Устанавливаем позицию камеры равной позиции цели
        transform.position = _target.transform.position;
        // Устанавливаем угол поворота камеры по оси Y
        transform.rotation = Quaternion.Euler(0, _angleY, 0);
    }
}