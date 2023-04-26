using OTBG.Utility;

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

