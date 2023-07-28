using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SettingButtonClick : MonoBehaviour
{
    [SerializeField]
    Sprite[] onSprite;
    [SerializeField]
    Sprite[] offSprite;


    // Start is called before the first frame update
    private void Start()
    {
        //onSprite = new Sprite[2];
        //offSprite = new Sprite[2];

        //onSprite[0] = Resources.Load<Sprite>("On");
        //onSprite[1] = Resources.Load<Sprite>("GUL_7");

        //offSprite[0] = Resources.Load<Sprite>("Off");
       // offSprite[1] = Resources.Load<Sprite>("Group 1");

        InitFunction();
        HandleSwitches("Music");
        HandleSwitches("Audio");
        HandleSwitches("Vibration");
    }

    private void HandleSwitches(string swtich)
    {
        var mySwitch = gameObject.transform.Find("SettingBoard").Find(swtich).Find("Switch");

        var musicSwitchText = mySwitch.Find("SwitchStatus");

        mySwitch.GetComponent<Button>().onClick.AddListener(() =>
        {
            mySwitch.GetComponent<AudioSource>().Play();
            var isMusicActive = PlayerPrefs.GetInt(swtich, 1);
            if (isMusicActive == 1)
            {
                musicSwitchText.GetComponent<Image>().sprite = offSprite[0];
                mySwitch.GetComponent<Image>().sprite = offSprite[1];
                PlayerPrefs.SetInt(swtich, 0);
            }
            else
            {
                musicSwitchText.GetComponent<Image>().sprite = onSprite[0];
                mySwitch.GetComponent<Image>().sprite = onSprite[1];
                PlayerPrefs.SetInt(swtich, 1);
            }
        });

    }

    private void InitFunction()
    {
        var settingBoard = gameObject.transform.Find("SettingBoard");

        var closeButton = settingBoard.Find("SettingBoardClose").GetComponent<Button>();
        closeButton.onClick.AddListener(() => {
            closeButton.GetComponent<AudioSource>().Play();
            LeanTween.scale(settingBoard.gameObject, new Vector2(0.5f, 0.5f), 1f).setEaseInQuart().setOnComplete(()=>{
                gameObject.SetActive(false);
            });
        });
    }
}
