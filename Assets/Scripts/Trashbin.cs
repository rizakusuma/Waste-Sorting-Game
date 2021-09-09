using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashType
{
    Green, Yellow, Red
}

public class Trashbin : MonoBehaviour
{
    [SerializeField] private SpriteRenderer trashbinSpriteRenderer;
    [SerializeField] private Sprite[] trashbinSprites;

    private TrashType _trashType;
    public TrashType trashType {
        get
        {
            return _trashType;
        }

        private set {
            _trashType = value;
            RefreshTrashType();
        }
    }

    private int trashTypeCount;

    private void Awake()
    {
        trashTypeCount = Enum.GetValues(typeof(TrashType)).Length;
    }

    public void ChangeNextTrashType()
    {
        ChangeTrashType((TrashType)(((int)trashType + 1) % trashTypeCount));
    }

    public void ChangeTrashType(TrashType trashType)
    {
        this.trashType = trashType;
    }

    private void RefreshTrashType()
    {
        int typ = (int)trashType;

        if (typ >= 0 && typ < trashTypeCount)
        {
            trashbinSpriteRenderer.sprite = trashbinSprites[typ];
        } else
        {
            trashbinSpriteRenderer.sprite = trashbinSprites[0];
        }
    }
}
