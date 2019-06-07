using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectsInGame
{
    public static List<float> WasTakenFloatIDs = new List<float>();

    static public bool Key = true;

    static private SoundManager _soundManager;
    static public SoundManager SoundManager
    {
        get { return _soundManager; }
        set
        {
            if (_soundManager == null)
            {
                _soundManager = value;
            }
        }
    }

    static private PlayerController _playerController;
    static public PlayerController PlayerController
    {
        get { return _playerController; }
        set
        {
            if (_playerController == null)
            {
                _playerController = value;
            }
        }
    }

    static private PlayerDamage _playerDamage;
    static public PlayerDamage PlayerDamage
    {
        get { return _playerDamage; }
        set
        {
            if (_playerDamage == null)
                _playerDamage = value;
        }
    }

    static private PlayerHealth _playerHealth;
    static public PlayerHealth PlayerHealth
    {
        get { return _playerHealth; }
        set
        {
            if (_playerHealth == null)
                _playerHealth = value;
        }
    }

    static private ChangeSceneManager _changeSceneManager;
    static public ChangeSceneManager ChangeSceneManager
    {
        get { return _changeSceneManager; }
        set
        {
            if (_changeSceneManager == null)
                _changeSceneManager = value;
        }
    }

    static private CanvasController _canvasController;
    static public CanvasController CanvasController
    {
        get { return _canvasController; }
        set
        {
            if (_canvasController == null)
                _canvasController = value;
        }
    }
}
