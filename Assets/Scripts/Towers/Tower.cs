using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 10f;
    public int price = 0;
    private float fireCountdown = 0f;
 
    // Start is called before the first frame update
    [Header("Unity Variables")]
    private Transform target;
    public string enemyTag = "Enemy";
    public GameObject projectilePrefab;
    public Transform firePoint;

    //static instances
    BuildManager buildManager;


    void Start()
    {
        buildManager = BuildManager.instance;
        InvokeRepeating("UpdateTarget",0f,.05f);
    }
    
    //Finds the closest enemy in range and sets target
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position,enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
           
    }

    //creates a bomb object and fires it towards the current target.
    void Shoot()
    {
        GameObject projectile = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile bomb = projectile.GetComponent<Projectile>();
        
        if(bomb != null)
        {
            bomb.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);  
    }

    //Prevent player from building over your own towers.
    void OnTriggerStay2D(Collider2D other)
    {
        buildManager.SetBuildable(false);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        buildManager.SetBuildable(true);
    }
}
