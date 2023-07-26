using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        AudioSource source = gameObject.GetComponent<AudioSource>();
        source.Play();
        Debug.Log("Test Working");
    }
}
