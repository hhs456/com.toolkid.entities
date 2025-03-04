using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Toolkid.Entities {
    public interface IComponent {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }

        public void Normalize() {
            Enabled = false;
            Index = -1;
        }
        public void Serialize(int index) {
            Enabled = true;
            Index = index;
        }
        public void Initialize(Scene scene) {
            if (Enabled) {
                // init value
            }
        }
    }
}