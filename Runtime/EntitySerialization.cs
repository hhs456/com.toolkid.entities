using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Toolkid.Entities {

    [CreateAssetMenu(fileName = "Entity Serialization", menuName = "Entity Serialization", order = 0)]
    public class EntitySerialization : ScriptableObject {
        [SerializeField] private Entity[] entities;
        [SerializeField] private TransformComponent[] transforms;

        public TransformComponent[] Transforms { get => transforms;}

        public void Serialize(EntityHolder[] entityHolders) {
            for (int i = 0; i < entityHolders.Length; i++) {
                entities[i] = new Entity(i, entityHolders[i].name);
            }            
        }
    }
    [Serializable]
    public struct TransformComponent : IComponent {
        [SerializeField] private bool enabled;
        [SerializeField] private int index;
        [SerializeField] private string name;
        [SerializeField] private Vector3 position;
        [SerializeField] private Quaternion rotation;

        public bool Enabled { get => enabled; set => enabled = value; }
        public int Index { get => index; set => index = value; }
        public string Name { get => name; set => name = value; }
        public Vector3 Position { get => position; set => position = value; }
        public Quaternion Rotation { get => rotation; set => rotation = value; }
    }
}

