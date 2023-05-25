using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class Numpad : MonoBehaviour
{  
    public GameObject bowlingController;

    TextMeshProUGUI text;
    string state = "8";

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddDigit(int digit) {
        state = state + digit;
        UpdateDisplay();
    }

    public void Backspace() {
        state = state.Remove(state.Length - 1, 1);
        UpdateDisplay();
    }

    void UpdateDisplay() {
        if (state.Length >= 2) {
            text.text = state.Remove(state.Length - 1, 1) + "." + state[state.Length - 1];
            float val = float.Parse(text.text, CultureInfo.InvariantCulture.NumberFormat);
            bowlingController.GetComponent<BowlingController>().ChangeBallMass(val);
        } else if (state.Length == 1) {
            text.text = "0." + state[state.Length - 1];
            float val = float.Parse(text.text, CultureInfo.InvariantCulture.NumberFormat);
            bowlingController.GetComponent<BowlingController>().ChangeBallMass(val);
        } else {
            text.text = "0.0`";
            bowlingController.GetComponent<BowlingController>().ChangeBallMass(0.0f);
        }
    }
}
