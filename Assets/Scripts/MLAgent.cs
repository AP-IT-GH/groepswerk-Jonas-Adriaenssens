using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class MLAgent : Agent
{
    public GameObject Arm;
    public TextMeshPro ScoreBoard;

    public float RotationSpeed;
    public float ArmRotationSpeed;

    private ScoreHelper scoreHelper;
    private int oldScore = 0;
    private int oldPunishment = 0;

    Rigidbody body;
    Rigidbody arm;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        arm = Arm.GetComponent<Rigidbody>();

        if (!scoreHelper)
            scoreHelper = gameObject.GetComponentInParent<ScoreHelper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreBoard != null)
            ScoreBoard.text = GetCumulativeReward().ToString("f4");
    }

    public override void OnEpisodeBegin()
    {
        if(scoreHelper != null)
        {
            scoreHelper.resetScore();
            scoreHelper.resetPunishment();
        }    

        oldScore = 0;
        oldPunishment = 0;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log("OnActionReceived");

        var vectorAction = actions.DiscreteActions;

        Vector3 rotation = Vector3.zero;

        // rotate body
        if(vectorAction[0] != 0)
        {
            float bodyRotation = RotationSpeed * (vectorAction[0] * 2 - 3) * Time.deltaTime;
            Debug.Log("Rotate Body - " + vectorAction[0] + " | " + rotation);
            transform.Rotate(0, bodyRotation, 0);
        }

        // horizontal rotation arm - Y
        if(vectorAction[1] != 0)
        {
            rotation.y = ArmRotationSpeed * (vectorAction[1] * 2 - 3) * Time.deltaTime;
            Debug.Log("Rotate Arm Horizontal - " + vectorAction[1] + " | " + rotation.y);
        }

        // vertical rotation arm    - X
        if(vectorAction[2] != 0)
        {
            rotation.x = ArmRotationSpeed * (vectorAction[2] * 2 - 3) * Time.deltaTime;
            Debug.Log("Rotate Arm Horizontal - " + vectorAction[2] + " | " + rotation.x);
        }

        // shoot
        if(vectorAction[3] != 0)
        {
            Debug.Log("Shoot - " + vectorAction[3]);
        }

        arm.transform.Rotate(rotation);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        Debug.Log("Heuristic");
        var actions = actionsOut.DiscreteActions;

        actions[0] = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Input - Turn Left");
            actions[0] = 1;     // left turn
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Input - Turn Right");
            actions[0] = 2;     // right turn
        }


        actions[1] = 0;
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Input - Arm Turn Left");
            actions[1] = 1;     // left turn
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Input - Arm Turn Right");
            actions[1] = 2;     // turn right
        }


        actions[2] = 0;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Input - Arm Turn Left");
            actions[2] = 1;     // left up
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("Input - Arm Turn Right");
            actions[2] = 2;     // turn down
        }

        actions[3] = 0;
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Input - Shoot");
            actions[3] = 1;     // shoot
        }
    }
}
