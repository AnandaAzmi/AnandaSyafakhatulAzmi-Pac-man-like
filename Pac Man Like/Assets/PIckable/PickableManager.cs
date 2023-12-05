using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    private List<Pickable> _pickableList = new List<Pickable>();
    // Start is called before the first frame update
    void Start()
    {
        initPickableList();
    }

    private void initPickableList()
    {
        Pickable[] pickableObjects = GameObject.FindObjectsOfType<Pickable>();
        for (int i = 0; i < pickableObjects.Length; i++)
        {
            _pickableList.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePicked;
        }
        Debug.Log("Pickable List : " + _pickableList.Count);
    }
    private void OnPickablePicked (Pickable pickable)
    {
        _pickableList.Remove(pickable);
        if (pickable.PickableType == PickableType.PowerUp)
        {
            _player?.PickPowerUp();
        }
        if (_pickableList.Count <= 0)
        {
            Debug.Log("YOU ARE WINNER");
        }
    }
}
