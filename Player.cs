using JetBrains.Annotations;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    #region variable

    public float playerHP = 3f;
    // Movement
    public float speed = 5f;
    float moveX;
    float moveY;
    Vector2 moveInput;
    // Animation Clips
    public AnimationClip idleAnimation;
    public AnimationClip moveUpAnimation;
    public AnimationClip moveDownAnimation;
    public AnimationClip moveLeftAnimation;
    public AnimationClip moveRightAnimation;

    // Item collected count
    public int oldKeyCount = 0;
    public int newKeyCount = 0;
    public int hammerCount = 0;
    public int ragCount = 0;
    public int ropeCount = 0;
    
    private Rigidbody2D rb;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // อ่านการกดปุ่มจากผู้เล่น
        moveX = Input.GetAxis("Horizontal"); // A, D หรือ ลูกศรซ้ายขวา
        moveY = Input.GetAxis("Vertical");   // W, S หรือ ลูกศรขึ้นลง
        #region move animation
        // กำหนดแอนิเมชันตามทิศทางการเคลื่อนที่
        Animator animator = GetComponent<Animator>();
        if (moveX > 0)
        {
            animator.Play(moveRightAnimation.name);
        }
        else if (moveX < 0)
        {
            animator.Play(moveLeftAnimation.name);
        }
        else if (moveY > 0)
        {
            animator.Play(moveUpAnimation.name);
        }
        else if (moveY < 0)
        {
            animator.Play(moveDownAnimation.name);
        }
        else
        {
            animator.Play(idleAnimation.name);
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Crafting Item...");
            //Craft_Item();
        }
        moveInput = moveInput.normalized;

        

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX * speed, moveY * speed);
    }

    public void TakeDamage(float dmg)
    {
        playerHP -= dmg;
        Debug.Log("Player HP: " + playerHP);

        if (playerHP <= 0)
        {
            Debug.Log("Player Dead!");
            Destroy(gameObject);
        }
    }

    // System เดินเข้าของ (KEEP)
    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Old Key"))
        {
            Destroy(target.gameObject);
            oldKeyCount++;
            Debug.Log("Old Keys: " + oldKeyCount);
        }
        if (target.gameObject.CompareTag("Hammer"))
        {
            Destroy(target.gameObject);
            hammerCount++;
            Debug.Log("Hammers: " + hammerCount);
        }
        if (target.gameObject.CompareTag("Rag"))
        {
            Destroy(target.gameObject);
            ragCount++;
            Debug.Log("Rags: " + ragCount);
        }
    }

    // ปุ่มคราฟไอเท็ม
    public void Craft_Item()
    {
        if (oldKeyCount >= 1 && hammerCount >= 1)
        {
            newKeyCount += 1;
            oldKeyCount -= 1;
            hammerCount -= 1;
            Debug.Log("Craft New Key");
        }

        if (ragCount >= 3)
        {
            ropeCount += 1;
            ragCount -= 3;
            Debug.Log("Craft Rope");
        }
    }
}
