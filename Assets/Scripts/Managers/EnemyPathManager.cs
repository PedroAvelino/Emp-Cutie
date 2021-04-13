using UnityEngine;
using PathCreation;
using System.Collections.Generic;

public class EnemyPathManager : MonoBehaviour
{
    [SerializeField] PoolableObject _testObject;
    List<GameObject> _crossingEnemies = new List<GameObject>();
    PathCreator _path;
    [SerializeField] EndOfPathInstruction _end;

    [SerializeField] float _travelSpeed = 4f;

    float _dstTravelled;

    private void Awake()
    {
        _path = GetComponent<PathCreator>();
    }


    private void Start()
    {

        //TODO this shit is just test code yo
        _crossingEnemies.Add( CreateEnemyInPath() );
        _crossingEnemies.Add( CreateEnemyInPath() );
        _crossingEnemies.Add( CreateEnemyInPath() );
        _crossingEnemies.Add( CreateEnemyInPath() );
    }

    private void Update()
    {
        _dstTravelled += _travelSpeed * Time.deltaTime;
        float offset = 0f;
        foreach (var enemy in _crossingEnemies)
        {
            enemy.transform.position = _path.path.GetPointAtDistance( _dstTravelled - offset, _end);
            offset += 1f;
        }
    }

    private GameObject CreateEnemyInPath()
    {
        var enemy = Pooler.GetObject( _testObject, transform.position, Quaternion.identity);

        return enemy.gameObject;
    }
}
