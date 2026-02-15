using UnityEngine;

// Notice how much simpler the ProjectileScript can be if we handle rotation and velocity when spawning the projectile
public class ProjectileScript : MonoBehaviour
{    
    private void Update()
    {
        // Simple if statements with 1 line can be written without the {} body syntax
        if (transform.position.y >= 6f || transform.position.y <= -6f) Destroy(gameObject);
    }
}
