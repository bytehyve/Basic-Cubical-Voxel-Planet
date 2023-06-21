using UnityEngine;

public enum VoxelStates {
    Empty,
    Dirt,
    Stone
}

public struct Voxel 
{
    private readonly int x, y, z;
    private readonly Direction direction;
    private readonly Vector3[] vertices;
    private VoxelStates state;

    // Constructor
    public Voxel(int x, int y, int z, Direction side,  ushort gridSize, ushort gridHeight, float scale, float deformation, VoxelStates state = VoxelStates.Empty) {
        this.x = x;
        this.y = y;
        this.z = z;
        this.direction = side;
        this.state = state;
            
        this.vertices = new Vector3[8];
        this.vertices = CalculateVertices(gridSize, gridHeight, scale, deformation);
    }

    // Calculate vertices
    private Vector3[] CalculateVertices(ushort gridSize, ushort gridHeight, float scale, float deformation) {
        Vector3[] result = new Vector3[8];
        byte vertIndex = 0;

        // Loop over all eight vertices
        for (int y = -1; y < 2; y += 2) {
            for (int x = -1; x < 2; x += 2) {
                for (int z = -1; z < 2; z += 2) {
                    // Calculate position inside plane (of `upwards` direction)
                    float localX = (this.x + x / 2f) / gridSize * 2f;
                    float localY = 1f;
                    float localZ = (this.z + z / 2f) / gridSize * 2f;

                    // Correct position offset
                    localX += 1f / gridSize - 1f;
                    localZ += 1f / gridSize - 1f;

                    // Minimize deformation
                    localX = Mathf.Tan(localX * Mathf.PI * deformation / 4f);
                    localZ = Mathf.Tan(localZ * Mathf.PI * deformation / 4f);
                    localY = Mathf.Tan(localY * Mathf.PI * deformation / 4f);

                    // Calculate normalized position (of `upwards` direction)
                    result[vertIndex] = new Vector3(localX, localY, localZ).normalized;

                    // Calculate height
                    result[vertIndex] *= 1f + (this.y + y / 2f) / gridSize;

                    // Apply scale
                    result[vertIndex] *= (gridSize * scale * 2f) / Mathf.PI;

                    // Rotate to correct direction
                    Vector3 fromAxis = RotationAxes[(int)Direction.Up];
                    Vector3 toAxis= RotationAxes[(int)direction];
                    result[vertIndex] = Quaternion.FromToRotation(fromAxis, toAxis) * result[vertIndex];

                    // Increase vertice index
                    vertIndex++;
                }
            }
        }

        // Return result
        return result;
    }

    // Return vertices
    public Vector3 GetVertice(byte index) {
        return vertices[index];
    }

    // Return state of voxel
    public VoxelStates GetState() {
        return state;
    }

    // Change state of voxel
    public void ChangeState(VoxelStates state) {
        this.state = state;
    }

    // Define vectors/axes
    public static Vector3[] RotationAxes = {
        Vector3.forward,
        Vector3.back,
        Vector3.left,
        Vector3.right,
        Vector3.up,
        Vector3.down
    };
}
