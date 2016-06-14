using UnityEngine;


    public static class Extentions
    {
    public static bool Is<T>(this GameObject obj)
    {
        return obj.GetComponent<T>()!= null;
    }
    }

