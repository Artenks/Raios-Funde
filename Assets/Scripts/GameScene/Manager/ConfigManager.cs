using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public ConfigSave ConfigSave;
    public GameOnFirstExecute GameOnFirstExecute;
    public MusicManager MusicManager;
    public EffectsManager EffectsManager;
    public ResolutionScript ResolutionScript;

    void Awake()
    {
        ConfigSave.Load();

        if (!ConfigSave.Info.noIsFirstTimePlaying)
        {
            GameOnFirstExecute.FirstExecuteInvoke();
        }
        else
        {
            MusicManager.ConfigurateMusicOnOpen();
            EffectsManager.ConfigurateEffectOnOpen();
            ResolutionScript.ConfigurateResolutionOpen();

            GameOnFirstExecute.FirstExecuteInvoke();
        }
    }

}
