using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{

    public int giftCount = 0;
    public TextMeshProUGUI giftCountText;
    void Start()
    {
        UpdateUI();
    }

    public void AddItem()
    {
        giftCount++;
        UpdateUI();
        YouWin();
    }

    public void YouWin()
    {
        if (giftCount == 3)
        {
            Debug.Log("You win!");
            SceneManager.LoadScene(0);
        }
    }
    void UpdateUI()
    {
        giftCountText.text = "Gifts: " + giftCount + "/3";
    }
}
