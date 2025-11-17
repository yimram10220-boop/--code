using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRange = 5f;
    public float attackDamage = 1f;
    public float maxHP = 1f;

    private float currentHP;
    private Rigidbody2D rb;
    private Transform player;

    private Vector2 randomDir;
    private float changeDirectionTime = 2f;
    private float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
            ChasePlayer();
        else
            RandomWalk();
    }

    // เดินสุ่ม
    void RandomWalk()
    {
        timer += Time.deltaTime;

        if (timer >= changeDirectionTime)
        {
            randomDir = Random.insideUnitCircle.normalized;
            timer = 0f;
        }

        rb.velocity = randomDir * speed;
    }

    // ไล่ผู้เล่น
    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * chaseSpeed;
    }

    // โจมตีผู้เล่น
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Zombie Attacked Player! Damage: " + attackDamage);
            collision.GetComponent<Player>().TakeDamage(attackDamage);
        }
    }

    // รับดาเมจ (จากปืน, ไม้ ฯลฯ)
    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
            Destroy(gameObject);
    }
}
