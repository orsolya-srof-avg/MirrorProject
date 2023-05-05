using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {
    private Transform _targetCamre;
    private Vector3 _targetPosition = new Vector3();

    private void Start() {
        _targetCamre = Camera.main.transform;
    }

    private void Update() {
        _targetPosition = _targetCamre.position - transform.position;
        transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(_targetPosition, Vector3.up).eulerAngles.y - 180, 0);
    }
}
