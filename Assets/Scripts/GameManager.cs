//DISCLAIMER:

// In this example, I am just using stationary meteors to keep track of the score. DO NOT attempt to reproduce any of the changes I have made to this script!
// You will mess up your project, and your METEORS WILL NOT SPAWN!!



using UnityEngine; 
using TMPro; 

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private TextMeshProUGUI scoreTMP;

    private int score = 0;

    public void AddPoint()
    {
        score++; 
        scoreTMP.text = $"Score: {score:D3}";
    }
}
