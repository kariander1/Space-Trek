using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> wayPoints;
    List<float> delays;

    private float counter = 0;

    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        delays = waveConfig.getDelays();
        wayPoints = waveConfig.getWaypoints();
        transform.position = wayPoints[waypointIndex].position;
       // waypointIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypointIndex==0 || delays == null || (waypointIndex-1)>=delays.Count || counter >= delays[waypointIndex-1])
            Move();
        else
            counter += Time.deltaTime;
        
    }
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
  
    private void Move()
    {

        if (waypointIndex <= wayPoints.Count-1)
        {
            var targetPostion = wayPoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.getMoveSpeed() * Time.deltaTime;
            var rotationThisFrame = waveConfig.getRotateSpeed() * Time.deltaTime;


          
            Vector3 vectorToTarget = targetPostion- transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, q,rotationThisFrame);
            transform.position = Vector2.MoveTowards(transform.position, targetPostion, movementThisFrame);

            if (transform.position == targetPostion)
            {
                counter = 0;
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
