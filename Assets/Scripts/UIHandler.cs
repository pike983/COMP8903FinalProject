using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject ball;
    public Slider angleSlider;
    public InputField angleField;
    public Button launchButton;

    private Rigidbody ballRigidBody;
    private bool launched = false;

    // Start is called before the first frame update
    void Start()
    {
        ballRigidBody = ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateBall(float angle)
    {
        if (!launched)
        {
            ball.transform.eulerAngles = new Vector3(angle, 0, 0);
            Debug.Log(angle);
        }
    }

    public void UpdateAngleField()
    {
        if (!launched)
        {
            angleField.text = angleSlider.value.ToString();
        }
    }

    public void UpdateAngleSlider()
    {
        if (!launched)
        {
            angleSlider.value = (angleField.text != "") ? float.Parse(angleField.text) : 0f;
        }
    }

    public void LaunchBall()
    {
        launched = true;
        angleSlider.enabled = false;
        angleField.enabled = false;
        //Vector3 launchAngle = ball.transform.eulerAngles * 200f;
        Vector3 launchAngle = new Vector3(0f, Mathf.Tan(angleSlider.value), Mathf.Tan(angleSlider.value)) * 200f;
        Debug.Log(launchAngle);
        ballRigidBody.AddForce(launchAngle);
    }
}
