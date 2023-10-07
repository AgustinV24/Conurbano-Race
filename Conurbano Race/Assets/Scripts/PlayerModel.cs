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
    public float rotationThreshold = 0.001f;
    public Item[] items = new Item[3];

    [SerializeField] GameObject _camera;
    float rotation;
    private bool canRotate;
    private bool canMove;
    float forwardSpeed;

    public GameObject inkImage;
    public GameObject horn;

    public override void Spawned()
    {
        items[0] = new SpeedBoost();
        items[0] = new InkItem();
        items[0] = new HornItem();
        
        if (!HasInputAuthority)
        {
            _camera.SetActive(false);
        }
    }
    public void Update()
    {
        forwardSpeed = Vector3.Dot(kartRigidbody.Rigidbody.velocity, transform.forward);

        
        canRotate = Mathf.Abs(forwardSpeed) > rotationThreshold;
    }
    public override void FixedUpdateNetwork()
    {
       
        if (GetInput(out _inputData))
        {
            if (canMove)
            {
                Move(_inputData.xMovement, _inputData.zMovement);
            }        
            
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

        float speedReduction = 100 - Mathf.Clamp(Vector3.Angle(transform.forward, kartRigidbody.Rigidbody.velocity), 0, 70);
        if(!_inputData.isBeingBoosted)
            kartRigidbody.Rigidbody.velocity = Vector3.ClampMagnitude(kartRigidbody.Rigidbody.velocity, speedReduction);


        // Aplicar fuerza al Rigidbody para mover el auto



        // Calcular la velocidad de rotación

        if (canRotate)
        {
            rotation = xAxis * rotationSpeed * Time.fixedDeltaTime;
            // Rotar el auto
            Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotation);
            kartRigidbody.Rigidbody.MoveRotation(kartRigidbody.Rigidbody.rotation * deltaRotation);
        }

        
    }

    public void CouroutineActivator(IEnumerator Cou)
    {
        StartCoroutine(Cou);
    }

    public IEnumerator ActivateImage()
    {
        inkImage.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        inkImage.SetActive(false);
    }

    public IEnumerator HornActivation()
    {
        horn.SetActive(true);
        yield return new WaitForSeconds(3f);
        horn.SetActive(false);
    }

    public IEnumerator MovementLimiting()
    {
        canMove = false;
        yield return new WaitForSeconds(1.5f);
        canMove = true;
    }
}
