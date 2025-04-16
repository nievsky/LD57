using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public TextMeshProUGUI timerText;
    private bool timerIsRunning = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timerIsRunning = true;
        }
        
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else if (timeRemaining <= 0)
            {
                timerText.text = "00:00";
                timeRemaining = 0;
                timerIsRunning = false;
                TimerEnded();
            }
        }
    }
    
    void UpdateTimerUI()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnded()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Timer Ended");
    }
    
}
