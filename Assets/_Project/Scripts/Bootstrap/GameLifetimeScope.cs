using _Project.Scripts.Data;
using _Project.Scripts.Events;
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
        builder.RegisterEntryPoint<EnemySystem>().AsSelf();
        builder.RegisterEntryPoint<LevelManager>();
        builder.RegisterComponentInHierarchy<EnemySpawner>();
        builder.RegisterComponentInHierarchy<UIManager>();
    }
}
