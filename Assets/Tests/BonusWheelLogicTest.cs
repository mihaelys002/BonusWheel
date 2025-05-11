using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BonusWheelLogicTest
{
    [Test]
    public void Spin1000Times()

    {

        GameObject go = new GameObject();
        GameController controller = go.AddComponent<GameController>();
        controller.Init();

        var prefabPath = "Assets/Prefabs/BonusWheel.prefab";
        var prefabGO = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        Assert.IsNotNull(prefabGO, $"Couldn’t find prefab at {prefabPath}");

        var tempRoot = PrefabUtility.LoadPrefabContents(prefabPath);
        var wheel = tempRoot.GetComponent<BonusWheel>();
        Assert.IsNotNull(wheel, "Prefab missing BonusWheel component");
        var rewardData = wheel.RewardData;

        Dictionary<string, int> result = new();

        foreach (var reward in rewardData)
            result.Add(reward.name, 0);


        for (int i = 0; i < 1000; i++)
        {
            int option = controller.SpinBonusWheel(rewardData).Result;
            result[rewardData[option].name]++;
        }

        foreach (var reward in rewardData)
        {
            Assert.Greater(result[reward.name], 0, $"{reward.name} was never recieved.");
        }
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        var directoryPath = $"{Application.persistentDataPath}/TestResults";
        var path = $"{directoryPath}/spin_results_{timestamp}.csv";

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        using (var w = new StreamWriter(path))
        {
            w.WriteLine("Reward,Count,Probability");
            foreach (var reward in rewardData)
            {
                w.WriteLine($"{reward.name},{result[reward.name]},{result[reward.name] / 10f}%");
            }
        }
        TestContext.WriteLine($"Results written to {path}");

    }

}
