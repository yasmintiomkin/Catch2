using UnityEngine;

public class BottomCollider : MonoBehaviour
{
    static public string Tag = "BottomCollider";
}

namespace ExtensionMethods
{
    public static class ColliderExtensions
    {
        public static bool IsBottomCollider(this GameObject gameObject)
        {
            return gameObject.CompareTag(BottomCollider.Tag);
        }
    }
}