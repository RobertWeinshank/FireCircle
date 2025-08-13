using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveDirection;
    
    public float health = 3;
    public float moveSpeed = 5f;

    DamageCircle damageCircle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        damageCircle = FindFirstObjectByType<DamageCircle>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void fireDamage()
    {
        health--;
        Debug.Log(health);
    }

    public void StartFireDamage()
    {
        Invoke("fireDamage", 3);
    }

    public void StopFireDamage()
    {
        CancelInvoke("fireDamage");
    }
}
