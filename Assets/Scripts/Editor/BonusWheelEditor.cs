using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(BonusWheel))]
public class BonusWheelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var wheel = (BonusWheel)target;

        if (GUILayout.Button("Init"))
        {
            Undo.RecordObject(wheel, "Init Bonus Wheel");
            wheel.Init();
            EditorUtility.SetDirty(wheel);
        }

        if (wheel.RewardData != null)
        {
            for (int i = 0; i < wheel.RewardData.Count; ++i)
            {
                if (GUILayout.Button("Win " +wheel.RewardData[i].name))
                {
                    if (!EditorApplication.isPlaying)
                    {
                        Debug.Log("Works in play mode only");
                    }
                    else
                    {
                        wheel.OnSpinButtonPressed(i);
                    }


                }
            }
        }


    }

}

