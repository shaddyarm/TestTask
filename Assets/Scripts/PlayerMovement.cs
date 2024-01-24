using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Обязательный компонент для нашего игрока
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera; // Трансформ камеры для ориентации движения игрока
    [SerializeField] private MovementCharacteristics _characteristics; // Характеристики движения игрока
    [SerializeField] private Transform bulletSpawner; // Место, откуда будет вылетать пуля
    [SerializeField] private GameObject bullet; // Объект пули
    [SerializeField] private GameObject skill; // Объект навыка 
    [SerializeField] private Transform skillspawner; // Место, откуда будет выполняться навык
    private float _vertical, _horizontal; // Значения для вертикального и горизонтального ввода
    private const float _distanceOffsetCamera = 5f; // Расстояние до камеры, ориентированное на игрока
    private CharacterController _controller; // Ссылка на компонент CharacterController
    private Animator _animator; // Ссылка на компонент Animator
    private Vector3 _direction; // Направление движения игрока
    private Quaternion _look; // Направление взгляда игрока
    // Вычисляемая переменная для определения места поворота игрока
    private Vector3 TargetRotate => _camera.forward * _distanceOffsetCamera;
    private bool Idle => _horizontal == 0.0f && _vertical == 0.0f; // Проверка, находится ли игрок в состоянии покоя
    private bool isAttacking = false; // Переменная для контроля, атакует ли игрок в данный момент
    private bool skillOnCooldown = false; // Переменная для определения, находится ли навык в режиме восстановления

    // Инициализация компонентов при запуске
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Вызывается на каждом кадре для обновления физики
    private void FixedUpdate()
    {
        Movement(); // Вызов метода для управления движением игрока
        Rotate(); // Вызов метода для управления поворотом игрока
        StartCoroutine(Attack()); // Вызов метода для выполнения атаки
        StartCoroutine(UseSkill()); // Вызов метода для использования навыка
    }

    // Метод для управления движением игрока
    private void Movement()
    {
        // Проверка, находится ли игрок на земле
        if (_controller.isGrounded)
        {
            _horizontal = Input.GetAxis("Horizontal"); // Получение ввода для горизонтального движения
            _vertical = Input.GetAxis("Vertical"); // Получение ввода для вертикального движения

            _direction = transform.TransformDirection(_horizontal, 0, _vertical); // Определение направления движения игрока

            PlayAnimation(); // Вызов метода для управления анимацией игрока
            Jump(); // Вызов метода для выполнения прыжка
        }
        _direction.y -= _characteristics.Gravity * Time.fixedDeltaTime; // Применение гравитации к движению игрока

        Vector3 dir = _direction * _characteristics.MovementSpeed * Time.fixedDeltaTime; // Рассчет направления и скорости движения игрока
        _controller.Move(dir); // Применение движения к игроку
    }

    // Метод для управления поворотом игрока
    private void Rotate()
    {
        if (Idle) return; // Если игрок в состоянии покоя, то выход из метода
        Vector3 target = TargetRotate; // Определение целевого места поворота игрока
        target.y = 0;

        _look = Quaternion.LookRotation(target); // Определение направления взгляда игрока

        float speed = _characteristics.AngularSpeed * Time.fixedDeltaTime; // Определение скорости поворота игрока
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _look, speed); // Применение поворота к игроку
    }

    // Метод для управления анимацией игрока
    private void PlayAnimation()
    {
        _animator.SetFloat("Horizontal", _horizontal);
        _animator.SetFloat("Vertical", _vertical);
    }

    // Метод для выполнения прыжка
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
            _direction.y += 2f;
        }
    }

    // Метод для выполнения атаки
    private IEnumerator Attack()
    {
        if (isAttacking)
            yield break; // Если игрок уже атакует, то выход из метода
        else if (Input.GetMouseButton(0) && !isAttacking)
        {
            isAttacking = true; // Установка флага атаки
            Instantiate(bullet, bulletSpawner); // Создание пули
            _animator.SetTrigger("Attack"); // Вызов анимации атаки
            yield return new WaitForSeconds(1f); // Временной интервал между атаками
            isAttacking = false; // Сброс флага атаки
        }
    }

    // Метод для использования навыка
    private IEnumerator UseSkill()
    {
        if (skillOnCooldown)
            yield break; // Если навык находится в режиме восстановления, то выход из метода
        else if (Input.GetKey(KeyCode.F))
        {
            skillOnCooldown = true; // Установка флага восстановления навыка
            Instantiate(skill, skillspawner); // Вызов использования навыка
            _animator.SetTrigger("Skill"); // Вызов анимации использования навыка
            yield return new WaitForSeconds(8f); // Время восстановления навыка
            skillOnCooldown = false; // Сброс флага восстановления навыка
        }
    }
}