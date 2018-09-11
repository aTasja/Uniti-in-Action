using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//поворот персонажа относительно камеры
[RequireComponent(typeof(CharacterController))]
public class RelatativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target; //объект, относительно которого будет перемещение
    public float rotSpeed = 9.0f;
    public float moveSpeed = 6.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    public float pushForce = 3.0f;

    private float _vertSpeed;

    private CharacterController _charController;
    private ControllerColliderHit _contact;
    private Animator _animator;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _vertSpeed = minFall;
        _animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Update()
    {
        Vector3 movement = Vector3.zero; //начиныем с вектора (0,0,0) непрерывно жоюавляя компоненты движения

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        if (horInput != 0 || vertInput != 0) //движение обрабатывается только при нажатии клавш со стрелками
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            Quaternion tmp = target.rotation; //сохраняем начальную ориентацию, чтобы к ней вернуться после работы с целевым объектом
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement); //преобразуем направление движения из локальных в глобальные координаты
            target.rotation = tmp;

            //transform.rotation = Quaternion.LookRotation(movement); //LookRotation вычисляет кватернион, смотрящий в этом направлении
            //чтобы не двигался рывками:
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);

        //посылаем лучи вниз, чтобы распознать есть ли поверхность под игроком
        bool hitGround = false;
        RaycastHit hit;
        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (_charController.height + _charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        if (hitGround)
        {
            if (Input.GetButtonDown("Jump"))            {
                _vertSpeed = jumpSpeed;
            }            else            {
                _vertSpeed = minFall;
                _animator.SetBool("Jumping", false);
            }
        }        else        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)            {
                _vertSpeed = terminalVelocity;
            }
            if(_contact != null)
            {
                _animator.SetBool("Jumping", true);
            }
            if (_charController.isGrounded)            {
                if (Vector3.Dot(movement, _contact.normal) < 0)                {
                    movement = _contact.normal * moveSpeed;
                }                else                {
                    movement += _contact.normal * moveSpeed;
                }
            }
        }

        movement.y = _vertSpeed;
        movement *= Time.deltaTime;
        _charController.Move(movement);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;
        if(body !=null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }

}
