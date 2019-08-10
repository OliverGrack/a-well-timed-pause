using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectivManager : MonoBehaviour
{
    public BoolVar[] objectivs;
    public TransformVar sceneTransition;
    
    private IEnumerator Start() {
        foreach (var o in objectivs) {
            o.Reset();
        }

        while (!objectivs.All(o => o.Value)) {
            yield return null;
        }
        yield return new WaitForSeconds(2.5f);
        sceneTransition.Value.GetComponent<SceneTransitioner>().StartTransitionTo("WonScene");
    }
}
