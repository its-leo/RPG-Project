using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    #region Singleton


    public static PlayerManager instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<PlayerManager>();
            }
            return _instance;
        }
    }
    static PlayerManager _instance;

    void Awake() {
        _instance = this;
    }

    #endregion

    public GameObject player;

    public void KillPlayer() {
        //Restart the Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
