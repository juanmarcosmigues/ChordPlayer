using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

public static class ArrayExtensions
{
    public static T GetRandom<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
    public static T[] Copy <T>(this T[] array)
    {
        T[] cloned = new T[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            cloned[i] = array[i];
        }
        return cloned;
    }
    public static T[] StartFrom<T>(this T[] array, int newStartingIndex)
    {
        // Rearrange the array
        T[] rearrangedArray = new T[array.Length];
        int currentIndex = 0;

        // Add elements starting from the new starting index
        for (int i = newStartingIndex; i < array.Length; i++)
        {
            rearrangedArray[currentIndex++] = array[i];
        }

        // Add the remaining elements from the start of the array
        for (int i = 0; i < newStartingIndex; i++)
        {
            rearrangedArray[currentIndex++] = array[i];
        }

        return rearrangedArray;
    }
}
