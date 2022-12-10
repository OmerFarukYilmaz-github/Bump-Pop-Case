using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloner : MonoBehaviour
{
    [SerializeField] int ballCountToSpawn = 10;
    [SerializeField] GameObject ballPrefab;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            StartCoroutine(CloneBalls());
        }
    }

    IEnumerator CloneBalls()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        for (int i = 0; i < ballCountToSpawn; i++)
        {
            GameObject ball = Instantiate(ballPrefab, this.transform.position, Quaternion.identity, null);



            ball.GetComponent<Rigidbody>().AddForce(new Vector3(
                                                                Random.Range(-360f, 360f),
                                                                transform.position.y,
                                                                Random.Range(-360f, 360f)) * 4f);
        }
        Destroy(gameObject);
    }
}
