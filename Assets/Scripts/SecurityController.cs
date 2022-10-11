using UnityEngine;
using UnityEngine.AI;

public class SecurityController : MonoBehaviour
{
    [SerializeField] private Transform[] _target;
    [SerializeField] private float distanceToChangeGoal;
    [SerializeField] private int _currentTarger;

    private NavMeshAgent _agent;
    [SerializeField] private float _timer;
    private bool _isDanceActive;

    private void Start()
    {
        _isDanceActive = false;
        _timer = 0f;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        GetPoint();
        if (_isDanceActive == true)
        {
            Timer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out var _character))
        {
            _isDanceActive = true;
            _timer = 3f;
            _character.Stoped();
            _agent.speed = 0f;
        }
        else
        {
            _isDanceActive = false;
            _character.Continue();
            _agent.speed = 12f;
        }
    }

    private void Timer()
    {       
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _agent.speed = 12f;
        }
    }

    private void GetPoint()
    {
        if (_agent.remainingDistance < distanceToChangeGoal)
        {
            _currentTarger++;
            if (_currentTarger == _target.Length)
            {
                _currentTarger = 0;
            }

            _agent.destination = _target[Random.Range(0, _currentTarger)].position;
        }
    }
}
