using UnityEngine;
using Phidget22;
using Phidget22.Events;

public class TestExample : MonoBehaviour {

    private RCServo servo = null;

    private void Start () {

        servo = new RCServo();
        servo.Open();
        servo.Attach += OnAttach;
    }

    void OnAttach(object sender, Phidget22.Events.AttachEventArgs e) {

        try {

            servo.TargetPosition = servo.MaxPosition;
            servo.Engaged = true;

        } catch (PhidgetException ex) {
            Debug.Log("Error setting initial data: " + ex.Message);
        }

    }

    private void CloseConnection() {

        if(servo != null) {
            servo.Engaged = false;
            servo.Close();
            servo = null;
        }
    }

    private void OnDestroy() {
        CloseConnection();
    }

    private void OnApplicationQuit() {
        CloseConnection();
    }
}
