using System;
using UnityEngine;

public class FinishFlag : MonoBehaviour
{
    public event EventHandler IsFinished;

    private void OnTriggerEnter2D(Collider2D col)
    {
        IsFinished?.Invoke(this, EventArgs.Empty);
    }
}
