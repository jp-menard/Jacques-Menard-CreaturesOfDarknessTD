using UnityEngine;
[RequireComponent(typeof(BasicEnemy))]

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;
    public Animator animator;
    public SpriteRenderer mSpriteRender;

    //[HideInInspector]
    public float distanceTraveled = 0f;
    private Vector2 lastPosition;

    private BasicEnemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[0];
        enemy = GetComponent<BasicEnemy>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Finds the direction to next waypoint
        Vector2 dir = target.position - transform.position;

        animator.SetFloat("VerticalDirection", dir.y);
        //flips the sprite horizontally to match movement direction`
        if (dir.x < -.5)
        {
            mSpriteRender.flipX = false;
        }
        if(dir.x > .5)
        {
            mSpriteRender.flipX = true;
        }        
        //records the distance traveled since the last frame
        distanceTraveled += Vector2.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        //moves towards next waypoint
        transform.Translate(dir.normalized * enemy.speed*Time.deltaTime,Space.World);

        if(Vector2.Distance(transform.position,target.position) <=.1f)
        {
            GetNextWaypoint();   
        }
    }

    //sets next waypoint and destroy's the enemy upon reaching the last waypoint.
    void GetNextWaypoint()
    {   
        if(waypointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    private void EndPath()
    {
        Destroy(gameObject);
        PlayerStats.TakeLives(enemy.livesDamage);
    }
}
