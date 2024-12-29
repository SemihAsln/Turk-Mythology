using UnityEngine;

public class TriggerVoiceOver : MonoBehaviour
{
    public VoiceOverSystem voiceOverSystem; // VoiceOverSystem referansý

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            voiceOverSystem.TriggerVoiceOver(); // VoiceOver tetikle
        }
    }
}
