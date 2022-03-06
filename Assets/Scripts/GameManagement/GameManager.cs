using UnityEngine;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        public GameSettingsStore gameSettingsStore;

        private void Awake() {
            gameSettingsStore.CheckExisting();
        }
    }
}
