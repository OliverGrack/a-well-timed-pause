using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStater : MonoBehaviour
{
    private IEnumerator Start() {
        yield return new WaitForSeconds(2);
        while (true) {
            if (Input.anyKey && Input.inputString != "") {
                Debug.Log(Input.inputString);
                SceneManager.LoadScene("WorldScene");
            }
            yield return null;
        }
    }
}
