using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCraftSpawner : MonoBehaviour
{
    [SerializeField] GameObject mineCartPrefab;
    [SerializeField] List<Transform> PathUpper;
    [SerializeField] List<Transform> PathLower;
    public void SpawnMineCart(Boolean isUpper)
    {
        Debug.Log("Spawning MineCraft");
        GameObject mineCart = Instantiate(mineCartPrefab, isUpper ? PathUpper[0].position : PathLower[0].position, Quaternion.identity);
        mineCart.GetComponent<MineCartController>().SetPath(isUpper ? PathUpper : PathLower);
    }
}
