using OTBG.Utility;
using UnityEngine.Audio;

namespace OTBG.Settings
{
    public class SettingsManager : Singleton<SettingsManager>
    {

        private void Awake()
        {
            base.Awake();
            SaveData.CreateDefaultData();
        }

    }
}

