using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameControl : MonoBehaviour
{

    int selectedImageIndex = 0;

    [SerializeField]
    private GameObject parentObject;

    [SerializeField]
    private GameObject winText;

    public static bool youWin;

    Transform[] childArray;

    void Start(){
        Transform[] value = parentObject.GetComponentsInChildren<Transform>();
        int parent = System.Array.IndexOf(value, parentObject);
        value = value.Where((_, index) => index != parent).ToArray();


        Transform[] internalImage = value[0].GetComponentsInChildren<Transform>();
        int parent2 = System.Array.IndexOf(internalImage, value[0]);
        internalImage = internalImage.Where((_, index) => index != parent2).ToArray();

        childArray = internalImage[1].GetComponentsInChildren<Transform>();

        winText.SetActive(false);
        int parentIndex = System.Array.IndexOf(childArray, internalImage[1].transform);
        childArray = childArray.Where((_, index) => index != parentIndex).ToArray();

        Debug.Log("Start " + childArray);

        foreach (Transform child in childArray)
        {
            
            float randomRotation = Random.Range(1, 4) * 90f;

            Debug.Log("Start " + randomRotation);
            child.rotation = Quaternion.Euler(0f, 0f, randomRotation);
        }
        youWin = false;
    }

    void Update(){

        foreach (Transform child1 in childArray)
        {
            float rotationZ = child1.rotation.eulerAngles.z;
            if(Mathf.Abs(rotationZ) > 0.1f) return;
        }
        Debug.Log("Working");
        youWin = true;

        winText.SetActive(true);
    }
    
    

}
