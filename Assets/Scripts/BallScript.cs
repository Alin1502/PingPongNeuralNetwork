using UnityEngine;

public class BallScript : MonoBehaviour
{
    Vector3 initialPos;
    Rigidbody2D ballRb;
    public GameObject player;
    float speed = 3.5f;

    private void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        initialPos = player.transform.position;
        ResetPos();
    }

    void ResetPos()
    {
        initialPos = this.transform.position;
        ballRb.linearVelocity = Vector3.zero;
        Vector3 direction = new Vector3(Random.Range(0, 100), Random.Range(100, 200), 0);
        ballRb.AddForce(direction * speed);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetPos();
        }
    }
}
