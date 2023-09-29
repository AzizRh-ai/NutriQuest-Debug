using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTests
{
    private GameObject _go;
    private PlayerMovement _playerMovement;


    [TearDown]
    public void Teardown()
    {
        Object.Destroy(_go);
    }
    [UnityTest]
    public IEnumerator MovePlayerTest()
    {
        _go = new GameObject();
        _playerMovement = _go.AddComponent<PlayerMovement>();
        _playerMovement.rigidBody = _go.AddComponent<Rigidbody>();
        //optionnel
        _playerMovement.Animations = _go.AddComponent<PlayerAnimations>();
        _playerMovement.Animations.Animator = _go.AddComponent<Animator>();

        _playerMovement.speed = 5f;

        _playerMovement.Move(new Vector2(1f, 1f));

        yield return new WaitForSeconds(0.5f);

        Assert.AreNotEqual(Vector3.zero, _playerMovement.rigidBody.velocity);
    }

}
