using System.Collections.Generic;
using UnityEngine;

public class DynamicColliderMerger : MonoBehaviour
{
    public List<BoxCollider> collidersToMerge = new List<BoxCollider>();
    private BoxCollider mergedCollider;

    void Update()
    {
        MergeColliders();
    }

    private void MergeColliders()
    {
        if (mergedCollider != null)
        {
            Destroy(mergedCollider.gameObject);
        }

        if (collidersToMerge.Count == 0) return;

        Bounds bounds = new Bounds(collidersToMerge[0].transform.position, Vector3.zero);

        foreach (BoxCollider col in collidersToMerge)
        {
            bounds.Encapsulate(col.bounds);
        }

        GameObject mergedObject = new GameObject("MergedCollider");
        mergedCollider = mergedObject.AddComponent<BoxCollider>();
        mergedCollider.center = bounds.center;
        mergedCollider.size = bounds.size;

        mergedObject.transform.position = bounds.center;
        mergedObject.transform.parent = this.transform;
    }

    // �ݶ��̴��� ���� ����Ʈ�� �߰�
    public void AddCollider(BoxCollider collider)
    {
        if (!collidersToMerge.Contains(collider))
        {
            collidersToMerge.Add(collider);
            collider.enabled = false;
        }
    }

    // �ݶ��̴��� ���� ����Ʈ���� ����
    public void RemoveCollider(BoxCollider collider)
    {
        if (collidersToMerge.Contains(collider))
        {
            collidersToMerge.Remove(collider);
            collider.enabled = true;
        }
    }
}