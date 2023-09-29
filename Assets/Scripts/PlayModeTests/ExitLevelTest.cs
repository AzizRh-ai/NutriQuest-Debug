using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class ExitLevelTest
{
    private GameObject _go;
    private OnTrigger onTrigger;

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(_go);
    }

    [UnityTest]
    public IEnumerator OnTriggerEnterEventIsTriggered()
    {
        Debug.Log("Start");

        _go = new GameObject();
        _go.tag = "Player";

        onTrigger = _go.AddComponent<OnTrigger>();
        onTrigger.cTag = "Player";

        BoxCollider collider = _go.AddComponent<BoxCollider>();

        collider.isTrigger = true;

        _go.layer = 6;
        bool wasCalled = false;

        onTrigger.OnTriggerEnterEvent.AddListener((Collider col) =>
        {
            wasCalled = true;
        });

        onTrigger.layerMask = 1 << 6;
        onTrigger.TestTriggerEnter(onTrigger.GetComponent<Collider>());

        yield return null;

        Assert.IsTrue(wasCalled);
    }




}
