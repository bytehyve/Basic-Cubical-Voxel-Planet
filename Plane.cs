using UnityEngine;

public class Plane 
{
    private readonly Voxel[,,] grid;

    // Constructor
    public Plane(ushort size, ushort height, Direction side, float scale, float deformation) {
        // Create grid
        grid = new Voxel[size, height, size];

        for (ushort x = 0; x < size; x++) {
            for (ushort z = 0; z < size; z++) {
                for (ushort y = 0; y < height; y++) {
                    grid[x, y, z] = new Voxel(x, y, z, side, size, height, scale, deformation);
                }
            }
        }
    }

    // Return voxel
    public Voxel GetVoxel(int x, int y, int z) {
        return grid[x, y, z];
    }
    public Voxel GetVoxel(Vector3Int localPosition) {
        return GetVoxel(localPosition.x, localPosition.y, localPosition.z);
    }
}
