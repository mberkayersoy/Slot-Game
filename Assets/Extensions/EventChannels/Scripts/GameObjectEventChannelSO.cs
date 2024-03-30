using UnityEngine;

namespace MyExtensions.EventChannels
{
    /// <summary>
    /// This is a ScriptableObject-based event that carries a GameObject as a payload.
    /// </summary>
    [CreateAssetMenu(fileName = "GameObjectChannelSO", menuName = "Events/GameObjectEventChannelSO")]
    public class GameObjectEventChannelSO : GenericEventChannelSO<GameObject>
    {
    }
}
