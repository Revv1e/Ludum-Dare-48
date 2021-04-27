using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeeperOrbController : MonoBehaviour
{
    private Camera gameCamera;
    private bool transportingPlayer;
    private bool transportingCamera;

    public GameManager gameManager;
    public PlayerPlatformerController player;

    public Transform endTransform;

    public BoxCollider2D foreGroundBoxCollider2D;
    public BoxCollider2D backGroundBoxCollider2D;

    public bool isLast = false;

    private void Start()
    {
        gameCamera = Camera.main;
    }

    private void Update()
    {
        if (!isLast)
        {

            if (transportingPlayer)
            {
                GetComponent<CircleCollider2D>().enabled = false;
                if (player.transform.position.z < endTransform.position.z)
                {
                    player.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                    player.transform.position = Vector3.Slerp(player.transform.position, endTransform.position, Time.deltaTime * 4);
                    GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0), Time.deltaTime * 4);
                    endTransform.GetComponent<SpriteRenderer>().color = Color.Lerp(endTransform.GetComponent<SpriteRenderer>().color, new Color(endTransform.GetComponent<SpriteRenderer>().color.r, endTransform.GetComponent<SpriteRenderer>().color.g, endTransform.GetComponent<SpriteRenderer>().color.b, 0), Time.deltaTime * 4);

                    if (player.transform.position.z > endTransform.position.z - 0.25f)
                    {
                        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, endTransform.position.z);
                        foreGroundBoxCollider2D.enabled = false;
                        backGroundBoxCollider2D.enabled = true;
                        player.lastTouchedOrb = endTransform;
                        player.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                        transportingCamera = true;
                        transportingPlayer = false;
                    }
                }
                else
                {
                    player.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                    player.transform.position = Vector3.Slerp(player.transform.position, endTransform.position, Time.deltaTime * 4);
                    GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0), Time.deltaTime * 4);
                    endTransform.GetComponent<SpriteRenderer>().color = Color.Lerp(endTransform.GetComponent<SpriteRenderer>().color, new Color(endTransform.GetComponent<SpriteRenderer>().color.r, endTransform.GetComponent<SpriteRenderer>().color.g, endTransform.GetComponent<SpriteRenderer>().color.b, 0), Time.deltaTime * 4);

                    if (player.transform.position.z < endTransform.position.z + 0.25f)
                    {
                        gameManager.addingIndex = true;
                        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, endTransform.position.z);
                        foreGroundBoxCollider2D.enabled = false;
                        backGroundBoxCollider2D.enabled = true;
                        player.lastTouchedOrb = endTransform;
                        player.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                        transportingCamera = true;
                        transportingPlayer = false;
                    }

                }

            }
            if (transportingCamera)
            {
                if (transform.position.z < endTransform.position.z)
                {
                    gameCamera.transform.position = Vector3.Lerp(gameCamera.transform.position, new Vector3(endTransform.position.x, endTransform.position.y, endTransform.position.z - 10.0f), Time.deltaTime * 4);
                    if (gameCamera.transform.position.z >= endTransform.position.z - 10.5f)
                    {
                        gameManager.addingIndex = true;
                        GetComponent<CircleCollider2D>().enabled = false;
                        transportingCamera = false;
                    }
                }
                else if (transform.position.z > endTransform.position.z)
                {
                    gameCamera.transform.position = Vector3.Lerp(gameCamera.transform.position, new Vector3(endTransform.position.x, endTransform.position.y, endTransform.position.z - 10.0f), Time.deltaTime * 4);


                    if (gameCamera.transform.position.z <= endTransform.position.z - 9.5f)
                    {
                        gameManager.addingIndex = true;
                        GetComponent<CircleCollider2D>().enabled = false;
                        transportingCamera = false;
                    }
                }
            }
        }
        else
        {
            if (transportingPlayer)
            {
                gameManager.fadeImg.color = Color.Lerp(gameManager.fadeImg.color, Color.white, Time.deltaTime * 2);

                if (gameManager.fadeImg.color.a >= .95)
                {
                    SceneManager.LoadScene(1);
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transportingPlayer = true;
        }
    }
}
