using UnityEngine;
using Pathfinding;
using System.Collections;

public enum enemyType { Normal, Ranged };

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class enemyAI : MonoBehaviour
{

    public Transform target;
    public float updateRate = 2f;
    public enemyType type;
    public GameObject healthPickup;

    private Seeker seeker;
    private Rigidbody2D rb;

    public Path path;
    [Range(700, 3000)]
    public float speed = 700f;
    [Range(0, 200)]
    public float followDistance = 1f;
    [Range(10, 1000)]
    public float shootDistance = 200f;
    public ForceMode2D fMode;
    public int health = 100;
    [HideInInspector]
    public bool pathIsEnded = false;
    public float nextWaypointDistance = 3;
    private int currentWaypoint = 0;
    private float playerDist = 300;
    bool isShooting = false;
    public bool isAttacking = false;

    public GameObject weapon;


    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(UpdatePath());


    }

    IEnumerator UpdatePath()
    {
        yield return new WaitForSeconds(1f / updateRate);

        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        seeker.StartPath(transform.position, target.position, OnPathComplete);
        StartCoroutine(UpdatePath());

    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        if (playerDist > 200)
            return;

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;

            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        dir *= speed * Time.fixedDeltaTime;

        if (playerDist > followDistance)
        {
            rb.AddForce(dir, fMode);
        }

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Die();
        }

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        playerDist = Vector3.Distance(transform.position, target.transform.position);

        if (playerDist > shootDistance)
        {
            StopCoroutine("Fire");
            isShooting = false;
            return;
        }
        else
        {
            // check if we are in view of target

            if (type == enemyType.Ranged)
            {

                //TODO: add raycast check

                // shoot at target!

                if (isShooting)
                {
                    return;
                }
                else
                {
                    StartCoroutine("Fire");
                }
            }
            else
            {
                if (isAttacking)
                {
                    return;
                }
                else
                {
                    StartCoroutine("Attack");
                }
            }
        }
    }

    IEnumerator Fire()
    {
        isShooting = true;
        Vector3 dir = target.position - transform.position;
        Instantiate(weapon, transform.position + dir.normalized, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        isShooting = false;
        StartCoroutine("Fire");
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        Vector3 dir = target.position - transform.position;
        rb.AddForce(dir / 10, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(2f);
        isShooting = false;
        StartCoroutine("Attack");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Bullet")
        {
            health -= 25;
            Vector3 dir = (transform.position - col.transform.position);
            rb.AddForce(dir.normalized * 10, ForceMode2D.Impulse);
            Destroy(col.transform.gameObject, 0.2f);
        }
        else if (col.transform.tag == "Player")
        {
            if (!isAttacking)
            {

                Vector3 dir = (transform.position - col.transform.position);
                col.transform.GetComponent<Rigidbody2D>().AddForce(-dir.normalized * 100, ForceMode2D.Impulse);

            }

        }
    }

    void Die()
    {
        StopAllCoroutines();
        health = 9999;
        GetComponent<AudioSource>().Play();
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        int h = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().health;

        if (h < 100 || PlayerPrefs.GetInt("Armor") == 1 && h < 200)
        {
            float r = Random.Range(0, 4);

            if (r < 1)
            {
                Instantiate(healthPickup, transform.position, Quaternion.identity);
            }
        }

        Destroy(this.gameObject, 1);
    }
}