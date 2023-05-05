using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFilter : MonoBehaviour {
    public Color filterColor = Color.red;

    public Color FilterColorRGB(Color original) {
        if (original == Color.white)
            return filterColor;

        Color newColor = new Color();
        newColor.r = 1 - Mathf.Sqrt((Mathf.Pow(1 - original.r, 2) + Mathf.Pow(1 - filterColor.r, 2)) / 2);
        newColor.g = 1 - Mathf.Sqrt((Mathf.Pow(1 - original.g, 2) + Mathf.Pow(1 - filterColor.g, 2)) / 2);
        newColor.b = 1 - Mathf.Sqrt((Mathf.Pow(1 - original.b, 2) + Mathf.Pow(1 - filterColor.b, 2)) / 2);
        newColor.a = 1;

        return newColor;
    }
}
