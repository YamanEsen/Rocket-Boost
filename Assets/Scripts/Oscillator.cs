using System.Numerics;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
   [SerializeField] UnityEngine.Vector3 movementVector;
   [SerializeField] float speed;
   UnityEngine.Vector3 startPosition;
   UnityEngine.Vector3 endPosition;
   float movementFactor;
   
    void Start()
    {
        startPosition =  transform.position;
        endPosition = startPosition + movementVector;
    }

        void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed,1f);
        transform.position = UnityEngine.Vector3.Lerp(startPosition, endPosition, movementFactor);
    }
}
