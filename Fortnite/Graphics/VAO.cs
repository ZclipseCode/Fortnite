using OpenTK.Graphics.OpenGL4;

public class VAO
{
    public int id;
    public VAO()
    {
        id = GL.GenVertexArray();
        GL.BindVertexArray(id);
    }
    public void LinkToVAO(int location, int size, VBO vbo)
    {
        Bind();
        vbo.Bind();
        GL.VertexAttribPointer(location, size, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(location);
        Unbind();
    }
    public void Bind() { GL.BindVertexArray(id); }
    public void Unbind() { GL.BindVertexArray(0); }
    public void Delete() { GL.DeleteVertexArray(id); }
}