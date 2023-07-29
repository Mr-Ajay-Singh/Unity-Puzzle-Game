using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    int selectedImageIndex = 0;

    [SerializeField]
    private GameObject parentObject;

    [SerializeField]
    private GameObject gameCanvas;

    [SerializeField]
    private Animator[] animator;

    [SerializeField]
    private Button helpButton;

    [SerializeField]
    private Material imageAlterMaterial;

    public static bool youWin;
    public static bool isFirstTimeInitialised;

    Transform[] childArray;
    KeyValuePair<float, float> scoreDone = new KeyValuePair<float, float>(3, 5);

    AudioSource audioSource;

    [SerializeField]
    public AudioClip[] audioClips;
    int currentCoinsCount = 0;


    private void Awake()
    {
        InitPrefsData.initPrefs();
        currentCoinsCount = InitPrefsData.getInt(InitPrefsData.coins);
        InitHelpClicks();
    }



    void Start(){


        
        Transform[] value = GetImmediateChildren(parentObject.transform);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
        CommonMusicPlayer.playMusic(audioSource);

        Transform[] internalImage = GetImmediateChildren(value[selectedImageIndex]);
        DisableOtherChildObjects(value[selectedImageIndex],value);


        childArray = GetImmediateChildren(internalImage[0]);

        gameCanvas.SetActive(false);
        isFirstTimeInitialised = false;


        foreach (Transform child in childArray)
        {
            float randomRotation = Random.Range(1, 4) * 90f;

            child.rotation = Quaternion.Euler(0f, 0f, randomRotation);
        }
        youWin = false;
        ClickListner();
        InitialiseAnimator();
        //GameCanvasActions();
    }

    void Update(){

        HelpButtonClick();

        foreach (Transform child1 in childArray)
        {
            float rotationZ = child1.rotation.eulerAngles.z;

            if (Mathf.Abs(rotationZ) > 0.01f) return;
        }

        if (!isFirstTimeInitialised)
        {
            youWin = true;
            isFirstTimeInitialised = true;
            

            GameCanvasActions();
        }
    }

    private void HelpButtonClick()
    {
        
    }

    RectTransform progress;
    private void GameCanvasActions()
    {
        gameCanvas.SetActive(true);
        audioSource.Stop();
        audioSource.clip = audioClips[1];
        CommonMusicPlayer.playMusic(audioSource);

        progress = gameCanvas.transform.Find("Progress").Find("ProgressContainer").Find("FilledProgress").gameObject.GetComponent<RectTransform>();


        //StartCoroutine(ScaleRectTransformAnimation());
        var levelCompleted = InitPrefsData.getInt(InitPrefsData.level) % 10;
        if (levelCompleted == 0) levelCompleted = 10;
        LeanTween.scaleX(progress.gameObject, levelCompleted/10, 1f).setEaseInCubic();

        Debug.Log("==>" + progress.name);

        TextMeshProUGUI progressText = gameCanvas.transform.Find("Progress").Find("ProgressText").gameObject.GetComponent<TextMeshProUGUI>();

        Debug.Log("==>" + progressText.name);
        progressText.SetText(levelCompleted + " / " + 10);
    }

    private IEnumerator ScaleRectTransformAnimation()
    {
        float animationDuration = 1f;
        Vector2 startScale = progress.localScale;
        Vector2 targetScale = new Vector2(scoreDone.Key / scoreDone.Value, startScale.y);
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            // Calculate the lerp factor based on the elapsed time
            float t = elapsedTime / animationDuration;

            // Interpolate between the startScale and targetScale using lerp
            progress.localScale = Vector3.Lerp(startScale, targetScale, t);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        // Ensure the RectTransform ends up at the exact target scale
        progress.localScale = targetScale;
    }

    private Transform[] GetImmediateChildren(Transform parentTransform)
    {
        int childCount = parentTransform.childCount;

        Transform[] children = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i);
            children[i] = childTransform.gameObject.transform;

        }

        return children;
    }


    private void DisableOtherChildObjects(Transform currentObjectTransform, Transform[] parentTransform)
    {
        int childCount = parentTransform.Length;


        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = parentTransform[i];
            if (parentTransform[i] != currentObjectTransform)
            {
                parentTransform[i].gameObject.SetActive(false);
            }

        }
        currentObjectTransform.gameObject.SetActive(true);
    }

    private void ClickListner()
    {
        //gameCanvas.transform.Find("NextButton").gameObject;

        Debug.Log("Start5 " + gameCanvas.transform.Find("Congratulations").Find("NextButton").gameObject.name);
        Button button  = gameCanvas.transform.Find("Congratulations").Find("NextButton").gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Debug.Log("Start4 "+ "Working112");

        AudioSource buttonAudioSource = gameCanvas.transform.Find("Congratulations").Find("NextButton").gameObject.gameObject.GetComponent<AudioSource>();
        CommonMusicPlayer.play(buttonAudioSource);
       

        animator[0].SetTrigger("run");
        animator[1].SetTrigger("run");
        StartCoroutine(WaitForAnimation());
        gameCanvas.SetActive(false);

        //animator[0].SetTrigger("run");
        //animator[1].SetTrigger("run");
        //


    }

    private void InitialiseAnimator()
    {
        animator[0].SetTrigger("rev");
        animator[1].SetTrigger("rev");
    }


    private IEnumerator WaitForAnimation()
    {

        yield return new WaitForSeconds(3f);

        selectedImageIndex = selectedImageIndex + 1;
        InitPrefsData.setInt(InitPrefsData.level, selectedImageIndex);
        Start();

    }

    private void InitHelpClicks()
    {

        helpButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            var getCoins = InitPrefsData.getInt(InitPrefsData.level); ;
            InitSolveOneBlock();
            if (getCoins >= 10)
            {

                InitPrefsData.setInt(InitPrefsData.coins, getCoins - 10);
                InitSolveOneBlock();
            }
            else
            {
                LeanTween.scale(helpButton.gameObject, new Vector2(1.2f, 1.2f), 1f).setEasePunch();
            }

        });
    }

    private void InitSolveOneBlock()
    {
        foreach (Transform child1 in childArray)
        {
            float rotationZ = child1.rotation.eulerAngles.z;

            if (Mathf.Abs(rotationZ) > 0.01f)
            {
                Debug.Log("==>Working Rotations");
                Vector3 rotationEulerAngles = child1.rotation.eulerAngles;
                rotationEulerAngles.z = 0f;
                child1.rotation = Quaternion.Euler(rotationEulerAngles);
                child1.GetComponent<Renderer>().material = imageAlterMaterial;
                return;
            }
        }
    }
}
