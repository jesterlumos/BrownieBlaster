using UnityEngine;

// We need this namespace for List
using System.Collections.Generic; 

// And these two for the example testing GUI; don't include these if you aren't using them
using TMPro; 
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
    #region Variable Declarations
    [SerializeField] private GameObject projectilePrefab; 

    [Header("Bullet Pattern Parameters")] // The Header attribute adds an organizational header in the Inspector
    [SerializeField] private float bulletSpeed;
    [SerializeField, Tooltip("How many bullets should be spawned per shot?")] private int count;
    [SerializeField, Tooltip("How far should bullets be spread from each other?")] private float spread;
    #endregion
    
    // This uses our old Input Manager implementation
    void Update()
    {
        if (Input.GetButtonDown("Jump")) 
            SpreadBullets(count, spread); // However you handle input, make sure your fire action calls this SpreadBullets() method!
    }

    #region Bullet Firing
    // We separate the spread logic from the instantiation logic for legibility
    // SpreadBullets() will calculate the correct rotation for each bullet and feed it to this method
    private void FireBullet(Quaternion rotation)
    {
        GameObject bullet = Instantiate(projectilePrefab, transform); 
        // Instantiate() returns the GameObject, so we can store a reference to it
        // This allows us to modify the rotation and velocity after spawning it, like so:

        bullet.transform.rotation = rotation; 
        if (bullet.TryGetComponent(out Rigidbody2D rigidBodyReference)) rigidBodyReference.linearVelocity = bullet.transform.up * bulletSpeed; 
        // We use TryGetComponent() to apply velocity to our bullets only if Unity can find a RigidBody2D component
    }

    // This is the actual spread algorithm
    private void SpreadBullets(int count, float spread)
    {
        // First we figure out which bullet should be in the middle (or half bullet for an even count)
        int median = count / 2;
        List<float> offsets = new(); // The List<> datatype allows us to store a collection of our offsets, rather than just one

        for (int i = 1; i <= count; i++) // This syntax looks scary, but a for loop just executes code on each item it loops through
        {
            float offset = median - i; // First, determine how far this specific bullet is from the median bullet
            if (count % 2 == 0) offset += 0.5f; // Then determine the appropriate stagger based on whether the number is even or odd
            else                offset += 1f;
            offsets.Add(offset * spread); // And finally, we can multiply it by our spread and add it to our List
        }

        foreach (float offset in offsets) { // foreach is similar to for, but easier to understand, taking advantage of our List
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(0, 0, offset); 
            // Our rotation is currently in Euler Angles, but we need a Quaternion. Don't worry about what this means for now.

            FireBullet(rotation); // This is where we pass our calculated rotation values to FireBullet()
        }
    } 
    #endregion 

    #region Example Debugging GUI
    // NOTE FOR STUDENTS:
    // This is just to make the example sliders work, you don't need to worry about this unless you are using something similar

    [Header("Example Debugging GUI")]
    [SerializeField] private Slider countSlider;
    [SerializeField] private Slider spreadSlider;

    [SerializeField] private TMP_Text countTMP;
    [SerializeField] private TMP_Text spreadTMP;

    public void UpdateValuesFromSliders()
    {
        count  = (int) countSlider.value;
        spread =       spreadSlider.value;

        countTMP.text  = $"Count:  {count}";
        spreadTMP.text = $"Spread: {(int) spread}";
    }
    #endregion
}