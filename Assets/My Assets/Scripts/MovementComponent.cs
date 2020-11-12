using UnityEngine;
using Pathfinding;


public class MovementComponent : MonoBehaviour
{

    [SerializeField] private Vector3 targetPoint;
    [SerializeField] private float stopDistance;
    [SerializeField] private float maxSpeed;

    private Seeker seeker;
    private Rigidbody2D rb;
    private Vector3 target;

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

        Vector3 dir = targetPoint - transform.position;

        if (dir.magnitude <= stopDistance)
        {
            //Debug.Log("Stop");
            StopMovement();
        }
            
    }

    private void Init()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        StopMovement();
        //seeker.StartPath(transform.position, targetPoint.transform.position, OnPathCompleted);
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

    public void StopMovement()
    {
        isStopped = true;
        GetComponent<AIPath>().maxSpeed = 0f;
    }

    public void StartMovement()
    {
        isStopped = false;
        GetComponent<AIPath>().maxSpeed = maxSpeed;
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

    public Vector3 GetTarget()
    {
        return target;
    }


    public void SetTargetPoint(Vector3 _target)
    {
        targetPoint = _target;
        seeker.StartPath(transform.position, targetPoint, OnPathCompleted);
        StartMovement();
    }

    public void ResetTarget()
    {
        target = targetPoint;
    }

}
