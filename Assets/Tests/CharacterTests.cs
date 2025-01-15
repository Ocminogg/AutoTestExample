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
        
        base.Setup();
        _keyboard = InputSystem.AddDevice<Keyboard>();

        var mouse = InputSystem.AddDevice<Mouse>();
        Press(mouse.rightButton);
        Release(mouse.rightButton);
    }
    
    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
        //Object.Destroy(_character);
        //Object.DestroyImmediate(_character, true);
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
        
        //Object.Destroy(characterInstance);
    }
    
    [UnityTest]
    public IEnumerator TestPlayerFallDamage()
    {
    // spawn the character in a high enough area in the test scene
        GameObject characterInstance = GameObject.Instantiate(_character, new Vector3(0f, 4f, 17.2f), Quaternion.identity);

    // Get a reference to the PlayerHealth component and assert currently at full health (1f)
        var characterHealth = characterInstance.GetComponent<PlayerHealth>();
        Assert.That(characterHealth.Health, Is.EqualTo(1f));

    // Walk off the ledge and wait for the fall
        Press(_keyboard.upArrowKey);
        yield return new WaitForSeconds(0.5f);
        Release(_keyboard.upArrowKey);
        yield return new WaitForSeconds(2f);

    // Assert that 1 health point was lost due to the fall damage
        Assert.That(characterHealth.Health, Is.EqualTo(0.9f));
        //Object.Destroy(characterInstance);
    }
    
}
