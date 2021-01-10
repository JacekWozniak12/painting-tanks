using System;
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
            GetCurrentScene();
        }

        private void GetCurrentScene()
        {
            Controller.Controls.Player.Reset.performed += ctx => ReloadScene();
            currentScene = SceneManager.GetActiveScene().name;
        }

        private void ReloadScene() => SceneManager.LoadScene(currentScene);
    }
}