using PaintingTanks.Actor.Control;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaintingTanks.Menu
{
    public class Reset : MonoBehaviour
    {
        private string currentScene;

        private void Awake()
        {
            Controller.Controls.Player.Reset.performed += ctx => ReloadScene();
            currentScene = SceneManager.GetActiveScene().name;
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene(currentScene);
        }
    }
}