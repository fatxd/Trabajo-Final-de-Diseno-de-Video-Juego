using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        triggered = true;

        if (WinManager.Instance != null)
            WinManager.Instance.ShowWin();
    }
}