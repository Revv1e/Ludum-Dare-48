using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class StoryText
    {
        public string[] chatText;
    }

    public PlayerPlatformerController player;
    public DeeperOrbController[] senderOrbs;
    public GameObject[] layers;
    public GameObject[] storyTrigger;

    
    public bool addingIndex = false;
    public int currentLayerIndex = 0;
    private int maxLayers = 10;

    public Text UIText;
    public Image fadeImg;

    public StoryText[] storyTexts;

    public bool newChat;
    public GameObject appearingPlatform;

    private float timeLeft = 2.0f;
    private int textIndex = 0;
    private int storyTriggerIndex = 0;

    [HideInInspector]
    public bool hasDiedOnce = false;
    private bool doNotScold = false;

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, storyTrigger[storyTriggerIndex].transform.position) <= 5.0f)
        {
            newChat = true; 
        }

        if (hasDiedOnce && !doNotScold)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                UIText.text = "Death has little meaning here. Do not worry.";

                timeLeft = 1.0f;
                doNotScold = true;
            }
        }

        if (newChat)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                if (textIndex != storyTexts[storyTriggerIndex].chatText.Length)
                {
                    UIText.text = storyTexts[storyTriggerIndex].chatText[textIndex];
                    textIndex++;

                    timeLeft = 2.5f;
                }
                else
                {
                    UIText.text = "";
                    newChat = false;
                    textIndex = 0;
                    if (storyTriggerIndex + 1 < storyTexts.Length)
                    {
                        storyTriggerIndex++;
                    }
                }
            }
        }        

        if (addingIndex)
        {
            if (currentLayerIndex != layers.Length-1)
            {
                currentLayerIndex++;
            }
            foreach (Transform child in layers[currentLayerIndex - 1].transform)
            {
                if (child.GetComponent<BoxCollider2D>())
                {
                    child.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (child.GetComponent<SpriteRenderer>())
                {
                    child.GetComponent<SpriteRenderer>().enabled = false;
                }
                if (child.GetComponent<CircleCollider2D>())
                {
                    child.GetComponent<CircleCollider2D>().enabled = false;
                }
            }
            foreach (Transform child in layers[currentLayerIndex].transform)
            {
                if (child.GetComponent<BoxCollider2D>())
                {
                    child.GetComponent<BoxCollider2D>().enabled = true;
                }
                if (child.GetComponent<SpriteRenderer>())
                {
                    child.GetComponent<SpriteRenderer>().enabled = true;
                }
                if (child.GetComponent<CircleCollider2D>())
                {
                    child.GetComponent<CircleCollider2D>().enabled = true;
                }
            }

            fadeImg.color = Color.Lerp(fadeImg.color, Color.black, (float)((float)currentLayerIndex / (float)maxLayers));
            addingIndex = false;
        }

        if (Vector3.Distance(player.transform.position, storyTrigger[3].transform.position) <= 1.5f)
        {
            appearingPlatform.GetComponent<SpriteRenderer>().color = Color.Lerp(appearingPlatform.GetComponent<SpriteRenderer>().color, new Color(appearingPlatform.GetComponent<SpriteRenderer>().color.r, appearingPlatform.GetComponent<SpriteRenderer>().color.g, appearingPlatform.GetComponent<SpriteRenderer>().color.b, 255), Time.deltaTime * 4);
            appearingPlatform.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
