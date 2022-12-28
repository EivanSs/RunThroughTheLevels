using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldToLoading : MonoBehaviour
{
    private static bool _loaded;
    private void Awake()
    {
        if (!_loaded)
        {
            SceneManager.LoadScene(0);

            _loaded = true;
        }
    }
}
