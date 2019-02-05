using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float totalHealth=100f;
    public int bounty = 25;
    public int armor = 1;
    public bool damaged = false;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = totalHealth;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        //TODO Implement an armor calculation
        float armorCalc = (1);
        currentHealth -= armorCalc * amount;

        if (currentHealth<=0)
        {
            //TODO replace with animation.
            Die();
        }
        
    }

    void Die()
    {
        PlayerStats.Gold += bounty ;
        Destroy(gameObject);
    }
}
