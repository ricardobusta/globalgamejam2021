using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public const string PROPS_LAYER = "Props";

    [SerializeField] private Transform _target;
    [SerializeField] private float _followSpeed = 30f;
    [SerializeField] private Material _litMaterial;
    [SerializeField] private Material _diffuseMaterial;

    private void Update()
    {
        if (_target == null) return;

        Vector2 movement = (_target.position - transform.position) * _followSpeed * Time.deltaTime;
        transform.position += new Vector3(movement.x, movement.y);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var spriteRenderer = collider.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && spriteRenderer.sortingLayerName == PROPS_LAYER)
        {
            spriteRenderer.material = _litMaterial;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        var spriteRenderer = collider.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sortingLayerName == PROPS_LAYER)
        {
            spriteRenderer.material = _diffuseMaterial;
        }
    }
}
