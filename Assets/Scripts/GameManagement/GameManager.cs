using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Global;

public class GameManager : MonoBehaviour
{
    public GameSettingsStore gameSettingsStore;

    private void Awake() {
        gameSettingsStore.CheckExisting();
    }
}
