using OpenTK.Graphics.OpenGL4;

public class ShaderProgram
{
    public int id;

    public ShaderProgram(string vertexShaderFilepath, string fragmentShaderFilePath)
    {
        // create the shader program
        id = GL.CreateProgram();

        // create the vertex shader
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        // add the source code from "Default.vert" in the Shaders file
        GL.ShaderSource(vertexShader, LoadShaderSource(vertexShaderFilepath));
        // Compile the Shader
        GL.CompileShader(vertexShader);

        // Same as vertex shader
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, LoadShaderSource(fragmentShaderFilePath));
        GL.CompileShader(fragmentShader);

        // Attach the shaders to the shader program
        GL.AttachShader(id, vertexShader);
        GL.AttachShader(id, fragmentShader);

        // Link the program to OpenGL
        GL.LinkProgram(id);

        // delete the shaders
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    public void Bind() { GL.UseProgram(id); }
    public void Unbind() { GL.UseProgram(0); }
    public void Delete() { GL.DeleteShader(id); }

    // Function to load a text file and return its contents as a string
    public static string LoadShaderSource(string filePath)
    {
        string shaderSource = "";

        try
        {
            using (StreamReader reader = new StreamReader("../../../Shaders/" + filePath))
            {
                shaderSource = reader.ReadToEnd();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to load shader source file: " + e.Message);
        }

        return shaderSource;
    }
}