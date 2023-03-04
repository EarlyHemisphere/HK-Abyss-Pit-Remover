using UnityEngine;
using HutongGames.PlayMaker;
using Modding;

// i wonder if anyone will ever read this code
public class AbyssPitFinder : MonoBehaviour {
    private bool removed = false;
    private PlayMakerFSM attackChoices;
    private GameObject abyssPit = null;
    private GameObject abs = null;

    public void Update() {
        if (abs == null) {
            abs = GameObject.Find("Absolute Radiance");
        }

        if (!removed && abs != null) {
            attackChoices = abs.LocateMyFSM("Attack Choices");
            
            if (attackChoices != null && attackChoices.ActiveStateName == "A2 End" && abs.transform.position.y >= 150f) {
                abyssPit = GameObject.Find("Abyss Pit");

                if (abyssPit) {
                    abyssPit.SetActive(false);
                    foreach (Component comp in abyssPit.GetComponents(typeof(Component))) {
                        Modding.Logger.Log(comp.gameObject.name);
                        Modding.Logger.Log(comp.GetType());
                    }
                    removed = true;
                    Modding.Logger.Log("Destroyed Abyss Pit");
                }
            }
        }
    }

    public void Unload() {
        removed = false;
        if (abyssPit) {
            abyssPit.SetActive(true);
        }
        abs = null;
    }
}