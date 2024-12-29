using UnityEngine;

public class TriggerVoiceOver : MonoBehaviour
{
    public VoiceOverSystem voiceOverSystem; // VoiceOverSystem referans�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            voiceOverSystem.TriggerVoiceOver(); // VoiceOver tetikle
        }
    }
}
