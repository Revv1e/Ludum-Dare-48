using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, new Color(255,255,255,0), (Time.deltaTime / 4) * 2);
    }
}
