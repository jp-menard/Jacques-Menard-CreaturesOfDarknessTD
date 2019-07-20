using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float totalHealth=100f;
    
    public int armor = 0;
    
    public float startSpeed = 2f;
    [HideInInspector]
    public float speed;
    public float currentHealth;
    public int bounty = 25;
    public int livesDamage = 1;

    [HideInInspector]
    public bool underFire = false;
    private Renderer rend;

    void Awake()
    {
        currentHealth = totalHealth;
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        speed = startSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        //sets values to defualt when no longer being attacked
        if (underFire == false)
        {
            speed = startSpeed;
            rend.material.color = Color.white;
        }
        
    }

    public void TakeDamage(float amount)
    {
        float armorCalc = ((100f - armor) / 100f);
        currentHealth -= armorCalc * amount;

        if (currentHealth<=0)
        {
            //TODO add animation.
            Death();
        }
        
    }

    public void Slow(float slowAmount)
    {
        speed = startSpeed*(1f-slowAmount);
        rend.material.color= Color.blue;
    }

    void Death()
    {
        PlayerStats.Gold += bounty ;
        Destroy(gameObject);
    }
}
