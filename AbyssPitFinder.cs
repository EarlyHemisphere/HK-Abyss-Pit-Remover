using UnityEngine;
using HutongGames.PlayMaker;
using Modding;

namespace AbyssPitRemover {
    internal class AbyssPitFinder : MonoBehaviour {
        private bool removed = false;
        private PlayMakerFSM attackChoices;

        private void Update() {
            GameObject abs = GameObject.Find("Absolute Radiance");

            if (!removed && abs != null) {
                attackChoices = abs.LocateMyFSM("Attack Choices");
                
                if (attackChoices != null && attackChoices.ActiveStateName == "A2 End" && abs.transform.position.y >= 150f) {
                    Destroy(GameObject.Find("Abyss Pit"));
                    removed = true;
                    Modding.Logger.Log("Destroyed Abyss Pit");
                }
            }
        }
    }
}