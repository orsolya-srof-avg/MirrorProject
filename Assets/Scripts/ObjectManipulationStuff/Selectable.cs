using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour, IDraggable {
    public bool canRotate = false;

    public GameObject outline = null;
    public GameObject rotationGizmo;

    public UnityAction OnSelect;

    private Rigidbody _rigidbody;

    private Vector3 _originalObjPos;
    private Vector3 _originalScreenTargetPos;

    private Camera _cam;

    private void Awake() {
        _cam = Camera.main;

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void SelectForDrag() {
        Debug.Log("Select for drag obj: " + gameObject.name);
        outline?.SetActive(true);
        BeginDrag();
    }

    public void SelectForRotation() {
        Debug.Log("Select for rotation obj: " + gameObject.name);
        rotationGizmo?.SetActive(true);
    }

    public void SelectForAdjustment() {
        Debug.Log("Select for adjustment obj: " + gameObject.name);
    }

    public void Deselect() {
        if (outline != null && outline.activeInHierarchy) {
            outline?.SetActive(false);
            EndDrag();
        }

        if (canRotate && rotationGizmo.activeInHierarchy)
            rotationGizmo.SetActive(false);
    }

    public void Delete() {
        Destroy(gameObject);
    }

    public void BeginDrag() {
        Debug.Log("Start drag obj: " + gameObject.name);

        _rigidbody.isKinematic = false;

        _originalObjPos = transform.position;
        _originalScreenTargetPos = GetHitPoint(_originalObjPos);
    }

    public void Drag() {
        Vector3 offset = GetHitPoint(transform.position) - _originalScreenTargetPos;

        Vector3 newVelocity = _originalObjPos + offset - transform.position;

        newVelocity.x = Mathf.Clamp(newVelocity.x, -ObjectManipulation.MAX_VELOCITY, ObjectManipulation.MAX_VELOCITY);
        newVelocity.z = Mathf.Clamp(newVelocity.z, -ObjectManipulation.MAX_VELOCITY, ObjectManipulation.MAX_VELOCITY);

        _rigidbody.velocity = newVelocity * ObjectManipulation.SPEED * Time.fixedDeltaTime;
    }

    public void EndDrag() {
        _rigidbody.isKinematic = true;
    }

    private Vector3 GetHitPoint(Vector3 objPosition) {
        Plane plane = new Plane(_cam.transform.forward, objPosition);

        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        float length;
        plane.Raycast(ray, out length);

        return ray.GetPoint(length);
    }
}
