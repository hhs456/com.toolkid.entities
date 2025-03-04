using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Toolkid.Entities {
    public abstract class SystemBase : MonoBehaviour {
        [SerializeField]
        List<Scene> scenes = new List<Scene>();
        public List<Scene> Scenes { get => scenes; }

        // Start is called before the first frame update
        protected virtual void Awake() {
            DontDestroyOnLoad(this);
            scenes = new List<Scene>();
            SceneManager.sceneLoaded += OnAnySceneLoaded;
            SceneManager.sceneUnloaded += OnAnySceneUnloaded;
        }
        protected virtual void OnAnySceneUnloaded(Scene arg0) {
            SystemManager.DeleteSystem(arg0);
            scenes.Remove(arg0);
        }
        protected virtual void OnAnySceneLoaded(Scene arg0, LoadSceneMode arg1) {
            scenes.Add(arg0);
            Initialize(arg0);
        }

        protected virtual void Initialize(Scene scene) {

        }

        // Update is called once per frame
        protected void Update() {
            foreach (Scene scene in scenes) {
                if (SystemManager.Spaces.ContainsKey(scene)) {
                    SystemManager.Schedule(scene);
                }
            }
        }
        protected void LateUpdate() {
            foreach (Scene scene in scenes) {
                if (SystemManager.Spaces.ContainsKey(scene)) {
                    SystemManager.Complele(scene);
                }
            }
        }
    }
}