﻿using System;
using UnityEngine;

[System.Serializable]
public class GameObjectReferences
{
    #region Fields
    [SerializeField]
    protected GameObject[] references;
    protected int[] hashIds;
    #endregion

    #region Initializer
    public void Initialize()
    {
        ObjectContainerHelper.InitializeHashIds(
            Array.ConvertAll(this.references, reference => reference.name),
            ref this.hashIds);
    }
    #endregion

    #region Behaviour Methods
    public GameObject Get(string refenceName)
    {
        return this.references[ObjectContainerHelper.GetHashCodeIndex(refenceName, ref this.hashIds)];
    }

    public GameObject Instantiate(string gameobjectName)
    {
        return GameObject.Instantiate(this.Get(gameobjectName)) as GameObject;
    }

    public GameObject Instantiate(string gameobjectName, Vector3 position, Vector3 eulerAngles)
    {
        return GameObject.Instantiate(this.Get(gameobjectName), position, Quaternion.Euler(eulerAngles)) as GameObject;
    }

    public GameObject Instantiate(string gameobjectName, Vector3 position, Vector3 eulerAngler, Transform parent)
    {
        GameObject newObject = this.Instantiate(gameobjectName, parent);

        newObject.transform.localPosition = position;
        newObject.transform.eulerAngles = eulerAngler;

        return newObject;
    }

    public GameObject Instantiate(string gameobjectName, Vector3 position, Vector3 rotation, Vector3 scale, Transform parent)
    {
        GameObject newObject = this.Instantiate(gameobjectName, position, rotation, parent);

        newObject.transform.localScale = scale;

        return newObject;
    }

    public GameObject Instantiate(string gameobjectName, Transform parent)
    {
        GameObject newObject = this.Instantiate(gameobjectName);

        newObject.transform.SetParent(parent);

        return newObject;
    }
    #endregion
}