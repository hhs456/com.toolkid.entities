using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public interface IEntity
{    
    public int Index { get; set; }

    public void Normalize() {
        Index = -1;
    }

    public void Serialize(int i) {
        Index = i;
        IList args = new List<Argument>();
        var props = this.GetType().GetProperties();
        foreach ( var prop in props ) {
            if(prop.PropertyType == typeof(Argument) || prop.PropertyType == typeof(Argument)) {
                args.Add(prop.GetValue(this));
            }
        }        
        foreach(var argument in (List<Argument>)args) {
            argument.Serialize(i);
        }
    }
    public void Initialize(Scene scene) {        
        IList args = new List<Argument>();
        var props = this.GetType().GetProperties();
        foreach (var prop in props) {
            if (prop.PropertyType == typeof(Argument) || prop.PropertyType == typeof(Argument)) {
                args.Add(prop.GetValue(this));
            }
        }
        foreach (var argument in (List<Argument>)args) {
            argument.Initialize(scene);
        }
    }
    
}
