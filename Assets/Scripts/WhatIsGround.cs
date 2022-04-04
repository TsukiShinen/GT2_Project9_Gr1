using UnityEngine;

public class WhatIsGround : MonoBehaviour
{
    [SerializeField] private GameParameters parameters;

    public string GroundTag { get; private set; }

    void FixedUpdate()
    {
        var hits = Physics2D.RaycastAll(transform.position, transform.forward);
        foreach (var item in hits)
        {
            if (item.collider.CompareTag(parameters.TagTank)) { continue; }
            GroundTag = item.collider.tag;
            return;
        }
    }
}
