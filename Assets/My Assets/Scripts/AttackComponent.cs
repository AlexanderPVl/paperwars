using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;

    [SerializeField] private string fireTargetTag;
    [SerializeField] private int range;

    private Transform fireTarget;
    private float fireCountdown;

    private void Start()
    {
        fireTarget = null;
        InvokeRepeating("UpdateFireTarget", 0f, 0.05f);
    }

    private void Update()
    {
        if (GetComponent<MovementComponent>() != null)
        {
            if (fireTarget == null)
            {
                GetComponent<MovementComponent>().StartMovement();
                return;
            }
            else
            {
                GetComponent<MovementComponent>().StopMovement();
            }
        }
        else if (fireTarget != null)
        {
            Vector3 vec = fireTarget.position - transform.position;
            transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg, Vector3.forward);
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(fireTarget);
    }


    private void UpdateFireTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(fireTargetTag);

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            fireTarget = nearestEnemy.transform;
            return;
        }
        else
        {
            fireTarget = null;
        }

        
    }
}