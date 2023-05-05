using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectManipulation : MonoBehaviour {
    public const float SPEED = 2000f;
    public const float MAX_VELOCITY = 1f;
    public const float HOVER_TIME = 1f;

    private Camera _cam;
    private Selectable _selectedObj;
    private Selectable _hoveredObj;

    private float _counter = 0;
    private bool _hovering = false;
    private bool _positioning = false;

    private void Start() {
        _cam = Camera.main;
    }

    private void Update() {
        if (!_positioning)
            CheckHovering();

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0) && _selectedObj == null) {
            SelectObject();
        } else if (Input.GetMouseButtonDown(0) && _selectedObj != null) {
            DeselectObject();
        }
    }

    private void FixedUpdate() {
        if (!_hovering)
            _selectedObj?.Drag();
    }

    private void SelectObject() {
        if (_hovering)
            StopHovering();

        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            _selectedObj = hit.collider.gameObject.GetComponentInParent<Selectable>();

            if (_selectedObj != null) {
                _selectedObj.SelectForDrag();
                _positioning = true;
            }
        }
    }

    private void DeselectObject() {
        _selectedObj?.Deselect();
        _selectedObj = null;
        _positioning = false;
    }

    private void CheckHovering() {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            Selectable current = hit.collider.GetComponentInParent<Selectable>();
            if (current == null) {
                StopHovering();
                _hoveredObj = null;
                _counter = 0;
                _hovering = false;
            } else if (current != _hoveredObj) {
                StopHovering();
                _hoveredObj = current;
                _counter = 0;
                _hovering = false;
            } else if (!_hovering) {
                _counter += Time.deltaTime;
                if (_counter >= HOVER_TIME) {
                    Debug.Log("Hovered on obj: " + _hoveredObj.name);
                    _hoveredObj.SelectForRotation();
                    _hovering = true;
                }
            }
        } else
            StopHovering();
    }

    private void StopHovering() {
        if (!_hovering)
            return;

        _hovering = false;
        _hoveredObj.Deselect();
        _hoveredObj = null;
        _counter = 0;
    }
}