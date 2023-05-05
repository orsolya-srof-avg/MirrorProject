using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour {

    private LineRenderer _lineRenderer;

    private void Awake() {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void Setup(Vector3 startPosition, Vector3 endPosition, Color color) {
        _lineRenderer.transform.position = Vector3.zero;
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, startPosition);
        _lineRenderer.SetPosition(1, endPosition);
        _lineRenderer.startColor = _lineRenderer.endColor = color;
    }
}
