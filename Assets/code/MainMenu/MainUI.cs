using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField]
    GameObject gameCanvas;

    [SerializeField]
    GameObject settingCanvas;

    // Start is called before the first frame update
    void Start()
    {
        var startButton = gameCanvas.transform.Find("NextButton").gameObject.GetComponent<RectTransform>();
        startButton.localScale = new Vector2(0.05f, 0.05f);
        LeanTween.scale(startButton, new Vector2(0.15f, 0.15f), 1f).setEase(LeanTweenType.easeInOutCubic).setOnComplete(()=>{
            LeanTween.scale(startButton, new Vector2(0.1f, 0.1f), 1.5f).setEase(LeanTweenType.easeOutElastic);
        });

        var coinImage = gameCanvas.transform.Find("Board").Find("NameComp").Find("CoinImage").gameObject.GetComponent<RectTransform>();
        LeanTween.rotateAround(coinImage, Vector3.up, 180f, 1.5f).setEase(LeanTweenType.easeInBounce);

        var catchThe = gameCanvas.transform.Find("Board").Find("NameComp").Find("CatchThe").gameObject.GetComponent<RectTransform>();
        catchThe.position = new Vector2(-230f,catchThe.position.y);


        var match = gameCanvas.transform.Find("Board").Find("NameComp").Find("Match").gameObject.GetComponent<RectTransform>();
        match.position = new Vector2(140f, match.position.y);

        LeanTween .moveX(catchThe,0f, 1f).setEase(LeanTweenType.easeInOutCubic).setOnComplete(() =>
        {
            LeanTween.moveX (match, 0f, 1f).setEase(LeanTweenType.easeInOutCubic);
        });

        var nextButton = gameCanvas.transform.Find("NextButton").gameObject.GetComponent<Button>();
        nextButton.onClick.AddListener(() => {
            nextButton.gameObject.GetComponent<AudioSource>().Play();
            LeanTween.scale(nextButton.gameObject, new Vector2(0.15f, 0.15f), 1f).setEasePunch().setOnComplete(() =>
            {
                SceneManager.LoadScene(1);
            });
        });


        var settinButton = gameCanvas.transform.Find("Setting").gameObject.GetComponent<Button>();
        settinButton.onClick.AddListener(() => {
            settinButton.gameObject.GetComponent<AudioSource>().Play();
            settingCanvas.SetActive(true);
            var settingBoard = settingCanvas.transform.Find("SettingBoard").GetComponent<RectTransform>();
            settingBoard.localScale = new Vector2(0.5f, 0.5f);
            LeanTween.scale(settingBoard, new Vector2(1f, 1f), 1f).setEaseInOutQuart();
        });


    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
