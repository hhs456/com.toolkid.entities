## Toolkid.Entities

This is a set of C# code implementing a **ECS (Entity-Component-System-like)** architecture, which is designed with reference to Unity DOTS. It is mainly used for data-oriented game development patterns, which separates data and logic in the game runtime, handling a large number of game entities more efficiently.

### Features
1. **Data-Oriented**: Data-oriented design provides better performance by separating data structures from processing logic, better utilizing the hardware features of modern computers.
2. **Readability**: Organize entities, components, and systems in a clear way, making the entire architecture easy to understand and extend.
3. **Scalability**: With the use of interfaces and generics, your architecture has high flexibility and scalability, allowing for easy addition of new components and systems to meet the needs of different games.
4. **Testability**: Organizing game logic in components and systems makes unit testing and debugging easier, ensuring better code quality and stability.
5. **Unity Integration**: Code integrates with the Unity engine, using Unity's scene loading events and MonoBehaviour lifecycle, allowing it to be used directly in Unity projects.

### Definitions

In this codebase, I have defined the following classes and interfaces:

1. **Argument**: Used to describe the data of a component, each component has a set of properties, such as basedEntity, enabled, name, and index, as well as some methods for normalization, serialization, and initialization.
2. **IEntity**: Interface describing a game entity, each entity should implement this interface and provide methods for normalization, serialization, and initialization.
3. **ISystem**: Interface describing a system, each system should implement this interface and provide methods for initialization and updating.
4. **Space**: Used to manage all entities and systems in the game world, including initialization, serialization, and updating.
5. **SystemBase**: Inherits from Unity's MonoBehaviour, responsible for listening to scene loading and unloading events, as well as updating systems per frame.

### Usage

Suppose we are developing a simple game that includes some ball entities, each ball has position and color components, and we have a system to update the positions of these balls.

First, we define the components and system for the ball entities:

```C#
using System;
using UnityEngine;

// Position component
public class Position : Argument {
    public Vector3 Value { get; set; }

    public Position() { }

    public Position(Vector3 value) {
        Value = value;
    }
}

// Color component
public class ColorComponent : Argument {
    public Color Value { get; set; }

    public ColorComponent() { }

    public ColorComponent(Color value) {
        Value = value;
    }
}

// Ball movement system
public class BallMovementSystem : ISystem {
    public void Initialize(Scene scene) {
        Debug.Log("Ball movement system initialized.");
    }

    public void Update(Scene scene) {
        var space = SystemManager.Spaces[scene];
        var balls = space.GetEntities<Ball>();
        foreach (var ball in balls) {
            var position = ball.GetArgument<Position>();
            position.Value += Vector3.one * Time.deltaTime;
            Debug.Log($"Ball at {position.Value}");
        }
    }
}
```

Then, we create the ball entities:

```C#
public class Ball : IEntity {
    public int Index { get; set; }

    public void Initialize(Scene scene) {
        Debug.Log("Ball initialized.");
    }
}
```

Finally, we use these components and systems in the Unity scene by creating an empty GameObject and adding a SystemController script:

```C#
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemController : SystemBase {    

    void OnEnable(){
        SystemManager.EnableSystem(scene);
    }

    protected override void Initialize(Scene scene) {        
        SystemManager.CreateSystem(scene, new BallMovementSystem());
        var space = new Space();
        space.Initialize(new IEntity[] { new Ball() }, scene);
        SystemManager.CreateSpace(scene, space);
    }
}
```

Now, every time the scene is loaded, the systems and entities will be created, initialized, and ready to go, and the systems will be driven when the script is enabled.

### Notes
Usage may change due to version updates. If you have any questions, please feel free to contact me.

If you find a better way, or have any suggestions and feedback, please feel free to contact me. Your contributions will help improve this toolkit and benefit more Unity developers.

## License

MIT License

Copyright (c) [2024] [Hanson]