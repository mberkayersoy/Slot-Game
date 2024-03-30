using UnityEngine;

namespace MyExtensions.EventChannels
{
    /// <summary>
    /// This event channel broadcasts and carries Boolean payload.
    /// </summary>
    [CreateAssetMenu(fileName = "BoolEventChannelSO", menuName = "Events/BoolEventChannelSO")]
    public class BoolEventChannelSO : GenericEventChannelSO<bool>
    {

    }
}