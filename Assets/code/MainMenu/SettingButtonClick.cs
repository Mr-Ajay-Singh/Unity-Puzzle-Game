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
        HandleSwitches("Music",InitPrefsData.isMusicOn);
        HandleSwitches("Audio",InitPrefsData.isAudioOn);
        //HandleSwitches("Vibration");
    }

    private void HandleSwitches(string swtich,string prefs)
    {
        var mySwitch = gameObject.transform.Find("SettingBoard").Find(swtich).Find("Switch");

        var musicSwitchText = mySwitch.Find("SwitchStatus");


        var isSwitchActive = InitPrefsData.getInt(prefs);

        if (isSwitchActive == 1)
        {
            musicSwitchText.GetComponent<Image>().sprite = onSprite[0];
            mySwitch.GetComponent<Image>().sprite = onSprite[1];
            InitPrefsData.setInt(prefs, 1);
        }
        else
        {
            musicSwitchText.GetComponent<Image>().sprite = offSprite[0];
            mySwitch.GetComponent<Image>().sprite = offSprite[1];
            InitPrefsData.setInt(prefs, 0);
        }


        mySwitch.GetComponent<Button>().onClick.AddListener(() =>
        {
            CommonMusicPlayer.play(mySwitch.GetComponent<AudioSource>());
            isSwitchActive = InitPrefsData.getInt(prefs);
            if (isSwitchActive == 1)
            {
                musicSwitchText.GetComponent<Image>().sprite = offSprite[0];
                mySwitch.GetComponent<Image>().sprite = offSprite[1];
                InitPrefsData.setInt(prefs, 0);
            }
            else
            {
                musicSwitchText.GetComponent<Image>().sprite = onSprite[0];
                mySwitch.GetComponent<Image>().sprite = onSprite[1];
                InitPrefsData.setInt(prefs, 1);
            }
        });

    }

    private void InitFunction()
    {
        var settingBoard = gameObject.transform.Find("SettingBoard");

        var closeButton = settingBoard.Find("SettingBoardClose").GetComponent<Button>();
        closeButton.onClick.AddListener(() => {
            CommonMusicPlayer.play(closeButton.GetComponent<AudioSource>());
            LeanTween.scale(settingBoard.gameObject, new Vector2(0.5f, 0.5f), 1f).setEaseInQuart().setOnComplete(()=>{
                gameObject.SetActive(false);
            });
        });
    }
}
