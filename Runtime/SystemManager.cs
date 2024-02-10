using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine.SceneManagement;

public static class SystemManager {
    public struct SystemUpdateJob : IJob {
        public int index;
        public Scene scene;

        public void Execute() {
            SystemManager.Systems[scene][index].Update(scene);
        }
    }

    private static Dictionary<Scene,IList<ISystem>> systems = new Dictionary<Scene,IList<ISystem>>();
    private static Dictionary<Scene, JobHandle[]> updateJobs = new Dictionary<Scene, JobHandle[]>();
    private static Dictionary<Scene, Space> spaces = new Dictionary<Scene, Space>();

    public static Dictionary<Scene, IList<ISystem>> Systems { get => systems;}
    public static Dictionary<Scene, JobHandle[]> UpdateJobs { get => updateJobs;}
    public static Dictionary<Scene, Space> Spaces { get => spaces;}

    public static void CreateSpace(Scene scene, Space space) {
        spaces.TryAdd(scene, space);        
    }
    public static void DeleteSpace(Scene scene) {
        spaces.Remove(scene);        
    }
    public static void CreateSystem(Scene scene, params ISystem[] enabledSystems) {
        systems.TryAdd(scene, enabledSystems);
        updateJobs.TryAdd(scene, new JobHandle[enabledSystems.Length]);
    }
    public static void DeleteSystem(Scene scene) {
        systems.Remove(scene);
        updateJobs.Remove(scene);
    }
    public static void EnableSystem(Scene scene) {
        foreach (var system in systems[scene]) {
            system.Initialize(scene);
        }
        spaces[scene].Ready();
    }
    public static void Schedule(Scene scene) {
        if(spaces[scene].HasInitialized) {
            int size = systems[scene].Count;
            for (int i = 0; i < size; i++) {
                var job = new SystemUpdateJob() {
                    index = i,
                    scene = scene
                };
                updateJobs[scene][i]= job.Schedule();
            }
        }
    }
    public static void Complele(Scene scene) {
        if (spaces[scene].HasInitialized) {
            foreach (var job in updateJobs[scene]) {
                job.Complete();
            }
        }
    }
}
