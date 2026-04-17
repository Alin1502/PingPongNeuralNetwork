using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    float bound = 4.3f;
    float playerSpeed = 15.0f;
    float velX;
    public GameObject player;
    public GameObject bot;

    Vector3 playerPos;
    Vector3 botPos;
    void Start()
    {
        playerPos = player.transform.position;
        botPos = bot.transform.position;
        
    }

    void Update()
    {
        velX = Input.GetAxis("Horizontal");
        float posX = Mathf.Clamp(transform.position.x + (velX * Time.deltaTime * playerSpeed), -bound, bound);
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
        ResetPos();
    }

    void ResetPos()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.transform.position = playerPos;
            bot.transform.position = botPos;
        }
    }
}
