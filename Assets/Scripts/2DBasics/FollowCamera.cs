using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        Vector2 targetPosition = new Vector2(_target.position.x, _target.position.y);
        Vector2 currentPosition = new Vector2(_transform.position.x, _transform.position.y);
        Vector2 movement = targetPosition - currentPosition;

        Vector2 result = currentPosition + movement * _speed * Time.deltaTime;
        _transform.position = new Vector3(result.x, result.y, _transform.position.z);
    }
}
