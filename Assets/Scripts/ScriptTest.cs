using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScriptTest : MonoBehaviour
{

    private Text modList;
    
    void Start ()
    {
        DontDestroyOnLoad(transform.gameObject);

        //modList = GameObject.FindGameObjectWithTag("ModList").GetComponent<Text>();
        
        var wrapper = new CompilerWrapper();

        // load text files and run them
        foreach (var file in Directory.GetFiles(Application.streamingAssetsPath, "*.txt")) {
            wrapper.Execute(file);
            //modList.text +=$"{Path.GetFileName(file)}";
            Debug.Log($"Read file {Path.GetFileName(file)}, errors: {wrapper.ErrorsCount}, result: {wrapper.GetReport()}");
        }
        
        // see what we got! this includes built-ins as well as loaded ones
        var enemies = wrapper.CreateInstancesOf<IEnemy>();
        foreach (var enemy in enemies)
        {
            Debug.Log($"Type: {enemy.GetType()}, attack {enemy.attackDamage}, health = {enemy.hitPoints} hp");
            
        }
        
    }
}