using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TestSaver : MonoBehaviour
{
    [SerializeField] private TestData testData;
    private string _json;
    private string savePath;
    
    [EditorButton]
    public void Save()
    {
        var tags = new List<AbstractTag>
        {
            new FloatTag(777.7f),
            new StringTag("Hello")
        };

        var testData = new TestData(1.1f, new Vector3(1f, 2f, 1f), 3, tags);
        _json = JsonUtility.ToJson(testData, true);
        print(_json);
        PlayerPrefs.SetString("TestData", _json);
        
        savePath = Path.Combine(Application.streamingAssetsPath, "testData.json");
        if(File.Exists(savePath) == false)
            File.Create(savePath).Close();
                
        File.WriteAllText(savePath, _json);
        
        Resources.LoadAll("TestData");
    }
    
    [EditorButton]
    public void Load()
    {
        var test = JsonUtility.FromJson<TestData>(PlayerPrefs.GetString("TestData"));
        print(test.ToString());
        File.ReadAllText(savePath);
    }
}