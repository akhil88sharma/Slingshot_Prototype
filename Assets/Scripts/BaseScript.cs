using UnityEngine;

public class BaseScript : MonoBehaviour
{
    [SerializeField] GameObject target;

    private void Start()
    {
        SpawnTarget();
    }

    public void SpawnTarget()
    {
        //genrate a random location on x-axis
        Vector3 location = new Vector3(Random.Range(-10, 10), 10f, 7.35f);
        //assign the location to target
        target.transform.position = location;
    }
}
