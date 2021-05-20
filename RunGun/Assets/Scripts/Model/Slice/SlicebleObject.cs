using Slice;
using UnityEngine;

public class SlicebleObject : MonoBehaviour
{
    [SerializeField]
    private Transform _cutPlane;
    private Collider[] _productsCollider = new Collider[1];
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField] private Material crossMaterial;
    
    [SerializeField] private float rbPower;
    [SerializeField] private GameObject pickUp;
    [SerializeField] private GameObject blood;

    void Start()
    {
        
    }
    public void Slice() 
    {
        var hits = Physics.OverlapBoxNonAlloc(_cutPlane.position, new Vector3(.5f, .1f, .5f), _productsCollider, _cutPlane.rotation, layerMask);
        if (hits <= 0)
            return;
        SlicedHull hull = SliceObject(_productsCollider[0].gameObject, crossMaterial);
        if (hull != null)
        {
            if(_productsCollider[0].gameObject.CompareTag("golem"))
            {
                //нижняя часть
                GameObject bottom = hull.CreateLowerHull(_productsCollider[0].gameObject, crossMaterial);
                bottom.transform.localScale = new Vector3(4f,4f,4f);
                bottom.layer = 8;
                Rigidbody rbBot = bottom.AddComponent(typeof(Rigidbody)) as Rigidbody;
                BoxCollider boxBottom = bottom.AddComponent(typeof(BoxCollider)) as BoxCollider;
                rbBot.AddForce(Vector3.up*rbPower);
                rbBot.AddForce(Vector3.back*rbPower);
                //верхняя часть
                GameObject top = hull.CreateUpperHull(_productsCollider[0].gameObject, crossMaterial);
                top.transform.localScale = new Vector3(4f,4f,4f);
                top.layer = 8;
                Rigidbody rbTop = top.AddComponent(typeof(Rigidbody)) as Rigidbody;
                rbTop.AddForce(Vector3.forward*rbPower);
                BoxCollider boxTop = top.AddComponent(typeof(BoxCollider)) as BoxCollider;
                //частицы
                Instantiate(blood, new Vector3(_productsCollider[0].gameObject.transform.position.x-0.5f,_productsCollider[0].gameObject.transform.position.y+1.5f, _productsCollider[0].gameObject.transform.position.z - 1) , Quaternion.identity);
                Destroy(_productsCollider[0].transform.parent.gameObject);
            }
            else
            {
                GameObject axe = Instantiate(pickUp,new Vector3(_productsCollider[0].gameObject.transform.position.x,_productsCollider[0].gameObject.transform.position.y+0.7f, _productsCollider[0].gameObject.transform.position.z - 3) , Quaternion.identity);
                //нижняя часть
                GameObject bottom = hull.CreateLowerHull(_productsCollider[0].gameObject, crossMaterial);
                bottom.transform.localScale = new Vector3(2f,2f,2f);
                bottom.layer = 10;
                Rigidbody rbBot = bottom.AddComponent(typeof(Rigidbody)) as Rigidbody;
                BoxCollider boxBottom = bottom.AddComponent(typeof(BoxCollider)) as BoxCollider;
                rbBot.AddForce(Vector3.up*rbPower);
                rbBot.AddForce(Vector3.back*rbPower);
                rbBot.AddForce(Vector3.left*rbPower);
                //верхняя часть
                GameObject top = hull.CreateUpperHull(_productsCollider[0].gameObject, crossMaterial);
                top.transform.localScale = new Vector3(2f,2f,2f);
                top.layer = 8;
                Rigidbody rbTop = top.AddComponent(typeof(Rigidbody)) as Rigidbody;
                rbTop.AddForce(Vector3.up*rbPower);
                rbTop.AddForce(Vector3.back*rbPower);
                rbTop.AddForce(Vector3.right*rbPower);
                BoxCollider boxTop = top.AddComponent(typeof(BoxCollider)) as BoxCollider;
                //частицы
                Instantiate(blood, new Vector3(_productsCollider[0].gameObject.transform.position.x-0.5f,_productsCollider[0].gameObject.transform.position.y+1.5f, _productsCollider[0].gameObject.transform.position.z - 1) , Quaternion.identity);
                Destroy(_productsCollider[0].transform.parent.gameObject);
            }
        }
    }

    private void Update()
    {
        Slice();
    }
    
    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(_cutPlane.position, _cutPlane.up, crossSectionMaterial);
    }
}
