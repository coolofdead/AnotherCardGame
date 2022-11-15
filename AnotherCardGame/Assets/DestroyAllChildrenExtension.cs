using UnityEngine;

public static class DestroyAllChildrenExtension
{
    public static void DestroyAllChildren(this Transform transform)
    {
        int nbChildren = transform.childCount;

        for (int i = nbChildren - 1; i >= 0; i--)
        {
            Object.Destroy(transform.GetChild(i).gameObject);
        }
    }
}
