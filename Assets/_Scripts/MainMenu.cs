using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CoopHead
{
    public class MainMenu : MonoBehaviour
    {
        private Rewired.Player rewiredPlayer;
        private void Start()
        {
            rewiredPlayer = ReInput.players.GetPlayer(0);
        }

        private void Update()
        {
            if (rewiredPlayer.GetAnyButtonDown())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}