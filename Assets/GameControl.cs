using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    
    [SerializeField]
    private GameObject parentObject;

    [SerializeField]
    private GameObject[] winText;

    public static bool youWin;

    Transform[] childArray;

    void Start(){
        childArray = parentObject.GetComponentsInChildren<Transform>(true);

        winText[0].SetActive(false);
        winText[1].SetActive(false);
        int parentIndex = System.Array.IndexOf(childArray, parentObject.transform);
        childArray = childArray.Where((_, index) => index != parentIndex).ToArray();

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

        winText[0].SetActive(true);
        winText[1].SetActive(true);
    }
    
    

}
