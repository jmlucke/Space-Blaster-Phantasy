using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
   
    
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 startPosition = transform.position;
        float amountOfTime = 0f;
 
        while (amountOfTime < duration)
        {
            //x and y axis
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, -10f);
            amountOfTime += Time.deltaTime;
            yield return 0;
        }
        transform.position = startPosition;
    }
}
