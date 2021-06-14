using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUpdatePlayerShow : MonoBehaviour
{
    public Player playerInGame;
    public UiInventory inv;
    [SerializeField] private GameObject[] parts = new GameObject[7];
    public GameObject hair;

    private void Start()
    {
        inv.RefreshAllButtonsEvent += UpdateMesh;
    }

    private void OnEnable()
    {
        UpdateMesh();
    }

    public void UpdateMesh()
    {
        for (int i = 0; i < 5; i++)
        {
            parts[i].GetComponent<SkinnedMeshRenderer>().sharedMesh = playerInGame.playerMesh[i].GetComponent<SkinnedMeshRenderer>().sharedMesh;
        }
        for (int i = 5; i < parts.Length; i++)
        {
            parts[i].GetComponent<MeshFilter>().sharedMesh = playerInGame.playerMesh[i].GetComponent<MeshFilter>().sharedMesh;
        }

        hair.SetActive(playerInGame.hairMesh.activeSelf);

    }
}
