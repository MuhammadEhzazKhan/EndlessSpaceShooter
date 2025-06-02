using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex;
   
    private void Start()
    {
        target = GameManager.main.path[0];
    }

    private void Update() 
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.2f)
        {
            pathIndex++;

            if (pathIndex == GameManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }else
            {
                target = GameManager.main.path[pathIndex];
            }
        }
    }
    private void FixedUpdate() 
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed * Time.deltaTime;
    }
// yeh bhi istemaal kr sakty path ki jagha
    // public static Transform[] points;

    // void Awake() {
    //     points = new Transform[transform.childCount];
    //     for (int i = 0; i < points.Length; i++)
    //     {
    //         points[i] = transform.GetChild(i);
    //     }
    // }
}