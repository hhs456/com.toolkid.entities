using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Argument 
{
    protected Type basedEntity;
    protected bool enabled;
    protected string name = string.Empty;
    protected int index = -1;    

    public Type BasedEntity { get => basedEntity; }
    public bool Enabled { get => enabled;}
    public string Name { get => name;}
    public int Index { get => index;}

    public Argument() { }

    public Argument(Type basedEntity) : this(basedEntity, false, string.Empty, -1) { }

    public Argument(Type basedEntity, bool enabled, string name, int index) {
        this.basedEntity = basedEntity;
        this.enabled = enabled;
        this.name = name;
        this.index = index;
    }
    public void Normalize() {        
        this.enabled = false;
        this.name = $"{basedEntity.Name}.{GetType()}";
        this.index = -1;
    }
    public void Serialize(int index) {
        this.enabled = true;        
        this.index = index;
    }
    public void Initialize(Scene scene) {
        if (enabled) {
            // init value
        }
    }
}
