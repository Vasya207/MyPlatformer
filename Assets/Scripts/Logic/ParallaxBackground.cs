using UnityEngine;

namespace Core
{
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private Vector2 parallaxEffectMultiplier;
        [SerializeField] private bool infiniteHorizontal;
        [SerializeField] private bool infiniteVertical;
        [SerializeField] private bool menuEffect;
        [SerializeField] private float movingSpeed;


        private Transform cameraTransform;
        private Vector3 lastCameraPosition;
        private float textureUnitSizeX;
        private float textureUnitSizeY;

        private void Start()
        {
            if (Camera.main != null) cameraTransform = Camera.main.transform;
            lastCameraPosition = cameraTransform.position;
            var sprite = GetComponent<SpriteRenderer>().sprite;
            var texture = sprite.texture;
            textureUnitSizeX = texture.width / sprite.pixelsPerUnit * transform.localScale.x;
            textureUnitSizeY = texture.width / sprite.pixelsPerUnit * transform.localScale.y;
        }

        private void LateUpdate()
        {
            var deltaMovement = cameraTransform.position - lastCameraPosition;
            transform.position -= new Vector3(deltaMovement.x * parallaxEffectMultiplier.x,
                deltaMovement.y * parallaxEffectMultiplier.y);
            lastCameraPosition = cameraTransform.position;

            if (infiniteHorizontal)
                if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
                {
                    var offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
                    transform.position =
                        new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
                }

            if (infiniteVertical)
                if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
                {
                    var offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
                    transform.position =
                        new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY);
                }

            if (menuEffect) transform.position -= new Vector3(movingSpeed, 0f, 0f) * Time.deltaTime;
        }
    }
}