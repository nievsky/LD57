using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    [Header("Intro screen")]
    [Tooltip("Elements of UI in sequence")]
    public List<GameObject> prologueScreens;

    [Header("Switch settings")]
    [Tooltip("If false, screens will switch automatically after a delay")]
    public bool waitForInput = false;
    [Tooltip("Delay between screens in seconds")]
    public float delayBetweenScreens = 3f;

    private int currentScreenIndex = 1;
    private bool isTransitioning = false;
    
    [Header("Transition to the next scene")]
    [Tooltip("Scene to load after the prologue")]
    public string nextSceneName = "SampleScene";
    private AsyncOperation asyncLoad;
    private bool readyToActivate = false;

     void Start()
    {
        // Отключаем все экраны пролога
        foreach (GameObject screen in prologueScreens)
        {
            screen.SetActive(false);
        }

        // Если список не пустой – включаем первый экран
        if (prologueScreens.Count > 0)
        {
            currentScreenIndex = 0;
            prologueScreens[currentScreenIndex].SetActive(true);
            
            if (!waitForInput)
            {
                StartCoroutine(ShowNextScreenAfterDelay());
            }
        }
        else
        {
            Debug.LogWarning("Список экранов пролога пуст!");
        }

        // Запускаем асинхронную загрузку следующей сцены в фоне
        StartCoroutine(LoadNextSceneAsync());
    }

    IEnumerator LoadNextSceneAsync()
    {
        asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        // Запрещаем автоматическую активацию загруженной сцены
        asyncLoad.allowSceneActivation = false;

        // Ждём, пока загрузка не достигнет 90%
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        // Ожидаем, пока не завершится пролог
        while (!readyToActivate)
        {
            yield return null;
        }

        // Активация сцены происходит мгновенно
        asyncLoad.allowSceneActivation = true;
    }

    IEnumerator ShowNextScreenAfterDelay()
    {
        yield return new WaitForSeconds(delayBetweenScreens);
        ShowNextScreen();
    }

    void Update()
    {
        // Если включено ручное переключение, ждем клика мыши
        if (waitForInput && !isTransitioning && Input.GetMouseButtonDown(0))
        {
            ShowNextScreen();
        }
    }

    void ShowNextScreen()
    {
        if (currentScreenIndex < prologueScreens.Count)
        {
            isTransitioning = true;
            // Отключаем текущий экран
            prologueScreens[currentScreenIndex].SetActive(false);
            currentScreenIndex++;

            if (currentScreenIndex < prologueScreens.Count)
            {
                // Включаем следующий экран
                prologueScreens[currentScreenIndex].SetActive(true);
                if (!waitForInput)
                {
                    StartCoroutine(ShowNextScreenAfterDelay());
                }
            }
            else
            {
                // Если все экраны показаны – завершаем пролог
                OnPrologueFinished();
            }
            isTransitioning = false;
        }
    }

    void OnPrologueFinished()
    {
        Debug.Log("Пролог завершен. Переход к следующей сцене.");
        readyToActivate = true;
    }
}
