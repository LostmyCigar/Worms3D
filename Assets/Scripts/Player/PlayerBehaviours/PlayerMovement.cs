using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement
{
    private PlayerInputHandler _inputHandler;
    private Rigidbody _rb;
    private Camera _camera;
    private Collider _collider;
    private CustomGravityObject _customGravityObject;
    private PlayerData _playerData;
    private LayerMask _ground;

    public PlayerMovement(PlayerInputHandler inputHandler, Rigidbody rb, Camera camera, Collider collider, CustomGravityObject customGravityObject, PlayerData playerData)
    {
        _inputHandler = inputHandler;
        _rb = rb;
        _camera = camera;
        _collider = collider;
        _customGravityObject = customGravityObject;
        _playerData = playerData;
        _ground = _playerData._ground;
    }

    public void PhysicsUpdates()
    {
        HandleRotation();
        HandleMovement();
        //Thought rigidbodies were calculated in fixedupdate anyway,
        //but movement is stuttering when put in update. ???
    }

    public void Updates()
    {
        HandleJump();
        HandleFalling();
    }


    private void HandleMovement()
    {
        Vector3 moveDir = _camera.transform.forward * _inputHandler._moveDir.z;
        moveDir += _camera.transform.right * _inputHandler._moveDir.x;

        moveDir.y = 0;
        moveDir.Normalize();

        moveDir *= _playerData._moveSpeed;
        moveDir.y = _rb.velocity.y;
        _rb.velocity = moveDir;
    }
    private void HandleRotation()
    {
        Vector3 targetDir;

        targetDir = _camera.transform.forward * _inputHandler._moveDir.z;
        targetDir += _camera.transform.right * _inputHandler._moveDir.x;
        targetDir.y = 0;


        if (targetDir == Vector3.zero)
        {
            targetDir = _rb.transform.forward;
        }

        Quaternion tr = Quaternion.LookRotation(targetDir);
        _rb.transform.rotation = Quaternion.Slerp(_rb.transform.rotation, tr, _playerData._rotationSpeed * Time.deltaTime);
    }


    #region Jump/grounded
    private void HandleJump()
    {
        if (_inputHandler._jumpInput && GroundedCheck())
        {
            Jump();
            _inputHandler.UseJumpInput();
        }
    }

    private void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        _rb.AddForce(Vector3.up * _playerData._jumpForce, ForceMode.Impulse);
    }

    private bool GroundedCheck()
    {
        Vector3 lowCenter = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y + 0.2f, _collider.bounds.center.z);
        return Physics.CheckSphere(lowCenter, _collider.bounds.extents.z, _ground);
    }

    private void HandleFalling()
    {
        if (!GroundedCheck() && _rb.velocity.y < _playerData._startFallingVelocity)
        {
            _customGravityObject.gravityScale = _playerData._fallingGravityScale;
        } else _customGravityObject.gravityScale = _playerData._normalGravityScale;
    }

    #endregion

}
