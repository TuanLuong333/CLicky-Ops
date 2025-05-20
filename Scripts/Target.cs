using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    protected GameManager gameManager;
    protected EffectManager effectManager;
    private float minForce = 12;
    private float maxForce = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float yPos = -3;
    public int pointValue;
    public ParticleSystem explosiveParticle;

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        effectManager = GameObject.Find("Effect Manager").GetComponent<EffectManager>();
        rb.AddForce(RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        
    }

    protected Vector3 RandomForce()
    {
        // return Vector3.up * Random.Range(minForce, maxForce);
        float verticalForce = Random.Range(minForce, maxForce);
        float horizontalForce = Random.Range(-minForce / 6, maxForce / 8); 
        return new Vector3(horizontalForce, verticalForce, 0f);
    }

    protected float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    protected Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), yPos);
    }

    //private void OnMouseDown()
    //{
    //    if (gameManager.isGameActive)
    //    {
    //        Destroy(gameObject);
    //        Instantiate(explosiveParticle, transform.position, explosiveParticle.transform.rotation);
    //        gameManager.UpdateScore(pointValue);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (gameObject.CompareTag("Good"))
        {
            gameManager.UpdateLives(-1);

        }
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosiveParticle, transform.position, explosiveParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
