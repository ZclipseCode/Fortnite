using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class VBO
{
    public int id;

    public VBO(List<Vector3> data)
    {
        id = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, id);
        GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector3.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);
    }
    public VBO(List<Vector2> data)
    {
        id = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, id);
        GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector2.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);
    }

    public void Bind() { GL.BindBuffer(BufferTarget.ArrayBuffer, id); }
    public void Unbind() { GL.BindBuffer(BufferTarget.ArrayBuffer, 0); }
    public void Delete() { GL.DeleteBuffer(id); }
}