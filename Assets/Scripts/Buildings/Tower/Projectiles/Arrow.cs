using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform target;

    public int damage = 10;
    public float speed = 70f;

    public void Seek(Transform _target)
    {
        target = _target;
    }


    private void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.LookAt(target);

        Vector3 direction = target.position - transform.position;
        float disctanceThisFrame = speed * Time.deltaTime;

        if(direction.magnitude <= disctanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * disctanceThisFrame, Space.World);

    }

    void HitTarget()
    {
        Destroy(gameObject);
        Ork ork = target.GetComponent<Ork>();
        if(ork) ork.TakeDamage(damage);
    }
}
