using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public interface IEntity
{    
    public int Index { get; set; }
    public string Name { get; set; }    

    public Dictionary<Type,Argument> arguments { get; set; }

    public void Normalize() {
        Index = -1;
        arguments = new Dictionary<Type, Argument>();
    }

    public void Serialize(int i, params Argument[] args) {
        Index = i;
        arguments = new Dictionary<Type, Argument>();
        foreach (var argument in args) {
            arguments.TryAdd(argument.GetType(), argument);
            argument.Serialize(i);
        }
    }

    public void Initialize(Scene scene) {
        foreach (var argument in arguments) {
            argument.Value.Initialize(scene);
        }
    }
    
    public T GetArgument<T>() where T: Argument {
        return (T)arguments[typeof(T)];
    }
}
