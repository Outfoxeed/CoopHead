using System.Collections.Generic;
using OutFoxeed.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OutFoxeed.UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private UiInfo[] uiInfos;
        private bool mainMenu;
        private Dictionary<UiType, UiMenu> uiPrefabs;

        public enum UiType
        {
            None,
            Game,
            MainMenu,
            Pause,
            Options
        };

        [SerializeField, ReadOnly] private UiType currentUiType;
        [SerializeField, ReadOnly] private UiType lastUiType;
        [SerializeField, ReadOnly] private UiMenu currentUI;

        private void Awake()
        {
            // Init prefabs dictionnary
            uiPrefabs = new Dictionary<UiType, UiMenu>();
            for (int i = 0; i < uiInfos.Length; i++)
            {
                UiInfo uiInfo = uiInfos[i];
                uiPrefabs.Add(uiInfo.type, uiInfo.prefab);
            }

            mainMenu = SceneManager.GetActiveScene().buildIndex == 0;
            OpenUiOfType(mainMenu ? UiType.MainMenu : UiType.Game);
        }

        public void OpenUiOfType(UiType type)
        {
            if (type == currentUiType)
                return;

            // Destroy current UI if needed
            if (currentUI != null)
            {
                Destroy(currentUI.gameObject);
                currentUI = null;
            }
            
            // Instantiate the new UI
            if (uiPrefabs.TryGetValue(type, out UiMenu menuPrefab))
            {
                currentUI = Instantiate(menuPrefab, transform);
                currentUI.Init(this);
            }
            
            // Update vars
            lastUiType = currentUiType;
            currentUiType = type;
        }

        public void LeaveCurrentUiMenu()
        {
            if (currentUI == null)
                return;

            switch (currentUiType)
            {
                case UiType.None:
                    break;
                case UiType.Game:
                    break;
                case UiType.MainMenu:
                    break;
                case UiType.Pause:
                    ExitAllUi();
                    break;
                case UiType.Options:
                    OpenUiOfType(lastUiType);
                    break;
            }
        }

        public void ExitAllUi()
        {
            if (currentUI == null || currentUiType == UiType.Game)
                return;
            Destroy(currentUI.gameObject);
            currentUI = null;

            lastUiType = currentUiType;
            currentUiType = UiType.Game;
        }

        [System.Serializable]
        public struct UiInfo
        {
            public UiType type;
            public UiMenu prefab;
        }
    }
}