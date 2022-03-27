using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public InputManager inputManager;
    public UnitSelector unitSelector;

    private void Start()
    {
        inputManager = new InputManager(this);
        unitSelector.gm = this;

        inputManager.Start();
    }

    private void Update()
    {
        inputManager.Update();
    }


}

[System.Serializable]
public class InputManager
{
    public InputManager(GameManager _gm)
    {
        gm = _gm;
    }

    [HideInInspector]
    public GameManager gm;

    public void Start()
    {

    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Character"))
                {
                    gm.unitSelector.SelectCharacter(hitInfo.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            LayerMask mask = LayerMask.GetMask(new string[] { "Terrain" });

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, mask))
            {
                gm.unitSelector.selectedAlly.moveComp.SetDestination(hitInfo.point);
            }
        }
    }
}

[System.Serializable]
public class UnitSelector
{
    public List<CharacterHandler> allAllies;
    public CharacterHandler selectedAlly;

    public UnitSelector(GameManager _gm)
    {
        gm = _gm;
    }

    [HideInInspector]
    public GameManager gm;

    public void SelectCharacter(GameObject _characterGO)
    {
        if (allAllies.Select(a => a.gameObject).Contains(_characterGO))
        {
            SelectAlly(allAllies.Where(a => a.gameObject == _characterGO).First());
        }
    }

    private void SelectAlly(CharacterHandler _character)
    {
        if (selectedAlly != _character)
        {
            selectedAlly = _character;
        }
    }
}
