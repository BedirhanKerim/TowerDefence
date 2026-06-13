using _Project.Scripts.Systems;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IBoard, Board>(Lifetime.Singleton);
        builder.RegisterComponentInHierarchy<BoardBuilder>();
    }
}
