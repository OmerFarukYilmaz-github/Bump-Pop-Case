using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloner : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    UIManager uiManager;

    public void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            Debug.Log("CLONERa girdik");
            StartCoroutine(CloneBalls());
        }
    }

    IEnumerator CloneBalls()
    {
        // direk klonlarsa ana topa carpýyorlar
        yield return new WaitForSecondsRealtime(0.1f);

        for (int i = 0; i < DatabaseManager.instance.GetBallCountToClon(); i++)
        {
           
            GameObject ball = Instantiate(ballPrefab, this.transform.position, Quaternion.identity, null);
            
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(
                                                                Random.Range(-360f, 360f),
                                                                transform.position.y,
                                                                Random.Range(-360f, 360f)) * 4f);

            uiManager.AdjustMoney(DatabaseManager.instance.GetIncomePerBall());
            GameManager.instance.incomeInCurrentPlay += DatabaseManager.instance.GetIncomePerBall();
            uiManager.AdjustBallCount(1);
        }
       

        Destroy(gameObject);
    }
}
