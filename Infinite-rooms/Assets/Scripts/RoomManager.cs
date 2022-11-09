using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class RoomManager : MonoBehaviour{


    public XRGrabInteractable[] keyCubes;
    public int cubesGrabbed;
    public GameObject door;

    // Start is called before the first frame update
    void Start(){
        cubesGrabbed = 0;
        for (int x = 0; x < keyCubes.Length; x++) {
            // add the grabbedKey listener method to all the key cubes
            keyCubes[x].activated.AddListener(grabbedKey); 
        }
    }

    // Update is called once per frame
    void Update(){
        if (cubesGrabbed == 6){
            // if all the cubes are grabbed slowly move the door down with a little wobble
            Vector3 newPos = door.transform.position;
            newPos.y -= 1*Time.deltaTime;
            newPos.z += Random.Range(-0.5f, 0.5f)*Time.deltaTime; // wobble door to look like its sliding
            newPos.x += Random.Range(-0.05f, 0.05f) * Time.deltaTime; // wobble door to look like its sliding
            door.transform.position = newPos;
            if (door.transform.position.y < -50) {
                Destroy(door); // remove door once its out of sight to save on processing
                cubesGrabbed = -1; // to avoid if(cubesGrabbed == 6) running indefinitely
            }
        }
        
        
    }

    void grabbedKey(ActivateEventArgs args) {
        cubesGrabbed++;
        //add to cubes grabbed
        Vector3 newPos = args.interactableObject.transform.position;
        newPos.y -= 100;
        args.interactableObject.GetAttachTransform(args.interactorObject).position = newPos;
        args.interactableObject.transform.position = newPos;
        // the above removes the key cube from sight after activation, could not destroy
        Debug.Log(cubesGrabbed);
    }
}
