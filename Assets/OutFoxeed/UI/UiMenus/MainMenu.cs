using UnityEngine;
using UnityEngine.SceneManagement;

namespace OutFoxeed.UI.UiMenus
{
    public class MainMenu : UiMenu
    {
        public void PlayButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}