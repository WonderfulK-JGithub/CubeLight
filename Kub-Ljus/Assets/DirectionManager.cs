using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionManager : MonoBehaviour
{
    //lista av håll som kuber ska raycasta
    public List<Vector3> cubeDirList;

    //låst av håll som roterade kuber ska raycasta
    public List<Vector3> rotatedCubeDirList;

    public List<Vector3> circleDirList;
}
