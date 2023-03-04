using Modding;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using USceneManager = UnityEngine.SceneManagement.SceneManager;

public class AbyssPitRemover : Mod, ITogglableMod {
    internal static AbyssPitRemover instance;

    public AbyssPitRemover() : base("Abyss Pit Remover") {
        instance = this;
    }

    public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects) {
        Log("Initializing");

        USceneManager.activeSceneChanged += CheckForRadianceFight;
        if (USceneManager.GetActiveScene().name == "GG_Radiance") {
            new GameObject("AbyssPitFinder", typeof(AbyssPitFinder));
        }

        Log("Initialized");
    }

    public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

    public void Unload() {
        USceneManager.activeSceneChanged -= CheckForRadianceFight;
        GameObject abyssPitFinder = GameObject.Find("AbyssPitFinder");
        if (abyssPitFinder) {
            abyssPitFinder.GetComponent<AbyssPitFinder>().Unload();
            GameObject.DestroyImmediate(abyssPitFinder);
        }
    }

    private static void CheckForRadianceFight(Scene from, Scene to) {
        if (from.name == "GG_Radiance") {
            GameObject.DestroyImmediate(GameObject.Find("AbyssPitFinder"));
        }

        if (to.name != "GG_Radiance") {
            return;
        }

        new GameObject("AbyssPitFinder", typeof(AbyssPitFinder));
    }
}