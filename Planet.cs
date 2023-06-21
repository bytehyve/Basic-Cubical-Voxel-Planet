// Define directions
public enum Direction {
    Forward,
    Back,
    Left,
    Right,
    Up,
    Down
}

public class Planet 
{
    private readonly Plane[] planes;

    public Planet(ushort planeSize, ushort planeHeight, float scale, float heightScale, float deformation) {
        // Initializing the planes
        planes = new Plane[6];

        for (byte i = 0; i < 6; i++) {
            planes[i] = new Plane(planeSize, planeHeight, (Direction)i, scale, heightScale, deformation);
        }
    }

    // Return Voxel
    public Voxel GetVoxel(ushort x, ushort y, ushort z, Direction side) {
        return planes[(byte)side].GetVoxel(x, y, z);
    }
}
