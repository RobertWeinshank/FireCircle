using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class DamageCircle : MonoBehaviour
{
    SphereCollider sphereCollider;
    Player player;

    private Transform circleTransform;
    private static DamageCircle instance;
    private float circleShrinkSpeed;
    private Vector3 circleSize;
    private Vector3 circlePosition;
    private Vector3 targetCircleSize;
    private float currentSize = 100;
    
    public bool playerOutside = false;

    private void Awake()
    {
        instance = this;
        circleShrinkSpeed = 50f;
        circleTransform = transform.Find("circle");
        InvokeRepeating("CircleShrink", 0, 5);
    }

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        player = FindFirstObjectByType<Player>();
        sphereCollider.radius = 112f;
    }

    private void Update()
    {
        Vector3 sizechangeVector = (targetCircleSize - circleSize).normalized;
        Vector3 newCircleSize = circleSize + sizechangeVector * Time.deltaTime * circleShrinkSpeed;
        SetCircleSize(circlePosition, newCircleSize);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Inside");
            player.StopFireDamage();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Outside");
            player.StartFireDamage();
        }
    }

    private void SetCircleSize(Vector3 position, Vector3 size)
    {
        circlePosition = position;
        circleSize = size;
        circleTransform.localScale = size;
    }

    private void CircleShrink()
    {
        SetCircleSize(new Vector3((float)(currentSize * .7), 1, (float)(currentSize * .7)), new Vector3(currentSize, 1, currentSize));
        targetCircleSize = new Vector3((float)(currentSize * .7), 1, (float)(currentSize * .7));
        currentSize = (currentSize * 0.7f);
        sphereCollider.radius = sphereCollider.radius * .6f;
    }
}
