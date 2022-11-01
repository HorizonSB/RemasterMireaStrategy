using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float _speed = 1;
    public float _radius = 10;
    public Transform _target;
    public bool IsCameraMoving = false;

    private Touch _touch;
    private Vector3 _targetPos;

    private void Start()
    {
        if (_target == null)
        {
            _target = this.transform;
        }

        _targetPos = _target.position;
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {
                Vector3 movePos = new Vector3(
                    transform.position.x + _touch.deltaPosition.x * _speed * -1 * Time.deltaTime,
                    transform.position.y,
                    transform.position.z + _touch.deltaPosition.y * _speed * -1 * Time.deltaTime);

                Vector3 distance = movePos - _targetPos;

                if (distance.magnitude < _radius)
                    transform.position = movePos;
                IsCameraMoving = true;
            }
        }
        else IsCameraMoving = false;
    }
}