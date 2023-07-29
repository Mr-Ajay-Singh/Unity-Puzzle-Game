using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameDetailUIScript : MonoBehaviour
{
    // Start is called before the first frame update

    private TextMeshProUGUI levelText;

    void Start()
    {
        InitData();
        InitPauseMenu();
        InitCoinCount();
        InitGlobalUI();
        //InitHelpClicks();
    }

    private void InitGlobalUI()
    {
        levelText = gameObject.transform.Find("InGameCanvas").Find("LevelText").GetComponent<TextMeshProUGUI>();
    }

    private void InitCoinCount()
    {
        var cointText = gameObject.transform.Find("InGameCanvas").Find("CoinsCollection").Find("CoinText").GetComponent<TextMeshProUGUI>();
        cointText.SetText(InitPrefsData.getInt(InitPrefsData.coins).ToString());
    }

    private void InitPauseMenu()
    {
        var resumeButton = gameObject.transform.Find("PauseMenu").Find("Board").Find("Resume");
        var mainMenuButton = gameObject.transform.Find("PauseMenu").Find("Board").Find("Main Menu");
        var pauseMenu = gameObject.transform.Find("PauseMenu");
        var pauseMenuBoard = gameObject.transform.Find("PauseMenu").Find("Board");

        resumeButton.GetComponent<Button>().onClick.AddListener(() => {
            pauseMenu.gameObject.SetActive(false);
        });

        mainMenuButton.GetComponent<Button>().onClick.AddListener(() => {
            LeanTween.scale(pauseMenuBoard.gameObject, new Vector2(0.5f, 0.5f), 1f).setEaseOutBack().setOnComplete(() =>
            {
                SceneManager.LoadScene(0);
            });
        });

    }

    private void InitData()
    {
        var pauseButton = gameObject.transform.Find("InGameCanvas").Find("Pause");

        var pauseMenu = gameObject.transform.Find("PauseMenu");
        pauseMenu.gameObject.SetActive(false); 

        var pauseMenuBoard = gameObject.transform.Find("PauseMenu").Find("Board");

        pauseButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            pauseMenu.gameObject.SetActive(true);
            LeanTween.scale(pauseMenuBoard.gameObject, new Vector2(1.2f, 1.2f), 1f).setEasePunch();

        });
    }

    // Update is called once per frame
    void Update()
    {
        InitCoinCount();
        SetLeveText();
    }

    private void SetLeveText()
    {
        levelText.SetText("Level : "+InitPrefsData.getInt(InitPrefsData.level).ToString());
    }
}
