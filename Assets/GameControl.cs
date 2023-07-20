using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public static bool youWin;

    Transform[] childArray;

    void Start(){


        
        Transform[] value = GetImmediateChildren(parentObject.transform);


        Transform[] internalImage = GetImmediateChildren(value[selectedImageIndex]);
        DisableOtherChildObjects(value[selectedImageIndex],value);


        childArray = GetImmediateChildren(internalImage[0]);

        gameCanvas.SetActive(false);


        foreach (Transform child in childArray)
        {
            float randomRotation = Random.Range(1, 4) * 90f;

            child.rotation = Quaternion.Euler(0f, 0f, randomRotation);
        }
        youWin = false;
        ClickListner();
    }


    void Update(){

        foreach (Transform child1 in childArray)
        {
            float rotationZ = child1.rotation.eulerAngles.z;

            if (Mathf.Abs(rotationZ) > 0.01f) return;
        }
        youWin = true;

        gameCanvas.SetActive(true);
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
        selectedImageIndex = selectedImageIndex + 1;
        Start();
    }

}
