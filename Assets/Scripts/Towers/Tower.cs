using UnityEngine;
public class Tower : MonoBehaviour
{
    //Cached instance variables
    private Transform target;
    private BasicEnemy targetEnemy;

    [Header("General")]
    public float range = 15f;
    public int upgradeCost;
    public int price = 0;

    [HideInInspector]
    public int sellPrice = 0;

    [Header("Use Projectiles(defualt)")]
    public float fireRate = 10f;
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
    public GameObject upgradePrefab;
    public string targetMethod;
    
    

    //static instances
    BuildManager buildManager;
    TowerSelectManager selectManager;

    private void Awake()
    {
        targetMethod = "TargetFirst";
        sellPrice = price / 2;
    }
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
        selectManager = TowerSelectManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        TargetEnemy(targetMethod);
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
        PlayerStats.Gold += sellPrice;
        Destroy(gameObject);
    }

    public void UpgradeTower()
    {
        
        if (PlayerStats.Gold>upgradeCost & upgradePrefab!=null)
        {
            Debug.Log("Tower Upgraded");
            PlayerStats.Gold -= upgradeCost;

            GameObject upgradedTower = (GameObject)Instantiate(upgradePrefab, transform.position, Quaternion.identity);
            selectManager.currentlySelected = upgradedTower;
            //Sets the sell price of the newly created upgrade to half the cost of all previous upgrades
            Tower upgradeScript = upgradedTower.GetComponent<Tower>();
            upgradeScript.sellPrice = upgradeScript.sellPrice =sellPrice + upgradeCost/2;

            Destroy(gameObject);
        }
        else{
            Debug.Log("Tower Not Upgraded");
        }
    }
    //Determines what targeting method is being used and calls the appropraite method.
    void TargetEnemy(string targetCase)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        switch (targetCase)
        {
            //Targets closest enemy in range
            case "TargetClosest":
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

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
                break;
            //Targets the enemy with the most health in range
            case "TargetStrong":
                float mostHealth = 0f;
                GameObject strongestEnemy = null;
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                    BasicEnemy enemyScript = enemy.GetComponent<BasicEnemy>();
                    if (distanceToEnemy <= range && enemyScript.currentHealth > mostHealth)
                    {
                        Debug.Log("Reached");
                        mostHealth = enemyScript.currentHealth;
                        strongestEnemy = enemy;
                    }
                }
                if (strongestEnemy != null)
                {

                    if (target != null)
                    {
                        targetEnemy.underFire = false;
                    }
                    target = strongestEnemy.transform;
                    targetEnemy = strongestEnemy.GetComponent<BasicEnemy>();

                }
                else
                {
                    if (target != null)
                    {
                        targetEnemy.underFire = false;
                    }
                    target = null;
                }
                break;
            //Targets Last enemy in range
            case "TargetLast":
                float leastTraveled = Mathf.Infinity;
                GameObject lastEnemy = null;
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                    EnemyMovement mvmtScrpt = enemy.GetComponent<EnemyMovement>();
                    if (distanceToEnemy <= range && mvmtScrpt.distanceTraveled < leastTraveled)
                    {
                        Debug.Log("Reached");
                        leastTraveled = mvmtScrpt.distanceTraveled;
                        lastEnemy = enemy;
                    }
                }
                if (lastEnemy != null)
                {

                    if (target != null)
                    {
                        targetEnemy.underFire = false;
                    }
                    target = lastEnemy.transform;
                    targetEnemy = lastEnemy.GetComponent<BasicEnemy>();

                }
                else
                {
                    if (target != null)
                    {
                        targetEnemy.underFire = false;
                    }
                    target = null;
                }
                break;
            //Targets First enemy in range
            default:
                float mostTraveled=0f;
                GameObject firstEnemy=null;
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                    EnemyMovement mvmtScrpt = enemy.GetComponent<EnemyMovement>();
                    if (distanceToEnemy <= range && mvmtScrpt.distanceTraveled > mostTraveled)
                    {
                        mostTraveled = mvmtScrpt.distanceTraveled;
                        firstEnemy = enemy;
                    }
                }
                if (firstEnemy != null)
                {

                    if (target != null)
                    {
                        targetEnemy.underFire = false;
                    }
                    target = firstEnemy.transform;
                    targetEnemy = firstEnemy.GetComponent<BasicEnemy>();

                }
                else
                {
                    if (target != null)
                    {
                        targetEnemy.underFire = false;
                    }
                    target = null;
                }
                break;
        }
        
    }

    private void OnMouseDown()
    {
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
