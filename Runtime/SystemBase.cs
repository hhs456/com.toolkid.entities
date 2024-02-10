using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemBase : MonoBehaviour {
    [SerializeField]
    List<Scene> scenes = new List<Scene>();

    public List<Scene> Scenes { get => scenes; }

    // Start is called before the first frame update
    void Awake() {
        DontDestroyOnLoad(this);
        scenes = new List<Scene>();
        SceneManager.sceneLoaded += OnAnySceneLoaded;
        SceneManager.sceneUnloaded += OnAnySceneUnloaded;
    }
    private void OnAnySceneUnloaded(Scene arg0) {
        SystemManager.DeleteSystem(arg0);
        scenes.Remove(arg0);
    }
    private void OnAnySceneLoaded(Scene arg0, LoadSceneMode arg1) {
        scenes.Add(arg0);
    }
    // Update is called once per frame
    void Update() {
        foreach (Scene scene in scenes) {
            if (SystemManager.Spaces.ContainsKey(scene)) {
                SystemManager.Schedule(scene);
            }
        }
    }
    void LateUpdate() {
        foreach (Scene scene in scenes) {
            if (SystemManager.Spaces.ContainsKey(scene)) {
                SystemManager.Complele(scene);
            }
        }
    }
}