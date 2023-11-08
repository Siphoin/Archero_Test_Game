using UnityEngine;
using UnityEngine.SceneManagement;

namespace Archero.Services
{
    public class LevelService : IService
    {       
        public void Initialize()
        {
            Debug.Log("level service up");
        }

        public void RestartLevel ()
        {
            var thisScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(thisScene);
        }
    }
}
