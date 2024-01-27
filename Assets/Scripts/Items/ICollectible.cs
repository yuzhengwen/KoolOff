using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectible 
{
    public void OnCollect();
    public void Use();

    public ItemSO GetItemSO();
}
