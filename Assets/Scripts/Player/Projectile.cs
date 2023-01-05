using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;

    private float direction;
    private bool hit;
    float lifeTime = 0;

    BoxCollider2D myCollider;

    private void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit)
        {
            lifeTime += Time.deltaTime;
            Debug.Log(lifeTime);

            if (lifeTime > 3)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            float movementSpeed = speed * Time.deltaTime * direction;
            transform.Translate(movementSpeed, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        myCollider.enabled = false;
        if(collision.tag == "Enemy")
            gameObject.SetActive(false);
    }

    public void SetDirection(float _direction)
    {
        lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        myCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
