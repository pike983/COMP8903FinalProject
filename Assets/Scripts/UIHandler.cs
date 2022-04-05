using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject ball;
    public Slider angleSlider;
    private Rigidbody ballRigidBody;

    public InputField angleField;
    public InputField AccelerationField;
    public InputField DragField;
    public InputField densityField;

    public Button launchButton;

    private float AccelerationForce = 800;
    private float DragCoeficient = 0;
    private float DragSize;

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
            //ball.transform.eulerAngles = new Vector3(angle, 0, 0);
            //Debug.Log(angle);
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

    public void UpdateDrag()
    {
        if (DragField.text != "")
        {
            DragCoeficient = float.Parse(DragField.text);
        }
    }

    public void UpdateAcceleration()
    {
        if (AccelerationField.text != "")
        {
            AccelerationForce = float.Parse(AccelerationField.text);
        }
    }


    public void UpdateDragSize()
    {
        float fluidDensity = (densityField.text != "") ? float.Parse(densityField.text) : 0f;
        float ballSurfaceArea = 4 * Mathf.PI * (Mathf.Pow(15, 2)); // Diameter of the ball is 30cm, radius is 15cm
        float launchVelocity = 1f; // launchVector.magnitude; // The launch velocity

        DragSize = 0.5f * fluidDensity * ballSurfaceArea * Mathf.Pow(launchVelocity, 2) * DragCoeficient;
    }


    public void LaunchBall()
    {
        UpdateAcceleration();
        UpdateDrag();

        launched = true;
        angleSlider.enabled = false;
        angleField.enabled = false;
        float xcomponent = Mathf.Cos(angleSlider.value * Mathf.PI / 180) * AccelerationForce;
        float ycomponent = Mathf.Sin(angleSlider.value * Mathf.PI / 180) * AccelerationForce;

        //Vector3 launchAngle = ball.transform.eulerAngles * 200f;
        Vector3 launchAngle = new Vector3(0f, ycomponent, xcomponent);
        Debug.Log("Velocity Vector:   " + launchAngle);
        Debug.Log("Acceleration:   " + AccelerationForce);

        UpdateDragSize();

        //drag
        //float dragForce = DragCoeficient * ballRigidBody.mass * Mathf.Pow(launchAngle.magnitude, 2);
        Vector3 dragVector = DragSize * -launchAngle.normalized;

        Debug.Log("Drag Vector:   " + dragVector);

        ballRigidBody.AddForce(dragVector);
        ballRigidBody.AddForce(launchAngle);
    }
}
