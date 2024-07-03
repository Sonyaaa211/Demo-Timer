using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public static class PlayerData
{
    const string TASK_COMPLETED = "Task_completed";
    
    public static void SetTaskCompleted(int taskId)
    {
        string str = PlayerPrefs.GetString(TASK_COMPLETED, "");
        if (str != "")
        {
            str = str + "-" + taskId.ToString();
        }
        else
        {
            str = taskId.ToString();
        }
        PlayerPrefs.SetString(TASK_COMPLETED, str);
    }
    public static int[] GetTasksCompleted()
    {
        return GetList(TASK_COMPLETED, "0");
    }
    private static int[] GetList(string key, string defaultValue = "")
    {
        string str = PlayerPrefs.GetString(key, defaultValue);
        string[] parts = str.Split('-');

        int[] nums = new int[parts.Length];
        for (int i = 0; i < parts.Length; i++)
        {
            nums[i] = int.Parse(parts[i]);
        }

        return nums;
    }
}
