using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateObject : MonoBehaviour {
    public float rotationAngle;
    public Transform objectToRotate;

    public Button leftRotation;
    public Button rightRotation;

    private void Awake() {
        leftRotation.onClick.AddListener(() => Rotate(rotationAngle));
        rightRotation.onClick.AddListener(() => Rotate(-rotationAngle));
    }

    private void Rotate(float angle) {
        objectToRotate.Rotate(Vector3.up, angle);
    }
}
