using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CandyCoded.HapticFeedback;

public class VibrationHandler : MonoBehaviour
{
    [SerializeField]
    private Button defaultVibrationButton;

    private void OnEnable()
    {
        defaultVibrationButton.onClick.AddListener(DefaultVibration);
    }

    private void OnDisable()
    {
        defaultVibrationButton.onClick.RemoveListener(DefaultVibration);
    }
        
    private void DefaultVibration()
    {
        Debug.Log("Default Vibration performed.");
        Handheld.Vibrate();
    }

    private void LightVibration()
    {
        Debug.Log("Light Vibration performed.");
        HapticFeedback.LightFeedback();
    }

    private void MediumVibration()
    {
        Debug.Log("Medium Vibration performed.");
        HapticFeedback.MediumFeedback();
    }

    private void HeavyVibration()
    {
        Debug.Log("Heavy Vibration performed.");
        HapticFeedback.HeavyFeedback();
    }
}
