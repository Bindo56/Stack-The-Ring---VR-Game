
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ring_Object : XRGrabInteractable
{
    private Transform followTarget;

    [SerializeField] Material ringMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Get the interactor (hand/ray controller) that selected the object
        followTarget = args.interactorObject.transform;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // Stop following when released
        followTarget = null;
    }

    public void AddMAterial()
    {
        MeshRenderer meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;

       
        Material[] newMaterials = new Material[materials.Length + 1];

       
        for (int i = 0; i < materials.Length; i++)
        {
            newMaterials[i] = materials[i];
        }

       
        newMaterials[newMaterials.Length - 1] = ringMaterial;
        // newMaterials = GetComponent<Renderer>().material;
        newMaterials[0].SetColor("_BaseColor", Color.red);
        newMaterials[0].SetColor("_OutlineColor", Color.white);
        newMaterials[0].SetFloat("Outline Width", 0.001f);

        meshRenderer.materials = newMaterials;
    }
    // Update is called once per frame
    void Update()
    {
        if (followTarget != null)
        {
            // Smoothly move towards the interactor position
            transform.position = Vector3.Lerp(transform.position, followTarget.position, Time.deltaTime * 10f);
            transform.rotation = Quaternion.Lerp(transform.rotation, followTarget.rotation, Time.deltaTime * 10f);
        }
    }
}
