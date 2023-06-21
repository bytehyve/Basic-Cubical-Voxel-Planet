using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private ushort planeSize = 16;
    [SerializeField] private ushort planeHeight = 4;
    [SerializeField] private float baseScale = 1f;
    [SerializeField] private float heightScale = 1f;
    [SerializeField, Range(0.0001f, 1f)] private float deformation = 0.75f;

    private Planet planet;

    // Update planet
    private void OnValidate() {
        planet = new Planet(planeSize, planeHeight, baseScale, heightScale, deformation);
    }
    
    // Draw vertices of planet
    private void OnDrawGizmos() {
        if (planet == null) return;

        Gizmos.color = Color.white;

        for (byte i = 0; i < 6; i++) {
            for (ushort x = 0; x < planeSize; x++) {
                for (ushort y = 0; y < planeHeight; y++) {
                    for (ushort z = 0; z < planeSize; z++) {
                        Voxel voxel = planet.GetVoxel(x, y, z, (Direction)i);

                        for (byte v = 0; v < 8; v++) {
                            Gizmos.DrawSphere(voxel.GetVertice(v), 0.05f);
                        }
                    }
                }
            }
        }
    }
}