# Unity Module Library

Bibliothèque de modules C# réutilisables pour Unity — portée depuis une bibliothèque équivalente en Love2D/Lua, avec des ajouts propres à Unity.

## Sommaire

- [Installation](#installation)
- [Modules](#modules)
  - [Timer](#timer)
  - [StateMachine](#statemachine)
  - [SaveManager](#savemanager)
  - [CameraUtils](#camerautils)
  - [Singleton\<T\>](#singletont)
  - [ObjectPool](#objectpool)
  - [EventBus](#eventbus)
  - [Tween](#tween)
- [Roadmap](#roadmap)
- [License](#license)

## Installation

Copiez les fichiers `.cs` du dossier `Scripts/` dans le dossier `Assets/` de votre projet Unity (n'importe quel sous-dossier convient, aucune dépendance entre les fichiers autre que celles listées ci-dessous).

**Prérequis :** Unity 2021 LTS ou supérieur (aucune dépendance externe).

## Modules

### Timer

Exécute une action après un délai, ou à intervalles réguliers, sans écrire de coroutine à la main.

```csharp
Timer.Instance.After(2f, () => Debug.Log("2 secondes écoulées"));
Timer.Instance.Every(1f, () => Debug.Log("Tick"), repeatCount: 5);
```

### StateMachine

Gère des transitions propres entre états, via l'interface `IState`.

```csharp
var machine = new StateMachine();
machine.ChangeState(new IdleState());
machine.Update(Time.deltaTime);
```

### SaveManager

Sauvegarde/charge des données en JSON.

```csharp
SaveManager.Save(data, "playerSave");
PlayerData loaded = SaveManager.Load<PlayerData>("playerSave");
```

### CameraUtils

Shake, clamp aux limites du niveau, zoom fluide. À attacher directement sur la Camera.

```csharp
CameraUtils cam = Camera.main.GetComponent<CameraUtils>();
cam.Shake(duration: 0.3f, magnitude: 0.2f);
cam.ZoomTo(targetSize: 3f, duration: 1f);
```

### Singleton\<T\>

Classe de base générique pour des managers accessibles partout via `.Instance`.

```csharp
public class GameManager : Singleton<GameManager>
{
    public int score;
}

GameManager.Instance.score += 10;
```

### ObjectPool

Recycle des GameObjects au lieu de `Instantiate`/`Destroy` répétés.

```csharp
GameObject bullet = bulletPool.Get(bulletPrefab, transform);
bulletPool.Release(bullet);
```

### EventBus

Communication découplée entre scripts (`static class`, pas d'instance).

```csharp
EventBus.Subscribe("PlayerDied", HandlePlayerDied);
EventBus.Unsubscribe("PlayerDied", HandlePlayerDied);
EventBus.Publish("PlayerDied");
```

### Tween

Anime des valeurs (float, Vector3) dans le temps, lié au deltaTime, avec easing optionnel.

```csharp
Tween.Instance.Float(0f, 1f, 1f, val => canvasGroup.alpha = val, easing: Tween.Ease.EaseOutQuad);
```

### Spell System

Shape :
  - Par défaut ball
  - Peux


## Roadmap

- [ ] **Système de sorts modulable (glyphes)** — en conception, inspiré de L'Atelier des Sorciers :
  séquence de glyphes tapés au clavier, le premier définissant l'élément du sort, chaque glyphe suivant
  portant soit un mouvement (flèche) soit une forme (symbole). Point ouvert : gestion du mouvement lors
  d'un changement de forme en cours d'exécution.

## License

Projet personnel — pas de licence définie pour l'instant.
