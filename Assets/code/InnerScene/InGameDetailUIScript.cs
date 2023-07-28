using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameDetailUIScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InitData();
        InitPauseMenu();
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
            LeanTween.scale(pauseMenuBoard.gameObject, new Vector2(1.8f, 1.8f), 1f).setEaseInElastic();

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
