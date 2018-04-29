using System.IO;
using UnityEngine;

public class ScriptTest : MonoBehaviour {

    void Start () {
        var wrapper = new CompilerWrapper();

        // load text files and run them
        foreach (var file in Directory.GetFiles(Application.streamingAssetsPath, "*.txt")) {
            wrapper.Execute(file);
            Debug.Log($"Read file {Path.GetFileName(file)}, errors: {wrapper.ErrorsCount}, result: {wrapper.GetReport()}");
        }

        
        // see what we got! this includes built-ins as well as loaded ones
        var enemies = wrapper.CreateInstancesOf<IEnemy>();
        foreach (var enemy in enemies) {
            Debug.Log($"Type: {enemy.GetType()}, attack {enemy.attackDamage}, health = {enemy.hitPoints} hp");
        }
        
    }
}