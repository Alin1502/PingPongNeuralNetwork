using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject player;
    public GameObject ball;

    Rigidbody2D ballRb;
    float velocityX;
    float bound = 4.3f;
    float playerSpeed = 15.0f;

    NeuralNetwork nn;

    private void Start()
    {
        nn = new NeuralNetwork(5, 1, 1, 4, 0.1);
        ballRb = ball.GetComponent<Rigidbody2D>();
    }

    List<double> ImplementNN(double ballXPos, double ballYPos, double ballVelX, double ballVelY, double paddleX, double paddleVel, bool train)
    {
        List<double> inputs = new List<double>();
        List<double> outputs = new List<double>();
        inputs.Add(ballXPos);
        inputs.Add(ballYPos);
        inputs.Add(ballVelX);
        inputs.Add(ballVelY);
        inputs.Add(paddleX);
        outputs.Add(paddleVel);
        if (train)
        {
            return nn.Train(inputs, outputs);
        }
        else
        {
            return nn.ForwardPropagation(inputs, outputs);
        }
    }

    private void Update()
    {
        float posX = Mathf.Clamp(player.transform.position.x + (velocityX * Time.deltaTime * playerSpeed), -bound, bound);
        player.transform.position = new Vector3(posX, player.transform.position.y, player.transform.position.z);

        List<double> output  = new List<double>();
        int layerMask = 1 << 8;
        RaycastHit2D ray = Physics2D.Raycast(ball.transform.position, ballRb.linearVelocity, 100, layerMask);

        if(ray.collider != null)
        {
            if(ray.collider.gameObject.tag == "side")
            {
                Vector3 reflection = Vector3.Reflect(ballRb.linearVelocity, ray.normal);
                ray = Physics2D.Raycast(ray.point, reflection, 100, layerMask);
            }

            if(ray.collider.gameObject.tag == "enemywall")
            {
                float dx = ray.point.x - player.transform.position.x;
                output = ImplementNN(ball.transform.position.x, ball.transform.position.y, ballRb.linearVelocity.x, ballRb.linearVelocity.y, player.transform.position.x, dx, true);
                velocityX = (float)output[0];
            }
            else
            {
                velocityX = 0;
            }
        }
    }
}
