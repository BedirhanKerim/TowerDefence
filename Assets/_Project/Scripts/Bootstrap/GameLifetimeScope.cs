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

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(new GenericEventBus<IGameEvent>());
        builder.RegisterInstance(_gridPrefab);
        builder.Register<IBoard, Board>(Lifetime.Singleton);
        builder.RegisterEntryPoint<EnemySystem>().AsSelf();
        builder.RegisterComponentInHierarchy<EnemySpawner>();
        builder.RegisterComponentInHierarchy<UIManager>();
    }
}
