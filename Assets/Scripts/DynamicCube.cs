using UnityEngine;

public class DynamicCube : MonoBehaviour
{
    [SerializeField] float _float_a = 1f;
    [SerializeField] float _float_b = 2f;
    void Update()
    {
        transform.Rotate(_float_a * Time.deltaTime, _float_b * Time.deltaTime, 0f);
    }
}
