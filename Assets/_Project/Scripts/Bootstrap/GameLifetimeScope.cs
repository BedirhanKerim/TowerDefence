using _Project.Scripts.Data;
using _Project.Scripts.Events;
using _Project.Scripts.Services;
using _Project.Scripts.Systems;
using _Project.Scripts.UI;
using GenericEventBus;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameObject _gridPrefab;
    [SerializeField] private LevelConfig _levelConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(new GenericEventBus<IGameEvent>());
        builder.RegisterInstance(_gridPrefab);
        builder.RegisterInstance(_levelConfig);
        builder.Register<IBoard, Board>(Lifetime.Singleton);
        builder.Register<ISaveService, JsonSaveService>(Lifetime.Singleton);
        builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
        builder.RegisterEntryPoint<EnemySystem>().AsSelf();
        builder.RegisterEntryPoint<TowerSystem>().AsSelf();
        builder.RegisterEntryPoint<WinLoseSystem>();
        builder.RegisterEntryPoint<LevelManager>();
        builder.RegisterComponentInHierarchy<EnemySpawner>();
        builder.RegisterComponentInHierarchy<UIManager>();
        builder.RegisterComponentInHierarchy<TowerPlacementSystem>();
        builder.RegisterComponentInHierarchy<TowerToolbar>();

    }
}
