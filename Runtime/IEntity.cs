using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Toolkid.Entities {
    public interface IEntity {
        public int Id { get; set; }
        public string Name { get; set; }

        public string[] Components { get; set; }        

        public void Normalize() {
            Id = -1;
            Components = new string[0];
        }

        //    public void Serialize(int i, params IComponent[] args) {
        //        Id = i;
        //        arguments = new Dictionary<Type, IComponent>();
        //        foreach (var argument in args) {
        //            arguments.TryAdd(argument.GetType(), argument);
        //            argument.Serialize(i);
        //        }
        //    }

        //    public void Initialize(Scene scene) {
        //        foreach (var argument in arguments) {
        //            argument.Value.Initialize(scene);
        //        }
        //    }

        //    public T GetArgument<T>() where T : IComponent {
        //        return (T)arguments[typeof(T)];
        //    }
    }
    [Serializable]
    public struct Entity : IEntity {
        [SerializeField]
        private int id;
        [SerializeField]
        private string name;
        [SerializeField]
        public string[] components;
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }        
        public string[] Components { get => components; set => components = value; }


        public Entity(int id, string name, params string[] components) {
            this.id = id;
            this.name = name;
            this.components = components;
        }
    }
}