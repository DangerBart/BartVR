using UnityEngine;
using System.Collections;

public class SteamVR_Teleporter : MonoBehaviour
{
	public int maxDist = 5;
	//Set the maxCooldown before teleporting again
	public int maxCooldown;
	//The cooldown itself
	private int cooldown;
	//The material of the teleport hand
	public Material hand;

	public enum TeleportType
	{
		TeleportTypeUseTerrain,
		TeleportTypeUseCollider,
		TeleportTypeUseZeroY
	}

	public bool teleportOnClick = false;
	public TeleportType teleportType = TeleportType.TeleportTypeUseZeroY;

	Transform reference
	{
		get
		{
			var top = SteamVR_Render.Top();
			return (top != null) ? top.origin : null;
		}
	}

	void Start()
	{
		cooldown = maxCooldown;

		var trackedController = GetComponent<SteamVR_TrackedController>();
		if (trackedController == null)
		{
			trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
		}

		trackedController.TriggerClicked += new ClickedEventHandler(DoClick);

		if (teleportType == TeleportType.TeleportTypeUseTerrain)
		{
			// Start the player at the level of the terrain
			var t = reference;
			if (t != null)
				t.position = new Vector3(t.position.x, Terrain.activeTerrain.SampleHeight(t.position), t.position.z);
		}
	}

	//Teleport cooldown
	void FixedUpdate() {
		if (teleportOnClick == false) {
			cooldown -= 1;
			//Make the hand red when teleport is false.
			hand.color = Color.red;
			//Reset the cooldown and color hand after teleport cooldown
			if(cooldown == 0) {
				teleportOnClick = true;
				cooldown = maxCooldown;
				hand.color = Color.white;
			}
		}
	}

	void DoClick(object sender, ClickedEventArgs e)
	{
		if (teleportOnClick)
		{
			// First get the current Transform of the the reference space (i.e. the Play Area, e.g. CameraRig prefab)
			var t = reference;
			if (t == null)
				return;

			// Get the current Y position of the reference space
			float refY = t.position.y;
			Debug.Log(refY.ToString());

			// Create a plane at the Y position of the Play Area
			// Then create a Ray from the origin of the controller in the direction that the controller is pointing
			Plane plane = new Plane(Vector3.up, -refY);
			Ray ray = new Ray(this.transform.position, transform.forward);

			// Set defaults
			bool hasGroundTarget = false;
			float dist = 0f;
			if (teleportType == TeleportType.TeleportTypeUseTerrain) // If we picked to use the terrain
			{
				RaycastHit hitInfo;
				TerrainCollider tc = Terrain.activeTerrain.GetComponent<TerrainCollider>();
				hasGroundTarget = tc.Raycast(ray, out hitInfo, 1000f);
				dist = hitInfo.distance;
			}
			else if (teleportType == TeleportType.TeleportTypeUseCollider) // If we picked to use the collider
			{
				RaycastHit hitInfo;
				hasGroundTarget = Physics.Raycast(ray, out hitInfo);
				dist = hitInfo.distance;
			}
			else // If we're just staying flat on the current Y axis
			{
				// Intersect a ray with the plane that was created earlier
				// and output the distance along the ray that it intersects
				hasGroundTarget = plane.Raycast(ray, out dist);
			}

			if (hasGroundTarget)
			{
				// Get the current Camera (head) position on the ground relative to the world
				Vector3 headPosOnGround = new Vector3(SteamVR_Render.Top().head.position.x, refY, SteamVR_Render.Top().head.position.z);

				// We need to translate the reference space along the same vector
				// that is between the head's position on the ground and the intersection point on the ground
				// i.e. intersectionPoint - headPosOnGround = translateVector
				// currentReferencePosition + translateVector = finalPosition
				//Teleport a maxdistance
				t.position = t.position + (ray.origin + (ray.direction * maxDist)) - headPosOnGround;
				//Make sure the player doesn't fly
				t.position = new Vector3(t.position.x, 0, t.position.z);
				//Make sure the player can't teleport again
				teleportOnClick = false;
			}
		}
	}
}

