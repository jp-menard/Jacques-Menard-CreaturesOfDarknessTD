using UnityEngine;
public class Tower : MonoBehaviour
{
    //Cached instance variables
    private Transform target;
    private BasicEnemy targetEnemy;

    [Header("General")]
    public float range = 15f;

    [Header("Use Projectiles(defualt)")]
    public float fireRate = 10f;
    public int price = 0;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public int damageOverTime = 20;
    public float slowAmount = .3f;

 
    [Header("Unity Variables")] 
    public string enemyTag = "Enemy";
    public GameObject projectilePrefab;
    public Transform firePoint;
    

    //static instances
    BuildManager buildManager;
    TowerSelectManager selectManager;


    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
        selectManager = TowerSelectManager.instance;
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
            //sets the old target as no longer under fire
            if (target != null)
            {
                targetEnemy.underFire = false;
            }
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<BasicEnemy>();
        }
        else
        {
            //sets the last target to leave range as no longer under fire.
            if (target != null)
            {
                targetEnemy.underFire = false;
            }
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                lineRenderer.enabled = false;
            }
            return;
        }

        if (useLaser)
        {
            Laser();
            targetEnemy.underFire = true;
        }

        else { 
        
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
         }
    }

    void Laser()
    {
        //handle damage and slow
        targetEnemy.TakeDamage(damageOverTime*Time.deltaTime);
        targetEnemy.Slow(slowAmount);
        
        //create line effect
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0,transform.position);
        lineRenderer.SetPosition(1, target.position);

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

    public void SellTower()
    {
        PlayerStats.Gold += price/2;
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Debug.Log("Tower Selected");
        selectManager.SelectTower(gameObject, transform.position);
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
