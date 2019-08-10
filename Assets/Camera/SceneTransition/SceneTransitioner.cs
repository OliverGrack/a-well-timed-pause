using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    private string nextScene;
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    public void StartTransitionTo(string scene) {
        nextScene = scene;
        anim.SetBool("Show", true);
    }

    public void _handleTransitionFinished() {
        SceneManager.LoadScene(nextScene);
    }
}
