using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Transform[] _target;
    [SerializeField] private float distanceToChangeGoal;
    [SerializeField] private int _currentTarger;

    private NavMeshAgent _agent;
    [SerializeField] private float _timer;
    private bool _isDanceActive;
    private Animator _anim;

    private void Start()
    {
        _isDanceActive = false;
        _timer = 0f;
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        GetPoint();
        if(_isDanceActive == true)
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0)
            {
                _anim.SetBool("IsDance", false);
                Continue();
            }
        }
    }

    public void Stoped()
    {
        _agent.speed = 0f;
    }

    public void Continue()
    {
        _agent.speed = 7f;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stay"))
        {
            _isDanceActive = true;
            Stoped();
        }

        if (other.CompareTag("Dance"))
        {
            _anim.SetBool("IsDance", true);
            _isDanceActive = true;
            Stoped();
        }

        if (other.CompareTag("Security"))
        {
            _timer = 5f;
        }
    }
}
