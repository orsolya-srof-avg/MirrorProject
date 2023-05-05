using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {
    public bool PuzzleSolved {
        get;
        private set;
    }

    public GameObject endGameUI;

    [SerializeField]
    private MeshRenderer _door;
    [SerializeField]
    private MeshRenderer _button;

    private Color _targetColor;

    [SerializeField]
    private List<ColorFilter> _colorFilters = new List<ColorFilter>();

    private void Awake() {
        _colorFilters.AddRange(GameObject.FindObjectsOfType<ColorFilter>());

        _targetColor = GenerateTargetColor();

        SetColors(Color.red);
    }

    private Color GenerateTargetColor() {
        ShuffleList();

        Color color = _colorFilters[0].filterColor;

        for (int i =1; i < _colorFilters.Count; i++) {
            color = _colorFilters[i].FilterColorRGB(color);
        }

        return color;
    }

    public void CheckPuzzleSolved(Color color) {
        if (_targetColor.Equals(color)) {
            Debug.Log("Success!");
            SetColors(Color.green);
            PuzzleSolved = true;
            endGameUI.SetActive(true);
        } else {
            Debug.Log("Failed!");
            SetColors(Color.red);
            PuzzleSolved = false;
        }
    }

    private void SetColors(Color color) {
        _door.material.color = color;
        _button.material.color = color;
    }

    private void ShuffleList() {
        int count = _colorFilters.Count;

        while (count > 1) {
            count--;
            int i = Random.Range(0, count + 1);
            ColorFilter helper = _colorFilters[i];
            _colorFilters[i] = _colorFilters[count];
            _colorFilters[count] = helper;
        }
    }
}
