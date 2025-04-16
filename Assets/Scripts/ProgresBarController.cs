using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ProgresBarController : MonoBehaviour
{
    public CollisionManager collisionManager;
    public Slider[] progresBars;
    
    private bool previousAllZeroState = false;
    private int zeroCount = 0;
    
    
    
    
    void Update()
    {
        
        if (collisionManager.hasCollectedColor)
        {
            Debug.Log("Color collected!" + collisionManager.CollidedTag);
        }

        if (collisionManager.CollidedTag == "BlackTag")
        {
            progresBars[UnityEngine.Random.Range(0, progresBars.Length)].value = 0f;
        }

        foreach (var slider in progresBars)
        {
            if (slider.gameObject.CompareTag(collisionManager.CollidedTag))
            {
                slider.value = collisionManager.ColorProgress;
            }
        }
        
        if (AreAllBarsMax())
        {
            SceneManager.LoadScene(2);
            Debug.Log("All bars are full!");
        }

        // Check if all bars are at zero
        bool allZero = AreAllBarsZero();
        if (allZero && !previousAllZeroState)
        {
            zeroCount++;
            Debug.Log($"All bars zero count: {zeroCount}");
            
            if (zeroCount >= 2)
            {
                SceneManager.LoadScene(1);
                Debug.Log($"All bars zero count: {zeroCount}");
            }
        }
        previousAllZeroState = allZero;
    }

    public bool AreAllBarsZero()
    {
        foreach (var bar in progresBars)
        {
            if (bar.value > 0)
                return false;
        }
        return true;
    }

    public bool AreAllBarsMax()
    {
        foreach (var bar in progresBars)
        {
            if (bar.value < 1)
                return false;
        }
        return true;
    }
}
