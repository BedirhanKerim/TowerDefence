using _Project.Scripts.Data;

namespace _Project.Scripts.Services
{
    public interface ISaveService
    {
        SaveData Load();
        void Save(SaveData data);
    }
}
