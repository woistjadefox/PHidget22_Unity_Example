using UnityEngine;
using Phidget22;
using Phidget22.Events;

public class TestExample : MonoBehaviour {

    private RCServo servo = null;
    private DistanceSensor sensorDistance = null;
    private float currentSensorDistance = 0f;

    private void Start () {

        servo = new RCServo();
        servo.Open();
        servo.Attach += OnAttachServo;

        sensorDistance = new DistanceSensor();
        sensorDistance.Open();
        sensorDistance.Attach += OnAttachDistanceSensor;
    }

    void OnAttachServo(object sender, Phidget22.Events.AttachEventArgs e) {

        try {

            servo.TargetPosition = servo.MaxPosition;
            servo.Engaged = true;

        } catch (PhidgetException ex) {
            Debug.Log("Error setting initial data: " + ex.Message);
        }

    }

    void OnAttachDistanceSensor(object sender, Phidget22.Events.AttachEventArgs e) {

        try {
            sensorDistance.SonarQuietMode = true;

        } catch (PhidgetException ex) {
            Debug.Log("Error setting initial data: " + ex.Message);
        }

    }

    void OnDistanceChange() {
        
    }

    private void CloseConnection() {

        if(servo != null) {
            servo.Close();
            servo = null;
        }

        if(sensorDistance != null) {
            sensorDistance.Close();
            sensorDistance = null;
        }
    }

    private void OnDestroy() {
        CloseConnection();
    }

    private void OnApplicationQuit() {
        CloseConnection();
    }

	private void Update() {
		
        if(sensorDistance != null && sensorDistance.Attached) {
            try {
                currentSensorDistance = sensorDistance.Distance;
            } catch (PhidgetException ex){
                Debug.Log("Error getting distance: " + ex.Message);
            }
        }


        Debug.Log(currentSensorDistance);
	}
}
