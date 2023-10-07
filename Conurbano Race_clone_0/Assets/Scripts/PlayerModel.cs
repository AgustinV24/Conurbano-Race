using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class PlayerModel : NetworkBehaviour
{
    public float speed = 10f;             // Velocidad de movimiento del auto
    public float rotationSpeed = 100f;    // Velocidad de rotación del auto

    private float horizontalInput;        // Entrada horizontal (izquierda/derecha)
    private float verticalInput;          // Entrada vertical (adelante/atrás)
    public NetworkRigidbody kartRigidbody;     // Referencia al componente Rigidbody del auto
    public Item _currentItem;
    public NetworkInputData _inputData;

    [SerializeField] GameObject _camera;

    public override void Spawned()
    {
        _currentItem = new SpeedBoost();
        if (!HasInputAuthority)
        {
            _camera.SetActive(false);
        }
    }
    public void Update()
    {
        
    }
    public override void FixedUpdateNetwork()
    {
       
        if (GetInput(out _inputData))
        {
          
            Move(_inputData.xMovement, _inputData.zMovement);
        }

        if (_inputData.isUsingItem)
        {
            Debug.Log("Entra");
            _currentItem.Actions(this);
        }
    }
    void FixedUpdate()
    {

        // Calcular la velocidad de movimiento

    }
    void Move(float xAxis, float zAxis)
    {
        Debug.Log(zAxis);
        Vector3 movement = Vector3.zero;
        if (zAxis < 0)
        {
            movement = transform.forward * zAxis * speed/3 * Time.fixedDeltaTime;
        }
        else
        {
            movement = transform.forward * zAxis * speed * Time.fixedDeltaTime;
        }

        kartRigidbody.Rigidbody.AddForce(movement, ForceMode.Impulse);

        float speedReduction = 50 - Mathf.Clamp(Vector3.Angle(transform.forward, kartRigidbody.Rigidbody.velocity), 0, 20);
        if(!_inputData.isBeingBoosted)
            kartRigidbody.Rigidbody.velocity = Vector3.ClampMagnitude(kartRigidbody.Rigidbody.velocity, speedReduction);


        // Aplicar fuerza al Rigidbody para mover el auto

      
        // Calcular la velocidad de rotación
        float rotation = xAxis * rotationSpeed * Time.fixedDeltaTime;

        // Rotar el auto
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotation);
        kartRigidbody.Rigidbody.MoveRotation(kartRigidbody.Rigidbody.rotation * deltaRotation);
    }
}
