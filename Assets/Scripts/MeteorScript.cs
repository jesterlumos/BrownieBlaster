//DISCLAIMER:

// In this example, I am just using stationary meteors to keep track of the score. DO NOT attempt to reproduce any of the changes I have made to this script!
// You will mess up your project, and your METEORS WILL NOT SPAWN!!


using UnityEngine; 

[RequireComponent(typeof(Rigidbody2D))]
public class MeteorScript : MonoBehaviour
{
    [SerializeField] private float speed; 

    private GameManager gameManager;

    private void Awake() => gameManager = FindFirstObjectByType<GameManager>();
 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile")) 
        {
            Destroy(other.gameObject);  
            gameManager.AddPoint(); 
        }
    }
}
