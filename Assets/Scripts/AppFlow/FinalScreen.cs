using UnityEngine;

public class FinalScreen : MonoBehaviour
{
    [SerializeField] private Transform result;
    
    public void Start()
    {
        result.parent = transform;
    }
}
