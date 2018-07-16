using UnityEngine;

public static class MonobehaviourExtensions
{
    public static IMessenger GetMessenger(this Behaviour behaviour)
    {
        IMessenger objectMessenger = behaviour.GetComponent<IMessenger>();
        if (objectMessenger == null)
        {
            objectMessenger = behaviour.GetComponentInParent<IMessenger>();
        }
        if (objectMessenger == null)
        {
            objectMessenger = behaviour.GetComponentInChildren<IMessenger>();
        }
        return objectMessenger;
    }

    public static T GetComponentInHierarchy<T>(this Behaviour behaviour)
    {
        T comp = behaviour.GetComponent<T>();
        if (comp == null)
        {
            comp = behaviour.GetComponentInParent<T>();
        }
        if (comp == null)
        {
            comp = behaviour.GetComponentInChildren<T>();
        }
        return comp;
    }

    /// <summary>
    /// Returns the component of type T in the parent, or the parent's children
    /// (allowing access to other children of the parent, not only children of this object).
    /// </summary>
    /// <returns>The component of type T belonging to this object's parent or the parent's children.</returns>
    /// <param name="behaviour">Behaviour.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T GetAdjacentComponent<T>(this Behaviour behaviour)
    {
        if (behaviour.transform.parent == null)
        {
            return default(T);
        }
        return behaviour.transform.parent.GetComponentInChildren<T>();
    }
}
