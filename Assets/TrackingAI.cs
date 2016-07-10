using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TrackingAI : MonoBehaviour
{

    public enum targetTypes
    {
        friendly,
        enemy,
        both
    }

    static List<GameObject> targets;

    public float degreesPerSecond = 180;
    public float trackingRadius = 2;
    public targetTypes targetType = targetTypes.enemy;

    GameObject target = null;

    Team team = null;

    // Use this for initialization
    void Start ()
    {
        // set once and is static. All enemies exist on spawn so we don't want to call this slow function repeatedly
        if (targets == null) {
            targets = Object.FindObjectsOfType<Team> ().Select (team => team.gameObject).ToList();
        }
        UpdateTarget ();
    }

    void UpdateTarget()
    {
        targets.RemoveAll (target => target == null);

        // I love me some lambdas
        // Get objects within range
        List<GameObject> trackableTargets = targets.Where((target) => (target.transform.position - this.transform.position).sqrMagnitude < trackingRadius * trackingRadius).ToList();

        // Get trackable targets
        trackableTargets = targets.Where((target) => (target.GetComponent("Flag") && target.GetComponent<Flag>().isTrackable)).ToList();

        // Filter targets by target type, ignore if no team was set
        if (this.team)
        {
            switch ((int)targetType)
            {
            case (int)targetTypes.friendly:
                trackableTargets = trackableTargets.Where (target =>
                {
                    Team team = target.GetComponent<Team> ();
                    return team && team.IsFriendly(this.team);
                }).ToList();
                break;
            case (int)targetTypes.enemy:
                trackableTargets = trackableTargets.Where (target =>
                {
                    Team team = target.GetComponent<Team> ();
                    return team && team.IsEnemy(this.team);
                }).ToList();
                break;
            case (int)targetTypes.both:
                // do nothing, already have both
                break;
            default:
                // do nothing, default to both, shouldn't be possible due to editor setting targetType
                break;
            }
        }

        // Sort and get the closest target
        if (trackableTargets.Count() == 0)
        {
            target = null;
        }
        else
        {
            trackableTargets.Sort((target1, target2) => (int)(target1.transform.position - this.transform.position).sqrMagnitude - (int)(target2.transform.position - this.transform.position).sqrMagnitude);
            target = trackableTargets [0];
        }
    }

    
    // Update is called once per frame
    void Update ()
    {
        UpdateTarget ();

        // when target is null, don't apply AI (continue flying in a straight line)
        if (target != null)
        {
            int sign = Vector3.Cross (target.transform.position - this.transform.position, this.transform.up).z < 0 ? 1 : -1;
            float angle = Vector3.Angle (target.transform.position - this.transform.position, this.transform.up);
            float rate = degreesPerSecond * Time.deltaTime;

            // if remaining angle is less than turn rate, use remaining angle (won't overshoot)
            rate = angle < rate ? angle : rate;

            transform.Rotate (Vector3.forward, sign * rate);
        }
    }

    void OnDrawGizmos()
    {
        if (target != null)
        {
            // draw line to target
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine (this.transform.position, target.transform.position);

            // draw ray in facing direction
            Gizmos.color = Color.red;
            Gizmos.DrawRay (this.transform.position, this.transform.up);
        }

        // draw tracking radius
        Gizmos.color = Color.yellow;
        float theta = 0;
        float x = trackingRadius * Mathf.Cos(theta);
        float y = trackingRadius * Mathf.Sin(theta);
        Vector3 pos = transform.position + new Vector3(x, y, 0);
        Vector3 newPos = pos;
        Vector3 lastPos = pos;
        for(theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
        {
            x = trackingRadius * Mathf.Cos(theta);
            y = trackingRadius * Mathf.Sin(theta);
            newPos = transform.position + new Vector3(x, y, 0);
            Gizmos.DrawLine(pos, newPos);
            pos = newPos;
        }
        Gizmos.DrawLine(pos,lastPos);
    }
}
