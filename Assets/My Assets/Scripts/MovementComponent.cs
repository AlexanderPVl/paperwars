using UnityEngine;
using Pathfinding;


public class MovementComponent : MonoBehaviour
{

    [SerializeField] private Transform targetPoint;
    [SerializeField] private float stopDistance;

    private Seeker seeker;
    private Rigidbody2D rb;
    private Transform target;

    private bool isStopped;

    private void Start()
    {
        Init();
        //InvokeRepeating("CheckTarget", 0f, 0.1f);
    }

    private void Update()
    {
        if (isStopped == true)
            return;

        Vector3 dir = targetPoint.transform.position - transform.position;

        if (dir.magnitude <= stopDistance)
        {
            Debug.Log("Stop");
            isStopped = true;
            GetComponent<AIPath>().maxSpeed = 0f;
        }
            
    }

    private void Init()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        isStopped = false;
        seeker.StartPath(transform.position, targetPoint.transform.position, OnPathCompleted);
        //ResetTarget();
    }

    private void OnPathCompleted(Path path)
    {

    }

    private void CheckTarget()
    {
        if (target == null)
        {
            //gameObject.GetComponent<AttackComponent>().SearchTarget();
        }
    }

    public void Stop()
    {
        //navMeshAgent.isStopped = true;
    }

    public void Resume()
    {
        /*
        if (!navMeshAgent.isActiveAndEnabled)
        {
            Debug.Log("Disabled");
            return;
        }

        navMeshAgent.isStopped = false;
        */
    }

    public void SetTarget(Transform _target)
    {
        /*
        target = _target;
        if (!navMeshAgent.isActiveAndEnabled)
            return;
        navMeshAgent.SetDestination(target.position);
        */
    }

    public Transform GetTarget()
    {
        return target;
    }


    public void SetTargetPoint(Transform _target)
    {
        targetPoint = _target;
    }

    public void ResetTarget()
    {
        target = targetPoint;
    }

}
