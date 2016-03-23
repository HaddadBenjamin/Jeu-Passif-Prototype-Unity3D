﻿using UnityEngine;
using System.Collections;

public class ServiceLocator : MonoBehaviour
{
    #region Attributes & Properties
    public TextureManager TextureManager { get; protected set; }
    public MaterialManager MaterialManager { get; protected set; }
    public SpriteManager SpriteManager { get; protected set; }
    public GameObjectManager GameObjectManager { get; protected set; }
    public GameObjectReferenceManager GameObjectReferenceManager { get; protected set; }
    public BuildingsConfiguration BuildingsConfiguration { get; protected set; }
    public EventManager<EEvent> EventManager { get; protected set; }

    private static ServiceLocator instance = null;
    
    public static ServiceLocator Instance
    {
        get
        {
            if (null == instance)
            {
                instance = GameObject.FindGameObjectWithTag("ServiceLocator").GetComponent<ServiceLocator>();

                instance.Initialize();

                DontDestroyOnLoad(instance);
            }

            return instance;
        }

        private set
        { 
            instance = value; 
        }
    }
    #endregion

    #region Initialisation
    private void Initialize()
    {
        this.BuildingsConfiguration = gameObject.GetComponent<BuildingsConfiguration>();
        this.EventManager = new EventManager<EEvent>();

        AServiceComponent[] servicesComponent =
        {
            (this.TextureManager = gameObject.GetComponent<TextureManager>()),
            (this.SpriteManager = gameObject.GetComponent<SpriteManager>()),
            (this.MaterialManager = gameObject.GetComponent<MaterialManager>()),
            (this.GameObjectManager = gameObject.GetComponent<GameObjectManager>()),
            (this.GameObjectReferenceManager = gameObject.GetComponent<GameObjectReferenceManager>()),
        };

        foreach (AServiceComponent serviceComponent in servicesComponent)
            serviceComponent.InitializeByServiceLocator();
    }
    #endregion
}