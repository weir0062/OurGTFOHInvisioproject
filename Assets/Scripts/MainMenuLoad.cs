using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoad : MonoBehaviour
{
    public TransitionSettings transition;
    public float startDelay;

    public GameObject settingsMenu;

    TransitionManager manager;

    // starting from this point i don't understand PlEaSEe HELP ╭( ๐_๐)╮
    public void OpenSettingsMenu()
    {
        manager = TransitionManager.Instance();
        manager.onTransitionCutPointReached += ActivateSettingsMenuPanel;
        manager.Transition(transition, startDelay);
    }

    public void ActivateSettingsMenuPanel()
    {
        settingsMenu.SetActive(true);
        manager.onTransitionCutPointReached -= ActivateSettingsMenuPanel;
    }
}
