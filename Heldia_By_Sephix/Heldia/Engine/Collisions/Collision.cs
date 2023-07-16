using System.Collections.Generic;
using Heldia.Engine.Enum;
using Heldia.Managers;
using Heldia.Objects;

namespace Heldia.Engine.Collisions;

public class Collision
{
    private GameObject _mainObj;
    private GameObject _secondObj;
    private List<GameObject> _objects;
    
    public Function FdelegateX { private get; set; }
    public Function FdelegateY { private get; set; }

    // Constructors
    public Collision(Function codeToExecuteX = null, Function codeToExecuteY = null)
    {
        FdelegateX = codeToExecuteX;
        FdelegateY = codeToExecuteY;
    }
    
    public Collision(GameObject obj, GameObject secondObj, Function codeToExecuteX = null, Function codeToExecuteY = null)
    {
        _mainObj = obj;
        _secondObj = secondObj;
        FdelegateX = codeToExecuteX;
        FdelegateY = codeToExecuteY;
    }
    
    public Collision(GameObject obj, List<GameObject> objects, Function codeToExecuteX = null, Function codeToExecuteY = null)
    {
        _mainObj = obj;
        _objects = objects;
        FdelegateX = codeToExecuteX;
        FdelegateY = codeToExecuteY;
    }

    // Update
    public void Update()
    {
        CheckCollisions();
    }

    // Sets
    public void SetMainObj(GameObject obj)
    {
        _mainObj = obj;
    }
    
    public void SetObjList(List<GameObject> objList)
    {
        _objects = objList;
    }
    
    public void SetSecondObj(GameObject obj)
    {
        _secondObj = obj;
    }
    
    // Other
    /// <summary>
    /// Verify the type of the instance (collision type == obj/obj or obj/objList)
    /// and apply the delegates code on x and y.
    /// </summary>
    public void CheckCollisions()
    {
        if (_mainObj != null && _objects != null)
        {
            foreach (var obj in _objects)
            {
                if (_mainObj.GetId() == (int)EObjectId.Player)
                {
                    if (obj.GetCollisionState())
                    {
                        if (Player.east || Player.west)
                        {
                            if (_mainObj.IsTouchingRight(obj) ||
                                _mainObj.IsTouchingLeft(obj))
                            {
                                FdelegateX();
                            }
                        }
                        else if (Player.north || Player.south)
                        {
                            if (_mainObj.IsTouchingTop(obj) ||
                                _mainObj.IsTouchingBottom(obj))
                            {
                                FdelegateY();
                            }
                        }
                    }
                } 
                else if (obj.GetCollisionState())
                { 
                    if (_mainObj.IsTouchingLeft(obj) || 
                        _mainObj.IsTouchingRight(obj))
                    {
                        FdelegateX();
                    } 
                    if (_mainObj.IsTouchingTop(obj) || 
                        _mainObj.IsTouchingBottom(obj))
                    {
                        FdelegateY();
                    }
                }
            }
        }
        else if (_mainObj != null && _secondObj != null)
        {
            if (_secondObj.GetCollisionState())
            { 
                if (_mainObj.IsTouchingLeft(_secondObj) || 
                    _mainObj.IsTouchingRight(_secondObj))
                { 
                    FdelegateX();
                } 
                if (_mainObj.IsTouchingTop(_secondObj) || 
                    _mainObj.IsTouchingBottom(_secondObj))
                { 
                    FdelegateY();
                }
            }
        }
    }
}