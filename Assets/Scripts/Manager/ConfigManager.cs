using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public ConfigSave ConfigSave;
    public GameOnFirstExecute GameOnFirstExecute;
    public MusicManager MusicManager;
    public ResolutionScript ResolutionScript;

    void Awake()
    {
        ConfigSave.Load();

        if (!ConfigSave.Info.noIsFirstTime)
        {
            GameOnFirstExecute.FirstExecuteInvoke();
        }
        else
        {
            MusicManager.ConfigurateMusicOnOpen();
            ResolutionScript.ConfigurateResolutionOpen();

            GameOnFirstExecute.FirstExecuteInvoke();
        }
    }

}
