using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using UnityEngine.SceneManagement;

public interface ISystem {
    public abstract void Initialize(Scene scene);
    public abstract void Update(Scene scene);
}

public class Space
{
    private static bool hasInitialized = false;
    private static Dictionary<Type, IList> entities = new Dictionary<Type, IList>();
    private static Dictionary<Type, IList> arguments = new Dictionary<Type, IList>();
    public bool HasInitialized { get { return hasInitialized; } }

    public void Ready() {
        hasInitialized = true;
    }

    public void Normalize() {
        entities = new Dictionary<Type, IList>();
        arguments = new Dictionary<Type, IList>();
    }    

    public void Serialise<T>(T[] entities) where T : IEntity {
        int size = entities.Length;
        for(int i = 0; i < size; i++) {
            entities[i].Serialize(i);
        }
    }

    public void Initialize<T>(T[] ts, Scene scene) where T : IEntity {
        int size = ts.Length;
        for (int i = 0; i < size; i++) {
            ts[i].Initialize(scene);
        }
        entities.TryAdd(typeof(T), ts);
    }

    public T[] GetEntities<T>() where T : IEntity {
        return (T[])entities[typeof(T)];
    }

    public T GetEntity<T>(int i) where T : IEntity {
        return ((T[])entities[typeof(T)])[i];
    }
    
}
