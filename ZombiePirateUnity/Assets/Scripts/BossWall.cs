using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall : MonoBehaviour
{
    public GameObject walls;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (NormalZombieAI.NumberOfZombies <= 0)
        {
            Destroy(walls);
        }

    }
}
