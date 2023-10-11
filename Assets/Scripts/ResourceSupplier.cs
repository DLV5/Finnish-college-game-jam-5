using System.Collections;
using UnityEngine;

public class ResourceSupplier : SnapPoint
{
    [SerializeField] private GameObject _prefab;
    public GameObject Prefab 
    {
        get { return _prefab; } 
        set { _prefab = value; } 
    }

    public bool ProduceResources;
    [SerializeField] private float _cooldown;

    private void Start()
    {
        snapPointType = SnapPointType.ResourceSupplier;
        StartCoroutine(SpawnMaterialWithCooldown());
    }
    private IEnumerator SpawnMaterialWithCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(_cooldown);
            if (ProduceResources)
            {
                SpawnMaterial();
            }
        }
    }
    private void SpawnMaterial()
    {
        if (ConnectedConveyorPoint == null) 
            return;

        GameObject material = Instantiate(_prefab, gameObject.transform.position, Quaternion.identity);
        material.GetComponent<MaterialMovement>().NextPointToMove = ConnectedConveyorPoint;
    }
}
