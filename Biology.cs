using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This scrpit is responsible for moving, charging from a battery and dying.
/// </summary>
public class Biology : MonoBehaviour
{
    public float energy = 100;
    Rigidbody rb;
    Material material;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Color c = Color.white;
        c.r = 100 / energy;
        c.b = energy / 100;
        material.color = c;

        if (energy <= 0)
        {
            Die();
        }
        if (transform.position.y < -5)
        {
            Die();
        }
    }
    public void Die()
    {
        Instantiate<GameObject>(explosion, transform.position, new Quaternion());
        Destroy(this.gameObject);
    }
    public void Move(Vector3 direction)
    {
        rb.AddForce(direction / 100, ForceMode.VelocityChange);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            if (energy < 150)
            {
                energy += .5f;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            //Attack
            collision.rigidbody.AddRelativeForce(rb.velocity * energy * 10, ForceMode.Force);
            Biology b = collision.transform.GetComponent<Biology>();
            if (b)
            {
                b.energy -= energy * rb.velocity.magnitude / 10;
            }
        }
    }
    public void GoTo(Vector3 place)
    {
        Move(Vector3.Normalize(place - transform.position));
    }
}
