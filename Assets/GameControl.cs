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

    public static bool youWin;
    public static bool isFirstTimeInitialised;

    Transform[] childArray;
    KeyValuePair<float, float> scoreDone = new KeyValuePair<float, float>(3, 5);




    void Start(){


        
        Transform[] value = GetImmediateChildren(parentObject.transform);


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

    RectTransform progress;
    private void GameCanvasActions()
    {
        gameCanvas.SetActive(true);

        progress = gameCanvas.transform.Find("Progress").Find("ProgressContainer").Find("FilledProgress").gameObject.GetComponent<RectTransform>();

        //RectTransform congratulations = gameCanvas.transform.Find("Congratulations").Find("CongratulationsImage").gameObject.GetComponent<RectTransform>();
        //congratulations.localScale = new Vector2(0.8f, 0.8f);
        //LeanTween.scale(congratulations, new Vector2(1f, 1f), 1.5f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        //LeanTween.move(congratulations, new Vector2(congratulations.localPosition.x, congratulations.localPosition.y + 30f), 1.5f).setDelay(1f).setEase(LeanTweenType.easeInCubic);


        StartCoroutine(ScaleRectTransformAnimation());

        Debug.Log("==>" + progress.name);

        TextMeshProUGUI progressText = gameCanvas.transform.Find("Progress").Find("ProgressText").gameObject.GetComponent<TextMeshProUGUI>();

        Debug.Log("==>" + progressText.name);
        progressText.SetText(scoreDone.Key + " / " + scoreDone.Value);
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

        animator[0].SetTrigger("run");
        animator[1].SetTrigger("run");
        gameCanvas.SetActive(false);
        StartCoroutine(WaitForAnimation());

        //animator[0].SetTrigger("run");
        //animator[1].SetTrigger("run");
        //


    }

    private void InitialiseAnimator()
    {
        animator[0].SetTrigger("rev");
        animator[1].SetTrigger("rev");
        //StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation(Animation animation)
    {
        do
        {
            yield return null;
        } while (animation.isPlaying);
    }


    private IEnumerator WaitForAnimation()
    {
        // Wait until the animation is no longer playing
        //while (animator[0].GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        //{
        //    yield return null;
        //}

        //while (animator[1].GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        //{
        //    yield return null;
        //}

        yield return new WaitForSeconds(3f);

        selectedImageIndex = selectedImageIndex + 1;
        Start();

    }

}
