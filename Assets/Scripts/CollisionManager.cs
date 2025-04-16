using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    public string CollidedTag { get; private set; }
    
    private Light2D localLight2D;
    [SerializeField][Range(0f, 1f)] private float lerpFactor = 0.1f;

    public bool hasCollectedColor;

    private float colorProgress;
    public float ColorProgress { get { return colorProgress; } }



    void Start()
    {
        localLight2D = GetComponentInChildren<Light2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CollidedTag = other.tag;
        if (localLight2D != null)
        {
            Light2D otherLight2D = other.GetComponentInChildren<Light2D>();
            if (otherLight2D != null)
            {
                StartCoroutine(TransferColor(otherLight2D));
                StartCoroutine(DestroyObject(other.gameObject));
            }
        }
    }
    
    IEnumerator TransferColor(Light2D otherLight)
    {
        hasCollectedColor = false;
        colorProgress = 0f;

        Color startLocal = localLight2D.color;
        Color startOther = otherLight.color;

        // Gradually blend the colors
        for (float t = 0f; t < 1f; t += Time.deltaTime * 2f)
        {
            colorProgress = t;
            localLight2D.color = Color.Lerp(startLocal, startOther, t);
            otherLight.color = Color.Lerp(startOther, startLocal, t);
            yield return null;
        }

        // Final update
        localLight2D.color = startOther;
        otherLight.color = startLocal;
        colorProgress = 1f;
        hasCollectedColor = true;
    }

    IEnumerator DestroyObject(GameObject gameObject)
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
