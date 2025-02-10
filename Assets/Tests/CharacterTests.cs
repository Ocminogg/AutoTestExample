using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using StarterAssets;

public class CharacterTests : InputTestFixture
{
    GameObject _character = Resources.Load<GameObject>("Character");
    Keyboard _keyboard;
    
    
    public override void Setup()
    {
        SceneManager.LoadScene("Scenes/SimpleTesting");
        
        //зачем эта хрень base.Setup()????
        //base.Setup();
        
        _keyboard = InputSystem.AddDevice<Keyboard>();

        var mouse = InputSystem.AddDevice<Mouse>();
        Press(mouse.rightButton);
        Release(mouse.rightButton);
    }
    
    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
    }
    
    [UnityTest]
    public IEnumerator TestPlayerInstantiation()
    {
        yield return new WaitForSeconds(1f);
        GameObject characterInstance = GameObject.Instantiate(_character, Vector3.zero, Quaternion.identity);
        Assert.That(characterInstance, !Is.Null);
        
    }
    
    [UnityTest]
    public IEnumerator TestPlayerMoves()
    {
        yield return new WaitForSeconds(1f);
        GameObject characterInstance = GameObject.Instantiate(_character, Vector3.zero, Quaternion.identity);

        Press(_keyboard.upArrowKey);
        yield return new WaitForSeconds(1f);
        Release(_keyboard.upArrowKey);
        yield return new WaitForSeconds(1f);
        Assert.That(characterInstance.transform.GetChild(0).transform.position.z, Is.GreaterThan(1.5f));
    }
    
    [UnityTest]
    public IEnumerator TestPlayerFallDamage()
    {
    
        GameObject characterInstance = GameObject.Instantiate(_character, new Vector3(0f, 4f, 17.2f), Quaternion.identity);

        var characterHealth = characterInstance.GetComponent<PlayerHealth>();
        Assert.That(characterHealth.Health, Is.EqualTo(1f));

        Press(_keyboard.upArrowKey);
        yield return new WaitForSeconds(0.5f);
        Release(_keyboard.upArrowKey);
        yield return new WaitForSeconds(2f);

        Assert.That(characterHealth.Health, Is.EqualTo(0.9f));
    }
    
}
