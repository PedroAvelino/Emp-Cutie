using UnityEngine;

public class ShadowManager : MonoBehaviour
{
    [SerializeField] string _prefabName = "Shadow";

    [SerializeField] float _shadowScale;
    [SerializeField] Vector2 _position = Vector2.zero;
    private void Awake()
    {
        SpawnShadow();
    }

    void SpawnShadow( )
    {
        if( _prefabName.Length <= 0 ) return;

        string path = "Prefabs/" + _prefabName;
        GameObject loadedAsset = Resources.Load(path) as GameObject;
        if( loadedAsset == null ) return;

        GameObject shadow = GameObject.Instantiate(loadedAsset); //Spawn the object

        //Set the parent
        shadow.transform.parent = transform;

        shadow.transform.localPosition = _position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawIcon( transform.TransformPoint( _position), "Shadow", true);
    }
}
