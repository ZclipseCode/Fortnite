using OpenTK.Graphics.OpenGL4;

public class IBO
{
    public int id;
    public IBO(List<uint> data)
    {
        id = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
        GL.BufferData(BufferTarget.ElementArrayBuffer, data.Count * sizeof(uint), data.ToArray(), BufferUsageHint.StaticDraw);
    }
    public void Bind() { GL.BindBuffer(BufferTarget.ElementArrayBuffer, id); }
    public void Unbind() { GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0); }
    public void Delete() { GL.DeleteBuffer(id); }
}