using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    public static int IsWalking = Animator.StringToHash(nameof(IsWalking));

    [SerializeField] private Waypoints _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    private Queue<Transform> _forwardWaypoints = new Queue<Transform>();
    private Stack<Transform> _backwardWaypoints = new Stack<Transform>();
    private Transform _currentWaypoint;
    private float _checkInDistance = 0.2f;
    private bool _isWalking = false;

    private void Awake()
    {
        foreach (var waypoint in _waypoints.GetPath())
        {
            _forwardWaypoints.Enqueue(waypoint);
        }

        GetNextPosition();
    }

    private void Update()
    {
        if (IsWaypointReached())
        {
            if ((_forwardWaypoints.Count == 0) && (_backwardWaypoints.Count == 0))
            {
                _animator.SetBool(IsWalking, _isWalking);
                return;
            }

            GetNextPosition();
        }

        transform.LookAt(_currentWaypoint);
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void GetNextPosition()
    {
        if (_forwardWaypoints.Count == 0)
        {
            _currentWaypoint = _backwardWaypoints.Pop();
        }
        else
        {
            _currentWaypoint = _forwardWaypoints.Dequeue();
            _backwardWaypoints.Push(_currentWaypoint);
        }
    }

    private bool IsWaypointReached()
    {
        return Vector3.Distance(transform.position, _currentWaypoint.position) < _checkInDistance;       
    }
}
