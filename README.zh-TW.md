# Toolkid.Entities

這是一組**類ECS(Entity-Component-System-like)**架構的 C# 程式碼，這個架構參考了 Unity DOTS 的設計，主要用於數據導向的遊戲開發模式，執行時可將遊戲中的數據和邏輯分離，以較為高效的方式來處理大量的遊戲實體。

## 特點
1. 數據導向
> 數據導向的設計可以提供更好的性能，因為它將數據結構與處理邏輯分離開來，更好地利用了現代計算機的硬件特性。
2. 可閱讀性
> 以清晰的方式組織遊戲世界中的實體、組件和系統，使得整個架構易於閱讀與理解。
3. 可擴展性
> 通過介面和泛型的使用，此架構具有較高的可塑性，可以輕鬆地添加新的組件和系統，以滿足不同的需求。
4. 可測試性
> 以組件和系統的方式組織遊戲邏輯，這使得單元測試和調試變得更加容易，可以更好地維持程式碼的穩定性。
5. Unity 整合
> 程式碼與 Unity 引擎整合，採用 Unity 的場景加載事件和 MonoBehaviour 生命週期，可以直接在 Unity 中使用。

## 定義

在這組程式碼中，我定義了以下類(class)和介面(interface)：

1. Argument
> 用於描述組件的數據，每個組件都有一組屬性，如 basedEntity、enabled、name 和 index，以及一些方法來進行標準化(Normalize)、序列化(Serialize)和初始化(Initialize)。
2. IEntity
> 描述遊戲實體的介面，每個實體應該實現該介面，並提供標準化、序列化和初始化的方法。
3. ISystem
> 描述系統的介面，每個系統都應該實現該介面，並提供初始化和更新的方法。
4. Space
> 用於管理遊戲世界中的所有實體和系統，包括初始化、序列化和更新。
5. SystemBase
> 繼承 Unity 的 MonoBehaviour，負責監聽場景載入和退出事件，以及在每一幀中更新系統。

## 使用方法

假設我們正在開發一個簡單的遊戲，其中包含一些球體實體，每個球體具有位置和顏色組件，我們還有一個系統來更新這些球體的位置。

首先，我們定義球體實體的組件和系統：

```C#
using System;
using UnityEngine;

// 位置組件
public class Position : Argument {
    public Vector3 Value { get; set; }

    public Position() { }

    public Position(Vector3 value) {
        Value = value;
    }
}

// 顏色組件
public class ColorComponent : Argument {
    public Color Value { get; set; }

    public ColorComponent() { }

    public ColorComponent(Color value) {
        Value = value;
    }
}

// 球體移動系統
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

然後，我們創建球體實體：

```C#
public class Ball : IEntity {
    public int Index { get; set; }

    public void Initialize(Scene scene) {
        Debug.Log("Ball initialized.");
    }
}
```

最後，我們將在 Unity 場景中使用這些組件和系統，為此創建一個空的 GameObject，並添加 SystemController 腳本：

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
現在，在每次場景載入完成後，都會建立系統與實體，完成初始化和準備工作，並在腳本啟用時驅動系統。

## 注意事項

使用方法可能因版本更新而有異動，若有疑問可以直接聯繫我。

若您找到了更好的方法，或者有任何建議和反饋，請隨時與我聯繫。您的貢獻將有助於改善這個工具包，並且讓更多的 Unity 開發者受益。

## 授權

MIT 授權

版權所有（c）2024 [Hanson]