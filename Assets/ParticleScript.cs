using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    private const float kLifeTime=0.4f;
    private float leftLifeTime;

    private Vector3 velocity;
    private Vector3 defaultScale;
    const float kMaxVelocity = 6;

    // Start is called before the first frame update
    void Start()
    {
        
        leftLifeTime = kLifeTime;
        defaultScale = transform.localScale;

        velocity = new Vector3(
            Random.Range(-kMaxVelocity, kMaxVelocity),
            Random.Range(-kMaxVelocity, kMaxVelocity),
            0
            );
    }

    // Update is called once per frame
    void Update()
    {
        leftLifeTime-=Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        transform.localScale = Vector3.Lerp(
            new Vector3(0, 0, 0),
            defaultScale,
            leftLifeTime / kLifeTime);

        if ( leftLifeTime < 0 ) { Destroy(gameObject); }
    }
}
