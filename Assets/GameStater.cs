using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStater : MonoBehaviour {
    public TransformVar sceneTransition;
    private IEnumerator Start() {
        yield return new WaitForSeconds(2);
        while (true) {
            if (Input.anyKey && Input.inputString != "") {
                Debug.Log(Input.inputString);
                sceneTransition.Value.GetComponent<SceneTransitioner>().StartTransitionTo("WorldScene");
            }
            yield return null;
        }
    }
}
