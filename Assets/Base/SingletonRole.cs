using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ScriptableObjectArchitecture;

public interface IScreenListener: ISingletonRole
{
    BoolGameEvent Touching { get; }

    BoolReference Holding { get; }
}

public interface IMasterHaptic: ISingletonRole, ISettingOptionToggler<IPlayShake>
{
    bool isHapticOn { get; set; }

    void Vibrate();
}

public interface IMasterSound : ISingletonRole, ISettingOptionToggler<IPlaySound>
{
    bool isMusicOn { get; set; }

    bool isSoundOn { get; set; }
    
    void PlaySound(string soundName);
    
    void PlaySound(string soundName, GameObject owner = null, bool singleton = true);
    
    void FastPlaySound(string soundName);

    void PlayMusic(string musicName, GameObject owner = null, bool singleton = true);

    void PlaySoundAtPosition(AudioClip clip, Vector3 position);

    void StopSound(string soundName, GameObject owner = null);

    public void StopMusic(string musicName, GameObject owner = null);

    public void StopAllMusic();

    void StartPlaylist(string playlistName);

    void PlayBackground(bool isPlaying);   
}

public interface IPlayer : ISingletonRole
{
    Animator Animator { get; }
}

public interface IMainCamera : ISingletonRole, ICamera
{
    Vector3 direction { get; }

    void ResetRange();

    void ZoomAtScale(float scale = 1);

    void Normalize();
}

public interface ISingletonRole : IMono
{

}

public class SingletonRole : MonoBehaviour
{
    static IPlayer _player;
    static IMainCamera _mainCamera;
    static IGameplayFlow _gameFlow;
    static IMasterSound _masterSound;
    static IMasterHaptic _masterHaptic;
    static IScreenListener _screenListener;

    private static void TryToFindSingleton<T>(ref T singleton) where T: ISingletonRole
    {
        try
        {
            if (singleton == null || singleton.gameObject == null)
            {
                singleton = TypeFinder.FindMultiComponents<T>().First();
            }
        }
        catch
        {
            try
            {
                singleton = TypeFinder.FindMultiComponents<T>().First();
            }
            catch
            {
                singleton = default(T);
            }
        }
    }

    public static IPlayer Player
    {
        get
        {
            TryToFindSingleton(ref _player);
            return _player;
        }
    }

    public static IMainCamera Cameraman
    {
        get
        {
            TryToFindSingleton(ref _mainCamera);
            return _mainCamera;
        }
    }

    public static IGameplayFlow GameFlow
    {
        get
        {
            TryToFindSingleton(ref _gameFlow);
            return _gameFlow;
        }
    }

    public static IMasterSound MasterSound
    {
        get
        {
            TryToFindSingleton(ref _masterSound);
            return _masterSound;
        }
    }

    public static IMasterHaptic MasterHaptic
    {
        get
        {
            TryToFindSingleton(ref _masterHaptic);
            return _masterHaptic;
        }
    }

    public static IScreenListener ScreenListener
    {
        get
        {
            TryToFindSingleton(ref _screenListener);
            return _screenListener;
        }
    }
}
