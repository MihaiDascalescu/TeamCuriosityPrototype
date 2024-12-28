using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isVisited;
    public bool IsVisited
    {
        get => isVisited;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        isVisited = true;
    }
}
