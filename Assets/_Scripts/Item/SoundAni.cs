using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAni : MonoBehaviour
{
    [SerializeField]GameObject g;
    public void fuck()
    {
        if (g.activeSelf)        
            g.SetActive(false);      
        else
            g.SetActive(true);
    }
}
