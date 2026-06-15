using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Systems
{
    public class SceneLoader : ISceneLoader
    {
        public void Reload()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
